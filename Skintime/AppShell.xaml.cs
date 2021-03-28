﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Skintime.Views;

namespace Skintime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DiaryEntryPage), typeof(DiaryEntryPage));
        }
    }
}