ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Organisation1] FOREIGN KEY ([StaffMemberBusinessAreaCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

