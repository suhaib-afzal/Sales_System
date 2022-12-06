using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Library.DataModels;
using Dapper;

namespace Class_Library.DataAccess
{
    public static class CartData
    {
        public static void UpdateCartTable(List<CartModel> updatedCarts)
        {
            throw new NotImplementedException();
        }

        public static int InsertNewCart(CartModel newCart)
        {
            if (newCart.Customer == null)
            {
                throw new ArgumentException("No customer associated with Cart to input to Database");
            }

            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {

                List<int> IDofInserted = connection.Query<int>("EXEC dbo.CartTable_insertNewCart @Customer_ID, @ProfitMade, @DateTime", 
                    new { Customer_ID = newCart.Customer.Customer_ID, ProfitMade = newCart.ProfitMade, DateTime = newCart.DateTime }).ToList();

                if (IDofInserted.Count > 1)
                {
                    throw new Exception();
                }

                return IDofInserted[0];
            }
        }

        public static List<CartModel> GetCartTable()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                return connection.Query<CartModel>("dbo.CartTable_GetCartTable").ToList();
            }
        }
    }
}
