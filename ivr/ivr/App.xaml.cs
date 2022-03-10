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
        //public static FirebaseHelper helper;
        private static string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string LogsFile = Path.Combine(localAppDataPath, "logs.txt");
        public static string EventItemsFile = Path.Combine(localAppDataPath, "eventItems.txt");
        public static string TaskItemsFile = Path.Combine(localAppDataPath, "taskItems.txt");
        public static FirebaseAuthProvider AuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.FirebaseApiKey));
        public static FirebaseClient Client { get; set; }
        public static string UserID { get; set; }
        public static DateTime DeviceLatestLog { get; set; }

        public App()
        {
            InitializeComponent();
            /*try
            {
                App.DeviceLatestLog = DateTime.Parse(File.ReadAllText(App.LogsFile));
            }
            catch
            {
                App.DeviceLatestLog = DateTime.MinValue;
            }*/
            
            DependencyService.Register<ItemDataStore>();
            MainPage = new AppShell();

            /*try
            {
                File.WriteAllText(App.LogsFile, DateTime.Now.ToString());
                List<string> ev = new List<string>();
                List<string> ts = new List<string>();
                foreach (var item in _viewModel.DataStore.GetItemsAsync().Result)
                {
                    if (item.GetType() == typeof(ItemEvent))
                    {
                        ev.Add(JsonSerializer.Serialize((ItemEvent)item));
                    }
                    else
                    {
                        ts.Add(JsonSerializer.Serialize((ItemTask)item));
                    }
                }
                File.WriteAllLines(App.EventItemsFile, ev);
                File.WriteAllLines(App.TaskItemsFile, ts);

                if (!string.IsNullOrEmpty(App.UserID))
                {
                    App.Client.Child(App.UserID).Child("LatestLog").PutAsync(DateTime.Now);
                }
            }
            catch { }*/
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
