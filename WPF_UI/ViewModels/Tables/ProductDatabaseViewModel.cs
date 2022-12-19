using Caliburn.Micro;
using Class_Library.DataAccess;
using Class_Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Sales_System_UI.ViewModels.Tables
{
    //This is the ProductDatabase this will display a readonly DataGrid with "Add boxes" and "Update Boxes"
    //These boxes will be editble by the user and they can enter their values into them to add and update rows
    //When the user clicks on a row (selects it) the update bpoxes will immieditly get the existing values to be changed.
    public class ProductDatabaseViewModel : Screen, ITableViewModel
    {
        private ProductModel? _selectedProductDataGrid;

        public static List<ProductModel> OriginalProductDataGrid { get; set; } = ProductData.GetProductTable();

        public BindableCollection<ProductModel> ProductDataGrid { get; set; } = new BindableCollection<ProductModel>(OriginalProductDataGrid);

        public List<ProductModel> UpdatedRows { get; set; } = new List<ProductModel>();

        public List<ProductModel> AddedRows { get; set; } = new List<ProductModel>();

        public ValidIDManager validIDManager { get; set; }

        //When user selects a different row will update the Update row boxes
        public ProductModel? SelectedProductDataGrid 
        {
            get
            {
                return _selectedProductDataGrid;
            }
            set
            {
                _selectedProductDataGrid = value;
                NotifyOfPropertyChange(() => SelectedProductDataGrid);
                ResetUpdateInputs();
            }
        }

        public ProductDatabaseViewModel(ValidIDManager ValidIDManager)
        {
            validIDManager = ValidIDManager;
        }

        //Update boxes
        public string? UpdateName { get; set; }
        public string? UpdateDescription { get; set; }

        public string? UpdateStock { get; set; }

        public string? UpdateSalePrice { get; set; }

        public string? UpdateCostPrice { get; set; }


        //Add boxes
        public string? AddName { get; set; }

        public string? AddDescription { get; set; }

        public string? AddStock { get; set; }

        public string? AddSalePrice { get; set; }

        public string? AddCostPrice { get; set; }



        public bool isSaved { get; private set; } = true;

        public void ResetUpdateInputs()
        {
            if (SelectedProductDataGrid == null)
            {
                UpdateName = null;
                UpdateDescription = null;
                UpdateStock = null;
                UpdateSalePrice = null;
                UpdateCostPrice = null;                
            }
            else
            {
                UpdateName = SelectedProductDataGrid.Name;
                UpdateDescription = SelectedProductDataGrid.Description;
                UpdateStock = $"{SelectedProductDataGrid.Stock}";
                UpdateSalePrice = $"{SelectedProductDataGrid.SalePrice}";
                UpdateCostPrice = $"{SelectedProductDataGrid.CostPrice}";
            }

            NotifyOfPropertyChange(() => UpdateName);
            NotifyOfPropertyChange(() => UpdateDescription);
            NotifyOfPropertyChange(() => UpdateStock);
            NotifyOfPropertyChange(() => UpdateSalePrice);
            NotifyOfPropertyChange(() => UpdateCostPrice);
        }

        public void ResetAddInputs()
        {
            AddName = null;
            AddDescription = null;
            AddStock = null;
            AddSalePrice = null;
            AddCostPrice = null;

            NotifyOfPropertyChange(() => AddName);
            NotifyOfPropertyChange(() => AddDescription);
            NotifyOfPropertyChange(() => AddStock);
            NotifyOfPropertyChange(() => AddSalePrice);
            NotifyOfPropertyChange(() => AddCostPrice);
        }

        //The save method is divided into sections because we do not want to send an
        //empty list to the Data Access classes to update the Database
        //We could tackle this differently by changing the methods themselves
        public void Save()
        {
            if (UpdatedRows.Count > 0)
            {
                ProductData.UpdateProductTable(UpdatedRows);
                UpdatedRows.Clear();
            }

            if (AddedRows.Count > 0)
            {
                ProductData.InsertNewProductsWithID(AddedRows);
                validIDManager.ResetProductIDs();
                AddedRows.Clear();
            }
            
            if (AddedRows.Count > 0 || UpdatedRows.Count > 0)
            {
                SelectedProductDataGrid = null;
                ResetUpdateInputs();

                ResetAddInputs();

                OriginalProductDataGrid = ProductData.GetProductTable();
                ProductDataGrid = new BindableCollection<ProductModel>(OriginalProductDataGrid);
                NotifyOfPropertyChange(() => ProductDataGrid);
            }
            
        }
        public void ResetAll()
        {
            UpdatedRows.Clear();
            validIDManager.ResetProductIDs();
            AddedRows.Clear();

            SelectedProductDataGrid = null;
            ResetUpdateInputs();

            ResetAddInputs();

            OriginalProductDataGrid = ProductData.GetProductTable();
            ProductDataGrid = new BindableCollection<ProductModel>(OriginalProductDataGrid);
            NotifyOfPropertyChange(() => ProductDataGrid);
        }

        public void UpdateRow()
        {
            if (SelectedProductDataGrid == null)
            {
                MessageBox.Show("Please make sure a row is selected to be updated before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }


            bool successfulName = ValidateName(UpdateName);
            bool succesfulDescript = ValidateDescription(UpdateDescription);
            int UpdateStockInt = ValidateStock(UpdateStock);
            decimal UpdateSalePriceDecimal = ValidatePrices(UpdateSalePrice, "Sale Price");
            decimal UpdateCostPriceDecimal = ValidatePrices(UpdateCostPrice, "Cost Price");

            if (successfulName == false ||
                succesfulDescript == false ||
                UpdateStockInt == -1 ||
                UpdateSalePriceDecimal == -1m ||
                UpdateCostPriceDecimal == -1m)
            {
                return;
            }

            if (UpdateName == SelectedProductDataGrid.Name &&
                UpdateDescription == SelectedProductDataGrid.Description &&
                UpdateStockInt == SelectedProductDataGrid.Stock &&
                UpdateSalePriceDecimal == SelectedProductDataGrid.SalePrice &&
                UpdateCostPriceDecimal == SelectedProductDataGrid.CostPrice)
            {
                MessageBox.Show("Please change the row before attempting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return;
            }

            ProductModel UpdatedProduct = new ProductModel
            {
                Product_ID = SelectedProductDataGrid.Product_ID,
                Name = UpdateName,
                Description = UpdateDescription,
                Stock = UpdateStockInt,
                SalePrice = UpdateSalePriceDecimal,
                CostPrice = UpdateCostPriceDecimal
            };

            //Changes the Datagrid visible to user
            int SelectedIndex = ProductDataGrid.ToList().FindIndex(p => p == SelectedProductDataGrid);

            ProductDataGrid[SelectedIndex] = UpdatedProduct;
            NotifyOfPropertyChange(() => ProductDataGrid);

            //Adds to updated rows
            UpdatedRows = AddToUpdatedRows(UpdatedRows, UpdatedProduct);

            //Resets
            SelectedProductDataGrid = null;
            NotifyOfPropertyChange(() => SelectedProductDataGrid);
            ResetUpdateInputs();
        }

        //Bound the Add button. Will validate the inputted values.
        //Will add the row to the visible DataGrid
        public void AddNew()
        {
            bool successfulName = ValidateName(AddName);
            bool succesfulDescript = ValidateDescription(AddDescription);
            int AddStockInt = ValidateStock(AddStock);
            decimal AddSalePriceDecimal = ValidatePrices(AddSalePrice, "Sale Price");
            decimal AddCostPriceDecimal = ValidatePrices(AddCostPrice, "Cost Price");

            if (successfulName == false ||
                succesfulDescript == false ||
                AddStockInt == -1 ||
                AddSalePriceDecimal == -1m ||
                AddCostPriceDecimal == -1m)
            {
                return;
            }

            ProductModel AddedProduct = new ProductModel
            {
                Product_ID = validIDManager.GenerateValidProductID(),
                Name = AddName,
                Description = AddDescription,
                Stock = AddStockInt,
                SalePrice = AddSalePriceDecimal,
                CostPrice = AddCostPriceDecimal
            };

            //Adds to the visible DataGrid
            ProductDataGrid.Add(AddedProduct);
            NotifyOfPropertyChange(() => ProductDataGrid);

            //Adds to Added rows
            AddedRows.Add(AddedProduct);
            ResetAddInputs();
        }

        //Ensures the Updated row is not a duplicate in the UpdatedRows List.
        public List<ProductModel> AddToUpdatedRows(List<ProductModel> ListUpdatedProducts, ProductModel ToUpdateProd)
        {
            int IndexOfLastTimeUpdatedThisProduct = ListUpdatedProducts.FindLastIndex(p => p.Product_ID == ToUpdateProd.Product_ID);
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

        public bool ValidateName(string? Name)
        {
            if (Name == null || Name == "")
            {
                MessageBox.Show("Please make sure Name is not empty before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return false;
            }
            if (Name.Count() > 50 || Name.Count() <= 0)
            {
                MessageBox.Show("Length of Name is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public bool ValidateDescription(string? Description)
        {
            if (Description == null)
            {
                MessageBox.Show("Please make sure Description is not empty before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return false;
            }
            if (Description.Count() > 100 || Description.Count() <= 0)
            {
                MessageBox.Show("Length of Description is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public int ValidateStock(string? StockString)
        {
            if (StockString == null || StockString == "")
            {
                MessageBox.Show("Please make sure Stock is not empty before attemting to Update",
                                 "Update Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }
            if (!int.TryParse(StockString, out int Stock))
            {
                MessageBox.Show("Stock cannot be parsed",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            if (Stock > int.MaxValue || Stock < 0)
            {
                MessageBox.Show("Stock is out of bounds",
                                 "Validation Error",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                return -1;
            }

            return Stock;
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
