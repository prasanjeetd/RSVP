CREATE TABLE [dbo].[TeamPersonnel] (
    [TeamPersonnelId] BIGINT NOT NULL IDENTITY,
    [TeamId]          BIGINT NULL,
    [VolunteerId]     BIGINT NULL,
    [UserId]          BIGINT NULL,
    CONSTRAINT [PK_TeamPersonnel] PRIMARY KEY CLUSTERED ([TeamPersonnelId] ASC)
);

