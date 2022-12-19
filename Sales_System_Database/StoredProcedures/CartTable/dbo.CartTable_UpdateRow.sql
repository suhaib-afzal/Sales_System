CREATE PROCEDURE [dbo].[CartTable_UpdateRow]
	@Cart_ID int,
	@Customer_ID int,
	@ProfitMade money,
	@TimeofPurchase datetime
AS
	UPDATE CartTable SET Customer_ID = @Customer_ID , ProfitMade = @ProfitMade, DateTime = @TimeofPurchase
	WHERE Cart_ID = @Cart_ID;
