ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup]
    ADD CONSTRAINT [FK_ApplicationOrganisationTypeGroup_Organisation] FOREIGN KEY ([RootOrganisationForApplicationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

