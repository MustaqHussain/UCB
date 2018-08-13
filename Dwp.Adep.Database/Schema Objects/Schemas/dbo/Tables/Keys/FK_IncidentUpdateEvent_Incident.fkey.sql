ALTER TABLE [dbo].[IncidentUpdateEvent]
    ADD CONSTRAINT [FK_IncidentUpdateEvent_Incident] FOREIGN KEY ([IncidentCode]) REFERENCES [dbo].[Incident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

