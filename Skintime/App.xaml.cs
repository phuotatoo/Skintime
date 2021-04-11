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
