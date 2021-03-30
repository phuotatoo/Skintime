using System;
using Xamarin.Forms;
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
        
        async void Time_Changed(object sender, EventArgs e)
        {
            /*var diary = (Diary)BindingContext;
            chosentime = diary.Time;*/
        }
        async void Date_Changed(object sender, EventArgs e)
        {
            /*var diary = (Diary)BindingContext;
            chosendate = diary.Date;*/
        }
        async void OnNormalButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Normal = !note.Normal;
            ChangeColor(sender, note.Normal);
            BindingContext = note;
        }
        
        async void OnAcneButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Acne = !note.Acne;
            ChangeColor(sender, note.Acne);
            BindingContext = note;
        }


        async void OnEczemaButtonClicked(object sender, EventArgs e)
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