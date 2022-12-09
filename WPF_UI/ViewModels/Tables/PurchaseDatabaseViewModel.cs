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
    public class PurchaseDatabaseViewModel : Screen, ITableViewModel
    {
        public List<DisplayPurchaseModel> PurchaseDataGrid { get; set; } = PurchaseData.GetPurchaseTable();

        public List<DisplayPurchaseModel> UpdatedRows { get; set; } = new List<DisplayPurchaseModel>();

        public List<DisplayPurchaseModel> AddedRows { get; set; } = new List<DisplayPurchaseModel>();

        public bool isSaved { get; private set; } = true;

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
