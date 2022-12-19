ALTER PROCEDURE dbo.CartTable_insertNewCart
	@Customer_ID INT,
	@ProfitMade MONEY,
	@DateTime DateTime
AS
	SET NOCOUNT ON
	INSERT CartTable(Customer_ID, ProfitMade, DateTime) OUTPUT INSERTED.Cart_ID VALUES (@Customer_ID, @ProfitMade, @DateTime);
	


