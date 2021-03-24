using Skintime.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace Skintime
{
    public partial class App : Application
    {
        static DiaryDatabase database;
        //static InventoryDatabase inventory;

        // Create the database connection as a singleton.
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
        /*
        public static InventoryDatabase Inventory
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new InventoryDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Inventory.db3"));
                }
                return inventory;
            }
        }
        */
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
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
