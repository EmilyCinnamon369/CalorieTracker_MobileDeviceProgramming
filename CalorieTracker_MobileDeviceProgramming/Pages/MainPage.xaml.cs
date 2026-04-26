using CalorieTracker_MobileDeviceProgramming.Pages;
using System.Collections.ObjectModel;
using CalorieTracker_MobileDeviceProgramming.Models;
using System.Text.Json;

namespace CalorieTracker_MobileDeviceProgramming
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<MealEntry> _mealEntries;
        private ObservableCollection<ActivityEntry> _activityEntries;

        private readonly string _mealsFileName = Path.Combine(FileSystem.AppDataDirectory, "meal_entries.json");
        private readonly string _activitiesFileName = Path.Combine(FileSystem.AppDataDirectory, "activity_entries.json");

        public void AddMealEntry(MealEntry entry)
        {
            _mealEntries.Add(entry);
            SaveMeals();
            UpdateSummary();
        }

        public void AddActivityEntry(ActivityEntry entry)
        {
            _activityEntries.Add(entry);
            SaveActivities();
            UpdateSummary();
        }

        public MainPage()
        {
            InitializeComponent();
            _mealEntries = new ObservableCollection<MealEntry>();
            _activityEntries = new ObservableCollection<ActivityEntry>();

            MealsListView.ItemsSource = _mealEntries;
            ActivitiesListView.ItemsSource = _activityEntries;

            LoadSavedEntries();
            UpdateSummary();
        }

        private async void OnAddFoodClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFoodPage(_mealEntries));
        }

        private async void OnAddActivityClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddActivityPage(_activityEntries));
        }

        private void LoadSavedEntries()
        {
            try
            {
                if (File.Exists(_mealsFileName))
                {
                    string json = File.ReadAllText(_mealsFileName);
                    var entries = JsonSerializer.Deserialize<List<MealEntry>>(json);
                    if (entries != null)
                    {
                        foreach (var entry in entries)
                        {
                            _mealEntries.Add(entry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading meals: {ex.Message}");
            }

            try
            {
                if (File.Exists(_activitiesFileName))
                {
                    string json = File.ReadAllText(_activitiesFileName);
                    var entries = JsonSerializer.Deserialize<List<ActivityEntry>>(json);
                    if (entries != null)
                    {
                        foreach (var entry in entries)
                        {
                            _activityEntries.Add(entry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading activities: {ex.Message}");
            }
        }

        public void SaveMeals()
        {
            try
            {
                string json = JsonSerializer.Serialize(_mealEntries.ToList());
                File.WriteAllText(_mealsFileName, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving meals: {ex.Message}");
            }
        }

        public void SaveActivities()
        {
            try
            {
                string json = JsonSerializer.Serialize(_activityEntries.ToList());
                File.WriteAllText(_activitiesFileName, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving activities: {ex.Message}");
            }
        }

        public void UpdateSummary()
        {
            int totalCaloriesIn = _mealEntries.Sum(m => m.MealCalories);
            int totalCaloriesOut = _activityEntries.Sum(a => a.CaloriesBurned);
            int netCalories = totalCaloriesIn - totalCaloriesOut;

            TotalCaloriesInLabel.Text = totalCaloriesIn.ToString();
            TotalCaloriesOutLabel.Text = totalCaloriesOut.ToString();
            NetCaloriesLabel.Text = netCalories.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateSummary();
            System.Diagnostics.Debug.WriteLine("OnAppearing called - Summary updated");
        }
    }
}