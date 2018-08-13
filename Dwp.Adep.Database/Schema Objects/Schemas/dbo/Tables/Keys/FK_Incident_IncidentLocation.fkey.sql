ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_IncidentLocation] FOREIGN KEY ([IncidentLocationCode]) REFERENCES [dbo].[IncidentLocation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

