using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using Skintime.Models;

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
        List<InventoryCosmetics> chooselist = new List<InventoryCosmetics>();
        protected override async void OnAppearing()
        {
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            Diary note = (Diary)BindingContext;
            picker.SelectedItem = note.Product;
            picker.FontSize = 13;
            picker.TextColor = Color.Black;
            picker.Title = "Choose your item";

        }

        async void LoadDiary(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                Diary note = await App.Database.GetDiaryAsync(id);
                BindingContext = note;
                
                ChangeColor(normal, note.Normal);
                ChangeColor(acne, note.Acne);
                ChangeColor(eczema, note.Eczema);
                if (picker.Items.Count == 0)
                {
                    var list = await BlobCache.Secure.GetAllObjects<InventoryCosmetics>();
                    chooselist = list.ToList();
                    foreach (InventoryCosmetics a in chooselist)
                    {
                        picker.Items.Add(a.added.name);
                    }
                }
                picker.SelectedItem = note.Product;
                //normal.Text = note.Product;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load diary.");
            }
        }

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
            normal.Text = picker.SelectedItem.ToString();
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