using Akavache;
using Skintime.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace Skintime.Navigations
{
    class AddDataToApi
    {
        public bool Add()
        {
            Cosmetics added_cosmetics = new Cosmetics();
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            BlobCache.Secure.GetObject<Cosmetics>("Just_Added").Subscribe(x => added_cosmetics = x);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://skincare-api.herokuapp.com");
                var response = client.PostAsJsonAsync("/products", added_cosmetics).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;

            }
        }
    }
}
