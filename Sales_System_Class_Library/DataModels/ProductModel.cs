using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Class_Library.DataModels
{
    public class ProductModel
    {
        public int Product_ID { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        /// <summary>
        /// The number of this Item that are in stock.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// The per item price we are selling at.
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// The per item price we bought this item at.
        /// </summary>
        public decimal CostPrice { get; set; }


        public ProductModel(int product_ID, string name, string description,
                            int stock, decimal salePrice, decimal costPrice)
        {
            Product_ID = product_ID;
            Name = name;
            Description = description;
            Stock = stock;
            SalePrice = salePrice;
            CostPrice = costPrice;
        }

        public ProductModel()
        {
            Product_ID = 0;
            Name = "";
            Description = "";
            Stock = 0;
            SalePrice = 0;
            CostPrice = 0;
        }


    }
}
