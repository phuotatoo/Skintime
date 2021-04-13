using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Skintime.Models;
using Akavache;

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
