ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_FurtherInfoNarrative] FOREIGN KEY ([FurtherInfoNarrativeCode]) REFERENCES [dbo].[Narrative] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

