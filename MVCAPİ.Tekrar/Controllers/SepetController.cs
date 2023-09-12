using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces;
using MVCAPİ.Tekrar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.Controllers
{
   
    [Route("api/sepet")]
    [ApiController]
    public class SepetController : ControllerBase
    {

        ISepetDAL _sepetDal;
        public SepetController(ISepetDAL sepetDal)
        {
            _sepetDal = sepetDal;
        }

        [HttpGet("getproducts")]       
        public async Task<IActionResult> UrunleriGetir()
        {
          var donulecekDeger = await _sepetDal.UrunleriVer();

            return Ok(donulecekDeger);
        }
        [HttpGet("getsepetlist")]

        public async Task<IActionResult> SepetListesiniVer()
        {
            return Ok();
        }

        [HttpPost("sepeteekle")]     
        public async Task<IActionResult> SepeteEkle([Bind("UserID,Date,ProductID,Quantity")] AddToSepetDTO dto)
        {
            if (! ModelState.IsValid)
            {
                return BadRequest();
            }
          var sonuc =  await _sepetDal.SepetiKaydet(dto);
            if (sonuc)
            {
                return StatusCode(202);
            }
            else
            {
                return StatusCode(404);
            }
           


        }
    }
}
