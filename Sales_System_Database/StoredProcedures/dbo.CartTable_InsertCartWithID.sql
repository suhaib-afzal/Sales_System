CREATE PROCEDURE [dbo].[CartTable_InsertCartWithID]
	@Cart_ID int,
	@Customer_ID int,
	@ProfitMade Money,
	@DateTime datetime
AS
	SET IDENTITY_INSERT dbo.CartTable ON
	SET NOCOUNT ON
	INSERT INTO CartTable (Cart_ID, Customer_ID, ProfitMade, DateTime)  VALUES (@Cart_ID, @Customer_ID, @ProfitMade, @DateTime);
	SET IDENTITY_INSERT dbo.CartTable OFF
