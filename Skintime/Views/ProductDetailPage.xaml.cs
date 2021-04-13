using Akavache;
using Skintime.Models;
using System;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Skintime.Views
{

    public partial class ProductDetailPage : ContentPage
    {

        public ProductDetailPage()
        {
            InitializeComponent();

            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            //Xem o WelcomePage
        }

        protected override async void OnAppearing()
        {
            Cosmetics tmp = (Cosmetics)BindingContext;

            var pullcheck = await BlobCache.Secure.GetObject<InventoryCosmetics>(tmp.name);
            InventoryCosmetics tmp1 = (InventoryCosmetics)pullcheck;
            Cosmetics check = tmp1.added;
            if (check != null) AddButton.IsVisible = false;
            else DeleteButton.IsVisible = false;
            //BindingContext = check;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            //Add cosmetics to database
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            Cosmetics check = (Cosmetics)BindingContext;

            await BlobCache.Secure.InsertObject<InventoryCosmetics>(add.added.name, add);
            await BlobCache.Secure.Flush();

            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            await App.Inventorydatabase.SaveKeyAsync(tmp);

            await Navigation.PopAsync();
            //Navigate den InventoryPage
            await Shell.Current.GoToAsync("///Inven");
        }

        public async void Delete_Clicked(object sender, EventArgs e)
        {
            InventoryCosmetics add = new InventoryCosmetics();
            add.added = (Cosmetics)BindingContext;
            Cosmetics check = (Cosmetics)BindingContext;
            await BlobCache.Secure.InvalidateObject<InventoryCosmetics>(add.added.name);

            KetQua tmp = new KetQua();
            tmp.key = add.added.name;
            await App.Inventorydatabase.DeleteKeyAsync(tmp);

            //Navigate den InventoryPage
            await Shell.Current.GoToAsync("..");
        }
    }
}