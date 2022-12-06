using Class_Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System_UI.ViewModels
{
    public static class MockDataAccess
    {
        public static List<CustomerModel> GetCustomerModels()
        {
            List<CustomerModel> returnList = new List<CustomerModel>();
            returnList.Add(new CustomerModel { Customer_ID = 1, FirstName = "Roger", LastName = "Stevenson"});
            returnList.Add(new CustomerModel { Customer_ID = 2, FirstName = "Gabriel", LastName = "Wright" });
            returnList.Add(new CustomerModel { Customer_ID = 3, FirstName = "Emily", LastName = "Smith" });
            returnList.Add(new CustomerModel { Customer_ID = 4, FirstName = "Johnny", LastName = "TeamsCall" });
            returnList.Add(new CustomerModel { Customer_ID = 5, FirstName = "Guy", LastName = "Ritchie" });
            returnList.Add(new CustomerModel { Customer_ID = 6, FirstName = "Lady", LastName = "Poorie" });
            returnList.Add(new CustomerModel { Customer_ID = 7, FirstName = "Hugh", LastName = "Heffner" });
            returnList.Add(new CustomerModel { Customer_ID = 8, FirstName = "Gold", LastName = "Roger" });
            returnList.Add(new CustomerModel { Customer_ID = 9, FirstName = "Lady", LastName = "Poorie" });
            returnList.Add(new CustomerModel { Customer_ID = 10, FirstName = "Hugh", LastName = "Heffner" });
            returnList.Add(new CustomerModel { Customer_ID = 11, FirstName = "Gold", LastName = "Roger" });
            returnList.Add(new CustomerModel { Customer_ID = 12, FirstName = "Roger", LastName = "Stevenson" });
            returnList.Add(new CustomerModel { Customer_ID = 13, FirstName = "Gabriel", LastName = "Wright" });
            returnList.Add(new CustomerModel { Customer_ID = 14, FirstName = "Emily", LastName = "Smith" });
            returnList.Add(new CustomerModel { Customer_ID = 15, FirstName = "Johnny", LastName = "TeamsCall" });
            returnList.Add(new CustomerModel { Customer_ID = 16, FirstName = "Guy", LastName = "Ritchie" });
            returnList.Add(new CustomerModel { Customer_ID = 17, FirstName = "Lady", LastName = "Poorie" });
            returnList.Add(new CustomerModel { Customer_ID = 18, FirstName = "Hugh", LastName = "Heffner" });
            returnList.Add(new CustomerModel { Customer_ID = 19, FirstName = "Gold", LastName = "Roger" });
            returnList.Add(new CustomerModel { Customer_ID = 20, FirstName = "Lady", LastName = "Poorie" });
            returnList.Add(new CustomerModel { Customer_ID = 21, FirstName = "Hugh", LastName = "Heffner" });
            returnList.Add(new CustomerModel { Customer_ID = 22, FirstName = "Gold", LastName = "Roger" });

            return returnList;
        }

        public static List<ProductModel> GetProductModels()
        {
            List<ProductModel> returnList = new List<ProductModel>();
            returnList.Add(new ProductModel { Product_ID = 1, Name = "Hairbursh", SalePrice = 10M, Stock = 10 });
            returnList.Add(new ProductModel { Product_ID = 2, Name = "Prosthetic Arm", SalePrice = 100M, Stock = 3 });
            returnList.Add(new ProductModel { Product_ID = 3, Name = "Live Chicken", SalePrice = 20M, Stock = 1 });
            returnList.Add(new ProductModel { Product_ID = 4, Name = "Dead Chicken", SalePrice = 5M, Stock = 20 });
            returnList.Add(new ProductModel { Product_ID = 5, Name = "Lint Roller", SalePrice = 2.5M, Stock = 25 });
            returnList.Add(new ProductModel { Product_ID = 6, Name = "Notepad", SalePrice = 5M, Stock = 30 });
            returnList.Add(new ProductModel { Product_ID = 7, Name = "Paracetamol", SalePrice = 3.14M, Stock = 41 });

            return returnList;
        }
    }
}
