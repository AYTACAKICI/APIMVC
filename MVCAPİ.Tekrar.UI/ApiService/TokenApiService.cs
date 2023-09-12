using MVCAPİ.Tekrar.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.UI.ApiService
{
    public class TokenApiService
    {
        HttpClient _client;
        public TokenApiService(HttpClient client)
        {
            _client = client;
        }
        public async Task<string> TokenAl(string kullaniciAdi, string sifre)
        {
            LoginDTO dto = new LoginDTO();
            dto.UserName = kullaniciAdi;
            dto.Password = sifre;
            StringContent mycontent = new StringContent(JsonConvert.SerializeObject(dto));
            mycontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var apininGöndermisOlduguDeger = await _client.PostAsync("api/auth/login", mycontent);
            string token = "";
            if (apininGöndermisOlduguDeger.IsSuccessStatusCode)
            {
                token = await apininGöndermisOlduguDeger.Content.ReadAsStringAsync();
            }
            return token;
        }
        public async Task<string> KullaniciKaydet(RegisterDTO dto)
        {


            StringContent mycontent = new StringContent(JsonConvert.SerializeObject(dto));
            mycontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var apininGöndermisOlduguDeger = await _client.PostAsync("api/auth/register", mycontent);

            if (apininGöndermisOlduguDeger.IsSuccessStatusCode)
            {
                return "kayıt yapıldı...";
            }
            else
            {
                return "";
            }
           
        }
     
    }
}
