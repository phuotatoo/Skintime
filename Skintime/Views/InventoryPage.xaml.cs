using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using Skintime.Models;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            //Xem o WelcomePage
        }
        List<Cosmetics> disp1 = new List<Cosmetics>(); //CollectionView itemsource
        List<InventoryCosmetics> invent1 = new List<InventoryCosmetics>(); //Init disp1
        //Tạo InventoryCosmetics để tránh conflict với Cosmetics có sẵn trong database 
        //do Akavache lưu dưới dạng key-value nên mỗi Cosmetics với 1 cặp key-value là unique
        protected override void OnAppearing()
        {
            base.OnAppearing();
            disp1 = new List<Cosmetics>();
            InventoryCosmetics tmp = new InventoryCosmetics();
            tmp.added = new Cosmetics();
            tmp.added.name = "push";
            BlobCache.Secure.InsertObject<InventoryCosmetics>("push", tmp);
            BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => invent1 = X.ToList());
            //Get objects from memory
            foreach (InventoryCosmetics a in invent1)
            {
                if (a.added.name != "push") disp1.Add(a.added);
            }
            Disp1Coll.ItemsSource = disp1;
            BlobCache.Secure.InvalidateObject<InventoryCosmetics>("push");
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var cosmetics = (Cosmetics)e.CurrentSelection.FirstOrDefault();

                //Navigate to DetailPage
                var DetailPage = new ProductDetailPage("inventory");
                DetailPage.BindingContext = cosmetics;
                await Navigation.PushAsync(DetailPage);
            }
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await BlobCache.Secure.InvalidateAllObjects<InventoryCosmetics>();
            Disp1Coll.ItemsSource = new List<Cosmetics>();
        }

    }
}