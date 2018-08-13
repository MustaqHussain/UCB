ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_AbuseType] FOREIGN KEY ([AbuseTypeCode]) REFERENCES [dbo].[AbuseType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

