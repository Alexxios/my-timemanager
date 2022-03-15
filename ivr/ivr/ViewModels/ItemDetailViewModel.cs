using Firebase.Database.Query;
using ivr.Models;
using System;
using System.Linq;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.ObjectModel;

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

        private ObservableCollection<Item> subitems;
        public ObservableCollection<Item> Subitems
        {
            get { return subitems; }
            private set { SetProperty(ref subitems, value); }
        }

        #endregion
        public Command DeleteCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command FinishCommand { get; }
        public Command LoadItemsCommand { get; }
        public ItemDetailViewModel()
        {
            Subitems = new ObservableCollection<Item>();
            DeleteCommand = new Command(OnDelete);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            FinishCommand = new Command(OnFinish);
        }

        private async void OnFinish(object obj)
        {
            var item = await DataStore.GetItemAsync(ItemId);
            item.IsFinished = true;
            await DataStore.SaveItemAsync(item);

            if (Connectivity.NetworkAccess == NetworkAccess.Internet && !string.IsNullOrEmpty(App.UserId)
                && Preferences.Get("SyncEnabled", false))
            {
                await App.Client
                    .Child(App.UserId)
                    .Child("journal")
                    .Child(ItemId.ToString())
                    .PutAsync(item);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDelete()
        {
            var list = await DataStore.GetItemsAsync();
            var onErase = GetItemTree(ref list, ItemId);
            foreach (var id in onErase)
            {
                await DataStore.DeleteItemAsync(id);
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet && !string.IsNullOrEmpty(App.UserId)
                && Preferences.Get("SyncEnabled", false))
            {
                foreach (var id in onErase)
                {
                    await App.Client
                       .Child(App.UserId)
                       .Child("journal")
                       .Child(id.ToString())
                       .DeleteAsync();
                }

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

            if (Connectivity.NetworkAccess == NetworkAccess.Internet && !string.IsNullOrEmpty(App.UserId)
                && Preferences.Get("SyncEnabled", false))
            {
                await App.Client
                    .Child(App.UserId)
                    .Child("journal")
                    .Child(ItemId.ToString())
                    .PutAsync(item);
            }

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
                var items = (await DataStore.GetItemsAsync(true)).Where(i => i.ParentId == ItemId);
                //throw new Exception(item.ParentId.ToString());
                Title = item.Title;
                Data = item.Data;
                Date = item.Dt.Date;
                Time = item.Dt.TimeOfDay;
                EClass = item.EClass;
                foreach (var it in items)
                {
                    Subitems.Add(it);
                }
            } catch (Exception ex)
            {
                //throw ex;
                Debug.WriteLine(ex.Message);
            }
        }

    }
}