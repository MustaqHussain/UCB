ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Incident1] FOREIGN KEY ([Code]) REFERENCES [dbo].[Incident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

