ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_LineManagerNarrative] FOREIGN KEY ([LineManagerNarrativeCode]) REFERENCES [dbo].[Narrative] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

