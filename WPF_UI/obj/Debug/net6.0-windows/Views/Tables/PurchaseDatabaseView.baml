   M S B A M L   `   `   `  ????J  FSales_System_UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Sales_System_UI.ViewsSales_System_UI.Views  5      7    1Sales_System_UI.Views.Tables.PurchaseDatabaseView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5      /local#clr-namespace:Sales_System_UI.Views  5      
???????.??5      ?   ? RowDefinitions  5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6    ? ColumnDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ PurchaseDataGrid??5      $??1??5      $??1??6    $??7??5      $	??400??5       $	??900??6       
IsReadOnly$
 True??5!      5#      ?? 5&   
   $??Update:??6   $??2??5'      $??1??6!   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5)   
   -   5          Name$ UpdateCartID??5)   #   $??2??5*   #   $??2??60   $??0,0,0,0q?5+   #   $	??150??64      	Watermark$ Cart ID??5,   #     5.   
   -   5        $ UpdateProductID??5.   #   $??2??5/   #   $??3??60   $??0,0,0,0q?50   #   $	??150??64   $ 
Product ID??51   #     54   
   -   5        $ UpdateQuantity??54   #   $??2??55   #   $??4??60   $??0,0,0,0q?56   #   $	??150??64   $ Quantity??57   #   ?? 59   
   -   5         ??Name$ 	UpdateRow??59      $??2??5:      $??5??6   $??Update??5;      ?? 5>   
   $??Add New:??6   $??3??5?      $??1??6!     5A   
   -   5        $ 	AddCartID??5A   #   $??3??5B   #   $??2??60   $??0,0,0,0q?5C   #   $	??150??64   $ Cart ID??5D   #     5H   
   -   5        $ AddProductID??5H   #   $??3??5I   #   $??3??60   $??0,0,0,0q?5J   #   $	??150??64   $ 
Product ID??5K   #     5N   
   -   5        $ AddQuantity??5N   #   $??3??5O   #   $??4??60   $??0,0,0,0q?5P   #   $	??150??64   $ Quantity??5Q   #   ?? 5S   
   -	   5        $ AddNew??5S      $??3??5T      $??5??6   $	??Add??5U      5X      5Y      