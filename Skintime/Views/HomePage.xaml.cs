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
        public System.Windows.Input.ICommand SearchCommand { get; set; }

        async void OnSearchClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("searchpage");
        }
            
    }
}