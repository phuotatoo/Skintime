using Skintime.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace Skintime
{
    public partial class App : Application
    {
        static DiaryDatabase database;
        static KeyDatabase keydatabase;
        static InventoryDatabase inventorydatabase;

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
                    keydatabase = new KeyDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tmpkey1.db3"));
                }
                return keydatabase;
            }
        }

        public static InventoryDatabase Inventorydatabase
        {
            get
            {
                if (inventorydatabase == null)
                {
                    inventorydatabase = new InventoryDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "inventory.db3"));
                }
                return inventorydatabase;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new WelcomePage();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
