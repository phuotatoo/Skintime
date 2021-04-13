
using Skintime.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Skintime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DiaryEntryPage), typeof(DiaryEntryPage));
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            //Routing.RegisterRoute(nameof(InventoryPage), typeof(InventoryPage));
        }
    }
}