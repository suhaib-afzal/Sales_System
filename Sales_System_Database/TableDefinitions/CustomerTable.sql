CREATE TABLE [dbo].[CustomerTable]
(
	[Customer_ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(20) NOT NULL,
	[TotalPurchases] MONEY NOT NULL
)
