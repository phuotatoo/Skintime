using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skintime.Views;
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

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            // Let the animation completed
            await Task.Delay(1000);
            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailPage.productid)}={((Cosmetics)item).id}");
            //cái này còn tuỳ là bạn muốn đẩy cái nào qua để tìm
            // kiểu cái này nè
            // nó là dữ liệu để truyền qua cho cái DetailPage á
            // mình muốn tìm cosmetics bằng properties nào thì đẩy cái đó qua
            // hê chắc là bạn sẽ hiểu=))
            //cái này trước hiểu ròi :)) oke we gud
            // trưa này 3h của toi cũng có ích
            // hê
            // recommend tìm bằng name
            //sao không phải id?
            // id phải ToString, với lại sang bên detailpage phải đổi lại thành số=)) lừi
        }

        /*string GetNavigationTarget()
        {
            //return (Shell.Current as ItemDetails).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }*/
    }
}