﻿CREATE TABLE [dbo].[ProductTable]
(
	[Product_ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(100) NOT NULL,
	[Stock] INT NOT NULL,
	[SalePrice] MONEY NOT NULL,
	[CostPrice] MONEY NOT NULL
)
