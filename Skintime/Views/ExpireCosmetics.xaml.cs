using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skintime.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpireCosmetics : ContentPage
    {
        public ExpireCosmetics()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            List<Diary> list = await App.Database.GetDiaryAsync();
            var tmp = list.Where(c =>
            {
                DateTime check = DateTime.Now;
                check.AddHours(-check.Hour);
                check.AddMinutes(-check.Minute);
                check.AddSeconds(-check.Second);
                check.AddHours(c.datetime.Hour);
                check.AddMinutes(c.datetime.Minute);
                check.AddSeconds(c.datetime.Second);
                return c.datetime.Equals(check);
            });
            Collection.ItemsSource = tmp.ToList();
        }
    }
}