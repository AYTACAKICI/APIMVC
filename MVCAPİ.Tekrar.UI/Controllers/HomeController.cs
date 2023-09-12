using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCAPİ.Tekrar.UI.ApiService;
using MVCAPİ.Tekrar.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KargoApiService _service;
        private readonly TokenApiService _token;
        public HomeController(ILogger<HomeController> logger,KargoApiService service,TokenApiService token)
        {
            _logger = logger;
            _service = service;
            _token = token;
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index01()
        {
           var deger = _service.KargolariGetir();
            return View(deger);
        }

        //api/AddShippers

        public IActionResult Index03Post()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index03KullaniciKayit()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index03KullaniciKayit(RegisterDTO dto)
        {
            //apiye baglan register methodunu calıstır
            var deger = await _token.KullaniciKaydet(dto);
            return RedirectToAction("Index03KullaniciKayit");
        }
        /// <summary>
        /// 2.adım
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        public IActionResult IndexLogin()
        {
           
           
            if (Request.Cookies["giris"] !=null)
            {
                string kaydedilmişCookşeBilgisi = Request.Cookies["token"];
            }
            return View(new LoginDTO());
        }
        [HttpPost]
        public async Task<IActionResult> IndexLogin(LoginDTO dto)
        {
            //tokenal methodu çalıstır
            string uretilenTokenDegeri = await _token.TokenAl(dto.UserName, dto.Password);
            //cookie de tut
            CookieOptions mycookie = new CookieOptions();
            mycookie.Domain = "giris";
            mycookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("token", uretilenTokenDegeri);
            HttpContext.Session.SetString("myuser", dto.UserName);
            return RedirectToAction("Index","Satis");
        }
        /// <summary>
        /// 3.adım
        /// </summary>
        public void IndexSecureKargoKaydet()
        {

        }
      
        [HttpPost]
        public async Task<IActionResult> Index03Post([Bind("CompanyName,Phone")]ShipperDTO dto)
        {
            if (ModelState.IsValid)
            {
                //apiye bu dto yu gönder
              string apidenGelenEnSonCevap = await  _service.ViewToAddShip(dto);
            }

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index03PostWithToken()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index03PostWithToken([Bind("CompanyName,Phone")] ShipperDTO dto)
        {
            if (ModelState.IsValid)
            {
                //if (Request.Cookies["token"]! = null)
                //{
                TempData["servermesaj"] = await _service.TokenShipAddAsync(dto, Request.Cookies["token"]);
                //}
                //apiye bu dto yu gönder

            }
            

            return RedirectToAction("Index03PostWithToken");
        }
    }
}
