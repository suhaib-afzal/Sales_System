using Caliburn.Micro;
using Class_Library.DataAccess;
using Class_Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sales_System_UI.ViewModels.Tables
{
    public class CartDatabaseViewModel : Screen, ITableViewModel
    {
        private DisplayCartModel? _selectedCartDataGrid;

        public static List<DisplayCartModel> OriginalCartDataGrid { get; set; } = CartData.GetCartTable();

        public BindableCollection<DisplayCartModel> CartDataGrid { get; set; } = new BindableCollection<DisplayCartModel>(OriginalCartDataGrid);

        public List<DisplayCartModel> UpdatedRows { get; set; } = new List<DisplayCartModel>();

        public List<DisplayCartModel> AddedRows { get; set; } = new List<DisplayCartModel>();

        public ValidIDManager validIDManager { get; set; }

        public DisplayCartModel? SelectedCartDataGrid
        {
            get
            {
                return _selectedCartDataGrid;
            }
            set
            {
                _selectedCartDataGrid = value;
                NotifyOfPropertyChange(() => SelectedCartDataGrid);
                ResetUpdateInputs();
            }
        }

        public CartDatabaseViewModel(ValidIDManager ValidIDManager)
        {
            validIDManager = ValidIDManager;
        }

        public string? UpdateCustomerID { get; set; }

        public string? UpdateProfitMade { get; set; }

        public string? UpdateTimeOfPurchase { get; set; }



        public string? AddCustomerID { get; set; }

        public string? AddProfitMade { get; set; }

        public string? AddTimeOfPurchase { get; set; }

        public bool isSaved { get; private set; } = true;

        public void ResetUpdateInputs()
        {
            if (SelectedCartDataGrid == null)
            {
                UpdateCustomerID = null;
                UpdateProfitMade = null;
                UpdateTimeOfPurchase = null;

            }
            else
            {
                UpdateCustomerID = $"{ SelectedCartDataGrid.Customer_ID }";
                UpdateProfitMade = $"{ SelectedCartDataGrid.ProfitMade }";
                UpdateTimeOfPurchase = $"{ SelectedCartDataGrid.TimeofPurchase }";

            }

            NotifyOfPropertyChange(() => UpdateCustomerID);
            NotifyOfPropertyChange(() => UpdateProfitMade);
            NotifyOfPropertyChange(() => UpdateTimeOfPurchase);

        }

        public void ResetAddInputs()
        {
            AddCustomerID = null;
            AddProfitMade = null;
            AddTimeOfPurchase = null;


            NotifyOfPropertyChange(() => AddCustomerID);
            NotifyOfPropertyChange(() => AddProfitMade);
            NotifyOfPropertyChange(() => AddTimeOfPurchase);

        }

        public void Save()
        {
            if (UpdatedRows.Count > 0)
            {
                CartData.UpdateCartTable(UpdatedRows);
                UpdatedRows.Clear();
            }

            if (AddedRows.Count > 0)
            {
                CartData.InsertNewCartsWithID(AddedRows);
                validIDManager.ResetCartIDs();
                AddedRows.Clear();
            }

            if (AddedRows.Count > 0 || UpdatedRows.Count > 0)
            {
                SelectedCartDataGrid = null;
                ResetUpdateInputs();

                ResetAddInputs();

                OriginalCartDataGrid = CartData.GetCartTable();
                CartDataGrid = new BindableCollection<DisplayCartModel>(OriginalCartDataGrid);
                NotifyOfPropertyChange(() => CartDataGrid);
            }
        }

        public void ResetAll()
        {
            UpdatedRows.Clear();
            AddedRows.Clear();
            validIDManager.ResetCartIDs();

            SelectedCartDataGrid = null;
            ResetUpdateInputs();

            ResetAddInputs();

            OriginalCartDataGrid = CartData.GetCartTable();
            CartDataGrid = new BindableCollection<DisplayCartModel>(OriginalCartDataGrid);
            NotifyOfPropertyChange(() => CartDataGrid);
        }

        public void UpdateRow()
        {
            if (SelectedCartDataGrid == null)
            {
                MessageBox.Show("Please make sure a row is selected to be updated before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }


            int UpdateCustomer_IDInt = ValidateCustomer_ID(UpdateCustomerID);
            decimal UpdateProfitMadeDecimal = ValidatePrices(UpdateProfitMade, "ProfitMade");
            DateTime PurchaseDateTime = ValidateDateTime(UpdateTimeOfPurchase);


            if (UpdateCustomer_IDInt == -1 ||
                UpdateProfitMadeDecimal == -1m ||
                PurchaseDateTime == DateTime.MinValue)
            {
                return;
            }

            if (UpdateCustomer_IDInt == SelectedCartDataGrid.Customer_ID &&
                UpdateProfitMadeDecimal == SelectedCartDataGrid.ProfitMade &&
                PurchaseDateTime == SelectedCartDataGrid.TimeofPurchase)
            {
                MessageBox.Show("Please change the row before attempting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }

            DisplayCartModel UpdatedProduct = new DisplayCartModel
            {
                Cart_ID = SelectedCartDataGrid.Cart_ID,
                Customer_ID = UpdateCustomer_IDInt,
                ProfitMade = UpdateProfitMadeDecimal,
                TimeofPurchase = PurchaseDateTime
            };

            int SelectedIndex = CartDataGrid.ToList().FindIndex(p => p == SelectedCartDataGrid);

            CartDataGrid[SelectedIndex] = UpdatedProduct;
            NotifyOfPropertyChange(() => CartDataGrid);

            UpdatedRows = AddToUpdatedRows(UpdatedRows, UpdatedProduct);

            SelectedCartDataGrid = null;
            NotifyOfPropertyChange(() => SelectedCartDataGrid);
            ResetUpdateInputs();
        }

        public void AddNew()
        {
            int AddCustomer_IDInt = ValidateCustomer_ID(AddCustomerID);
            decimal AddProfitMadeDecimal = ValidatePrices(AddProfitMade, "ProfitMade");
            DateTime AddPurchaseDateTime = ValidateDateTime(AddTimeOfPurchase);

            if (AddCustomer_IDInt == -1 ||
                AddProfitMadeDecimal == -1m ||
                AddPurchaseDateTime == DateTime.MinValue)
            {
                return;
            }

            DisplayCartModel AddedProduct = new DisplayCartModel
            {
                Cart_ID = validIDManager.GenerateValidCartID(),
                Customer_ID = AddCustomer_IDInt,
                ProfitMade = AddProfitMadeDecimal,
                TimeofPurchase = AddPurchaseDateTime
            };

            CartDataGrid.Add(AddedProduct);
            NotifyOfPropertyChange(() => CartDataGrid);

            AddedRows.Add(AddedProduct);
            ResetAddInputs();
        }

        public List<DisplayCartModel> AddToUpdatedRows(List<DisplayCartModel> ListUpdatedProducts, DisplayCartModel ToUpdateProd)
        {
            int IndexOfLastTimeUpdatedThisProduct = ListUpdatedProducts.FindLastIndex(p => p.Customer_ID == ToUpdateProd.Customer_ID);
            if (IndexOfLastTimeUpdatedThisProduct == -1)
            {
                ListUpdatedProducts.Add(ToUpdateProd);
                return ListUpdatedProducts;
            }
            else
            {
                ListUpdatedProducts[IndexOfLastTimeUpdatedThisProduct] = ToUpdateProd;
                return ListUpdatedProducts;
            }
        }

        public int ValidateCustomer_ID(string? Customer_IDString)
        {
            if (Customer_IDString == null || Customer_IDString == "")
            {
                MessageBox.Show($"Please make sure Customer_ID is not empty before attemting",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (!int.TryParse(Customer_IDString, out int Customer_IDInt))
            {
                MessageBox.Show("Customer_ID cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (!validIDManager.ValidCustomerIDs.Exists(p => p == Customer_IDInt))
            {
                MessageBox.Show("Customer_ID does not exist",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            return Customer_IDInt;
        }

        public DateTime ValidateDateTime(string? TimeofPurchaseString)
        {
            if (TimeofPurchaseString == null || TimeofPurchaseString == "")
            {
                MessageBox.Show($"Please make sure TimeofPurchase is not empty before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                return DateTime.MinValue;
            }

            if(!DateTime.TryParse(TimeofPurchaseString, out DateTime PurchaseDateTime))
            {
                MessageBox.Show("TimeofPurchase cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                return DateTime.MinValue;
            }
            if (PurchaseDateTime == DateTime.MinValue)
            {
                MessageBox.Show($"TimeofPurchase is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                return DateTime.MinValue;
            }

            return PurchaseDateTime;
        }


        public decimal ValidatePrices(string? PriceString, string VarName)
        {
            if (PriceString == null || PriceString == "")
            {
                MessageBox.Show($"Please make sure {VarName} is not empty before attemting to Update",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1m;
            }
            if (!decimal.TryParse(PriceString, out decimal Price))
            {
                MessageBox.Show($"{VarName} cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1m;
            }

            if (Price > decimal.MaxValue || Price < 0)
            {
                MessageBox.Show($"{VarName} is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1m;
            }

            return Price;
        }
    }
}
