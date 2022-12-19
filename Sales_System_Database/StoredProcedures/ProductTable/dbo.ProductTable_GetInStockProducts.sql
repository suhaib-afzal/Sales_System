ALTER PROCEDURE dbo.ProductTable_GetInStockProducts
AS
	SET NOCOUNT ON
	SELECT Product_ID, Name, Description, Stock, SalePrice, CostPrice FROM ProductTable WHERE ProductTable.Stock > 0;

