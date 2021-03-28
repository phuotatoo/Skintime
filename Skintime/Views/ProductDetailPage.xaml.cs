using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Skintime.Models;

namespace Skintime.Views
{
    [QueryProperty(nameof(productid), nameof(productid))]

    public partial class ProductDetailPage : ContentPage
    {
        public string productid
        {
            set
            {
                LoadProduct(value);

                async void LoadProduct(string productid)
                {
                    Cosmetics res = new Cosmetics();
                    ItemSearchHandlerClass search = new ItemSearchHandlerClass();
                    List<Cosmetics> data = search.LayData();
                    try
                    {
                        res = data.Where(c =>
                        {
                            string finding = productid;
                            return c.id.Contains(finding);
                        }).FirstOrDefault();
                        BindingContext = res;
                        //oke we khá gud now
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("pleee");
                    }
                    //còn đó humm
                    // bạn có định sang làm nốt cái 
                    //button? u got me
                    //doi xiu okee
                }
            }
        }
        public ProductDetailPage()
        {
            InitializeComponent();
        }
    }
}