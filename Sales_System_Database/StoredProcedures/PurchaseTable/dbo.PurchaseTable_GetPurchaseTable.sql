ALTER PROCEDURE [dbo].[PurchaseTable_GetPurchaseTable]
AS
	SET NOCOUNT ON
	SELECT Purchase_ID, Cart_ID, Product_ID, Quantity FROM PurchaseTable;
