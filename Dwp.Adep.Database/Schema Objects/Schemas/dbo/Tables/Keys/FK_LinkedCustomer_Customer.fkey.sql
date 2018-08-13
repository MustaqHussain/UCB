ALTER TABLE [dbo].[LinkedCustomer]
    ADD CONSTRAINT [FK_LinkedCustomer_Customer] FOREIGN KEY ([Customer1Code]) REFERENCES [dbo].[Customer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

