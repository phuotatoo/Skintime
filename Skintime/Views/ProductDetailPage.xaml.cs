using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Skintime.Models;

namespace Skintime.Views
{
    [QueryProperty(nameof(product), nameof(product))]

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
    }
}