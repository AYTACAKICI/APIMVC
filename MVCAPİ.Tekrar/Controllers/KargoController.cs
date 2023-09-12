using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAPİ.Tekrar.DAL.Context;
using MVCAPİ.Tekrar.DAL.Entities;
using MVCAPİ.Tekrar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KargoController : ControllerBase
    {
        MyContext _myContext;
        IMapper _mapper;
        public KargoController(MyContext context, IMapper mapper)
        {
            _myContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var shippers = _myContext.Shippers.ToList();
            //GeneralApiType<List<ShippersDTO>> donulecekDeger = new GeneralApiType<List<ShippersDTO>>();
            //donulecekDeger.ApiStatusCode = 200;
            //donulecekDeger.ReturnObject = shippers.Select(x => new ShippersDTO() { CompanyName = x.CompanyName, Phone = x.Phone, ShipperID = x.ShipperID, AktifMi = x.AktifMi }).ToList();

            //List< ShippersDTO> _shipper = _mapper.Map<List<ShippersDTO>>(shippers);

            //return Ok(donulecekDeger);
            return Ok(shippers.Select(a => new ShippersDTO()
            {
                CompanyName = a.CompanyName,
                ShipperID = a.ShipperID,
                Phone = a.Phone
            }).ToList());

        }

        [HttpPost]
        [Route("~/api/AddShippers")]

        public IActionResult Post([FromBody]ShippersDTO dto)
        {
            var shippers = _mapper.Map<Shippers>(dto);
            _myContext.Shippers.Add(shippers);
            _myContext.SaveChanges();
            return Ok();
        }
    }
}
