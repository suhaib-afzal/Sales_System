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
    //This is the CustomerDatabase this will display a readonly DataGrid with "Add boxes" and "Update Boxes"
    //These boxes will be editble by the user and they can enter their values into them to add and update rows
    //When the user clicks on a row (selects it) the update bpoxes will immieditly get the existing values to be changed.
    public class CustomerDatabaseViewModel : Screen, ITableViewModel
    {
        private CustomerModel? _selectedCustomerDataGrid;

        public static List<CustomerModel> OriginalCustomerDataGrid { get; set; } = CustomerData.GetCustomerTable();

        public BindableCollection<CustomerModel> CustomerDataGrid { get; set; } = new BindableCollection<CustomerModel>(OriginalCustomerDataGrid);

        public List<CustomerModel> UpdatedRows { get; set; } = new List<CustomerModel>();

        public List<CustomerModel> AddedRows { get; set; } = new List<CustomerModel>();

        public ValidIDManager validIDManager { get; set; }

        //When user selects a different row will update the Update row boxes
        public CustomerModel? SelectedCustomerDataGrid
        {
            get
            {
                return _selectedCustomerDataGrid;
            }
            set
            {
                _selectedCustomerDataGrid = value;
                NotifyOfPropertyChange(() => SelectedCustomerDataGrid);
                ResetUpdateInputs();
            }
        }

        public CustomerDatabaseViewModel(ValidIDManager ValidIDManager)
        {
            validIDManager = ValidIDManager;
        }

        //Update boxes
        public string? UpdateFirstName { get; set; }

        public string? UpdateLastName { get; set; }

        public string? UpdateTotalPurchases { get; set; }


        //Add boxes
        public string? AddFirstName { get; set; }

        public string? AddLastName { get; set; }

        public string? AddTotalPurchases { get; set; }


        public bool isSaved { get; private set; } = true;

        public void ResetUpdateInputs()
        {
            if (SelectedCustomerDataGrid == null)
            {
                UpdateFirstName = null;
                UpdateLastName = null;
                UpdateTotalPurchases = null;

            }
            else
            {
                UpdateFirstName = SelectedCustomerDataGrid.FirstName;
                UpdateLastName = SelectedCustomerDataGrid.LastName;
                UpdateTotalPurchases = $"{SelectedCustomerDataGrid.TotalPurchases}";

            }

            NotifyOfPropertyChange(() => UpdateFirstName);
            NotifyOfPropertyChange(() => UpdateLastName);
            NotifyOfPropertyChange(() => UpdateTotalPurchases);

        }

        public void ResetAddInputs()
        {
            AddFirstName = null;
            AddLastName = null;
            AddTotalPurchases = null;


            NotifyOfPropertyChange(() => AddFirstName);
            NotifyOfPropertyChange(() => AddLastName);
            NotifyOfPropertyChange(() => AddTotalPurchases);

        }

        //The save method is divided into sections because we do not want to send an
        //empty list to the Data Access classes to update the Database
        //We could tackle this differently by changing the methods themselves
        public void Save()
        {
            if (UpdatedRows.Count > 0)
            {
                CustomerData.UpdateCustomerTable(UpdatedRows);
                UpdatedRows.Clear();
            }

            if (AddedRows.Count > 0)
            {
                CustomerData.InsertNewCustomersWithID(AddedRows);
                validIDManager.ResetCustomerIDs();
                AddedRows.Clear();
            }

            if (AddedRows.Count > 0 || UpdatedRows.Count > 0)
            {
                SelectedCustomerDataGrid = null;
                ResetUpdateInputs();

                ResetAddInputs();

                OriginalCustomerDataGrid = CustomerData.GetCustomerTable();
                CustomerDataGrid = new BindableCollection<CustomerModel>(OriginalCustomerDataGrid);
                NotifyOfPropertyChange(() => CustomerDataGrid);
            }
        }

        public void ResetAll()
        {
            UpdatedRows.Clear();
            AddedRows.Clear();
            validIDManager.ResetCustomerIDs();

            SelectedCustomerDataGrid = null;
            ResetUpdateInputs();

            ResetAddInputs();

            OriginalCustomerDataGrid = CustomerData.GetCustomerTable();
            CustomerDataGrid = new BindableCollection<CustomerModel>(OriginalCustomerDataGrid);
            NotifyOfPropertyChange(() => CustomerDataGrid);
        }

        //Bound to the update button. Will validate selections and make sure that the new
        //values are actually different.
        //Will also change the DataGrid visible to the user.
        public void UpdateRow()
        {
            if (SelectedCustomerDataGrid == null)
            {
                MessageBox.Show("Please make sure a row is selected to be updated before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }


            bool successfulFirstName = ValidateName(UpdateFirstName, "First Name");
            bool succesfulLastName = ValidateName(UpdateLastName, "Last Name");
            decimal UpdateTotalPurchaseDecimal = ValidatePrices(UpdateTotalPurchases, "Total Spent");


            if (successfulFirstName == false ||
                succesfulLastName == false ||
                UpdateTotalPurchaseDecimal == -1m )
            {
                return;
            }

            if (UpdateFirstName == SelectedCustomerDataGrid.FirstName &&
                UpdateLastName == SelectedCustomerDataGrid.LastName &&
                UpdateTotalPurchaseDecimal == SelectedCustomerDataGrid.TotalPurchases)
            {
                MessageBox.Show("Please change the row before attempting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }

            CustomerModel UpdatedProduct = new CustomerModel
            {
                Customer_ID = SelectedCustomerDataGrid.Customer_ID,
                FirstName = UpdateFirstName,
                LastName = UpdateLastName,
                TotalPurchases = UpdateTotalPurchaseDecimal
            };

            //Changes the Datagrid visible to user
            int SelectedIndex = CustomerDataGrid.ToList().FindIndex(p => p == SelectedCustomerDataGrid);

            CustomerDataGrid[SelectedIndex] = UpdatedProduct;
            NotifyOfPropertyChange(() => CustomerDataGrid);

            //Adds to updated rows
            UpdatedRows = AddToUpdatedRows(UpdatedRows, UpdatedProduct);

            //Resets
            SelectedCustomerDataGrid = null;
            NotifyOfPropertyChange(() => SelectedCustomerDataGrid);
            ResetUpdateInputs();
        }

        //Bound the Add button. Will validate the inputted values.
        //Will add the row to the visible DataGrid
        public void AddNew()
        {
            bool successfulFirstName = ValidateName(AddFirstName, "First Name");
            bool succesfulLastName = ValidateName(AddLastName, "Last Name");
            decimal AddTotalPuchaseDecimal = ValidatePrices(AddTotalPurchases, "Total Spent");

            if (successfulFirstName == false ||
                succesfulLastName == false ||
                AddTotalPuchaseDecimal == -1m)
            {
                return;
            }

            CustomerModel AddedProduct = new CustomerModel
            {
                Customer_ID = validIDManager.GenerateValidCustomerID(),
                FirstName = AddFirstName,
                LastName = AddLastName,
                TotalPurchases = AddTotalPuchaseDecimal
            };

            //Adds to the visible DataGrid
            CustomerDataGrid.Add(AddedProduct);
            NotifyOfPropertyChange(() => CustomerDataGrid);

            //Adds to Added rows
            AddedRows.Add(AddedProduct);
            ResetAddInputs();
        }

        //Ensures the Updated row is not a duplicate in the UpdatedRows List.
        public List<CustomerModel> AddToUpdatedRows(List<CustomerModel> ListUpdatedProducts, CustomerModel ToUpdateProd)
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

        public bool ValidateName(string? Name, string VarName)
        {
            if (Name == null || Name == "")
            {
                MessageBox.Show($"Please make sure {VarName} is not empty before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return false;
            }
            if (Name.Count() > 20 || Name.Count() <= 0)
            {
                MessageBox.Show($"Length of {VarName} is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return false;
            }

            return true;
        }


        public decimal ValidatePrices(string? PriceString, string VarName)
        {
            if (PriceString == null || PriceString == "")
            {
                MessageBox.Show($"Please make sure {VarName} is not empty before attemting to Update",
                                 "Update Error",
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
