ALTER TABLE [dbo].[StandardReport]
    ADD CONSTRAINT [DF_StandardReport_SortOrder] DEFAULT ((0)) FOR [SortOrder];

