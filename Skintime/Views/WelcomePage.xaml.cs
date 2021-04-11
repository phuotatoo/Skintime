using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Skintime.Models;
using Skintime.Views;
using System.Globalization;

namespace Skintime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        Image splashImage;
        public WelcomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "Logo.png",
                WidthRequest = 150,
                HeightRequest = 150
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);
            
            this.BackgroundColor = Color.FromHex("#A3BCB6");
            this.Content = sub;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            /*
             * Akavache là hệ thống cho phép lưu data vào bộ nhớ trong của máy (hoặc RAM)
             * Điểm cộng lớn nhất của Akavache là cho phép lưu nhiều kiểu dữ liệu khác nhau trên cùng 1 database
             * như bạn thấy thì mình dùng BlobCache.Secure để lưu Cosmetics và InventoryCosmetics (Có thể lưu cả Diary nếu cần)
             * Akavache có 4 destination để lưu data: UserAccount,LocalMachine,Secure,InMemory (Detail tự đọc ei)
             * Túm lại là dùng Secure làm destination để lưu data
             * BlobCache.ApplicationName để khai báo tên chương trình, để có vị trí xác định cho việc lưu data
             * EnsureInitialized là để đảm bảo khai báo thành công, mới thực hiện được CRUD Operation
             * Một vài hàm khác thì đọc tên chắc các bạn hiểu =))
             */
            await Task.Delay(3000); //Time-consuming processes such as initialization
            await splashImage.FadeTo(0, 1000, Easing.SinInOut);

            List<Key> check = await App.Keydatabase.GetKeyAsync();
            //check để kiểm tra xem trên database của app đã load api hay chưa
            //check.Count == 0 thì sẽ thực hiện thao tác load và one-time-only
            if (check.Count == 0)
            {
                ItemSearchHandlerClass tmp = new ItemSearchHandlerClass();
                List<Cosmetics> collected = tmp.LayData();
                foreach(Cosmetics a in collected)
                {
                    Key tmpkey = new Key();
                    a.brand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a.brand.ToLower());
                    a.name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a.name.ToLower());
                    tmpkey.brand = a.brand;
                    tmpkey.name = a.name;
                    App.Keydatabase.SaveKeyAsync(tmpkey);
                    await BlobCache.Secure.InsertObject(a.name, a);
                }
            }

            Application.Current.MainPage = new AppShell();   //After loading  MainPage it gets Navigated to our new Page
        }
    }
}