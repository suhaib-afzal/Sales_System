   M S B A M L   `   `   `  ????J  FSales_System_UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Sales_System_UI.ViewsSales_System_UI.Views  5      6    0Sales_System_UI.Views.Tables.ProductDatabaseView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5      /local#clr-namespace:Sales_System_UI.Views  5      
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
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ ProductDataGrid??5      $??1??5       $??1??6    $??7??5!      $	??400??5"      $	??900??6       
IsReadOnly$
 True??5#      5%      ?? 5(   
   $??Update:??6   $??2??5)      $??1??6!   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5+   
   -   5          Name$ 
UpdateName??5+   #   $??2??5,   #   $??2??60   $??0,0,0,0q?5-   #   $??90??64      	Watermark$
 Name??5.   #     50   
   -   5        $ UpdateDescription??50   #   $??2??51   #   $??3??60   $??0,0,0,0q?52   #   $	??300??64   $ Description??53   #     55   
   -   5        $ UpdateStock??55   #   $??2??56   #   $??4??60   $??0,0,0,0q?57   #   $??90??64   $ Stock??58   #     5:   
   -   5        $ UpdateSalePrice??5:   #   $??2??5;   #   $??5??60   $??0,0,0,0q?5<   #   $??90??64   $ 
Sale Price??5=   #     5?   
   -   5        $ UpdateCostPrice??5?   #   $??2??5@   #   $??6??60   $??0,0,0,0q?5A   #   $??90??64   $ 
Cost Price??5B   #   ?? 5D   
   -   5         ??Name$ 	UpdateRow??5D      $??2??5E      $??7??6   $??Update??5F      ?? 5I   
   $??Add New:??6   $??3??5J      $??1??6!     5L   
   -   5        $ AddName??5L   #   $??3??5M   #   $??2??60   $??0,0,0,0q?5N   #   $??90??64   $
 Name??5O   #     5Q   
   -	   5        $ AddDescription??5Q   #   $??3??5R   #   $??3??60   $??0,0,0,0q?5S   #   $	??300??64   $ Description??5T   #     5V   
   -
   5        $ AddStock??5V   #   $??3??5W   #   $??4??60   $??0,0,0,0q?5X   #   $??90??64   $ Stock??5Y   #     5[   
   -   5        $ AddSalePrice??5[   #   $??3??5\   #   $??5??60   $??0,0,0,0q?5]   #   $??90??64   $ 
Sale Price??5^   #     5`   
   -   5        $ AddCostPrice??5`   #   $??3??5a   #   $??6??60   $??0,0,0,0q?5b   #   $??90??64   $ 
Cost Price??5c   #   ?? 5e   
   -   5        $ AddNew??5e      $??3??5f      $??7??6   $	??Add??5g      5j      5l      