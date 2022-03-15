using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using Firebase.Storage;
using ivr.Views;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ivr.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region
        private string email;

        public string Email
        {
            get {
                if (string.IsNullOrEmpty(email)) return "Not authorized";
                return email;
            }
            private set { SetProperty(ref email, value); }
        }
        private bool toggled;

        public bool Toggled
        {
            get { return toggled; }
            set { SetProperty(ref toggled, value); }
        }

        private TimeSpan time;

        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { SetProperty(ref imageSource, value); }
        }
        public Command SaveCommand { get; }
        #endregion


        public SettingsViewModel()
        {
            Time = TimeSpan.Zero;
            Toggled = false;
            if (!string.IsNullOrEmpty(App.UserId))
            {
                Toggled = Preferences.Get("SyncEnabled", false);
                Email = App.User.Email;
            }
            ImageSource = ImageSource.FromFile("user_logo.png");
            SaveCommand = new Command(OnSave);
            /*if (Connectivity.NetworkAccess == NetworkAccess.Internet && !string.IsNullOrEmpty(App.UserId))
            {   
                Email = App.User.Email;
                var task = new FirebaseStorage("time-84b46.appspot.com",
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(App.Auth.FirebaseToken),
                        ThrowOnCancel = true,
                    })
                    .Child("avatar")
                    .Child(App.UserId)
                    .GetDownloadUrlAsync();
                Debug.WriteLine(task.Result);
                ImageSource = ImageSource.FromUri(new Uri(task.Result));
            } */

        }

        private void OnSave(object obj)
        {
            Preferences.Set("SyncEnabled", Toggled);
            Preferences.Set("DailyNotificationTime", Time.TotalSeconds);
        }
    }
}
