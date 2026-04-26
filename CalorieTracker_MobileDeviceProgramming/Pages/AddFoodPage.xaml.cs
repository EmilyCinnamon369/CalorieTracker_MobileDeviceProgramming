using CalorieTracker_MobileDeviceProgramming.Models;
using System.Collections.ObjectModel;

namespace CalorieTracker_MobileDeviceProgramming;

public partial class AddFoodPage : ContentPage
{
    private ObservableCollection<MealEntry> _mealEntries;
    public AddFoodPage(ObservableCollection<MealEntry> mealEntries)
    {
        InitializeComponent();
        _mealEntries = mealEntries;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(MealCalories.Text, out int calories))
        {
            await DisplayAlert("Invalid Input", "Please enter a valid number for calories", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(MealName.Text))
        {
            await DisplayAlert("Error", "Please enter a meal name", "OK");
            return;
        }

        var entry = new MealEntry
        {
            MealDate = MealDate.Date,
            MealName = MealName.Text,
            MealCalories = calories,
            MealDescription = ContentEditor.Text
        };

        _mealEntries.Add(entry);

        if (Navigation.NavigationStack.FirstOrDefault() is MainPage mainPage)
        {
            mainPage.SaveMeals();
            //mainPage.UpdateSummary();
        }

        await Navigation.PopAsync();
    }
}