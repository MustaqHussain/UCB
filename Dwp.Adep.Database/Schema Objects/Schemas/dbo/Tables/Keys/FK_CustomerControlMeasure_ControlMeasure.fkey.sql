ALTER TABLE [dbo].[CustomerControlMeasure]
    ADD CONSTRAINT [FK_CustomerControlMeasure_ControlMeasure] FOREIGN KEY ([ControlMeasureCode]) REFERENCES [dbo].[ControlMeasure] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

