ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_IncidentType] FOREIGN KEY ([IncidentTypeCode]) REFERENCES [dbo].[IncidentType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

