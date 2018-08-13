ALTER TABLE [dbo].[IncidentInterestedParty]
    ADD CONSTRAINT [FK_IncidentInterestedParty_Incident] FOREIGN KEY ([IncidentCode]) REFERENCES [dbo].[Incident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

