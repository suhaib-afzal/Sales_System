CREATE PROCEDURE dbo.PurchaseTable_InsertNewPurchase
	@Cart_ID INT,
	@Product_ID INT,
	@Quantity INT
AS
	SET NOCOUNT ON
	INSERT PurchaseTable(Cart_ID, Product_ID, Quantity) VALUES (@Cart_ID, @Product_ID, @Quantity);

