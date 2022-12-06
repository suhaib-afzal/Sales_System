ALTER PROCEDURE dbo.CustomerTable_GetCustomerTable
AS
	SET NOCOUNT ON
	SELECT Customer_ID, FirstName, LastName, TotalPurchases FROM CustomerTable;


