using MVCAPİ.Tekrar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.Models
{
    public class AddToSepetDTO
    {
        public int SepetID { get; set; }       
        public string UserName { get; set; }    
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
