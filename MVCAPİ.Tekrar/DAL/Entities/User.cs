using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }     
        public byte[] PasswordSalt { get; set; }
        public byte[] PaswordHash { get; set; }

        public ICollection<Sepet1> Sepet { get; set; }
    }
}
