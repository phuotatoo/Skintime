using System;
using System.Linq;
using Skintime.Models;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Skintime.Views
{

    public partial class DiaryPage : ContentPage
    {
        public DiaryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            List<Diary> res = await App.Database.GetDiaryAsync();
           
            collectionView.ItemsSource = res;
            
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage.
            await Shell.Current.GoToAsync(nameof(DiaryEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Diary note = (Diary)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(DiaryEntryPage)}?{nameof(DiaryEntryPage.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}