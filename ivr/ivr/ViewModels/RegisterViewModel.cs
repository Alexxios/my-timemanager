using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Auth;
using ivr.Services;
using ivr.Views;

namespace ivr.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        string email;
        string password, repassword;
        public Command RegisterCommand { get; }
        public string Email { get => email; set => SetProperty(ref email, value.Trim()); }
        public string Password { get => password; set => SetProperty(ref password, value.Trim()); }
        public string Repassword { get => repassword; set => SetProperty(ref repassword, value.Trim()); }

        public RegisterViewModel()
        {
            if (!string.IsNullOrEmpty(App.UserId)) Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

            RegisterCommand = new Command(OnRegisterClicked);
        }

        private bool IsValid()
        {
            if (Email == null || Password == null) return false;
            if (Email.Length == 0 || Password.Length < 6) return false;
            if (Password != Repassword) return false;
            if (Email.EndsWith(".")) return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(Email);
                return true;
            }
            catch { }
            return false;
        }

        private async void OnRegisterClicked()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Alert", "Seems you are not connected", "OK");
                return;
            }
            if (!IsValid())
            {
                await Shell.Current.DisplayAlert("Alert", "Incorrect input", "OK");
                return;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(Email);
                var auth = await App.AuthProvider.CreateUserWithEmailAndPasswordAsync(addr.Address, Password);
                App.User = auth.User;
                App.Client = new FirebaseClient(Constants.FirebasePath,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                    });
            }
            catch
            {
                await Shell.Current.DisplayAlert("Alert", "An unknown error occured. Try again", "OK");
                return;
            }
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

    }
}
