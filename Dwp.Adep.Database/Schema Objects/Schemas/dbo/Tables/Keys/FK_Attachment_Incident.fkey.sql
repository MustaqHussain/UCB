ALTER TABLE [dbo].[Attachment]
    ADD CONSTRAINT [FK_Attachment_Incident] FOREIGN KEY ([IncidentCode]) REFERENCES [dbo].[Incident] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

