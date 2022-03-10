using ivr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using ivr.Views;
using ivr.Models;
using System.Threading.Tasks;

namespace ivr.ViewModels
{
    class NewItemViewModel : BaseViewModel
    {
        #region
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private int parentId;

        public int ParentId
        {
            get { return parentId; }
            set { SetProperty(ref parentId, value); }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set {
                SetProperty(ref type, value);
                IsTask = type == 2;
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /*private bool isFinished;

        public bool IsFinished
        {
            get { return isFinished; }
            set { SetProperty(ref isFinished, value); }
        }*/

        private DateTime dateTime;

        public DateTime Date
        {
            get { return dateTime.Date; }
            set { SetProperty(ref dateTime, value.Date + dateTime.TimeOfDay); }
        }
        public TimeSpan Time
        {
            get { return dateTime.TimeOfDay; }
            set { SetProperty(ref dateTime, dateTime.Date + value); }
        }

        private string data;

        public string Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        private bool isTask;

        public bool IsTask
        {
            get { return isTask; }
            set { SetProperty(ref isTask, value); }
        }

        public Item Item
        {
            get
            {
                var item = new Item()
                {
                    Id = Id,
                    ParentId = ParentId,
                    Type = Type,
                    Title = Title,
                    IsFinished = false,
                    Data = Data,
                    Dt = Date + Time,
                    EClass = 0
                };
                return item;
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command AddCommand { get; }
        #endregion
        public NewItemViewModel()
        {
            Id = 0;
            Type = 0;
            Date = DateTime.Now.Date;
            Time = TimeSpan.FromDays(1) - TimeSpan.FromSeconds(1);
            IsTask = false;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            AddCommand = new Command(OnAdd);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Title);
        }

        private async void OnCancel()
        {
            if (Id != 0)
            {
                var list = await DataStore.GetItemsAsync();
                var onErase = GetItemTree(ref list, Id);
                foreach (var id in onErase)
                {
                    await DataStore.DeleteItemAsync(id);
                }
            }
            // This will pop the current page off the navigation stack
        }

        private async void OnSave()
        {

            Id = await DataStore.SaveItemAsync(Item);
            // This will pop the current page off the navigation stack
        }

        private async void OnAdd()
        {
            Id = await DataStore.SaveItemAsync(Item);
            await App.Current.MainPage.Navigation.ShowPopupAsync(new NewItemPopup(Item));
        }

        
    }
}
