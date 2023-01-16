using GymManagerApp.Data;

namespace GymManagerApp
{
    public partial class App : Application
    {
        public static GymManagerDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();
            
            Database = new GymManagerDatabase(new RestService());
            MainPage = new Login();// new AppShell();
        }
    }
}