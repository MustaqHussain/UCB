﻿ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup]
    ADD CONSTRAINT [FK_ApplicationOrganisationTypeGroup_OrganisationTypeGroup] FOREIGN KEY ([OrganisationTypeGroupCode]) REFERENCES [dbo].[OrganisationTypeGroup] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

