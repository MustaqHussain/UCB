ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_IncidentCategory] FOREIGN KEY ([IncidentCategoryCode]) REFERENCES [dbo].[IncidentCategory] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

