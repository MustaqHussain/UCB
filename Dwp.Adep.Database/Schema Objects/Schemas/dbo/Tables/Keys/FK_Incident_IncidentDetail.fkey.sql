ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_IncidentDetail] FOREIGN KEY ([IncidentDetailsCode]) REFERENCES [dbo].[IncidentDetail] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

