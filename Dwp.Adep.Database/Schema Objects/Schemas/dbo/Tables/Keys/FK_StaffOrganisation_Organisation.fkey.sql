﻿ALTER TABLE [dbo].[StaffOrganisation]
    ADD CONSTRAINT [FK_StaffOrganisation_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

