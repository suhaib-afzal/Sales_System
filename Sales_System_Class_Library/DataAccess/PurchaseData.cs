﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Library.DataModels;
using Dapper;

namespace Class_Library.DataAccess
{
    public static class PurchaseData
    {
        public static void UpdatePurchaseTable(List<PurchaseModel> changedPurchases)
        {
            throw new NotImplementedException();
        }

        public static void InsertNewPurchases(int Cart_ID, List<PurchaseModel> newPurchases)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                for (int i = 0; i < newPurchases.Count; i++)
                {
                    connection.Execute("PurchaseTable_InsertNewPurchase @Cart_ID, @Product_ID, @Quantity",
                        new { Cart_ID = Cart_ID,
                              Product_ID = newPurchases[i].Product.Product_ID,
                              Quantity = (int)newPurchases[i].Quantity });
                }
            }
        }

        public static List<DisplayPurchaseModel> GetPurchaseTable()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Sales_System_Database")))
            {
                return connection.Query<DisplayPurchaseModel>("EXEC dbo.PurchaseTable_GetPurchaseTable").ToList();
            }
        }

    }
}
