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
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        public LoginViewModel()
        {
            if (!string.IsNullOrEmpty(App.UserID)) Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var auth = await App.AuthProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                    App.UserID = auth.User.LocalId;
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
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}
