ALTER PROCEDURE dbo.ProductTable_GetProducts
AS
	SET NOCOUNT ON
	SELECT Product_ID, Name, Description, Stock, SalePrice, CostPrice FROM ProductTable;

