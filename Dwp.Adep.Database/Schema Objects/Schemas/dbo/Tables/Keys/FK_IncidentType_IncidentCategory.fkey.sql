ALTER TABLE [dbo].[IncidentType]
    ADD CONSTRAINT [FK_IncidentType_IncidentCategory] FOREIGN KEY ([IncidentCategoryCode]) REFERENCES [dbo].[IncidentCategory] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

