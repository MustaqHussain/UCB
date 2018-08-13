ALTER TABLE [dbo].[CustomerControlMeasure]
    ADD CONSTRAINT [FK_CustomerControlMeasure_Customer] FOREIGN KEY ([CustomerCode]) REFERENCES [dbo].[Customer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

