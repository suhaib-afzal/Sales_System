ALTER PROCEDURE [dbo].[CustomerTable_InsertCustomerWithID]
	@Customer_ID int,
	@FirstName VARCHAR(50),
	@LastName VARCHAR(100),
	@TotalPurchases money
AS
	SET IDENTITY_INSERT dbo.CustomerTable ON
	SET NOCOUNT ON
	INSERT INTO CustomerTable (Customer_ID, FirstName, LastName, TotalPurchases)  VALUES (@Customer_ID, @FirstName, @LastName, @TotalPurchases);
	SET IDENTITY_INSERT dbo.CustomerTable OFF
