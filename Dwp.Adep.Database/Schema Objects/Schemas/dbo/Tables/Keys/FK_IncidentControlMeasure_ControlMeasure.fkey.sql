ALTER TABLE [dbo].[CustomerControlMeasure]
    ADD CONSTRAINT [FK_IncidentControlMeasure_ControlMeasure] FOREIGN KEY ([ControlMeasureCode]) REFERENCES [dbo].[ControlMeasure] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

