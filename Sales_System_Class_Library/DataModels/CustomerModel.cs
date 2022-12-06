using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Library.DataModels
{
    public class CustomerModel
    {
        public int Customer_ID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        /// <summary>
        /// The stored total money spent by this customer at the store so far.
        /// </summary>
        public decimal TotalPurchases { get; set; } = 0M;
    }
}
