using ivr.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace ivr.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region
        private int itemId;
        public int ItemId { 
            get => itemId; 
            set
            {
                SetProperty(ref itemId, value);
                LoadItemId(value);
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string data;
                        
        public string Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        private int eclass;

        public int EClass
        {
            get { return eclass; }
            set { SetProperty(ref eclass, value); }
        }

        private DateTime dt;

        public DateTime Date
        {
            get { return dt.Date; }
            set { SetProperty(ref dt, value + dt.TimeOfDay); }
        }

        public TimeSpan Time
        {
            get { return dt.TimeOfDay; }
            set { SetProperty(ref dt, dt.Date + value); }
        }


        #endregion
        public Command DeleteCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command LoadItemsCommand { get; }
        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(OnDelete);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }


        private async void OnDelete()
        {
            var list = await DataStore.GetItemsAsync();
            var onErase = GetItemTree(ref list, ItemId);
            //throw new Exception(string.Join(" ", onErase));
            foreach (var id in onErase)
            {
                await DataStore.DeleteItemAsync(id);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var item = await DataStore.GetItemAsync(ItemId);
            item.Title = Title;
            item.Data = Data;
            item.Dt = Date + Time;
            await DataStore.SaveItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void LoadItemId(int id) 
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                //throw new Exception(item.ParentId.ToString());
                Title = item.Title;
                Data = item.Data;
                Date = item.Dt.Date;
                Time = item.Dt.TimeOfDay;
                EClass = item.EClass;
            } catch (Exception ex)
            {
                //throw ex;
                Debug.WriteLine(ex.Message);
            }
        }

    }
}