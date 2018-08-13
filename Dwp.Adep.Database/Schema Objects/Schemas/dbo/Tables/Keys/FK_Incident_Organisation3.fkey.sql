ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Organisation3] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

