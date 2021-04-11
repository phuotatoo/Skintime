﻿using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Models;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            
            InitializeComponent();
            BlobCache.ApplicationName = "Skintime";
            Registrations.Start("Skintime");
            //BlobCache.UserAccount.GetObjects<Cosmetics>().Subscribe(x => mycosmetics = x.ToList());
            Coll.ItemsSource = mycosmetics;
            Search.Text = mycosmetics.Count.ToString();
        }

        List<Key> MyList = new List<Key>();
        List<Cosmetics> mycosmetics = new List<Cosmetics>();
        
        private async void Search_TextChange(object sender, TextChangedEventArgs e)
        {
            MyList = await App.Keydatabase.GetKeyAsync();
            var SearchResult1 = MyList.Where(c =>
            {
                string text1 = Search.Text;
                return c.name.ToLower().Contains(text1.ToLower());
            });
            var SearchResult2 = mycosmetics.Where(c =>
            {
                string text1 = Search.Text;
                return c.brand.ToLower().Contains(text1.ToLower());
            });
            List<Key> a = SearchResult1.ToList();
            List<Cosmetics> res = new List<Cosmetics>();
            foreach (Key tmp in a)
            {
                Cosmetics push = new Cosmetics();
                BlobCache.InMemory.GetObject<Cosmetics>(tmp.name).Subscribe(X => push = X);
                res.Add(push);
            }
            //Search.Text = MyList.Count.ToString();
            Coll.ItemsSource = res;
            //check.Text = a.Count().ToString();
        }
        
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var cosmetics = (Cosmetics)e.CurrentSelection.FirstOrDefault();

                var DetailPage = new ProductDetailPage();
                DetailPage.BindingContext = cosmetics;
                await Navigation.PushAsync(DetailPage);
                //await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailPage.product)}={cosmetics}");
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

    }
}