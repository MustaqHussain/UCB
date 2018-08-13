ALTER TABLE [dbo].[CustomerContingencyArrangement]
    ADD CONSTRAINT [FK_CustomerContingencyArrangement_ContingencyArrangement] FOREIGN KEY ([ContingencyArrangementCode]) REFERENCES [dbo].[ContingencyArrangement] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

