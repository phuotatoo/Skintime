
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CDiaryPage : ContentPage
    {
        public CDiaryPage()
        {
            InitializeComponent();
        }

        public EventCollection Events { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Events = new EventCollection
            {
                /*
                [DateTime.Now] = new List<Diary>
                {
                    new Diary { Name = "Cool event1", Description = "This is Cool event1's description!" }
                },

                
                // 5 days from today
                [DateTime.Now.AddDays(5)] = new List<Diary>
                {
                    new Diary { Name = "Cool event3", Description = "This is Cool event3's description!" }
                },

                // 3 days ago
                [DateTime.Now.AddDays(-3)] = new List<Diary>
                {
                    new Diary { Name = "Cool event5", Description = "This is Cool event5's description!" }
                },

                // custom date
                [new DateTime(2020, 3, 16))] = new List<Diary>
                {
                    new Diary { Name = "Cool event6", Description = "This is Cool event6's description!" }
                }
                */
            };

        }
    }
}