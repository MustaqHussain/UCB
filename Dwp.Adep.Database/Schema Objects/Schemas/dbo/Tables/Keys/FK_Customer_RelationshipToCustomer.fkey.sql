ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_RelationshipToCustomer] FOREIGN KEY ([RelationshipToCustomerCode]) REFERENCES [dbo].[RelationshipToCustomer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

