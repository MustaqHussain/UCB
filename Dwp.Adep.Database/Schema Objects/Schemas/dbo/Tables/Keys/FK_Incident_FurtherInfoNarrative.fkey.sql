ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_FurtherInfoNarrative] FOREIGN KEY ([FurtherInfoNarrativeCode]) REFERENCES [dbo].[Narrative] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

