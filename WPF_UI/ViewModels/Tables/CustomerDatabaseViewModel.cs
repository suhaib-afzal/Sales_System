using Caliburn.Micro;
using Class_Library.DataAccess;
using Class_Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System_UI.ViewModels.Tables
{
    public class CustomerDatabaseViewModel : Screen, ITableViewModel
    {
        public List<CustomerModel> CustomerDataGrid { get; set; } = CustomerData.GetCustomerTable();

        public List<CustomerModel> UpdatedRows { get; set; } = new List<CustomerModel>();

        public List<CustomerModel> AddedRows { get; set; } = new List<CustomerModel>();

        public bool isSaved { get; private set; } = true;

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
