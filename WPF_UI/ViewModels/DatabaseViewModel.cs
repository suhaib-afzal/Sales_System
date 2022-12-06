using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System_UI.ViewModels
{
    public class DatabaseViewModel : Conductor<object>
    {
        public void LoadProducts()
        {
            ActivateItemAsync(new ProductDatabaseViewModel());
        }

        public void LoadCustomers()
        {
            ActivateItemAsync(new CustomerDatabaseViewModel());
        }

        public void LoadPurchases()
        {
            ActivateItemAsync(new PurchaseDatabaseViewModel());
        }

        public void LoadCarts()
        {
            ActivateItemAsync(new CartDatabaseViewModel());
        }
    }
}
