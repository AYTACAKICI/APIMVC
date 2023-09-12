
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MVCAPİ.Tekrar.UI.ApiService;
using MVCAPİ.Tekrar.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace MVCAPİ.Tekrar.UI.Controllers
{
    public class SatisController : Controller
    {
        UrunApiService _urunservice;
        public SatisController(UrunApiService urunservice)
        {
            _urunservice = urunservice;
        }
        List<ProductDTO> degerlerim = null;

        [HttpGet]
        public async Task<ViewResult> GridView()
        {
            degerlerim = await _urunservice.UrunleriListele();

            return View(degerlerim);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //ürünleri listele
            TempData["degerler123"] = await _urunservice.UrunleriListele();

            

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Detay(int id)
        {
            degerlerim = await _urunservice.UrunleriListele();
           
            SepetDTO urun = new SepetDTO()
            {
                UrunDetay = degerlerim.FirstOrDefault(x=> x.ProductID == id),
                Adet = 1
            };
            if (urun.UrunDetay == null)
            {
                return View("Hata");
            }
            return View(urun);
            //ProductDTO urun = degerlerim.FirstOrDefault(x => x.ProductID == id);

            //return View(new SepetDTO() { UrunDetay = degerlerim.FirstOrDefault(a => a.ProductID == ProductID), Adet = 0 });
           
        }

        [HttpGet]
        public async Task<IActionResult> Sepetim()
        {
            //ürünleri listele
            if ((HttpContext.Session.GetString("benimSepetim") == null))
            {
                return View("SepetBos");
            }
            else
            {


                TempData["degerler12"] = JsonConvert.DeserializeObject<List<SepetDTO>>(HttpContext.Session.GetString("benimSepetim"));

                return View();
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Sepetim(List<SepetDTO> dto)
        {
            var userName = HttpContext.Session.GetString("myuser");

            //AddToSepetDTO addToSepet = new AddToSepetDTO() { ProductID = dto.UrunDetay.ProductID, Quantity = dto.Adet, Date = DateTime.Now, UserName = userName };
            List<SepetDTO> sepet = new List<SepetDTO>();
            sepet.AddRange(dto);

            if (HttpContext.Session.GetString("myuser") == null)
            {
              return  RedirectToAction("IndexLogin", "Home");
               
            }
            else
            {
                //  await _urunservice.SepetiKaydet(sepet);
                return RedirectToAction("Sepetim");
            }


            
        }


        [HttpPost]
        public IActionResult SepeteAt(SepetDTO dto)
        {
            //Sepete ap 
            if (HttpContext.Session.GetString("benimSepetim") == null)
            {
                //sepet boş
                List<SepetDTO> sepetlistesi = new List<SepetDTO>();
                sepetlistesi.Add(dto);
                HttpContext.Session.SetString("benimSepetim", JsonConvert.SerializeObject(sepetlistesi));

            }
            else
            {
                //sepette veri var
                var sepettekiUrunler = JsonConvert.DeserializeObject<List<SepetDTO>>(HttpContext.Session.GetString("benimSepetim"));
                sepettekiUrunler.Add(dto);
                HttpContext.Session.SetString("benimSepetim", JsonConvert.SerializeObject(sepettekiUrunler));
            }

            return RedirectToAction("Index");
        }



    }
}
