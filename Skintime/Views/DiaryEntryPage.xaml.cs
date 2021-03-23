using System;
using Skintime.Models;
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
                LoadNote(value);
            }
        }

        public DiaryEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Diary();
        }
        string Text;

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Diary note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
                if (note.nor)
                {
                    normal.BackgroundColor = Color.Black;
                    normal.TextColor = Color.AntiqueWhite;
                }
                if (note.acn)
                {
                    acne.BackgroundColor = Color.Black;
                    acne.TextColor = Color.AntiqueWhite;
                }
                if (note.ecz)
                {
                    eczema.BackgroundColor = Color.Black;
                    eczema.TextColor = Color.AntiqueWhite;
                }
                //Text = note.Text;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load diary.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Date = DateTime.UtcNow;
            if (normal.BackgroundColor == Color.Black) note.nor = true;
            if (acne.BackgroundColor == Color.Black) note.acn = true;
            if (eczema.BackgroundColor == Color.Black) note.ecz = true;
            //List
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveNoteAsync(note);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnNormalButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).BackgroundColor == Color.Khaki)
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
        async void OnAcneButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).BackgroundColor == Color.Khaki)
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
        async void OnEczemaButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).BackgroundColor == Color.Khaki)
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
    }
}