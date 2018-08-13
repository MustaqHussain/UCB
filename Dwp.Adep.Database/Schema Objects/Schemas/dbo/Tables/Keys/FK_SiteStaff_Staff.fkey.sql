ALTER TABLE [dbo].[SiteStaff]
    ADD CONSTRAINT [FK_SiteStaff_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

