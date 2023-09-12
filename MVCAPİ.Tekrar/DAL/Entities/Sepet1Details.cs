using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Entities
{
    public class Sepet1Details
    {
        [Key]
        public int Sepet1ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }


        public Sepet1 Sepet1 { get; set; }
        public Products Products { get; set; }
    }
}
