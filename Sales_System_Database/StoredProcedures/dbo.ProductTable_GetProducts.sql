CREATE PROCEDURE dbo.ProductTable_GetProducts
AS
	SET NOCOUNT ON
	RETURN SELECT ProductTable(Product_ID, Name, Description, Stock, SalePrice, CostPrice) FROM ProductTable;

