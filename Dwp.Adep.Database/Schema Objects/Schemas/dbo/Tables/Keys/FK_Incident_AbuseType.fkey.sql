ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_AbuseType] FOREIGN KEY ([AbuseTypeCode]) REFERENCES [dbo].[AbuseType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

