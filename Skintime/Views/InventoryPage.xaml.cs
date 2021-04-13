using Akavache;
using Skintime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            disp1 = new List<Cosmetics>();
            var list = await BlobCache.Secure.GetAllObjects<InventoryCosmetics>();
            List<KetQua> res = await App.Inventorydatabase.GetKeyAsync();
            //Get objects from memory

            invent1 = list.ToList();

            foreach (InventoryCosmetics a in invent1)
            {
                disp1.Add(a.added);
            }
            Disp1Coll.ItemsSource = disp1;
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var cosmetics = (Cosmetics)e.CurrentSelection.FirstOrDefault();

                //Navigate to DetailPage
                var DetailPage = new ProductDetailPage();
                DetailPage.BindingContext = cosmetics;
                await Navigation.PushAsync(DetailPage);
            }

        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await BlobCache.Secure.InvalidateAllObjects<InventoryCosmetics>();
            Disp1Coll.ItemsSource = new List<Cosmetics>();
            await App.Inventorydatabase.DeleteAll();
        }

    }
}
