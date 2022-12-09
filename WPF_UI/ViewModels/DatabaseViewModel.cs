using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_System_UI.ViewModels.Tables;
using System.Threading;
using System.Windows.Controls.Primitives;
using Xceed.Wpf.Toolkit;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace Sales_System_UI.ViewModels
{
    public class DatabaseViewModel : Conductor<ITableViewModel>.Collection.OneActive
    {
        public DatabaseViewModel()
        {
            Items.Add(new ProductDatabaseViewModel());
            Items.Add(new CustomerDatabaseViewModel());
            Items.Add(new PurchaseDatabaseViewModel());
            Items.Add(new CartDatabaseViewModel());
            LoadProducts();
        }

        public void LoadProducts()
        {
            ActivateItemAsync(Items[0]);
        }

        public void LoadCustomers()
        {
            ActivateItemAsync(Items[1]);
        }

        public void LoadPurchases()
        {
            ActivateItemAsync(Items[2]);
        }

        public void LoadCarts()
        {
            ActivateItemAsync(Items[3]);
        }

        public void SaveAll()
        {
            Items.Apply(p => p.Save());
        }

        public void OnClose()
        {
            bool CanClose = Items.ToList().TrueForAll(p => p.isSaved == true);

            if (!CanClose)
            {
                var MessageBoxResult = MessageBox.Show("Are you sure you want to close without saving?",
                                       "Cart Error",
                                       MessageBoxButton.OKCancel,
                                       MessageBoxImage.Warning);

                if (MessageBoxResult == MessageBoxResult.OK)
                {
                    //Close
                }
                else
                {
                    return;
                }

            }
            else
            {
                //close
            }
        }
    }
}
