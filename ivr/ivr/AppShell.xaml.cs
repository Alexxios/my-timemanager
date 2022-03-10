using ivr.ViewModels;
using ivr.Views;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace ivr
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
