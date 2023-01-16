using CommunityToolkit.Maui.Alerts;
using GymManagerApp.Models;

namespace GymManagerApp;

public partial class WotkoutPlanCrudPage : ContentPage
{
	public WotkoutPlanCrudPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        var workoutPlan = (WorkoutPlanCrud)BindingContext;
        btnDelete.IsVisible = workoutPlan.Id != 0;
        base.OnAppearing();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var workoutPlan = (WorkoutPlanCrud)BindingContext;
        workoutPlan.SelectedWorkouts = workoutPlan.SelectedWorkouts.Where(x => x.Selected).ToList();
        if ((workoutPlan?.Name ?? "") != "" && workoutPlan.SelectedWorkouts.Any()) {
            if (await App.Database.SaveWotkoutPlanAsync(workoutPlan, workoutPlan.Id == 0))
            {
                await Navigation.PopAsync();
            }
            else
            {

            }
        }
        else
        {
            if ((workoutPlan?.Name ?? "") == "")
            {
                var toast = Toast.Make("Name required", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                await toast.Show();
            }

            if (!workoutPlan.SelectedWorkouts.Any())
            {
                var toast = Toast.Make("Please select a workout", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
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
            var workoutPlan = (WorkoutPlanCrud)BindingContext;
            if (await App.Database.DeleteWorkoutPlanAsync(workoutPlan))
            {
                await Navigation.PopAsync();
            }
            else
            {

            }
        }
    }

    
}