   M S B A M L   `   `   `  ????J  FSales_System_UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Sales_System_UI.ViewsSales_System_UI.Views  5      3    -Sales_System_UI.Views.Tables.CartDatabaseView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5      /local#clr-namespace:Sales_System_UI.Views  5      
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
   &   System.Windows.Controls.DataGrid  -   5          Name$ CartDataGrid??5      $??1??5      $??1??6    $??7??5       $	??400??5!      $	??900??6       
IsReadOnly$
 True??5"         AutoGenerateColumns$ False??5#         Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn   Header$ Cart_ID??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   ??  ?? Path$ Cart_ID?  5&      $ Customer_ID??6%    6:   ?? $ Customer_ID?  5'      $ Profit Made??6%    6:   ?? $ 
ProfitMade?  5(      $ Time of Purchase??6%    6?   ?? $ TimeofPurchase?	 ?? StringFormat$	 {0:yyyy/mm/dd hh/mm/ss}??5*      ?? 5-   
   $??Update:??6   $??2??5.      $??1??6!   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  50   
   -   5        
  Name$
 UpdateCustomerID??50   #   $??2??51   #   $??2??60   $??0,0,0,0q?52   #   $	??150??64      	Watermark$ Customer ID??53   #     55   
   -   5        $
 UpdateProfitMade??55   #   $??2??56   #   $??3??60   $??0,0,0,0q?57   #   $	??150??64   $ Profit Made??58   #     5;   
   -   5        $
 UpdateTimeOfPurchase??5;   #   $??2??5<   #   $??4??60   $??0,0,0,0q?5=   #   $	??150??64   $ Time of Purchase??5>   #   ?? 5@   
   -   5         ??Name$ 	UpdateRow??5@      $??2??5A      $??5??6   $??Update??5B      ?? 5E   
   $??Add New:??6   $??3??5F      $??1??6!     5H   
   -   5        $
 AddCustomerID??5H   #   $??3??5I   #   $??2??60   $??0,0,0,0q?5J   #   $	??150??64   $ Customer ID??5K   #     5O   
   -   5        $
 AddProfitMade??5O   #   $??3??5P   #   $??3??60   $??0,0,0,0q?5Q   #   $	??150??64   $ Profit Made??5R   #     5U   
   -   5        $
 AddTimeOfPurchase??5U   #   $??3??5V   #   $??4??60   $??0,0,0,0q?5W   #   $	??150??64   $ Time of Purchase??5X   #   ?? 5Z   
   -	   5        $ AddNew??5Z      $??3??5[      $??5??6   $	??Add??5\      5_      5`      