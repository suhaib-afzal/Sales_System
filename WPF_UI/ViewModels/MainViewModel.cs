using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Sales_System_UI.ViewModels
{
    //Parent view which will lanuch the Windows of the SalesView and the DatabaseView
    //Also allows us to manage the events raised when either the SalesView purchase is completed
    //or DatabaseView is saved, So that the other screen can be updated.
    public class MainViewModel
    {
        IWindowManager manager = new WindowManager();

        SalesViewModel salesViewModel = new SalesViewModel();

        DatabaseViewModel databaseViewModel = new DatabaseViewModel();

        //On Initiliasiation adds the Events assocaited with SalesUpdate (Purchase complete)
        //and DatabaseUpdate (Database updates saved)
        public MainViewModel()
        {
            salesViewModel.SalesUpdatedEvent += SalesViewModel_SalesUpdatedEvent;
            databaseViewModel.DatabaseUpdatedEvent += DatabaseViewModel_DatabaseUpdatedEvent;
        }

        //Here ClearEverything is Poorly named should be called ResetAll FIX
        private void DatabaseViewModel_DatabaseUpdatedEvent(object? sender, EventArgs e)
        {
            salesViewModel.ClearEverything();
        }

        private void SalesViewModel_SalesUpdatedEvent(object? sender, EventArgs e)
        {
            databaseViewModel.ResetAll();
        }

        //Bound to button "Make a Sale"
        public void LaunchSales()
        {
            manager.ShowWindowAsync(salesViewModel, null, null);
        }

        //Bound to button "Edit Database"
        public void LaunchDatabase()
        {
            manager.ShowWindowAsync(databaseViewModel, null, null);
        }
    }
}
