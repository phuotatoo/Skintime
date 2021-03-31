using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Skintime.Models;

namespace Skintime.Views
{
    [QueryProperty(nameof(product), nameof(product))]

    //[Xamarin.Forms.RenderWith(typeof(Xamarin.Forms.Platform._NavigationPageRenderer))]
    
    public partial class ProductDetailPage : ContentPage
    {
        public Cosmetics product
        {
            set
            {
                LoadProduct(value);

                async void LoadProduct(Cosmetics product)
                {
                    try
                    {

                        BindingContext = product;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to load product");
                    }
                }
            }
        }
        
        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = new Cosmetics();
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SearchPage();
        }


    }
}