﻿ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Site] FOREIGN KEY ([SiteCode]) REFERENCES [dbo].[Site] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

