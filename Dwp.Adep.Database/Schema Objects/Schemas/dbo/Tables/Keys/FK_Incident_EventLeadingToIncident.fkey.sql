ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_EventLeadingToIncident] FOREIGN KEY ([EventLeadingToIncidentCode]) REFERENCES [dbo].[EventLeadingToIncident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

