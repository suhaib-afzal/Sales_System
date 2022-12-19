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

namespace Sales_System_UI.ViewModels
{
    //ViewModel for the DatabaseView I.E the view from which the user can manipulate and view the Database.
    //Contains 4 buttons composing a Naviagtion Bar, each of which will load a Table.
    public class DatabaseViewModel : Conductor<ITableViewModel>.Collection.OneActive
    {
        //Event for the Database being edited via this view to update the SalesView if opened
        public event EventHandler? DatabaseUpdatedEvent;

        //On intialisation will start each Table's view, and add it to Items.
        //Items is inherited from the Conductor Class, and is the set of Initialised Screens
        //one of which is active.
        //ValidIDManager is passed through each, allowing synced generation
        //of IDs when the user Adds new rows (which need new valid IDs).
        public DatabaseViewModel()
        {
            ValidIDManager connectingIDManager = new ValidIDManager();
            Items.Add(new ProductDatabaseViewModel(connectingIDManager));
            Items.Add(new CustomerDatabaseViewModel(connectingIDManager));
            Items.Add(new CartDatabaseViewModel(connectingIDManager));
            Items.Add(new PurchaseDatabaseViewModel(connectingIDManager));
            LoadProducts();
        }

        //Bound to a button in the Navbar
        //Active Item is bound to the the majority of the screen.
        //So when LoadCustomers is clicked the screen will switch to the CustomerDatabaseView
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

        //Calls Save of each Screen which in turn submits that Screens changes to the database.
        //Rasies the Database changes event, so that if the SalesView is open, it will update the SalesView
        public void SaveAll()
        {
            Items.Apply(p => p.Save());
            DatabaseUpdatedEvent?.Invoke(this, EventArgs.Empty);
        }

        //Resets all of the screens by calling their Reset method also called ResetAll
        public void ResetAll()
        {
            Items.Apply(p => p.ResetAll());
        }

        //TODO: Implement a "Are you sure you want to Close without saving" MessageBox
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
