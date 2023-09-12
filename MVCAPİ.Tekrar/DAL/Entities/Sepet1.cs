using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Entities
{
    public class Sepet1
    {    
        [Key]
        public int Sepet1ID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Sepet1Details> Sepet1Details { get; set; }
        public User User { get; set; }

    }
}
