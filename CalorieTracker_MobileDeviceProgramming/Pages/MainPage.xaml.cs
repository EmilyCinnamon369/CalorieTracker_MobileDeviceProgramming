using CalorieTracker_MobileDeviceProgramming.Pages;

namespace CalorieTracker_MobileDeviceProgramming
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddFoodClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFoodPage());
        }

        private async void OnAddActivityClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddActivityPage());
        }

        private async void OnDaySelected(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DailyLogPage());
        }


    }
}
