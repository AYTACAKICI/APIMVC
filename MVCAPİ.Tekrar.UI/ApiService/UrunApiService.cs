using Microsoft.AspNetCore.Http;
using MVCAPİ.Tekrar.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.UI.ApiService
{
    public class UrunApiService
    {

        HttpClient _client;
        public UrunApiService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<ProductDTO>> UrunleriListele()
        {
            var degerler = await _client.GetAsync("api/sepet/getproducts");
           List<ProductDTO> listem = null;
            if (degerler.IsSuccessStatusCode)
            {
               listem = JsonConvert.DeserializeObject<List<ProductDTO>>(await degerler.Content.ReadAsStringAsync());
            }   
            return listem;
        }

        

        public async Task<string> SepetiKaydet(AddToSepetDTO dto)
        {
                     
            
            var eklenecekSepet = new StringContent(JsonConvert.SerializeObject(dto));
            eklenecekSepet.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            string donendeger = null;
            try
            {
              var donencevap =  await _client.PostAsync("api/sepet/sepeteekle", eklenecekSepet);
                if (donencevap.IsSuccessStatusCode)
                {
                    return "Sepet Kaydedilmiştir";
                }
                else
                {
                    return "Sepet Kaydedilememiştir";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           

           
        }
    }
}
