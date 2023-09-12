using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces;
using MVCAPİ.Tekrar.DAL.Context;
using MVCAPİ.Tekrar.DAL.Entities;
using MVCAPİ.Tekrar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.AuthDAL.Concrate
{
    public class SepetDAL : ISepetDAL
    {
        SepetContext _context;
        IMapper _mapper;
        public SepetDAL(SepetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> SepetiKaydet(AddToSepetDTO AddToSepet)
        {
            
            var kisi = await _context.User.FirstOrDefaultAsync(x => x.UserName == AddToSepet.UserName);
            int userID = kisi.UserID;

            int a = 0;
            try
            {
                var eklenmisSepet = await _context.Sepet1.AddAsync(new Sepet1() {UserID=userID,Date=DateTime.Now });
                await _context.SaveChangesAsync();

                int EklenmisSepetID = eklenmisSepet.Entity.Sepet1ID;

               var eklenmisSepet1Details = await _context.Sepet1Details.AddAsync(new Sepet1Details() {ProductID =AddToSepet.ProductID,Quantity=AddToSepet.Quantity,Sepet1ID=EklenmisSepetID });

              a =  await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }

            if (a == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<ProductDTO>> UrunleriVer()
        {
         var degerler =    await (from product in _context.Products
                          join cat in _context.Categories on product.CategoryID equals cat.CategoryID
                          join sup in _context.Suppliers on product.SupplierID equals sup.SupplierID
                          select  new ProductDTO { CategoryID = product.CategoryID, ProductID = product.ProductID, Discontinued = product.Discontinued, QuantityPerUnit = product.QuantityPerUnit, SupplierID = product.SupplierID, UnitPrice = product.UnitPrice, UnitsInStock = product.UnitsInStock, UnitsOnOrder = product.UnitsOnOrder, ReorderLevel = product.ReorderLevel, CategoryName = cat.CategoryName, ProductName = product.ProductName, CompanyName = sup.CompanyName }).ToListAsync();

            return degerler;

        }
    }
}
