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
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                foreach(CustomerModel customer in updatedCustomers)
                {
                    connection.Execute("dbo.CustomerTable_UpdateRow @Customer_ID, @FirstName, @LastName, @TotalPurchases", customer);
                }
            }
        }

        public static void UpdateCustomerSpending(CustomerModel customerToUpdate, decimal newTotalPurchases)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                connection.Execute("dbo.CustomerTable_UpdateTotalSpending @Customer_ID, @newTotalPurchases",
                    new { Customer_ID = customerToUpdate.Customer_ID, newTotalPurchases = newTotalPurchases });
            }
        }

        public static void InsertNewCustomer(List<CustomerModel> newCustomers)
        {
            throw new NotImplementedException();
        }

        public static void InsertNewCustomersWithID(List<CustomerModel> newCustomers)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                foreach (CustomerModel customer in newCustomers)
                {
                    connection.Execute("dbo.CustomerTable_InsertCustomerWithID @FirstName, @LastName, @TotalPurchases", customer);
                }
            }
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
