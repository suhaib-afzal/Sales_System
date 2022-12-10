using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Library.DataModels
{
    public class DisplayCartModel
    {
        public int Cart_ID { get; set; }

        public int Customer_ID { get; set; }

        public decimal ProfitMade { get; set; }

        public DateTime TimeofPurchase { get; set; }
    }
}
