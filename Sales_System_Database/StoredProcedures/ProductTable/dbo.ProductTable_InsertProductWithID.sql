CREATE PROCEDURE [dbo].[ProductTable_InsertProductWithID]
	@Product_ID int,
	@Name VARCHAR(50),
	@Description VARCHAR(100),
	@Stock int,
	@SalePrice money,
	@CostPrice money
AS
	SET IDENTITY_INSERT dbo.ProductTable ON
	SET NOCOUNT ON
	INSERT INTO ProductTable (Product_ID, Name, Description, Stock, SalePrice, CostPrice)  VALUES (@Product_ID, @Name, @Description, @Stock, @SalePrice, @CostPrice);
	SET IDENTITY_INSERT dbo.ProductTable OFF
