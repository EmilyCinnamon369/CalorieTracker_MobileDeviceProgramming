using CalorieTracker_MobileDeviceProgramming.Models;
using System.Collections.ObjectModel;

namespace CalorieTracker_MobileDeviceProgramming;

public partial class AddActivityPage : ContentPage
{
    private ObservableCollection<ActivityEntry> _activityEntries;

    public AddActivityPage(ObservableCollection<ActivityEntry> activityEntries)
    {
        InitializeComponent();
        _activityEntries = activityEntries;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(CaloriesEntry.Text, out int caloriesBurned))
        {
            await DisplayAlert("Invalid Input", "Please enter a valid number for calories burned", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ActivityName.Text))
        {
            await DisplayAlert("Error", "Please enter an activity name", "OK");
            return;
        }

        if (caloriesBurned < 0 || caloriesBurned > 3000)
        {
            await DisplayAlert("Invalid Input", "Please enter a valid calorie amount (0-3000)", "OK");
            return;
        }

        var entry = new ActivityEntry
        {
            ActivityDate = ActivityDate.Date,
            ActivityName = ActivityName.Text,
            CaloriesBurned = caloriesBurned,
            ActivityDescription = ContentEditor.Text
        };

        _activityEntries.Add(entry);

        if (Navigation.NavigationStack.FirstOrDefault() is MainPage mainPage)
        {
            mainPage.SaveActivities();
            //mainPage.UpdateSummary();
        }

        await Navigation.PopAsync();
    }
}