using AutoMapper;
using MVCAPİ.Tekrar.DAL.Entities;
using MVCAPİ.Tekrar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.Mapper
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Shippers, ShippersDTO>();
            CreateMap<ShippersDTO, Shippers>();
            //CreateMap<List<Shippers>, List<ShippersDTO>>();
            //CreateMap<List<ShippersDTO>, List<Shippers>>();
            //CreateMap<Products, ProductDTO>();
            //CreateMap<ProductDTO, Products>();
            //CreateMap<List<Products>, List<ProductDTO>>();
            //CreateMap<List<ProductDTO>, List<Products>>();

        }
    }
}
