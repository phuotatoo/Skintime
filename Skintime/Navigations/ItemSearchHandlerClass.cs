using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace Skintime.Models
{
    public class ItemSearchHandlerClass : SearchHandler
    {

        public Type SelectedItemNavigationTarget { get; set; }

        public class KetQua
        {
            [JsonProperty("DS_KetQua")]
            public Cosmetics cosmetics { get; set; }

        }

        public static string DoiString(string x)
        {
            string ans = x.Replace(' ', '+');
            return ans;
        }

        public List<Cosmetics> LayData()
        {
            var w = new WebClient();

            var json_data = string.Empty;

            var Link = "https://skincare-api.herokuapp.com/products";

            json_data = w.DownloadString(Link);
            var answer = JsonConvert.DeserializeObject<List<Cosmetics>>(json_data);
            return answer;
        }

        public void Load(List<string> MyList)
        {
            List<Cosmetics> SearchAns = LayData();
            int spt = SearchAns.Count;
            for (int i = 0; i < spt; ++i)
            {
                MyList.Add(SearchAns[i].name);
            }
        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this application.
            //await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailPage.product)}={((Cosmetics)item)}");

        }

        /*string GetNavigationTarget()
        {
            //return (Shell.Current as ItemDetails).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }*/
    }
}