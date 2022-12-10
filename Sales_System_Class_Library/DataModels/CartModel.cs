using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Library.DiscountModels;

namespace Class_Library.DataModels
{
    public class CartModel
    {
        public int? Cart_ID { get; set; }

        /// <summary>
        /// A list of all Purchases in a Cart, 
        /// i.e all items(x quantity) bought by a customer.
        /// </summary>
        public List<PurchaseModel> Purchases { get; set; } = new List<PurchaseModel>();

        /// <summary>
        /// Discounts being applied at time of purchase.
        /// </summary>
        public List<IDiscountModel> ActiveDiscounts { get; set; }

        /// <summary>
        /// Date and Time of purchased Cart.
        /// </summary>
        public DateTime? TimeofPurchase { get; set; }

        /// <summary>
        /// Customer involved in the Purchase.
        /// </summary>
        public CustomerModel? Customer { get; set; } = new CustomerModel();

        /// <summary>
        /// Profit that would be made in this Cart transaction.
        /// </summary>
        public decimal? ProfitMade { get; set; }

        /// <summary>
        /// Constructor for testing. Allows an input of Discounts.
        /// </summary>
        /// <param name="activeDiscounts"> List of Discounts to be used </param>
        public CartModel(List<IDiscountModel> activeDiscounts)
        {
            ActiveDiscounts = activeDiscounts;
        }

        /// <summary>
        /// Defualt Constructor to be used.
        /// </summary>
        public CartModel()
        {
            ActiveDiscounts = new List<IDiscountModel> { };
        }



        /// <summary>
        /// Calculates the total cost of this cart without discounts.
        /// </summary>
        /// <returns>
        /// The monetary (decimal) cost of the cart without discounts.
        /// </returns>
        public decimal CalculateSubTotal()
        {

            decimal subTotal = Purchases.Sum(p => p.Product.SalePrice * p.Quantity);
            return subTotal;

        }

        public List<IDiscountModel> FindApplicableDiscounts()
        {
            List<IDiscountModel> applicableDiscounts = new List<IDiscountModel>();
            foreach (IDiscountModel discount in ActiveDiscounts)
            {
                if (discount.DiscountCondition(Purchases))
                {
                    applicableDiscounts.Add(discount);
                }
            }
            return applicableDiscounts;
        }

        /// <summary>
        /// Applies Discounts to Subtotal.
        /// </summary>
        /// <returns>
        /// Returns final total to be paid
        /// </returns>
        public decimal CalculateTotal(List<IDiscountModel> applicableDiscounts)
        {

            decimal subTotal = CalculateSubTotal();
            foreach (IDiscountModel discount in applicableDiscounts)
            {
                if (discount.DiscountCondition(Purchases))
                {
                    subTotal -= discount.ApplyDiscount(Purchases);
                }
            }
            return subTotal;
        }

        /// <summary>
        /// Calculates the total profit of this cart for the business.
        /// </summary>
        /// <returns>
        /// The monetary (decimal) profit made in this cart.
        /// </returns>
        public decimal CalculateProfit(decimal FinalTotal)
        {
            decimal output =
                FinalTotal - Purchases.Sum(p => p.Product.CostPrice * p.Quantity);

            return output;
        }
    }
}
