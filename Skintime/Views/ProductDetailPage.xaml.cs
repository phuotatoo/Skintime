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
            //Check.Text = BindingContext.GetType().ToString()
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            //Xem o WelcomePage
        }

        protected override async void OnAppearing()
        {
            Cosmetics tmp = (Cosmetics)BindingContext;
            if (tmp == null) AddButton.Text = "Null";
            else AddButton.Text = "Khum Null";
            string okela = tmp.name;
            var pullcheck = await BlobCache.Secure.GetObject<InventoryCosmetics>(okela);
            InventoryCosmetics tmp1 = (InventoryCosmetics)pullcheck;
            Cosmetics check = tmp1.added;
            if (check != null) AddButton.IsVisible = false;
            //BindingContext = check;//
        }

        public async void Delete_Clicked(object sender, EventArgs e)
        {
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            Cosmetics check = (Cosmetics)BindingContext;
            if (check != null) AddButton.Text = "Khum Null";
            else AddButton.Text = "Null";

            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            await App.Inventorydatabase.SaveKeyAsync(tmp);

            await Shell.Current.GoToAsync("///Inven");
        }
        private async void Add_Clicked(object sender, EventArgs e)
        {
            //Add cosmetics to database
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            Cosmetics check = (Cosmetics)BindingContext;
            if (check != null) AddButton.Text = "Khum Null";
            else AddButton.Text = "Null";

            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            await App.Inventorydatabase.SaveKeyAsync(tmp);
            await BlobCache.Secure.InsertObject(add.added.name, add);

            await Shell.Current.GoToAsync("///Inven");
             
        }
    }
}