﻿ALTER TABLE [dbo].[ADRoleLookup]
    ADD CONSTRAINT [FK_ADRoleLookup_Role] FOREIGN KEY ([RoleCode]) REFERENCES [dbo].[Role] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

