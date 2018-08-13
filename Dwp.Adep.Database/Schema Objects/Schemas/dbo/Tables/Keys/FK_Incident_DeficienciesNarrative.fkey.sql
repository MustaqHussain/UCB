ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_DeficienciesNarrative] FOREIGN KEY ([DeficienciesNarrativeCode]) REFERENCES [dbo].[Narrative] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

