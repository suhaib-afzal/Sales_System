   M S B A M L   `   `   `  ????J  FSales_System_UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Sales_System_UI.ViewsSales_System_UI.Views  5      7    1Sales_System_UI.Views.Tables.CustomerDatabaseView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5      /local#clr-namespace:Sales_System_UI.Views  5      
???????.??5      ?   ? RowDefinitions  5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6    ? ColumnDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ CustomerDataGrid??5      $??1??5      $??1??6    $??7??5       $	??400??5!      $	??900??6       
IsReadOnly$
 True??5"      5$      ?? 5'   
   $??Update:??6   $??2??5(      $??1??6!   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5*   
   -   5          Name$ UpdateFirstName??5*   #   $??2??5+   #   $??2??60   $??0,0,0,0q?5,   #   $	??150??64      	Watermark$ 
First Name??5-   #     5/   
   -   5        $ UpdateLastName??5/   #   $??2??50   #   $??3??60   $??0,0,0,0q?51   #   $	??150??64   $ 	Last Name??52   #     55   
   -   5        $ UpdateTotalPurchases??55   #   $??2??56   #   $??4??60   $??0,0,0,0q?57   #   $	??150??64   $ Total Spent??58   #   ?? 5:   
   -   5         ??Name$ 	UpdateRow??5:      $??2??5;      $??5??6   $??Update??5<      ?? 5?   
   $??Add New:??6   $??3??5@      $??1??6!     5B   
   -   5        $ AddFirstName??5B   #   $??3??5C   #   $??2??60   $??0,0,0,0q?5D   #   $	??150??64   $ 
First Name??5E   #     5I   
   -   5        $ AddLastName??5I   #   $??3??5J   #   $??3??60   $??0,0,0,0q?5K   #   $	??150??64   $ 	Last Name??5L   #     5O   
   -   5        $ AddTotalPurchases??5O   #   $??3??5P   #   $??4??60   $??0,0,0,0q?5Q   #   $	??150??64   $ Total Spent??5R   #   ?? 5T   
   -	   5        $ AddNew??5T      $??3??5U      $??5??6   $	??Add??5V      5Y      5Z      