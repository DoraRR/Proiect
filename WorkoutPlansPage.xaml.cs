using GymManagerApp.Models;

namespace GymManagerApp;

public partial class WorkoutPlansPage : ContentPage
{
    public WorkoutPlansPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetWorkoutPlanListsAsync();
    }

    private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var workoutPlan = e.SelectedItem as WorkoutPlan;
            var wotkoutPlanCrud = new WorkoutPlanCrud
            {
                Id = workoutPlan.Id,
                Name = workoutPlan.Name,
                Trainer = workoutPlan.Trainer,
                TrainerId = workoutPlan.TrainerId
            };
            await PopulateSelectedWorkouts(wotkoutPlanCrud, workoutPlan);
            await Navigation.PushAsync(new WotkoutPlanCrudPage
            {
                BindingContext = wotkoutPlanCrud
            });
        }


    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var wotkoutPlanCrud = new WorkoutPlanCrud();
        await PopulateSelectedWorkouts(wotkoutPlanCrud, new WorkoutPlan());
        await Navigation.PushAsync(new WotkoutPlanCrudPage
        {
            BindingContext = wotkoutPlanCrud
        });
    }

    private async Task PopulateSelectedWorkouts(WorkoutPlanCrud workoutPlanCrud, WorkoutPlan workoutPlan)
    {
        var allWorkouts = await App.Database.GetWorkoutListsAsync();
        var selectedWorkouts = new HashSet<int>(workoutPlan.Workouts.Select(c => c.Id));
        workoutPlanCrud.SelectedWorkouts = new List<WorkoutCrud>();
        foreach (var workout in allWorkouts)
        {
            workoutPlanCrud.SelectedWorkouts.Add(new WorkoutCrud
            {
                Id = workout.Id,
                Name = workout.Name,
                Day = workoutPlan.Workouts.FirstOrDefault(x => x.Id == workout.Id)?.Day ?? 0,
                Selected = selectedWorkouts.Contains(workout.Id)
            });
        }
    }

}