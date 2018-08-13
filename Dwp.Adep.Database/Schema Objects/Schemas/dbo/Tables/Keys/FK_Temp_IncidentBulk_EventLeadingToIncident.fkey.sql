ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_EventLeadingToIncident] FOREIGN KEY ([EventLeadingToIncidentCode]) REFERENCES [dbo].[EventLeadingToIncident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

