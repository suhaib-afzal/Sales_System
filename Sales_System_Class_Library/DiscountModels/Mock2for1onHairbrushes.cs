using Class_Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Library.DiscountModels
{
    public class Mock2for1onHairbrushes : IDiscountModel
    {
        public string DiscountName { get; } = "Two For One on Hairbrushes!";

        public ProductModel Product_Affected { get; } 
            = new ProductModel { Product_ID = 1, Name = "Hairbursh", SalePrice = 10M};

        public bool DiscountCondition(List<PurchaseModel> Purchases)
        {
            uint totalQuantity = GetTotalQuantity(Purchases);

            return totalQuantity >= 2;
        }

        public decimal ApplyDiscount(List<PurchaseModel> Purchases)
        {
            uint totalQuantity = GetTotalQuantity(Purchases);

            return (totalQuantity / 2) * Product_Affected.SalePrice;
        }

        private uint GetTotalQuantity(List<PurchaseModel> Purchases)
        {
            return (uint)Purchases.FindAll(p => p.Product.Product_ID == this.Product_Affected.Product_ID).Sum(p => p.Quantity);
        }
    }
}
