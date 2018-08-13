ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_IncidentNarrative] FOREIGN KEY ([IncidentNarrativeCode]) REFERENCES [dbo].[Narrative] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

