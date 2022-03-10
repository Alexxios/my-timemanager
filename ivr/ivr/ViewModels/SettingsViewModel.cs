using System;
using System.Collections.Generic;
using System.Text;

namespace ivr.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string email;

        public string Email
        {
            get { return email; }
        }
        private bool toggled;

        public bool Toggled
        {
            get { return toggled; }
            set { SetProperty(ref toggled, value); }
        }

        private TimeSpan time;

        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }


        public SettingsViewModel()
        {

        }


    }
}
