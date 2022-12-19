CREATE PROCEDURE [dbo].[PurchaseTable_InsertPurchaseWithID]
	@Purchase_ID int,
	@Cart_ID int,
	@Product_ID int,
	@Quantity int
AS
	SET IDENTITY_INSERT dbo.PurchaseTable ON
	SET NOCOUNT ON
	INSERT INTO PurchaseTable (Purchase_ID, Cart_ID, Product_ID, Quantity)  VALUES (@Purchase_ID, @Cart_ID, @Product_ID, @Quantity);
	SET IDENTITY_INSERT dbo.PurchaseTable OFF
