using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Library.DataModels
{
    /// <summary>
    /// A Purchase of a Single Item
    /// </summary>
    public class PurchaseModel
    {
        /// <summary>
        /// Product in this purchase
        /// </summary>
        public ProductModel Product { get; set; }

        /// <summary>
        /// Number of the product being purchased
        /// </summary>
        public uint Quantity { get; set; }

        public PurchaseModel(ProductModel product, uint quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public PurchaseModel()
        {
            Product = new ProductModel();
        }

        /// <summary>
        /// This static method will take a List of PurchaseModels and combine them into one
        /// If they have the same Product (checked by equality of ProductModel and not Product_ID)
        /// </summary>
        /// <param name="inPurchaseModels"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static PurchaseModel Merge(List<PurchaseModel> inPurchaseModels)
        {
            if (inPurchaseModels.Count > 0)
            {
                ProductModel firstProduct = inPurchaseModels[0].Product;
                if (inPurchaseModels.TrueForAll(p => p.Product == firstProduct))
                {
                    return new PurchaseModel(firstProduct, (uint)inPurchaseModels.Sum(p => p.Quantity));
                }
                else
                {
                    throw new ArgumentException("All Purchase Models to be merged should have the same Product");
                }
            }
            else
            {
                return new PurchaseModel();
            }
        }
    }
}
