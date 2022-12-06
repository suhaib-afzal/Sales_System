﻿CREATE TABLE [dbo].[CartTable]
(
	[Cart_ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Customer_ID] INT NOT NULL FOREIGN KEY REFERENCES CustomerTable([Customer_ID]) ON DELETE CASCADE,
	[ProfitMade] MONEY, 
	[DateTime] DATETIME
)
