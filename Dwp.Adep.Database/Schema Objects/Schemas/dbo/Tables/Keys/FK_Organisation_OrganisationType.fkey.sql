ALTER TABLE [dbo].[Organisation]
    ADD CONSTRAINT [FK_Organisation_OrganisationType] FOREIGN KEY ([OrganisationTypeCode]) REFERENCES [dbo].[OrganisationType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

