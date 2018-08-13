ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_LineManagerNarrative] FOREIGN KEY ([LineManagerNarrativeCode]) REFERENCES [dbo].[Narrative] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

