ALTER TABLE [dbo].[StaffOrganisation]
    ADD CONSTRAINT [FK_StaffOrganisation_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

