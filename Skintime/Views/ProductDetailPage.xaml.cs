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
        
        public ProductDetailPage(string check)
        {
            InitializeComponent();
            //Check.Text = BindingContext.GetType().ToString();
            if (check == "inventory") AddButton.IsVisible = false;
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

            //Navigate to InventoryPage
            await Shell.Current.GoToAsync("..");
        }

     
    }
}