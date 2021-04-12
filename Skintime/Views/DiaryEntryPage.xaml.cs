using System;
using Xamarin.Forms;
using Skintime.Models;
using System.Reactive.Linq;
using Akavache;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

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
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            BindingContext = new Diary();

            //set min max date date picker
            date.MinimumDate = new DateTime(2020, 1, 1);
            date.MaximumDate = DateTime.Now;

            
        }

        protected override async void OnAppearing()
        {
            /*List<Cosmetics> res = new List<Cosmetics>();
            List<KetQua> getres = await App.Inventorydatabase.GetKeyAsync();
            foreach (KetQua tmp in getres)
            {
                Cosmetics push = new Cosmetics();
                BlobCache.Secure.GetObject<Cosmetics>(tmp.key).Subscribe(X => push = X);
                picker.Items.Add(push.name);
            }*/
            
        }

        async void Load_Clicked (object sender, EventArgs e)
        {
            /*if (picker.Items.Count == 0)
            {
                //List<string> dispchooselist = new List<string>();
                List<InventoryCosmetics> chooselist = new List<InventoryCosmetics>();
                BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => chooselist = X.ToList());
                picker.Title = chooselist.Count.ToString();
                foreach (InventoryCosmetics a in chooselist)
                {
                    picker.Items.Add(a.added.name);
                }
                //picker.ItemsSource = dispchooselist;
            }*/
            picker = new Picker();
            picker.FontSize = 13;
            picker.TextColor = Color.Black;
            picker.Title = "Choose your product";
            List<InventoryCosmetics> chooselist = new List<InventoryCosmetics>();
            BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => chooselist = X.ToList());
            //picker.Title = chooselist.Count.ToString();
            if (chooselist.Count != 0)
            {
                foreach (InventoryCosmetics a in chooselist)
                {
                    picker.Items.Add("Hi");
                }
            }
            else picker.Title = "Nothing here";
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

            note.Date = date.Date;
            note.Time = time.Time;
            note.Date.Add(note.Time);
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