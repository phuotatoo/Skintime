using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        async void OnSearchClicked(object sender, EventArgs e)
        {

            var Search = new SearchPage();
            await Navigation.PushAsync(Search);
        }
            
    }
}