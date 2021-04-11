using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using Skintime.Models;
using System.Globalization;

namespace Skintime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        Image splashImage;
        public WelcomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "Logo.png",
                WidthRequest = 150,
                HeightRequest = 150
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);
            
            this.BackgroundColor = Color.FromHex("#A3BCB6");
            this.Content = sub;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            await Task.Delay(3000); //Time-consuming processes such as initialization
            await splashImage.FadeTo(0, 1000, Easing.SinInOut);

            List<Key> check = await App.Keydatabase.GetKeyAsync();
            if (check.Count == 0)
            {
                ItemSearchHandlerClass tmp = new ItemSearchHandlerClass();
                List<Cosmetics> collected = tmp.LayData();
                foreach(Cosmetics a in collected)
                {
                    Key tmpkey = new Key();
                    a.brand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a.brand.ToLower());
                    a.name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a.name.ToLower());
                    tmpkey.brand = a.brand;
                    tmpkey.name = a.name;
                    await App.Keydatabase.SaveKeyAsync(tmpkey);
                    await BlobCache.Secure.InsertObject(a.name, a);
                }
            }

            Application.Current.MainPage = new AppShell();   //After loading  MainPage it gets Navigated to our new Page
        }
    }
}