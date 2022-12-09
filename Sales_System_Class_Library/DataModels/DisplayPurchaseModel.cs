using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Library.DataModels
{
    public class DisplayPurchaseModel
    {
        public int Purchase_ID { get; set; }

        public int Cart_ID { get; set; }

        public int Product_ID { get; set; }

        public int Quantity { get; set; }
    }
}
