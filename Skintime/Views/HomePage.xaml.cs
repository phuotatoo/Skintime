using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Skintime.Models;
using Skintime;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        async void OnSearchClicked(object sender, EventArgs e)
        {

            var Search = new SearchPage();
            await Navigation.PushAsync(Search);
        }
        async void AddProduct_Clicked(object sender, EventArgs e)
        {
            var Addd = new AddCosmeticPage();
            await Navigation.PushAsync(Addd);
        }

        async void Expire_Clicked(object sender, EventArgs e)
        {
            var Expire = new ExpireCosmetics();
            await Navigation.PushAsync(Expire);
        }

        static bool Add()
        {

            using (var client = new HttpClient())
            {
                Cosmetics p = new Cosmetics();

                p.name = "a";
                p.brand = "b";
                p.ingredient_list = new List<String>();
                p.ingredient_list.Add("c");
                p.ingredient_list.Add("d");

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