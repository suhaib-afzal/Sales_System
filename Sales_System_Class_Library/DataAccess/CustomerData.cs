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
    public static class CustomerData
    {
        public static void UpdateCustomerTable(List<CustomerModel> updatedCustomers)
        {
            throw new NotImplementedException();
        }

        public static void UpdateCustomerSpending(CustomerModel customerToUpdate, decimal newTotalPurchases)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                connection.Execute("dbo.CustomerTable_Update @Customer_ID, @newTotalPurchases",
                    new { Customer_ID = customerToUpdate.Customer_ID, newTotalPurchases = newTotalPurchases });
            }
        }

        public static void InsertNewCustomer(List<CustomerModel> newCustomers)
        {
            throw new NotImplementedException();
        }

        public static List<CustomerModel> GetCustomerTable()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                return connection.Query<CustomerModel>("EXEC dbo.CustomerTable_GetCustomerTable").ToList();
            }
        }
    }
}
