using Skintime.Views;
using Xamarin.Forms;

namespace Skintime
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DiaryEntryPage), typeof(DiaryEntryPage));
        }
    }
}