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
        public event EventHandler? DatabaseUpdatedEvent;

        public DatabaseViewModel()
        {
            ValidIDManager connectingIDManager = new ValidIDManager();
            Items.Add(new ProductDatabaseViewModel(connectingIDManager));
            Items.Add(new CustomerDatabaseViewModel(connectingIDManager));
            Items.Add(new CartDatabaseViewModel(connectingIDManager));
            Items.Add(new PurchaseDatabaseViewModel(connectingIDManager));
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

        public void LoadCarts()
        {
            ActivateItemAsync(Items[2]);
        }

        public void LoadPurchases()
        {
            ActivateItemAsync(Items[3]);
        }

        public void SaveAll()
        {
            Items.Apply(p => p.Save());
            DatabaseUpdatedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void ResetAll()
        {
            Items.Apply(p => p.ResetAll());
        }


        /*public void OnClose()
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
        }*/
    }
}
