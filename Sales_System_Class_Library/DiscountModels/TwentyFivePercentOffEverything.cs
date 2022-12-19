using Class_Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Library.DiscountModels
{
    public class TwentyFivePercentOffEverything : IDiscountModel
    {
        public string DiscountName { get; set; } = "25% Off Everything!";

        public ProductModel? Product_Affected { get; set; } = null;

        public bool DiscountCondition(List<PurchaseModel> Purchases)
        {
            return true;
        }

        public decimal ApplyDiscount(List<PurchaseModel> Purchases)
        {
            decimal Total = Purchases.Sum(p => p.Product.SalePrice);
            return Total * 0.25m;
        }
    }
}
