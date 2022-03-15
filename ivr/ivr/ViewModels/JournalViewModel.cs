﻿using ivr.Models;
using ivr.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;
using Firebase.Database.Query;
using Xamarin.CommunityToolkit.Extensions;

namespace ivr.ViewModels
{

    public class JournalViewModel : BaseViewModel
    {
        private enum DataCode
        {
            OnlyDevice, OnlyFirebase, Either, Both
        }
        private Item _selectedItem;
        public ObservableCollection<Grouping> Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public JournalViewModel()
        {
            Items = new ObservableCollection<Grouping>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = (await DataStore.GetItemsAsync(true))
                    .OrderBy(i => i.Id)
                    .ToList();
                var syncEnabled = Preferences.Get("SyncEnabled", false);
                if (Connectivity.NetworkAccess == NetworkAccess.Internet && !string.IsNullOrEmpty(App.UserId)
                    && syncEnabled)
                {
                    var fireItems = (await App.Client
                        .Child(App.UserId)
                        .Child("journal")
                        .OnceAsync<Item>())
                        .Select(i => i.Object)
                        .OrderBy(i => i.Id)
                        .ToList();
                    if (!items.SequenceEqual(fireItems))
                    {
                        bool isSubset = !(items.Except(fireItems).Any());
                        bool isSuperset = !(fireItems.Except(items).Any());

                        //Debug.WriteLine($"{isSubset} {isSuperset}");
                        if (isSubset || isSuperset)
                        {
                            items = Merge(ref items, ref fireItems);
                        }
                        else
                        {
                            var result = await Shell.Current.ShowPopupAsync(new DatabaseChoosePopup());
                            if (result.Equals(0))
                            {
                                items = Merge(ref fireItems, ref items);
                            }
                            else
                            {
                                items = Merge(ref items, ref fireItems);
                            }
                        }
                        foreach (var item in items)
                        {
                            await DataStore.SaveItemAsync(item);
                            await App.Client
                                .Child(App.UserId)
                                .Child("journal")
                                .Child(item.Id.ToString())
                                .PutAsync(item);
                        }
                    }
                }

                var groups = items
                    .Where(i => i.IsFinished == false && i.Dt >= DateTime.Now)
                    .GroupBy(i => i.Dt.Date)
                    .OrderBy(i => i.Key)
                    .Select(g => new Grouping(g.Key.ToShortDateString(), g.OrderBy(i => i.Type).OrderBy(i => i.Dt)));
                foreach (var group in groups) Items.Add(group);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private DataCode CheckData()
        {
            if (!string.IsNullOrEmpty(App.UserId))
            {
                try
                {
                    DateTime FirebaseLatestLog = App.Client.Child(App.UserId).Child("LatestLog").OnceSingleAsync<DateTime>().Result;
                    if (FirebaseLatestLog != null)
                    {
                        if (App.DeviceLatestLog == DateTime.MinValue)
                        {
                            return DataCode.OnlyFirebase;
                        }
                        if (App.DeviceLatestLog - TimeSpan.FromSeconds(300) < FirebaseLatestLog
                                && FirebaseLatestLog < App.DeviceLatestLog + TimeSpan.FromSeconds(300))
                        {
                            return DataCode.Both;
                        }
                        return DataCode.Either;
                    }
                } catch { }
            }
            return DataCode.OnlyDevice;
        } 

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set 
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
            IsBusy = true;
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            //throw new Exception(item.Id.ToString());
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        /*private List<Item> Merge(ref List<Item> a, ref List<Item> b)
        {
            int i = 0, j = 0;
            var merged = new List<Item>();
            while (i < a.Count && j < b.Count)
            {
                if (a[i].Id == b[j].Id)
                {
                    merged.Add(a[i]);
                    i++; 
                    j++;
                } else if (a[i].Id < b[j].Id)
                {
                    merged.Add(a[i]);
                    i++;
                } else
                {
                    merged.Add(b[j]);
                    j++;
                }
            }
            while (i < a.Count)
            {
                merged.Add(a[i]);
                i++;
            }
            while (j < b.Count)
            {
                merged.Add(b[j]);
                j++;
            }
            return merged;
        }*/
    }
}