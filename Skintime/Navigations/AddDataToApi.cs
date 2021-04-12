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

namespace Skintime.Navigations
{
    class AddDataToApi
    {
        static bool Add()
        {

            using (var client = new HttpClient())
            {
                Cosmetics p = new Cosmetics();

                p.name = "a";
                p.brand = "b";
                p.ingredient_list[1] = "c";
                p.ingredient_list[2] = "d";

                client.BaseAddress = new Uri("https://skincare-api.herokuapp.com");
                var response = client.PostAsJsonAsync("/products", p).Result;
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
