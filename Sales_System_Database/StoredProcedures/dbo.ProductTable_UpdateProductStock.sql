CREATE PROCEDURE [dbo].[ProductTable_UpdateProductStock]
	@Product_ID INT,
	@New_Stock INT
AS
	SET NOCOUNT ON
	UPDATE ProductTable SET ProductTable.Stock = @New_Stock WHERE ProductTable.Product_ID = @Product_ID;

