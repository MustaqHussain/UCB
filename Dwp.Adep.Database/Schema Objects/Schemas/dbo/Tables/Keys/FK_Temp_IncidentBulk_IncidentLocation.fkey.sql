ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_IncidentLocation] FOREIGN KEY ([IncidentLocationCode]) REFERENCES [dbo].[IncidentLocation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

