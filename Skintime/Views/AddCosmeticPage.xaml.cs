using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Skintime.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCosmeticPage : ContentPage
    {
        public AddCosmeticPage()
        {
            InitializeComponent();
            BindingContext = new UserAddCosmetics();
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            UserAddCosmetics tmp= new UserAddCosmetics();
            tmp = (UserAddCosmetics)BindingContext;
            Cosmetics push = new Cosmetics();
            push.id = "test";
            push.name = tmp.name;
            push.brand = tmp.brand;
            push.ingredient_list = new List<String>();
            push.ingredient_list = tmp.list.Split(' ').ToList();
            var check = await BlobCache.Secure.InsertObject(push.name, push);
            BlobCache.Secure.Flush();
            await Shell.Current.GoToAsync("..");
        }
    }
}