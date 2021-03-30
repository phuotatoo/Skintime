using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Skintime.Models;

namespace Skintime.Views
{
    [QueryProperty(nameof(product), nameof(product))]

    //[Xamarin.Forms.RenderWith(typeof(Xamarin.Forms.Platform._NavigationPageRenderer))]
    //dang bi choang, nen la, toi se di nam 1 nhum
    // oklee di nghi di
    // ngoi day la pan' do'
    //j
    //hong
    // sao lai hong
    //thich the day
    // o kiaaa =(())
    //hui di day
    //okee dung luot discord
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
        // heee toi di an dayyy
        // tat teamview nha
        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = new Cosmetics();
        }
    }
}