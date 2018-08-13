ALTER TABLE [dbo].[Incident]
    ADD CONSTRAINT [FK_Incident_Customer] FOREIGN KEY ([CustomerCode]) REFERENCES [dbo].[Customer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

