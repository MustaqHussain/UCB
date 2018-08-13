ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup]
    ADD CONSTRAINT [FK_ApplicationOrganisationTypeGroup_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

