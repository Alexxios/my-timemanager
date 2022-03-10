using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;
using ivr.ViewModels;
using ivr.Models;

namespace ivr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPopup : Popup<Item>
    {
        NewItemViewModel _viewModel;
        public NewItemPopup(Item parent)
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewItemViewModel();
            if (parent != null) _viewModel.ParentId = parent.Id;
        }

        private void Clicked(object sender, EventArgs e)
        {
            Dismiss(_viewModel.Item);
        }
    }
}