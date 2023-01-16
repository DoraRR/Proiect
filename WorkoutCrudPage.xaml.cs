using CommunityToolkit.Maui.Alerts;
using GymManagerApp.Models;

namespace GymManagerApp;

public partial class WorkoutCrudPage : ContentPage
{
    public WorkoutCrudPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var workout = (Workout)BindingContext;
        btnDelete.IsVisible = workout.Id != 0;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var workout = (Workout)BindingContext;
        if ((workout?.Name ?? "") != "" && (workout?.Description ?? "") != "")
        {
            if (await App.Database.SaveWotkoutAsync(workout, workout.Id == 0))
            {
                await Navigation.PopAsync();
            }
            else
            {

            }
        }
        else
        {
            if((workout?.Name ?? "") == "")
            {
                var toast = Toast.Make("Name required", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                await toast.Show();
            }
               
            if((workout?.Description ?? "") == "")
            {
                var toast = Toast.Make("Description required", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                await toast.Show();
            }
        }
    }
    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Attention", "Are you sure?", "Yes", "No");
        if (answer)
        {
            var workout = (Workout)BindingContext;
            if (await App.Database.DeleteWorkoutAsync(workout))
            {
                await Navigation.PopAsync();
            }
            else
            {

            }
        }
    }
}