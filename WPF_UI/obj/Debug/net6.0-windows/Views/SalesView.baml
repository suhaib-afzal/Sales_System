   M S B A M L   `   `   `  ????J  FSales_System_UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Sales_System_UI.ViewsSales_System_UI.Views  5   	   %    Sales_System_UI.Views.SalesView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Sales_System_UI.Views     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     ? Title$  	SalesView??5	   	   $	??430??6   $	??950??6(   $??Segoe UI Semilight)?5
   	   .??5      (?  (? Stretch$ Uniform=?6   $??Stretch=?6    $??Stretch=?6>   .??5   
   ?  ? ColumnDefinitions 5      ?? 5      $??20 ?6#   ?? 5      $
??auto ?6#   ?? 5      $
??auto ?6#   ?? 5      $??50??6#   ?? 5      $
??auto ?6#   ?? 5      $
??auto ?6#   ?? 5      $??20 ?6#   ?? 5      $
??auto ?6#   ?? 5      $
??auto ?6#   ?? 5      $??50??6#   ?? 5      $
??auto ?6#   ?? 5      $
??auto ?6#   ?? 5      $??20 ?6#    ? RowDefinitions 5      ?? 5      $??20 ?6    ?? 5       $
??auto ?6    ?? 5!      $
??auto ?6    $??20??6.   ?? 5"      $
??auto ?6    $??50??6.   ?? 5#      $
??auto ?6    $??25??6.   ?? 5$      $
??auto ?6    $??20??6.   ?? 5%      $
??auto ?6    ?? 5&      $
??auto ?6    $??25??6.   ?? 5'      $
??auto ?6    $??25??6.   ?? 5(      $
??auto ?6    ?? 5)      $
??auto ?6    ?? 5*      $
??auto ?6    $??20??6.   ?? 5+      $??* ?6    ?? 5,      $??20 ?6    .+?50      ?? $??1??6   $??1??6(   $??	Customers??51      ?? 54      -   5         ??Name$ CustomerSearchBox??54      $??1??55      $??2??6*   $??20??56      $??90??6)   $??Stretch=?57      ?? 5;      -   5         ??Name$ CustomerListBox??5;      $??1??5<      $??3??6*   $??2??5=      $??8??5>      $	??300??5?      $	??150??6'   $??Visible=?5@      ??5A      ?? 5B      .R?5C      ??  ?? Orientation$ 
Horizontal=?6%   .+?5D      ?? $
??ID: ??6(   ?? 5E      ??6(   ?? *Customer_ID+?? 5F      $?? ??6(   ?? 5G      ??6(   ?? *	FirstName+?? 5H      $?? ??6(   ?? 5I      ??6(   ?? *
LastName+5J      5K      5M      ?? 5T      -   5         ??Name$ CustomerSearchButton??5T      $??2??5U      $??2??6)   $??20??5V      $??55??6(   $??Stretch=?5W      $??Search??5X      ?? 5^      $??4??6   $??1??6(   $??Products??5_      ?? 5a      -   5        $ ProductSearchBox??5a      $??4??5b      $??2??6*   $??20??5c      $	??120??6)   $??Stretch=?5d      &   System.Windows.Controls.DataGrid  5f      -   5          Name$ ProductDataGrid??5f      $??4??5g      $??3??6*   $??2??5h      $??8??5i      $	??300??5j      $	??200??6'   $??Visible=?5k      	   
IsReadOnly$
	 True??5l      
   AutoGenerateColumns$
 False??5m      
???????   Columns 5o      0  *System.Windows.Controls.DataGridTextColumn  5p      ,  &System.Windows.Controls.DataGridColumn   Header$ ID??6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   ?? *
Product_ID+  5r      $ Product??6)    6:   ?? *Name+  5t      $ Price??6)    68   ?? *	SalePrice+ ?? StringFormat$ {0:C}??  5v      $ Stock??6)    68   ?? *Stock+5y      ?? 5{      -   5        $ 
AddProduct??5{      $??4??5|      $??11??6)   $??Stretch=?5}      $	??Add??5~      ?? 5?      -   5        $ ProductSearchButton??5?      $??5??5?      $??2??6)   $??20??5?      $??80??6(   $??Stretch=?5?      $??Search??5?      (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5?      -   5          Name$ Quantity??5?   '   $??5??5?      $??11??6*   $??80??5?      $??Stretch=?6%      	Watermark$ Quantity??5?      ?? 5?      $??7??6   $??1??6(   $
??Cart??5?      ?? 5?      -	   5        $ PurchasesListBox??5?      $??7??5?      $??2??6*   $??1??5?      $??9??5?      $	??300??5?      $	??150??6'   $??Visible=?5?      ??5?      ?? 5?      .R?5?      ?? $ 
Horizontal=?6%   .+?5?      ?? $
??ID: ??6(   ?? 5?      ??6(   ?? *Product.Product_ID+?? 5?      $?? ??6(   ?? 5?      ??6(   ?? *Product.Name+?? 5?      $	??  x??6(   ?? 5?      ??6(   ?? *
Quantity+5?      5?      5?      ?? 5?      -
   5        $ RemoveFromPurchases??5?      $??7??5?      $??11??6)   $??Remove??5?      ?? 5?      $??10??6   $??2??6)   $??70??5?      $??20??6+   $??	SubTotal:??5?      ?? 5?      -   5        $ FinaliseCart??5?      $??10??5?      $??3??6+   $??2??5?      $??30??5?      $??Finalise Cart??5?      ?? 5?      $??10??6   $??5??6)   $??2??5?      $??Discounts Applied??5?      ?? 5?      -   5        $ AppliedDiscountsListBox??5?      $??10??5?      $??6??6+   $??65??5?      $??2??5?      $??Visible=?5?      5?      ?? 5?      $??10??6   $??8??6)   $??70??5?      $??Total:??5?      $??16(?6*   ?? 5?      -   5        $	 Pay??5?      $??10??5?      $??10??6+   $??2??5?      $??20??5?      $??Pay and Finish??5?      $??16(?5?      ?? 5?      -   5        $ CurrentSubtotal??5?      $??2??5?      $??11??6'   $	??100??5?      $
??Left=?6*   $??0,0,0,49q?5?      $??2??6,   $	??100??6=    ?? 
IsReadOnly$
 True??5?      ?? 5?      -   5        $ 
FinalTotal??5?      $??8??5?      $??11??6'   $	??100??5?      $
 True??5?      ?? 5?      -   5         ??Name$ OpenDatabaseView??5?      $??11??5?      $??11??6+   $??Open Database??5?      $??	Underline{?61   $??Center=?5?      5?      5?      5?      