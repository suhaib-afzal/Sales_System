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
    //This is the PurchaseDatabase this will display a readonly DataGrid with "Add boxes" and "Update Boxes"
    //These boxes will be editble by the user and they can enter their values into them to add and update rows
    //When the user clicks on a row (selects it) the update bpoxes will immieditly get the existing values to be changed.
    public class PurchaseDatabaseViewModel : Screen, ITableViewModel
    {
        private DisplayPurchaseModel? _selectedPurchaseDataGrid;

        public static List<DisplayPurchaseModel> OriginalPurchaseDataGrid { get; set; } = PurchaseData.GetPurchaseTable();

        public BindableCollection<DisplayPurchaseModel> PurchaseDataGrid { get; set; } = new BindableCollection<DisplayPurchaseModel>(OriginalPurchaseDataGrid);

        public List<DisplayPurchaseModel> UpdatedRows { get; set; } = new List<DisplayPurchaseModel>();

        public List<DisplayPurchaseModel> AddedRows { get; set; } = new List<DisplayPurchaseModel>();

        public ValidIDManager validIDManager { get; set; }

        //When user selects a different row will update the Update row boxes
        public DisplayPurchaseModel? SelectedPurchaseDataGrid
        {
            get
            {
                return _selectedPurchaseDataGrid;
            }
            set
            {
                _selectedPurchaseDataGrid = value;
                NotifyOfPropertyChange(() => SelectedPurchaseDataGrid);
                ResetUpdateInputs();
            }
        }

        public PurchaseDatabaseViewModel(ValidIDManager ValidIDManager)
        {
            validIDManager = ValidIDManager;
        }

        public string? UpdateCartID { get; set; }

        public string? UpdateProductID { get; set; }

        public string? UpdateQuantity { get; set; }



        public string? AddCartID { get; set; }

        public string? AddProductID { get; set; }

        public string? AddQuantity { get; set; }

        public bool isSaved { get; private set; } = true;

        public void ResetUpdateInputs()
        {
            if (SelectedPurchaseDataGrid == null)
            {
                UpdateCartID = null;
                UpdateProductID = null;
                UpdateQuantity = null;

            }
            else
            {
                UpdateCartID = $"{SelectedPurchaseDataGrid.Cart_ID}";
                UpdateProductID = $"{SelectedPurchaseDataGrid.Product_ID}";
                UpdateQuantity = $"{SelectedPurchaseDataGrid.Quantity}";

            }

            NotifyOfPropertyChange(() => UpdateCartID);
            NotifyOfPropertyChange(() => UpdateProductID);
            NotifyOfPropertyChange(() => UpdateQuantity);

        }

        public void ResetAddInputs()
        {
            AddCartID = null;
            AddProductID = null;
            AddQuantity = null;


            NotifyOfPropertyChange(() => AddCartID);
            NotifyOfPropertyChange(() => AddProductID);
            NotifyOfPropertyChange(() => AddQuantity);

        }

        //The save method is divided into sections because we do not want to send an
        //empty list to the Data Access classes to update the Database
        //We could tackle this differently by changing the methods themselves
        public void Save()
        {
            if (UpdatedRows.Count > 0)
            {
                PurchaseData.UpdatePurchaseTable(UpdatedRows);
                UpdatedRows.Clear();
            }

            if (AddedRows.Count > 0)
            {
                PurchaseData.InsertNewPurchasesWithID(AddedRows);
                validIDManager.ResetPurchaseIDs();
                AddedRows.Clear();
            }

            if (AddedRows.Count > 0 || UpdatedRows.Count > 0)
            {
                SelectedPurchaseDataGrid = null;
                ResetUpdateInputs();

                ResetAddInputs();

                OriginalPurchaseDataGrid = PurchaseData.GetPurchaseTable();
                PurchaseDataGrid = new BindableCollection<DisplayPurchaseModel>(OriginalPurchaseDataGrid);
                NotifyOfPropertyChange(() => PurchaseDataGrid);
            }
        }

        public void ResetAll()
        {
            UpdatedRows.Clear();
            validIDManager.ResetPurchaseIDs();
            AddedRows.Clear();

            SelectedPurchaseDataGrid = null;
            ResetUpdateInputs();

            ResetAddInputs();

            OriginalPurchaseDataGrid = PurchaseData.GetPurchaseTable();
            PurchaseDataGrid = new BindableCollection<DisplayPurchaseModel>(OriginalPurchaseDataGrid);
            NotifyOfPropertyChange(() => PurchaseDataGrid);
        }

        public void UpdateRow()
        {
            if (SelectedPurchaseDataGrid == null)
            {
                MessageBox.Show("Please make sure a row is selected to be updated before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }


            int UpdateCart_IDInt = ValidateCart_ID(UpdateCartID);
            int UpdateProduct_IDInt = ValidateProduct_ID(UpdateProductID);
            int QuantityInt = ValidateQuantity(UpdateQuantity);


            if (UpdateCart_IDInt == -1 ||
                UpdateProduct_IDInt == -1 ||
                QuantityInt == -1)
            {
                return;
            }

            if (UpdateCart_IDInt == SelectedPurchaseDataGrid.Cart_ID &&
                UpdateProduct_IDInt == SelectedPurchaseDataGrid.Product_ID &&
                QuantityInt == SelectedPurchaseDataGrid.Quantity)
            {
                MessageBox.Show("Please change the row before attempting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }

            DisplayPurchaseModel UpdatedProduct = new DisplayPurchaseModel
            {
                Purchase_ID = SelectedPurchaseDataGrid.Purchase_ID,
                Cart_ID = UpdateCart_IDInt,
                Product_ID = UpdateProduct_IDInt,
                Quantity = QuantityInt
            };

            //Changes the Datagrid visible to user
            int SelectedIndex = PurchaseDataGrid.ToList().FindIndex(p => p == SelectedPurchaseDataGrid);

            PurchaseDataGrid[SelectedIndex] = UpdatedProduct;
            NotifyOfPropertyChange(() => PurchaseDataGrid);

            //Adds to updated rows
            UpdatedRows = AddToUpdatedRows(UpdatedRows, UpdatedProduct);

            //Resets
            SelectedPurchaseDataGrid = null;
            NotifyOfPropertyChange(() => SelectedPurchaseDataGrid);
            ResetUpdateInputs();
        }
        //Bound the Add button. Will validate the inputted values.
        //Will add the row to the visible DataGrid
        public void AddNew()
        {
            int AddCart_IDInt = ValidateCart_ID(AddCartID);
            int AddProduct_IDInt = ValidateProduct_ID(AddProductID);
            int AddQuantityInt = ValidateQuantity(AddQuantity);

            if (AddCart_IDInt == -1 ||
                AddProduct_IDInt == -1 ||
                AddQuantityInt == -1)
            {
                return;
            }

            DisplayPurchaseModel AddedProduct = new DisplayPurchaseModel
            {
                Purchase_ID = validIDManager.GenerateValidPurchaseID(),
                Cart_ID = AddCart_IDInt,
                Product_ID = AddProduct_IDInt,
                Quantity = AddQuantityInt
            };

            //Adds to the visible DataGrid
            PurchaseDataGrid.Add(AddedProduct);
            NotifyOfPropertyChange(() => PurchaseDataGrid);

            //Adds to Added rows
            AddedRows.Add(AddedProduct);
            ResetAddInputs();
        }

        //Ensures the Updated row is not a duplicate in the UpdatedRows List.
        public List<DisplayPurchaseModel> AddToUpdatedRows(List<DisplayPurchaseModel> ListUpdatedProducts, DisplayPurchaseModel ToUpdateProd)
        {
            int IndexOfLastTimeUpdatedThisProduct = ListUpdatedProducts.FindLastIndex(p => p.Cart_ID == ToUpdateProd.Cart_ID);
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
        
        public int ValidateCart_ID(string? Cart_IDString)
        {
            if (Cart_IDString == null || Cart_IDString == "")
            {
                MessageBox.Show($"Please make sure Cart_ID is not empty before attemting to Update",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (!int.TryParse(Cart_IDString, out int Cart_IDInt))
            {
                MessageBox.Show("Cart_ID cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (!validIDManager.ValidCartIDs.Exists(p => p == Cart_IDInt))
            {
                MessageBox.Show("Cart_ID does not exist",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            return Cart_IDInt;
        }

        public int ValidateProduct_ID(string? Product_IDString)
        {
            if (Product_IDString == null || Product_IDString == "")
            {
                MessageBox.Show($"Please make sure Product_ID is not empty before attemting to Update",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (!int.TryParse(Product_IDString, out int Product_IDInt))
            {
                MessageBox.Show("Product_ID cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (!validIDManager.ValidProductIDs.Exists(p => p == Product_IDInt))
            {
                MessageBox.Show("Product_ID does not exist",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            return Product_IDInt;
        }


        public int ValidateQuantity(string? QuantityString)
        {
            if (QuantityString == null || QuantityString == "")
            {
                MessageBox.Show($"Please make sure Quantity is not empty before attemting to Update",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }
            if (!int.TryParse(QuantityString, out int QuantInt))
            {
                MessageBox.Show($"Quantity cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (QuantInt > int.MaxValue || QuantInt < 0)
            {
                MessageBox.Show($"Quantity is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            return QuantInt;
        }
    }
}
