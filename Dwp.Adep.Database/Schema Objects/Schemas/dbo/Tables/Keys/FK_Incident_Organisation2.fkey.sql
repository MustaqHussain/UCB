ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Organisation2] FOREIGN KEY ([StaffMemberHomeOfficeCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

