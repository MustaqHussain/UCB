ALTER TABLE [dbo].[StaffOrganisation]
    ADD CONSTRAINT [FK_StaffOrganisation_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

