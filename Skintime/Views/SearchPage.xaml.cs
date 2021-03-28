using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Models;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            searchhandler.Load(MyList);
            mycosmetics = searchhandler.LayData();
            Coll.ItemsSource = MyList;
        }

        List<String> MyList = new List<string>();
        List<Cosmetics> mycosmetics = new List<Cosmetics>();
        ItemSearchHandlerClass searchhandler = new ItemSearchHandlerClass();
        //hmm đợi toi 
        private async void Search_TextChange(object sender, TextChangedEventArgs e)
        {
            var SearchResult = MyList.Where(c =>
            {
                string text1 = Search.Text;
                return c.ToLower().Contains(text1.ToLower());
            });
            Coll.ItemsSource = SearchResult;
            
        }
        // o day con thieu vai thu=))
        // à mà, vì text change hơi khó, nên nhóm tao định enter để search
        //nếu mà để ở homepage chắc cũng được mà nhỉ? con
        //load lau vl
        // a 
        // speedup bang data offline =)))
        // "CosmeticsDatabase.cs" =)))  
    }
}