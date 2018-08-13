ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_IncidentCategory] FOREIGN KEY ([IncidentCategoryCode]) REFERENCES [dbo].[IncidentCategory] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

