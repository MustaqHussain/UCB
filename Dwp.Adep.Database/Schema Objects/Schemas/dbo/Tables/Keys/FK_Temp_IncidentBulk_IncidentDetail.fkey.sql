ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_IncidentDetail] FOREIGN KEY ([IncidentDetailsCode]) REFERENCES [dbo].[IncidentDetail] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

