ALTER TABLE [dbo].[IncidentBulk]
    ADD CONSTRAINT [FK_Temp_IncidentBulk_Customer] FOREIGN KEY ([CustomerCode]) REFERENCES [dbo].[Customer] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

