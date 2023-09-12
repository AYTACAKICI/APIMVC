using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAPİ.Tekrar.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MVCAPİ.Tekrar.UI.ViewComponents
{
    public class SepetimViewComponent:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.GetString("benimSepetim") == null)
            {
                //HttpContext.Session.SetString("benimSepetim", "Sepette Ürün Bulunmamaktadır");
                return View(new SepetAdetiBilgisi() { SepetItem = 0 });
            }
            else
            {
                var deger = JsonConvert.DeserializeObject<List<SepetDTO>>(HttpContext.Session.GetString("benimSepetim"));
                return View(new SepetAdetiBilgisi() { SepetItem = deger.Count() });
            }
            //todo tek satırda yukarıdaki if i yaz.

        }
    }
}
