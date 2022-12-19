CREATE PROCEDURE [dbo].[CustomerTable_UpdateRow]
	@Customer_ID int,
	@FirstName VARCHAR(20),
	@LastName VARCHAR(20),
	@TotalPurchases money
AS
	UPDATE CustomerTable SET FirstName = @FirstName , LastName = @LastName, TotalPurchases = @TotalPurchases
	WHERE Customer_ID = @Customer_ID;
