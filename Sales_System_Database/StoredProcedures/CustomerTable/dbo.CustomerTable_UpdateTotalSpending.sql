CREATE PROCEDURE dbo.CustomerTable_Update
	@Customer_ID INT,
	@newTotalPurchases MONEY
AS
	SET NOCOUNT ON
	UPDATE CustomerTable SET CustomerTable.TotalPurchases = @newTotalPurchases WHERE Customer_ID = @Customer_ID;


