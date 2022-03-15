using ivr.Views;
using ivr.Models;
using ivr.Services;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth;

namespace ivr.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string email;
        string password;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command SkipCommand { get; }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        private bool toggled;

        public bool Toggled
        {
            get { return toggled; }
            set { SetProperty(ref toggled, value); }
        }

        public LoginViewModel()
        {
            var autoSignIn = Preferences.Get("AutoSignIn", false);
            if (autoSignIn)
            {
                Email = Preferences.Get("Email", "");
                Password = Preferences.Get("Password", "");
                Toggled = true;
            }
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            SkipCommand = new Command(OnSkipClicked);
        }

        private async void OnSkipClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(JournalPage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    var auth = await App.AuthProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                    if (Toggled)
                    {
                        Preferences.Set("AutoSignIn", true);
                        Preferences.Set("Email", Email);
                        Preferences.Set("Password", Password);
                    } else
                    {
                        Preferences.Set("AutoSignIn", false);
                        Preferences.Clear("Password");
                        Preferences.Clear("Email");
                    }
                    //App.Auth = auth;
                    App.User = auth.User;
                    App.Client = new FirebaseClient(Constants.FirebasePath,
                        new FirebaseOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                        });
                } catch {
                    await Shell.Current.DisplayAlert("Alert", "An unknown error occured. Try again", "OK");
                    return;
                }
            } else
            {
                await Shell.Current.DisplayAlert("Alert", "Seems you are not connected", "OK");
                return;
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(JournalPage)}");
        }

            private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}
