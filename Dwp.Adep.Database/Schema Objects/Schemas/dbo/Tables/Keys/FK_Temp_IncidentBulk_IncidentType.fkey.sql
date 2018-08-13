ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_IncidentType] FOREIGN KEY ([IncidentTypeCode]) REFERENCES [dbo].[IncidentType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

