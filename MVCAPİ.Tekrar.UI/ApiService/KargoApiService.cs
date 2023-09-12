using MVCAPİ.Tekrar.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.UI.ApiService
{
    public class KargoApiService
    {
        HttpClient _client;
        public KargoApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ShipperDTO>> KargolariGetir()
        {
            List<ShipperDTO> shipperDTOs = null;

            var donenDeger = await _client.GetAsync("api/Kargo");
            if (donenDeger.IsSuccessStatusCode)
            {
                shipperDTOs = JsonConvert.DeserializeObject<List<ShipperDTO>>(await donenDeger.Content.ReadAsStringAsync());
            }
            return shipperDTOs;

        }
        public async Task<string> ViewToAddShip(ShipperDTO dto)
        {
            var eklenecekIcerik = new StringContent(JsonConvert.SerializeObject(dto));
            eklenecekIcerik.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            string donendeger = null;
            try
            {
             var donenPostDegeri =   await _client.PostAsync("api/AddShippers", eklenecekIcerik);

                if (donenPostDegeri.IsSuccessStatusCode)
                {
                  donendeger = await  donenPostDegeri.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

                donendeger = "bir hata olustu";
            }
            return donendeger;
        }
        public async Task<string> TokenShipAddAsync(ShipperDTO dto, string token = null)
        {
            if (token == null)
            {
                return "token yok";
            }
            var hede = new StringContent(JsonConvert.SerializeObject(dto));
            hede.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);
            var donendeger = await _client.PostAsync("api/AddShippers", hede);
            string veri = "";
            if (donendeger.IsSuccessStatusCode)
            {
                veri = "veri tokenn bilgisi okunarak eklendi";
            }
            return veri;
        }
    }
}
