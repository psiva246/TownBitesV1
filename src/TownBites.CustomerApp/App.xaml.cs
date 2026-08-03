using TownBites.CustomerApp.Views;

namespace TownBites.CustomerApp
{
    public partial class App : Application
    {
        //public App(LoginPage loginPage)
        //{
        //    InitializeComponent();

        //    MainPage = new NavigationPage(loginPage);
        //}
        public App(AppShell appShell)
        {
            InitializeComponent();

            MainPage = appShell;
        }
    }
}

