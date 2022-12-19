ALTER PROCEDURE dbo.CartTable_GetCartTable
AS
	SET NOCOUNT ON
	SELECT Cart_ID, Customer_ID, ProfitMade, DateTime FROM CartTable;


