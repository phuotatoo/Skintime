using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Plugin.Calendar.Models;
using Skintime.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skintime.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiaryPageViewModels : INotifyPropertyChanged
    {
        public DiaryPageViewModels() : base()
        {
            //InitializeComponent();


            
        }
        public EventCollection Events = new EventCollection
        {
            //[DateTime.Now.AddDays(1)] = new List<Diary>(GenerateEvents(3, "Nice", DateTime.Now.AddDays(1)))
        };
        private IEnumerable<Diary> GenerateEvents(int count, string name,DateTime date)
        {
            return Enumerable.Range(1, count).Select(x => new Diary
            {
                Text = $"{name} event{x}",
                Date = date
            }); ;
        }
    }
}