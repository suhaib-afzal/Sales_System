using Caliburn.Micro;
using Class_Library;
using Class_Library.DataModels;
using Class_Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Class_Library.DiscountModels;

namespace Sales_System_UI.ViewModels
{
    public class SalesViewModel : Screen
    {
        public List<CustomerModel> CustomerListBox { get; set; } = CustomerData.GetCustomerTable();//= MockDataAccess.GetCustomerModels();

        public List<ProductModel> ProductDataGrid { get; set; } = ProductData.GetProductsInStock();//= MockDataAccess.GetProductModels();

        public CustomerModel? SelectedCustomerListBox { get; set; }

        public ProductModel? SelectedProductDataGrid { get; set; }

        public string? Quantity { get; set; }

        public BindableCollection<PurchaseModel> PurchasesListBox { get; set; } = new BindableCollection<PurchaseModel>();

        public PurchaseModel? SelectedPurchasesListBox { get; set; }


        public event EventHandler? SalesUpdatedEvent; 


        // A read-only property, that will independently
        // calculate and return as a formatted string the current
        // subtotal in the Cart. That is, the total without discounts.
        // Needs to be Notified of Purchase ListBox change.
        public string? CurrentSubtotal
        {
            get 
            {   
                CartModel currentCart = new CartModel();
                currentCart.Purchases = PurchasesListBox.ToList();
                decimal subTotal = currentCart.CalculateSubTotal();
                return String.Format("{0:C}", subTotal); 
            }
        }


        public BindableCollection<string> AppliedDiscountsListBox { get; set; } = new BindableCollection<string>();


        //This property allows us to initialise the Cart with the
        //currently active Discounts we have. Discounts need to
        //implement IDiscountModel.
        public static CartModel InitialFinalCart { get; }
            = new CartModel(new List<IDiscountModel> { //Input Active Discounts Here.
                                                       //new Mock2for1onHairbrushes() 
                                                     });

        public CartModel FinalCart { get; private set; } = InitialFinalCart;

        public string? FinalTotal { get; private set; }

        public decimal FinalTotalNum { get; private set; }

        //Implemented a Boolean HasFinalisedCart, this will be toggled
        //when the user has clicked the FinaliseCart button.
        //Making sure the user clicks this button before paying
        //allows for us to make any required checks to the cart
        //and calculate and display any applicable discounts.
        public bool HasFinalisedCart { get; private set; } = false;

        public string? CustomerSearchBox { get; set; }

        public string? ProductSearchBox { get; set; }


        public void CustomerSearchButton()
        {
            //Resets the CustomerListBox before its searched
            CustomerListBox = CustomerData.GetCustomerTable();//MockDataAccess.GetCustomerModels();
            NotifyOfPropertyChange(() => CustomerListBox);

            //If CustomerSearchBox is empty or white space, the search button
            //will simply reset the CustomerListBox. Allowing users to return
            //to the default List.
            if (CustomerSearchBox == "" || CustomerSearchBox == null)
            {
                return;
            }

            //Ensures the search does not contain any special charecters. Returns null when it does.
            string? ParsedSearch = Parsing.searchBoxParsing(CustomerSearchBox, "Customer Search Query");

            //Only if search does not contains special charecters, it carries out the search.
            if (ParsedSearch != null)
            {
                bool isUint = uint.TryParse(ParsedSearch, out uint uintResult);

                //If the search is a number, will search IDs. Otherwise will search First and Last Name.
                //TODO: Search could be improved by a similarity function instead of finding equality.
                if (isUint)
                {
                    CustomerListBox = CustomerListBox.FindAll(p => p.Customer_ID == uintResult);
                    NotifyOfPropertyChange(() => CustomerListBox);
                }
                else
                {
                    CustomerListBox = 
                    CustomerListBox.FindAll(
                        p => p.FirstName == ParsedSearch || p.LastName == ParsedSearch
                        );
                    NotifyOfPropertyChange(() => CustomerListBox);
                }
            }
        }

