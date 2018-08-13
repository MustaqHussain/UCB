ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Organisation] FOREIGN KEY ([StaffMemberBusinessCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

