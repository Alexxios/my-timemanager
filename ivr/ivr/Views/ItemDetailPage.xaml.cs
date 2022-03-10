using ivr.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ivr.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}