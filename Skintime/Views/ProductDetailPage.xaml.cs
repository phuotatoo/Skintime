using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Skintime.Models;
using System.Globalization;

namespace Skintime.Views
{
    
    public partial class ProductDetailPage : ContentPage
    {
        Image BG = new Image
        {
            Source = "LeafBackground.png",
        };
        
        public ProductDetailPage(string check)
        {
            InitializeComponent();
            //Check.Text = BindingContext.GetType().ToString();
            if (check == "inventory") AddButton.IsVisible = false;
            else DeleteButton.IsVisible = false;
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            //Xem o WelcomePage
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            //Add cosmetics to database
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            BlobCache.Secure.InsertObject<InventoryCosmetics>(add.added.name, add);
            BlobCache.Secure.Flush();

            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            App.Inventorydatabase.SaveKeyAsync(tmp);
            //Navigate to InventoryPage
            await Shell.Current.GoToAsync("..");
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            //Add cosmetics to database
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            BlobCache.Secure.InvalidateObject<InventoryCosmetics>(add.added.name);
            BlobCache.Secure.Flush();
            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            App.Inventorydatabase.DeleteKeyAsync(tmp);

            //Navigate to InventoryPage
            await Shell.Current.GoToAsync("..");
        }
    }
}