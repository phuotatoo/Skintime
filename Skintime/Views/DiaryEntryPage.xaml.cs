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