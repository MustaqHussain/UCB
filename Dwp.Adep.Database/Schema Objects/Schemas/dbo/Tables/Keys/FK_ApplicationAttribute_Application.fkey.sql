﻿ALTER TABLE [dbo].[ApplicationAttribute]
    ADD CONSTRAINT [FK_ApplicationAttribute_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

