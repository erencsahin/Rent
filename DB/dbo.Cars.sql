CREATE TABLE [dbo].[Cars] (
    [ID]          INT        IDENTITY (1, 1) NOT NULL,
    [BrandId]     INT        NOT NULL,
    [ColorId]     INT        NOT NULL,
    [ModelYear]   INT        NOT NULL,
    [DailyPrice]  FLOAT (53) NOT NULL,
    [Description] CHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

