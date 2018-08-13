ALTER TABLE [dbo].[IncidentLink]
    ADD CONSTRAINT [FK_IncidentLink_Incident1] FOREIGN KEY ([LinkedIncidentCode]) REFERENCES [dbo].[Incident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

