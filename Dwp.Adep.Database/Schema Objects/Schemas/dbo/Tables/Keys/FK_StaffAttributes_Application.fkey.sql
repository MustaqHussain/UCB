﻿ALTER TABLE [dbo].[StaffAttributes]
    ADD CONSTRAINT [FK_StaffAttributes_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

