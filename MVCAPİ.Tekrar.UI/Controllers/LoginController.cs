using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAPİ.Tekrar.UI.ApiService;
using MVCAPİ.Tekrar.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.UI.Controllers
{
    public class LoginController : Controller
    {
        TokenApiService _service ;
        public LoginController(TokenApiService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("mytoken");

            if (token==null)
            {
                return RedirectToAction("token zaten var");
                
            }
            return View(new LoginDTO());
        }
        [HttpPost]
        public async Task<IActionResult> ILogin(LoginDTO dto)
        {
            //tokenal methodu çalıstır
            var token = await _service.TokenAl(dto.UserName, dto.Password);
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                HttpContext.Session.SetString("myuser", dto.UserName);
                HttpContext.Session.SetString("mytoken", token);
                return RedirectToAction("Index", "Satis");
               
            }            
            //Session["Deger"] = uretilenTokenDegeri;

        }
    }
}
