using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ivr.ViewModels;
namespace ivr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissedItemsPage : ContentPage
    {
        MissedItemsViewModel _viewModel;
        public MissedItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MissedItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}