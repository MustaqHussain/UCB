ALTER TABLE [dbo].[OrganisationHierarchy]
    ADD CONSTRAINT [FK_OrganisationHierarchy_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

