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
    public class ProductDatabaseViewModel : Screen, ITableViewModel
    {
        private ProductModel? _selectedProductDataGrid;

        public static List<ProductModel> OriginalProductDataGrid { get; set; } = ProductData.GetProductTable();

        public BindableCollection<ProductModel> ProductDataGrid { get; set; } = new BindableCollection<ProductModel>(OriginalProductDataGrid);

        public List<ProductModel> UpdatedRows { get; set; } = new List<ProductModel>();

        public List<ProductModel> AddedRows { get; set; } = new List<ProductModel>();

        public ProductModel? SelectedProductDataGrid {
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

        private string? _updateName;
        private string? _updateDescription;
        private string? _updateStock;
        private string? _updateSalePrice;
        private string? _updateCostPrice;

        public string? UpdateName 
        { 
            get { return _updateName; }
            set { _updateName = value; }
        } 

        public string? UpdateDescription 
        {
            get { return _updateDescription; }
            set { _updateDescription = value; }
        }

        public string? UpdateStock 
        { 
            get { return _updateStock; }
            set { _updateStock = value; } 
        }

        public string? UpdateSalePrice 
        {
            get { return _updateSalePrice; }
            set { _updateSalePrice = value; }
        }

        public string? UpdateCostPrice 
        {
            get { return _updateCostPrice; }
            set { _updateCostPrice = value; }
        }

        private string? _addName;
        private string? _addDescription;
        private string? _addStock;
        private string? _addSalePrice;
        private string? _addCostPrice;

        public string? AddName
        {
            get { return _addName; }
            set { _addName = value; }
        }

        public string? AddDescription
        {
            get { return _addDescription; }
            set { _addDescription = value; }
        }

        public string? AddStock
        {
            get { return _addStock; }
            set { _addStock = value; }
        }

        public string? AddSalePrice
        {
            get { return _addSalePrice; }
            set { _addSalePrice = value; }
        }

        public string? AddCostPrice
        {
            get { return _addCostPrice; }
            set { _addCostPrice = value; }
        }

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

        public void Save()
        {
            throw new NotImplementedException();
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

            int SelectedIndex = ProductDataGrid.ToList().FindIndex(p => p == SelectedProductDataGrid);

            ProductDataGrid[SelectedIndex] = UpdatedProduct;
            NotifyOfPropertyChange(() => ProductDataGrid);

            UpdatedRows.Add(UpdatedProduct);

            SelectedProductDataGrid = null;
            NotifyOfPropertyChange(() => SelectedProductDataGrid);
            ResetUpdateInputs();
        }

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
                Product_ID = 0,
                Name = AddName,
                Description = AddDescription,
                Stock = AddStockInt,
                SalePrice = AddSalePriceDecimal,
                CostPrice = AddCostPriceDecimal
            };

            ProductDataGrid.Add(AddedProduct);
            NotifyOfPropertyChange(() => ProductDataGrid);

            AddedRows.Add(AddedProduct);
            ResetAddInputs();
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
