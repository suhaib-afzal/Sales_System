using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Library.DataModels;
using Dapper;

namespace Class_Library.DataAccess
{
    public static class ProductData
    {
        public static void UpdateProductTable(List<ProductModel> updatedProducts)
        {
            throw new NotImplementedException();
        }

        public static void UpdateProductsStock(List<ProductModel> updatedProducts)
        {
            List<int> Product_IDs = updatedProducts.ConvertAll(p => p.Product_ID);
            List<int> New_Stocks = updatedProducts.ConvertAll(p => p.Stock);

            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                for (int i = 0; i < updatedProducts.Count(); i++)
                {
                    connection.Execute("dbo.ProductTable_UpdateProductStock @Product_ID, @New_Stock",
                        new { Product_ID = Product_IDs[i], New_Stock = New_Stocks[i] } 
                        );
                }
            }
        }

        public static void InsertNewProducts(List<ProductModel> newProducts)
        {
            throw new NotImplementedException();
        }

        public static List<ProductModel> GetProductTable()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                var output = connection.Query<ProductModel>("EXEC dbo.ProductTable_GetProducts").ToList();

                return output;
            }
        }

        public static List<ProductModel> GetProductsInStock()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                return connection.Query<ProductModel>("EXEC dbo.ProductTable_GetInStockProducts").ToList();
            }
        }

    }
}
