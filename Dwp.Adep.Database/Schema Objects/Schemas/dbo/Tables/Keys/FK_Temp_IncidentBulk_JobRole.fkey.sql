ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_JobRole] FOREIGN KEY ([JobRoleCode]) REFERENCES [dbo].[JobRole] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

