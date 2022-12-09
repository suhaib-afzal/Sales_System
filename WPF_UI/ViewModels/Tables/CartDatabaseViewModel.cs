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
    public class CartDatabaseViewModel : Screen, ITableViewModel
    {
        public List<DisplayCartModel> CartDataGrid { get; set; } = CartData.GetCartTable();

        public List<CartModel> UpdatedRows { get; set; } = new List<CartModel>();

        public List<CartModel> AddedRows { get; set; } = new List<CartModel>();

        public bool isSaved { get; private set; } = true;

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
