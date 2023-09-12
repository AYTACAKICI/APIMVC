using MVCAPİ.Tekrar.DAL.Entities;
using MVCAPİ.Tekrar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces
{
   public interface ISepetDAL
    {
       Task<List<ProductDTO>> UrunleriVer();
       Task<bool> SepetiKaydet(AddToSepetDTO AddToSepet);
    }
}
