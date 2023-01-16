using GymManagerApp.Models;

namespace GymManagerApp;

public partial class WorkoutsPage : ContentPage
{
	public WorkoutsPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetWorkoutListsAsync();
    }

    private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new WorkoutCrudPage
            {
                BindingContext = e.SelectedItem as Workout
            });
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkoutCrudPage
        {
            BindingContext = new Workout()
        });
    }
}