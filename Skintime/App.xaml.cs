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
                    keydatabase = new KeyDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tmpkey1.db3"));
                }
                return keydatabase;
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
