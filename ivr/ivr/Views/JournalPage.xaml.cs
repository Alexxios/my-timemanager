using ivr.Models;
using ivr.ViewModels;
using ivr.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using Firebase.Database.Query;

namespace ivr.Views
{
    public partial class JournalPage : ContentPage
    {
        JournalViewModel _viewModel;
        public JournalPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new JournalViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            
        }
    }
}