CREATE  PROC [dbo].[sp_CreateDataFromTablesToInsertCommandsTempTableVersion] (
  @pvcTableList varchar (8000),
  @pbColList bit = 1
)
AS
/*----------------------------------------------------------------------
Name: 		sp_CreateDataFromTablesToInsertCommands

Description:    Creates insert Transact-SQL commands from the tables
                metadata and data. The table list can contain more than
                one table name, each one seperated by a comma.

Parameters:			
                @pvcTableList  - varchar containing the Tables seperated 
                                by commas
                @pbColList     - bit containing either 1 (default) or
                                0. If 1 then columns are also listed
                                in the insert statement.

Enhancements:   Needs to handle Text, Ntext and Image datatypes.
                Currently does not handle Image and will only
                get the first 4000 characters for Text and Ntext
                datatypes

Returns:	Integer - 0 Everthing was okay or an error code

Dependencies:   syscolumns, systypes

History:
--------
 1 May 2002   1.00.0000   ADJ Genesis.
12 Mar 2003   1.01.0000   gpl Allow for " ' " characters in strings
09 May 2012   1.02.0000   JB - added "systypes.name <> 'sysname'" to deal with nvarchar fields
---------------------------------------------------------------------- */
BEGIN

  DECLARE 
    @sTableName sysname,
    @sColumnName sysname,
    @sColumnType sysname,
    @tiStatus tinyint,
    @iColOrder int,
    @iTableID int,
    @vcExecStr varchar(8000),
    @vcHeadStr varchar(8000),
    @vcDataStr varchar(8000),
    @iMaxCol int,
    @iColList bit,
    @iPosition int,
    @bIsIdentity bit

  SET NOCOUNT ON
  PRINT '/****************************************************************************************************************'
  PRINT '** Created by: sp_CreateDataFromTablesToInsertCommands                                                         **'
  PRINT '*****************************************************************************************************************'
  PRINT '** WARNING - IF A TABLE CONTAINS A DATATYPE OF TEXT OR NTEXT ONLY 8K OR 4K CHARS FROM THIS FIELD WILL BE       **'
  PRINT '**           RETURNED RESPECTIVELY.                                                                            **'
  PRINT '**                                                                                                             **'
  PRINT '** BINARY, VARBINARY AND IMAGES ARE CURRENTLY NOT HANDLED AT ALL                                               **'
  PRINT '** DATE EXECUTED: ' + CAST(GETDATE() AS VARCHAR) + '                                                           **'
  PRINT '*****************************************************************************************************************/'
  
  SELECT @iPosition = PATINDEX('%,%', @pvcTableList)

  IF @iPosition = 0
  BEGIN
    IF LEN(@pvcTableList) != 0
    BEGIN
      SELECT @pvcTableList = @pvcTableList + ','
      SELECT @iPosition = PATINDEX('%,%', @pvcTableList)
    END
  END
  ELSE
  BEGIN
    IF RIGHT(@pvcTableList,1) != ','
    BEGIN
      SELECT @pvcTableList = @pvcTableList + ','
    END
  END

  WHILE (@iPosition <> 0)
  BEGIN

    SELECT @sTableName = RTRIM(LTRIM(SUBSTRING(@pvcTableList, 1, @iPosition - 1)))
    SELECT @pvcTableList = STUFF(@pvcTableList, 1, PATINDEX('%,%', @pvcTableList),'')
    SELECT @iPosition = PATINDEX('%,%', @pvcTableList)

    IF @pbColList = 1
      SELECT @iColList = 1
    ELSE
      SELECT @iColList = 0

    
    SELECT @iTableID = OBJECT_ID(@sTableName)
    IF @iTableID IS NOT NULL
    BEGIN

      SELECT @vcHeadStr = '('
      
      SELECT 
        @iMaxCol = MAX(colorder)
      FROM 
        syscolumns
      WHERE 
        id = @iTableID
      
      DECLARE curColumnList CURSOR SCROLL FOR 
        SELECT 
          syscolumns.name as name, 
          systypes.name AS typename, 
          syscolumns.status AS status, 
          syscolumns.colorder
        FROM 
          syscolumns 
        INNER JOIN 
          systypes 
        ON 
          syscolumns.xtype = systypes.xtype AND systypes.name <> 'sysname'
        WHERE 
          syscolumns.id = @iTableID
        ORDER BY 
          syscolumns.colorder
      
      OPEN curColumnList
      
      FETCH FIRST FROM 
          curColumnList 
        INTO 
          @sColumnName, 
          @sColumnType,
          @tiStatus,
          @iColOrder
      
      WHILE @@FETCH_STATUS <> -1
      BEGIN

        IF @sColumnType IN ('varbinary','binary')
        BEGIN
          PRINT '/****************************************************************************************************************'
          PRINT '** WARNING - THIS TABLE CONTAINS AN BINARY DATATYPE WHICH IS CURRENTLY NOT HANDLED AND WILL BE SET TO NULL     **'
          PRINT '****************************************************************************************************************/'
        END

        IF @sColumnType IN ('text')
        BEGIN
          PRINT '/****************************************************************************************************************'
          PRINT '** WARNING - THIS TABLE CONTAINS AN TEXT DATATYPE WHICH WILL ONLY RETURN 8K OF CHARACTERS                      **'
          PRINT '****************************************************************************************************************/'
        END

        IF @sColumnType IN ('ntext')
        BEGIN
          PRINT '/****************************************************************************************************************'
          PRINT '** WARNING - THIS TABLE CONTAINS AN NTEXT DATATYPE WHICH WILL ONLY RETURN 4K OF CHARACTERS                     **'
          PRINT '****************************************************************************************************************/'
        END

        IF @sColumnType IN ('image')
        BEGIN
          PRINT '/****************************************************************************************************************'
          PRINT '** WARNING - THIS TABLE CONTAINS AN IMAGE DATATYPE WHICH IS CURRENTLY NOT HANDLED AND WILL BE SET TO NULL      **'
          PRINT '****************************************************************************************************************/'
        END

        SELECT @vcHeadStr = @vcHeadStr + 
          CASE 
           WHEN @iColOrder < @iMaxCol 
             THEN '[' + @sColumnName + '], '
           ELSE '[' + @sColumnName + '])'
          END
      
        FETCH NEXT 
        FROM 
          curColumnList 
        INTO 
          @sColumnName, 
          @sColumnType,
          @tiStatus,
          @iColOrder
      
      END
      
      SELECT @vcExecStr = 'SELECT ' + '''INSERT INTO #tempTable ' 

      FETCH FIRST FROM 
        curColumnList 
      INTO 
        @sColumnName, 
        @sColumnType, 
        @tiStatus, 
        @iColOrder

      SELECT @vcDatastr =  ''

      WHILE @@FETCH_STATUS <> -1
      BEGIN
        SELECT @vcDatastr =  @vcDatastr +
            CASE 
              WHEN @sColumnType IN ('tinyint', 'bigint', 'smallint', 'int', 'money', 'numeric', 'float', 'decimal', 'smallmoney', 'real') 
                THEN 'COALESCE(CONVERT(VARCHAR(255), [' + @sColumnName + ']), ''NULL'')'
              WHEN @sColumnType IN ('bit') 
                THEN 'COALESCE(CONVERT(VARCHAR(1), [' + @sColumnName + ']), ''NULL'')'
              WHEN @sColumnType IN ('uniqueidentifier') 
                THEN 'CASE WHEN [' + @sColumnName + '] IS NULL THEN ''NULL'' ELSE ' + REPLICATE(CHAR(39),4) + ' + CONVERT(VARCHAR(255), [' + @sColumnName + ']) + ' + REPLICATE(CHAR(39),4) + ' END'
              WHEN @sColumnType IN ('datetime','smalldatetime') 
                THEN 'CASE WHEN [' + @sColumnName + '] IS NULL THEN ''NULL'' ELSE ' + REPLICATE(CHAR(39),4) + ' + CONVERT(VARCHAR(255), [' + @sColumnName + '], 103) + ' + REPLICATE(CHAR(39),4) + ' END'
              WHEN @sColumnType IN ('varbinary','binary') 
                THEN '''0x0'''
              WHEN @sColumnType IN ('text') 
                THEN 'CASE WHEN [' + @sColumnName + '] IS NULL THEN ''NULL'' ELSE ' + REPLICATE(CHAR(39),4) + ' + REPLACE(CAST([' + @sColumnName + '] AS VARCHAR(MAX)), '''''''', '''''''''''') + ' + REPLICATE(CHAR(39),4)  + ' END'
              WHEN @sColumnType IN ('ntext') 
                THEN 'CASE WHEN [' + @sColumnName + '] IS NULL THEN ''NULL'' ELSE ' + REPLICATE(CHAR(39),4) + ' + SUBSTRING(REPLACE([' + @sColumnName + '], '''''''', ''''''''''''), 0, 4000) + ' + REPLICATE(CHAR(39),4) + ' END'
              WHEN @sColumnType IN ('image') 
                THEN '''NULL'''
              WHEN @sColumnType IN ('timestamp') 
                THEN '''NULL'''
              ELSE 'CASE WHEN [' + @sColumnName + '] IS NULL THEN ''NULL'' ELSE ' + REPLICATE(CHAR(39),4) + ' + REPLACE([' + @sColumnName + '], '''''''', '''''''''''') + ' + REPLICATE(CHAR(39),4) + ' END'
            END 
            +
            CASE 
              WHEN @iColOrder < @iMaxCol 
                THEN ' + '', '' + '
              ELSE ' + '')''' + ' FROM [' + @sTableName + ']'
            END

        IF (@tiStatus & 0x80) <> 0 -- Check to see if the Column is an Identity Column
        BEGIN
          SELECT @bIsIdentity = 1
        END
          
      
        FETCH NEXT FROM 
          curColumnList 
        INTO 
          @sColumnName, 
          @sColumnType, 
          @tiStatus, 
          @iColOrder

      END
      
      DEALLOCATE curColumnList

      PRINT '/' + replicate('*', 40)
      PRINT '** Inserts for ' + @sTableName
      PRINT replicate('*', 40) + '/'

      PRINT 'PRINT ' + '''Inserts for ' + @sTableName + ''''

      IF @bIsIdentity = 1
      BEGIN
        SELECT 'SET IDENTITY_INSERT ' + @sTableName + ' ON'
        SELECT 'GO'
      END

--      select @vcDatastr
--      select len(@vcDatastr)

--      SELECT (@vcExecstr + @vcHeadStr + ' VALUES ('' + ' + @vcDatastr) + ' hello'

      IF @iColList = 1
        EXEC(@vcExecstr + @vcHeadStr + ' VALUES ('' + ' + @vcDatastr)
      ELSE
        EXEC(@vcExecstr + ' VALUES ('' + ' + @vcDatastr)  

      SELECT 'GO'

      IF @bIsIdentity = 1
      BEGIN
        SELECT @bIsIdentity = 0
        SELECT 'SET IDENTITY_INSERT ' + @sTableName + ' OFF'
        SELECT 'GO'
      END

    END
    ELSE
    BEGIN 
      PRINT 'Table ' + @sTableName + ' not found in current Database'
    END
  END
END


