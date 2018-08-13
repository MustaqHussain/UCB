ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Site1] FOREIGN KEY ([StaffMemberHomeOfficeSiteCode]) REFERENCES [dbo].[Site] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

