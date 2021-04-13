using Akavache;
using Skintime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Skintime.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class DiaryEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadDiary(value);
            }
        }

        public DiaryEntryPage()
        {
            InitializeComponent();
            // Set the BindingContext of the page to a new Diary.

            BindingContext = new Diary();

            //set min max date date picker
            date.MinimumDate = new DateTime(2020, 1, 1);
            date.MaximumDate = DateTime.Now;


        }

        protected override async void OnAppearing()
        {
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            List<InventoryCosmetics> chooselist = new List<InventoryCosmetics>();
            List<Cosmetics> disp1 = new List<Cosmetics>();
            BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => chooselist = X.ToList());
            //BlobCache.Secure.Dispose();
            //picker.Title = chooselist.Count.ToString();
            if (picker.Items.Count == 0)
            {
                var list = await BlobCache.Secure.GetAllObjects<InventoryCosmetics>();
                chooselist = list.ToList();
                foreach (InventoryCosmetics a in chooselist)
                {
                    disp1.Add(a.added);
                }
                foreach (Cosmetics a in disp1)
                {
                    picker.Items.Add(a.name);
                }
            }
            //else picker.Title = "Nothing here";
            List<KetQua> getres = await App.Inventorydatabase.GetKeyAsync();
            foreach (KetQua tmp in getres)
            {
                Cosmetics push = new Cosmetics();
                BlobCache.Secure.GetObject<Cosmetics>(tmp.key).Subscribe(X => push = X);
                //picker.Items.Add((string)push.name);
            }
            picker.FontSize = 13;
            picker.TextColor = Color.Black;
            picker.Title = "Choose your item";
        }

        async void Load_Clicked(object sender, EventArgs e)
        {
            //BlobCache.ApplicationName = "Skintime";
            //BlobCache.EnsureInitialized();
            List<InventoryCosmetics> chooselist = new List<InventoryCosmetics>();
            List<Cosmetics> disp1 = new List<Cosmetics>();
            var list = await BlobCache.Secure.GetAllObjects<InventoryCosmetics>();
            BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => chooselist = X.ToList());
            //BlobCache.Secure.Dispose();
            //picker.Title = chooselist.Count.ToString();
            chooselist = list.ToList();
            if (picker.Items.Count == 0)
            {
                //var list = await BlobCache.Secure.GetAllObjects<InventoryCosmetics>();
                chooselist = list.ToList();
                foreach (InventoryCosmetics a in chooselist)
                {
                    disp1.Add(a.added);
                }
                foreach (Cosmetics a in disp1)
                {
                    picker.Items.Add(a.name);
                }
            }
            //else picker.Title = "Nothing here";
            List<KetQua> getres = await App.Inventorydatabase.GetKeyAsync();
            foreach (KetQua tmp in getres)
            {
                Cosmetics push = new Cosmetics();
                BlobCache.Secure.GetObject<Cosmetics>(tmp.key).Subscribe(X => push = X);
                //picker.Items.Add((string)push.name);
            }
            picker.FontSize = 13;
            picker.TextColor = Color.Black;
            picker.Title = "Choose your item";
        }

        async void LoadDiary(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Diary note = await App.Database.GetDiaryAsync(id);
                BindingContext = note;

                ChangeColor(normal, note.Normal);
                ChangeColor(acne, note.Acne);
                ChangeColor(eczema, note.Eczema);
                picker.SelectedItem = note.Product;
                itemid = itemId;

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load diary.");
            }
        }
        string itemid;


        public void ChangeColor(object sender, bool state)
        {
            if (state)
            {
                (sender as Button).BackgroundColor = Color.Black;
                (sender as Button).TextColor = Color.AntiqueWhite;
            }
            else
            {
                (sender as Button).BackgroundColor = Color.Khaki;
                (sender as Button).TextColor = Color.Black;
            }
        }


        void OnNormalButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Normal = !note.Normal;
            ChangeColor(sender, note.Normal);
            BindingContext = note;
        }

        void OnAcneButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Acne = !note.Acne;
            ChangeColor(sender, note.Acne);
            BindingContext = note;
        }

        void OnEczemaButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Eczema = !note.Eczema;
            ChangeColor(sender, note.Eczema);
            BindingContext = note;
        }



        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;

            DateTime temp = date.Date + time.Time;
            //note.datetime = date.Date;
            //note.datetime.Add(time.Time);

            note.datetime = temp;


            note.Product = picker.SelectedItem.ToString();
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveDiaryAsync(note);
            }


            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }



        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            await App.Database.DeleteDiaryAsync(note);
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

    }
}