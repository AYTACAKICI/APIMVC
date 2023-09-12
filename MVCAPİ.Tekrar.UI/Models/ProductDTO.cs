﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.UI.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        //[Required(ErrorMessage = "Ürün Adı giriniz")]
        public string ProductName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string  CompanyName { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
