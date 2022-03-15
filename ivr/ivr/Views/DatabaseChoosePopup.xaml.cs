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
    public partial class DatabaseChoosePopup : Popup
    {
        public DatabaseChoosePopup()
        {
            InitializeComponent();
        }

        private void Clicked(object sender, EventArgs e)
        {
            Dismiss(0);
        }

        private void Clicked1(object sender, EventArgs e)
        {
            Dismiss(1);
        }
    }
}