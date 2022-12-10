using Class_Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System_UI.ViewModels.Tables
{
    public class ValidIDManager
    {
        public List<int> ValidProductIDs = ProductData.GetProductTable().ConvertAll(p => p.Product_ID);

        public List<int> ValidCustomerIDs = CustomerData.GetCustomerTable().ConvertAll(p => p.Customer_ID);

        public List<int> ValidCartIDs = CartData.GetCartTable().ConvertAll(p => p.Cart_ID);

        public List<int> ValidPurchaseIDs = PurchaseData.GetPurchaseTable().ConvertAll(p => p.Purchase_ID);

        public int GenerateValidProductID()
        {
            int MaxValidProductID = ValidProductIDs.Max();
            ValidProductIDs.Add(MaxValidProductID + 1);
            return MaxValidProductID +1;
        }

        public int GenerateValidCustomerID()
        {
            int MaxValidCustomerID = ValidCustomerIDs.Max();
            ValidCustomerIDs.Add(MaxValidCustomerID + 1);
            return MaxValidCustomerID + 1;
        }

        public int GenerateValidCartID()
        {
            int MaxValidCartID = ValidCartIDs.Max();
            ValidCartIDs.Add(MaxValidCartID + 1);
            return MaxValidCartID + 1;
        }

        public int GenerateValidPurchaseID()
        {
            int MaxValidPurchaseID = ValidPurchaseIDs.Max();
            ValidPurchaseIDs.Add(MaxValidPurchaseID + 1);
            return MaxValidPurchaseID + 1;
        }

        public void ResetProductIDs()
        {
            ValidProductIDs = ProductData.GetProductTable().ConvertAll(p => p.Product_ID);
        }

        public void ResetCustomerIDs()
        {
            ValidCustomerIDs = CustomerData.GetCustomerTable().ConvertAll(p => p.Customer_ID);
        }

        public void ResetCartIDs()
        {
            ValidCartIDs = CartData.GetCartTable().ConvertAll(p => p.Cart_ID);
        }

        public void ResetPurchaseIDs()
        {
            ValidPurchaseIDs = PurchaseData.GetPurchaseTable().ConvertAll(p => p.Purchase_ID);
        }
    }
}
