using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            await Task.Delay(2000); //Time-consuming processes such as initialization
            await splashImage.FadeTo(0, 1000, Easing.SinInOut);
            Application.Current.MainPage = new AppShell();   //After loading  MainPage it gets Navigated to our new Page
        }
    }
}