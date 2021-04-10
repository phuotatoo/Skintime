using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Data;
using Skintime.Models;
using Skintime.Views;
using System.Reactive.Linq;
using System.Collections.Generic;
using Akavache;

namespace Skintime
{
    public partial class App : Application
    {
        static DiaryDatabase database;
        static KeyDatabase keydatabase;
        ItemSearchHandlerClass itemsearch = new ItemSearchHandlerClass();

        public static DiaryDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DiaryDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Skintime.db3"));
                }
                return database;
            }
        }
        public static KeyDatabase Keydatabase
        {
            get
            {
                if (keydatabase == null)
                {
                    keydatabase = new KeyDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "key1.db3"));
                }
                return keydatabase;
            }
        }
        public App()
        {
            InitializeComponent();

            Registrations.Start("Skintime");
            MainPage = new SearchPage();
        }
        
        protected override async void OnStart()
        {
            List<Key> check = await App.keydatabase.GetKeyAsync();
            if (check.Count == 0)
            {
                //Insert loading page
                List<Cosmetics> collected = itemsearch.LayData();
                foreach (Cosmetics a in collected)
                {
                    Key tmp = new Key();
                    tmp.productbrand = a.brand;
                    tmp.productname = a.name;
                    await BlobCache.InMemory.InsertObject<Cosmetics>(a.name, a);
                    await App.Keydatabase.SaveKeyAsync(tmp);
                }
            }
        }

        protected override async void OnSleep()
        {
            //BlobCache.Shutdown().Wait();
        }

        protected override async void OnResume()
        {
        }
    }
}
