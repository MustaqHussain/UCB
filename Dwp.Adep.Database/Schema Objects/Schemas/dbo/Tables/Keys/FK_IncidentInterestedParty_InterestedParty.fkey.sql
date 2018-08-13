ALTER TABLE [dbo].[IncidentInterestedParty]
    ADD CONSTRAINT [FK_IncidentInterestedParty_InterestedParty] FOREIGN KEY ([InterestedPartyCode]) REFERENCES [dbo].[InterestedParty] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

