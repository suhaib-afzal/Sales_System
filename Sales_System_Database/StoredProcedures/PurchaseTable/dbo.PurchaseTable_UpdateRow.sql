CREATE PROCEDURE [dbo].[PurchaseTable_UpdateRow]
	@Purchase_ID int,
	@Cart_ID int,
	@Product_ID int,
	@Quantity int
AS
	UPDATE PurchaseTable SET Cart_ID = @Cart_ID , Product_ID = @Product_ID, Quantity = @Quantity
	WHERE Purchase_ID = @Purchase_ID;
