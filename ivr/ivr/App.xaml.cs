using ivr.Services;
using ivr.Views;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System.IO;

namespace ivr
{
    public partial class App : Application
    {
        private static string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string LogsFile = Path.Combine(localAppDataPath, "logs.txt");
        public static FirebaseAuthProvider AuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.FirebaseApiKey));
        public static FirebaseClient Client { get; set; }
        //public static FirebaseAuthLink Auth { get; set; }
        public static User User { get; set; }
        public static string UserId
        {
            get
            {
                if (User == null) return null;
                return User.LocalId;
            }
        }
        public static DateTime DeviceLatestLog { get; set; }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<ItemDataStore>();
            MainPage = new AppShell();
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
