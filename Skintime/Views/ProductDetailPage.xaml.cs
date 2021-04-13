using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Skintime.Models;
using System.Globalization;
using System.Reactive.Linq;

namespace Skintime.Views
{
    
    public partial class ProductDetailPage : ContentPage
    {

        public ProductDetailPage()
        {
            InitializeComponent();
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
        }

        protected override async void OnAppearing()
        {
            Cosmetics tmp = (Cosmetics)BindingContext;
            string okela = tmp.name;
            var pullcheck = await BlobCache.Secure.GetObject<InventoryCosmetics>(okela);
            InventoryCosmetics tmp1 = (InventoryCosmetics)pullcheck;
            Cosmetics check = tmp1.added;
            if (check != null) AddButton.IsVisible = false;
            else DeleteButton.IsVisible = false;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            //Add cosmetics to database
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            Cosmetics check = (Cosmetics)BindingContext;
            await BlobCache.Secure.InsertObject<InventoryCosmetics>(add.added.name, add);
            await BlobCache.Secure.InsertObject<Cosmetics>(add.added.name, check);
            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            await App.Inventorydatabase.SaveKeyAsync(tmp);

            //Navigate to InventoryPage
            await Shell.Current.GoToAsync("///Inven");
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            //Add cosmetics to database
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            Cosmetics check = (Cosmetics)BindingContext;
            await BlobCache.Secure.InvalidateObject<InventoryCosmetics>(add.added.name);
            await BlobCache.Secure.InvalidateObject<Cosmetics>(add.added.name);
            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            await App.Inventorydatabase.DeleteKeyAsync(tmp);

            //Navigate to InventoryPage
            await Shell.Current.GoToAsync("///Inven");
        }
    }
}