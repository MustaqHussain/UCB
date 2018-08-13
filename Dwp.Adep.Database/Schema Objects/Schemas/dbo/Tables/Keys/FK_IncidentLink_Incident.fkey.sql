ALTER TABLE [dbo].[IncidentLink]
    ADD CONSTRAINT [FK_IncidentLink_Incident] FOREIGN KEY ([IncidentCode]) REFERENCES [dbo].[Incident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

