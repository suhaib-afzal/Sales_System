using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Library.DataModels;

namespace Class_Library.DiscountModels
{
    public interface IDiscountModel
    {
        /// <summary>
        /// Name of the discount. For example: 2 for 1 Chicken.
        /// </summary>
        public string DiscountName { get; }

        /// <summary>
        /// Products affected if applicable
        /// </summary>
        public ProductModel Product_Affected { get; }

        /// <summary>
        /// Checks condition under which the Discount is to be applied.
        /// Can use CartModel props: DateTime, Customer.
        /// </summary>
        /// <param name="purchases"> List of of all purchases being transacted.
        /// Intended to reference Purchases under CartModel </param>
        /// <returns> True if DIscount is to be applied </returns>
        public bool DiscountCondition(List<PurchaseModel> purchases);

        /// <summary>
        /// Apply the discount calculation. Can use CartModel props: DateTime, Customer.
        /// </summary>
        /// <param name="purchases"> List of of all purchases being transacted.
        /// Intended to reference Purchases under CartModel </param>
        /// <returns> The monetary amount to be subtracted from the total</returns>
        public decimal ApplyDiscount(List<PurchaseModel> purchases);
    }
}
