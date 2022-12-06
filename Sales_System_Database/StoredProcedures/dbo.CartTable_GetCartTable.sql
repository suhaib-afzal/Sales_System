CREATE PROCEDURE dbo.CartTable_GetCartTable
AS
	SET NOCOUNT ON
	RETURN SELECT Cart_ID, Customer_ID, ProfitMade, DateTime FROM CartTable;


