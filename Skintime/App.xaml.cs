using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Data;
using Akavache;
using Skintime.Views;
using Skintime.Models;
using System.Collections.Generic;

namespace Skintime
{
    public partial class App : Application
    {
        static DiaryDatabase database;
        static KeyDatabase keydatabase;

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
                    keydatabase = new KeyDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tmpkey.db3"));
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
            ItemSearchHandlerClass itemsearch = new ItemSearchHandlerClass();
            if (check.Count == 0)
            {
                //Insert loading page
                List<Cosmetics> collected = itemsearch.LayData();
                foreach (Cosmetics a in collected)
                {
                    Key tmp = new Key();
                    tmp.brand = a.brand;
                    tmp.name = a.name;
                    BlobCache.InMemory.InsertObject<Cosmetics>(a.name, a);
                    await App.Keydatabase.SaveKeyAsync(tmp);
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
