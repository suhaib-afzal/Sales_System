CREATE PROCEDURE [dbo].[ProductTable_UpdateRow]
	@Product_ID int,
	@Name VARCHAR(50),
	@Description VARCHAR(100),
	@Stock int,
	@SalePrice MONEY,
	@CostPrice MONEY
AS
	UPDATE ProductTable SET Name = @Name , Description = @Description, Stock = @Stock, SalePrice = @SalePrice, CostPrice = @CostPrice 
	WHERE Product_ID = @Product_ID;
