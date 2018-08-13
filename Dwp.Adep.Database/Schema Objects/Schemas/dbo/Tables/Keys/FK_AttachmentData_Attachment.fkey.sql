ALTER TABLE [dbo].[AttachmentData]
    ADD CONSTRAINT [FK_AttachmentData_Attachment] FOREIGN KEY ([AttachmentCode]) REFERENCES [dbo].[Attachment] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

