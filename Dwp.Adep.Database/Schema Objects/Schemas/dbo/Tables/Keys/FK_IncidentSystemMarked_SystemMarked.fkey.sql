ALTER TABLE [dbo].[IncidentSystemMarked]
    ADD CONSTRAINT [FK_IncidentSystemMarked_SystemMarked] FOREIGN KEY ([SystemMarkedCode]) REFERENCES [dbo].[SystemMarked] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

