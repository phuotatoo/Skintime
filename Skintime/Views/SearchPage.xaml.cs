using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive.Linq;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            
            InitializeComponent();
            BlobCache.ApplicationName = "Skintime";
            //Registrations.Start("Skintime");
            BlobCache.EnsureInitialized();
            
        }
        protected override async void OnAppearing()
        {
            if (Coll.SelectedItem != null)
            {
                var Search = new SearchPage();
                await Navigation.PopAsync();
                await Navigation.PushAsync(Search);
            }
            var list = await BlobCache.Secure.GetAllObjects<Cosmetics>();
            mycosmetics = list.ToList();
            Coll.ItemsSource = mycosmetics;
        }   
        List<Cosmetics> mycosmetics = new List<Cosmetics>();

        private void Search_TextChange(object sender, TextChangedEventArgs e)
        {
            var SearchResult1 = mycosmetics.Where(c =>
            {
                string text1 = Search.Text;
                return c.name.ToLower().Contains(text1.ToLower());
            });
            var SearchResult2 = mycosmetics.Where(c =>
            {
                string text1 = Search.Text;
                return c.brand.ToLower().Contains(text1.ToLower());
            });
            List<Cosmetics> a = SearchResult1.ToList();
            List<Cosmetics> a2 = SearchResult2.ToList();
            a = a.Union(a2).ToList();
            Coll.ItemsSource = a;
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView collectionV = sender as CollectionView;
            //check.Text = 
            if (e.CurrentSelection != null)
            {
                var cosmetics = (Cosmetics)e.CurrentSelection.FirstOrDefault();
                var DetailPage = new ProductDetailPage();
                DetailPage.BindingContext = cosmetics;
                await Navigation.PushAsync(DetailPage);
            }
            else
            {
                Coll.SelectedItem = null;
            }
        }
    }

}