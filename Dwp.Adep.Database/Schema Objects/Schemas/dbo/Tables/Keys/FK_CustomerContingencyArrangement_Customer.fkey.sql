ALTER TABLE [dbo].[CustomerContingencyArrangement]
    ADD CONSTRAINT [FK_CustomerContingencyArrangement_Customer] FOREIGN KEY ([CustomerCode]) REFERENCES [dbo].[Customer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