        //Similar to CustomerSearchButton
        //TODO: make less repitive
        public void ProductSearchButton()
        {
            ProductDataGrid = ProductData.GetProductsInStock(); //MockDataAccess.GetProductModels();
            NotifyOfPropertyChange(() => ProductDataGrid);

            if (ProductSearchBox == "" || ProductSearchBox == null)
            {
                return;
            }

            string? ParsedSearch = Parsing.searchBoxParsing(ProductSearchBox, "Product Search Query");

            if (ParsedSearch != null)
            {
                bool isUint = uint.TryParse(ParsedSearch, out uint uintResult);

                if (isUint)
                {
                    ProductDataGrid = ProductDataGrid.FindAll(p => p.Product_ID == uintResult);
                    NotifyOfPropertyChange(() => ProductDataGrid);
                }
                else
                {
                    ProductDataGrid = ProductDataGrid.FindAll(p => p.Name == ParsedSearch);
                    NotifyOfPropertyChange(() => ProductDataGrid);
                }
            }
        }

        public void AddProduct()
        {
            if (SelectedProductDataGrid == null)
            {
                MessageBox.Show("Please select a product",
                                "Parsing Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Parsing.QuantityParsing will return null when the Quantity string has not
            // been successfully parsed. In that case there will be an error MessageBox.
            // Includes null check.
            uint? intQuant = Parsing.uintParsing(Quantity, "Quantity");
            if (intQuant != null)
            {
                PurchaseModel currentPurchase = new PurchaseModel();
                currentPurchase.Product = SelectedProductDataGrid;
                currentPurchase.Quantity = (uint)intQuant;

                //This will Add the new purchase model to the BindableCollection but will combine it with
                //existing Purchases of the same Product if applicable.
                //Assumes Products are unique by Product_ID.
                //TODO: Add greater checks in Models to ensure this is true.
                //TODO: Do this in Backend instead.
                List<PurchaseModel> tempPurchases = PurchasesListBox.ToList();
                tempPurchases.Add(currentPurchase);
                List<List<PurchaseModel>> tempListOfList = tempPurchases.GroupBy(p => p.Product.Product_ID,
                                                                                 p => p,
                                                                                 (ID, Products) => Products.ToList()
                                                                                 ).ToList();

                PurchasesListBox = new BindableCollection<PurchaseModel>(tempListOfList.ConvertAll(p => PurchaseModel.Merge(p)));
                NotifyOfPropertyChange(() => PurchasesListBox);
                NotifyOfPropertyChange(() => CurrentSubtotal);
            }
        }

        public void RemoveFromPurchases()
        {
            //Does nothing if Nothing selected in the Purchase List
            if (SelectedPurchasesListBox != null)
            {
                PurchasesListBox.Remove(SelectedPurchasesListBox);
                NotifyOfPropertyChange(() => CurrentSubtotal);
            }
        }


        public void FinaliseCart()
        {
            //If Cart has not been finalised yet 'HasFinalisedCart == false'
            //performs checks required, including checking if all products ordered
            //are sufficiently in stock, and finding all applicable discounts
            //and applying them
            if (HasFinalisedCart == false)
            {
                //Stock check
                if (PurchasesListBox.Any(p => p.Product.Stock < p.Quantity))
                {
                    MessageBox.Show("Please ensure ordered Quantity of any ordered " +
                                    "Product is less than or equal to the number in stock",
                                    "Cart Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }
                
                //Sets DateTime and Purchases but not Customer.
                //Customer ID only needed for Payment so set when paying.
                FinalCart.Purchases = PurchasesListBox.ToList();
                FinalCart.TimeofPurchase = DateTime.Now;

                //Finds and displays Discounts.
                List<IDiscountModel> ApplicableDiscounts = FinalCart.FindApplicableDiscounts();

                AppliedDiscountsListBox = new BindableCollection<string>(ApplicableDiscounts.ConvertAll(p => p.DiscountName));

                NotifyOfPropertyChange(() => AppliedDiscountsListBox);

                //Calculates and displays Total including Discounts.
                //Calculates and stores Profit.
                decimal finalTotal = FinalCart.CalculateTotal(ApplicableDiscounts);
                FinalTotalNum = finalTotal;

                FinalCart.ProfitMade = FinalCart.CalculateProfit(finalTotal);

                FinalTotal = String.Format("{0:C}", finalTotal);
                NotifyOfPropertyChange(() => FinalTotal);
                HasFinalisedCart = true;
            }
            else
            {
                //Allows for user to un-finalise their cart if they need to make
                //changes.
                FinalCart = InitialFinalCart;
                FinalTotal = String.Format("{0:C}", 0m);
                NotifyOfPropertyChange(() => FinalTotal);
                AppliedDiscountsListBox.Clear();
                NotifyOfPropertyChange(() => AppliedDiscountsListBox);
                HasFinalisedCart = false;
            }
        }

        public void Pay()
        {
            //Ensures customer is selected
            if (SelectedCustomerListBox == null)
            {
                MessageBox.Show("Please select a customer",
                                "Cart Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            else
            {
                FinalCart.Customer = SelectedCustomerListBox;
            }

            //Ensures cart is finalised
            if (!HasFinalisedCart)
            {
                MessageBox.Show("Please finalise cart",
                                "Cart Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            //TODO Make this a password box.
            var MessageBoxResult = MessageBox.Show("Are You Sure",
                    "Payment",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (MessageBoxResult == MessageBoxResult.Yes)
            {
                int IDofInserted = CartData.InsertNewCart(FinalCart);
                CustomerData.UpdateCustomerSpending(SelectedCustomerListBox, SelectedCustomerListBox.TotalPurchases + FinalTotalNum);
                PurchaseData.InsertNewPurchases(IDofInserted, PurchasesListBox.ToList());

                foreach (PurchaseModel purchase in PurchasesListBox)
                {
                    purchase.Product.Stock -= (int)purchase.Quantity;
                }

                List<ProductModel> ProductsWithUpdatedStock = PurchasesListBox.ToList().ConvertAll(p => p.Product);
                ProductData.UpdateProductsStock(ProductsWithUpdatedStock);

                //Raises the event in order to communicate to the database
                //view model
                SalesUpdatedEvent?.Invoke(this, EventArgs.Empty);

                ClearEverything();
            }
        }

        public void ClearEverything()
        {
            //this.Refresh();
            CustomerListBox = CustomerData.GetCustomerTable(); //MockDataAccess.GetCustomerModels();
            NotifyOfPropertyChange(() => CustomerListBox);

            ProductDataGrid = ProductData.GetProductsInStock(); //MockDataAccess.GetProductModels();
            NotifyOfPropertyChange(() => ProductDataGrid);

            CustomerSearchBox = null;
            NotifyOfPropertyChange(() => CustomerSearchBox);

            ProductSearchBox = null;
            NotifyOfPropertyChange(() => ProductSearchBox);

            SelectedCustomerListBox = null;
            NotifyOfPropertyChange(() => SelectedCustomerListBox);

            SelectedProductDataGrid = null;
            NotifyOfPropertyChange(() => SelectedProductDataGrid);

            PurchasesListBox = new BindableCollection<PurchaseModel>();
            NotifyOfPropertyChange(() => PurchasesListBox);
            NotifyOfPropertyChange(() => CurrentSubtotal);

            AppliedDiscountsListBox = new BindableCollection<string>();
            NotifyOfPropertyChange(() => AppliedDiscountsListBox);

            Quantity = null;
            NotifyOfPropertyChange(() => Quantity);

            FinalTotal = null;
            NotifyOfPropertyChange(() => FinalTotal);

            FinalCart = new CartModel();
            NotifyOfPropertyChange(() => FinalCart);

            HasFinalisedCart = false;
            FinalTotalNum = 0m;

        }
    }
}
