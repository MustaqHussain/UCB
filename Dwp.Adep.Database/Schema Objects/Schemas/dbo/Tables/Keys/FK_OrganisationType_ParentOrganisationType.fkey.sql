﻿ALTER TABLE [dbo].[OrganisationType]
    ADD CONSTRAINT [FK_OrganisationType_ParentOrganisationType] FOREIGN KEY ([ParentOrganisationTypeCode]) REFERENCES [dbo].[OrganisationType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

