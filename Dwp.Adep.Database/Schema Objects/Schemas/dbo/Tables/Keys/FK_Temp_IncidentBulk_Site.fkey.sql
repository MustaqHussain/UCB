ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_Site] FOREIGN KEY ([SiteCode]) REFERENCES [dbo].[Site] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

