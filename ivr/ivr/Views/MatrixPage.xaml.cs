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
    public partial class MatrixPage : ContentPage
    {
        MatrixViewModel _viewModel;
        public MatrixPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MatrixViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}