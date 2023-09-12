using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Entities
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }       
        public string ProductName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }


        public  Categories Categories { get; set; }    
        public  Suppliers Suppliers { get; set; }
        public ICollection<Sepet1Details> Sepet1Details { get; set; }
    }
}
