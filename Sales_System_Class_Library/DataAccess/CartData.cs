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
        public static void UpdateCartTable(List<DisplayCartModel> updatedCarts)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                foreach (DisplayCartModel displayCartModel in updatedCarts)
                {
                    connection.Execute("dbo.CartTable_UpdateRow @Cart_ID, @Customer_ID, @ProfitMade, @TimeofPurchase", displayCartModel);
                }
            }
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
                    new { Customer_ID = newCart.Customer.Customer_ID, ProfitMade = newCart.ProfitMade, DateTime = newCart.TimeofPurchase }).ToList();

                if (IDofInserted.Count > 1)
                {
                    throw new Exception();
                }

                return IDofInserted[0];
            }
        }

        public static void InsertNewCartsWithID(List<DisplayCartModel> displayCarts)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                foreach (DisplayCartModel displayCart in displayCarts)
                {
                    connection.Execute("dbo.CartTable_InsertCartWithID @Cart_ID, @Customer_ID, @ProfitMade, @TimeofPurchase",displayCart);
                }
            }
        }

        public static List<DisplayCartModel> GetCartTable()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                return connection.Query<DisplayCartModel>("EXEC dbo.CartTable_GetCartTable").ToList();
            }
        }
    }
}
