ALTER TABLE [dbo].[LinkedCustomer]
    ADD CONSTRAINT [FK_LinkedCustomer_Customer1] FOREIGN KEY ([Customer2Code]) REFERENCES [dbo].[Customer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

