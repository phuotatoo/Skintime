using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Skintime.Models;
using System.Globalization;

namespace Skintime.Views
{
    //[QueryProperty(nameof(product), nameof(product))]

    //[Xamarin.Forms.RenderWith(typeof(Xamarin.Forms.Platform._NavigationPageRenderer))]
    
    public partial class ProductDetailPage : ContentPage
    {
        
        
        public ProductDetailPage()
        {
            InitializeComponent();
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