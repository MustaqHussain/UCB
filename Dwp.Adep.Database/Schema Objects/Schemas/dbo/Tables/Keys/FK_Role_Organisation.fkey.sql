﻿ALTER TABLE [dbo].[Role]
    ADD CONSTRAINT [FK_Role_Organisation] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

