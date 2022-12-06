CREATE PROCEDURE [dbo].[GetLatestCartID]
AS
	SELECT TOP 1 Cart_ID FROM CartTable ORDER BY CartTable.Cart_ID DESC;

