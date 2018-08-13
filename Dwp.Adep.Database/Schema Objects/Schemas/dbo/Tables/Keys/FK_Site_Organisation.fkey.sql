ALTER TABLE [dbo].[Site]
    ADD CONSTRAINT [FK_Site_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

