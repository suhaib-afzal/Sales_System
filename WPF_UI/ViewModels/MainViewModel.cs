using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Sales_System_UI.ViewModels
{
    public class MainViewModel
    {
        IWindowManager manager = new WindowManager();

        SalesViewModel salesViewModel = new SalesViewModel();

        DatabaseViewModel databaseViewModel = new DatabaseViewModel();

        public MainViewModel()
        {
            salesViewModel.SalesUpdatedEvent += SalesViewModel_SalesUpdatedEvent;
            databaseViewModel.DatabaseUpdatedEvent += DatabaseViewModel_DatabaseUpdatedEvent;
        }

        private void DatabaseViewModel_DatabaseUpdatedEvent(object? sender, EventArgs e)
        {
            salesViewModel.ClearEverything();
        }

        private void SalesViewModel_SalesUpdatedEvent(object? sender, EventArgs e)
        {
            databaseViewModel.ResetAll();
        }

        public void LaunchSales()
        {
            manager.ShowWindowAsync(salesViewModel, null, null);
        }

        public void LaunchDatabase()
        {
            manager.ShowWindowAsync(databaseViewModel, null, null);
        }
    }
}
