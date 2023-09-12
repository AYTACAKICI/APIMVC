using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces;
using MVCAPİ.Tekrar.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.Controllers
{
   
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //DI
        IAuthDAL _authDAL;
        IConfiguration _conf;
        public AuthController(IAuthDAL authDAL,IConfiguration conf)
        {
            _authDAL = authDAL;
            _conf = conf;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO dto)
        {
            if (!await _authDAL.UserExists(dto.UserName))
            {
                ModelState.AddModelError("not valid", "zaten varsın");
            }
            if (! ModelState.IsValid)
            {
                return BadRequest();
            }
            _authDAL.Register(new DAL.Entities.User() { UserName = dto.UserName }, dto.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
          var bulunanUser =  await _authDAL.Login(dto.UserName, dto.Password);
            if (bulunanUser==null)
            {
                return BadRequest();
                //return null();
            }
            else
            {
                var desc = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddDays(1),
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,bulunanUser.UserID.ToString()),
                    new Claim(ClaimTypes.Name,bulunanUser.UserName)
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_conf.GetSection("AppSetting:Token").Value)),SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
              var uretilenToken =  tokenHandler.CreateToken(desc);
              var donulecekTokenDegeri =  tokenHandler.WriteToken(uretilenToken);
                return Ok(donulecekTokenDegeri);
            }
        }
    }
}
