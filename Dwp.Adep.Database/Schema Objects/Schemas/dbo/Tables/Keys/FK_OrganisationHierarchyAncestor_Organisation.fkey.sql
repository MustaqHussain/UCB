﻿ALTER TABLE [dbo].[OrganisationHierarchy]
    ADD CONSTRAINT [FK_OrganisationHierarchyAncestor_Organisation] FOREIGN KEY ([AncestorOrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

