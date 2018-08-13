ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_JobRole] FOREIGN KEY ([JobRoleCode]) REFERENCES [dbo].[JobRole] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

