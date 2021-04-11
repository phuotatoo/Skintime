using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Data;

namespace Skintime
{
    public partial class App : Application
    {
        static DiaryDatabase database;
        

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

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
