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
                
                List<string> dispchooselist = new List<string>();
                List<InventoryCosmetics> chooselist = new List<InventoryCosmetics>();
                BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => chooselist = X.ToList());
                foreach (InventoryCosmetics a in chooselist)
                {
                    picker.Items.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a.added.name));
                }
                picker.ItemsSource = dispchooselist;
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

        void Time_Changed(object sender, EventArgs e)
        {
            /*var diary = (Diary)BindingContext;
            chosentime = diary.Time;*/
        }
        void Date_Changed(object sender, EventArgs e)
        {
            /*var diary = (Diary)BindingContext;
            chosendate = diary.Date;*/
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
            note.Date = date.Date;
            note.Time = time.Time;
            note.Date.Add(note.Time);

            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveDiaryAsync(note);

                /*
                Events = new EventCollection()
                {
                    [new DateTime(2020, 3, 16)] = new List<Diary>
                    {
                        
                    }
                }
                */
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