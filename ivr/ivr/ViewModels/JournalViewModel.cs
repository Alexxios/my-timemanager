using ivr.Models;
using ivr.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
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
            //await Task.Delay(1000);
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                /*var ivr = new ItemTask { Dt = DateTime.Parse("10.03.2022"), Title = "ИВР" };
                ivr.Add(new ItemTask { Dt = DateTime.Parse("03.03.2022"), Title = "Новая страница приложения" });
                items.Concat(new List<Item>()
                {
                    new ItemEvent { Dt = DateTime.Parse("08.03.2022"), Title = "8 марта!"},
                    new ItemEvent { Dt = DateTime.Parse("10.03.2022"), Title = "ИВР. Дедлайн"},
                    ivr
                });*/
                Debug.WriteLine(string.Join(" ", items.Select(i => i.Title)));
                var groups = items.GroupBy(i => i.Dt.Date).OrderBy(i => i.Key).Select(g => new Grouping(g.Key.ToShortDateString(), g.OrderBy(i => i.Dt)));
                foreach (var group in groups) Items.Add(group);
                /*switch (CheckData())
                {
                    case DataCode.Either:
                        throw new NotImplementedException();
                        break;
                    case DataCode.OnlyDevice:
                        throw new NotImplementedException();
                        break;
                    default:
                        items = (await App.Client
                            .Child(App.UserID)
                            .Child("Journal")
                            .OnceAsync<Item>())
                            .Select(i => i.Object).ToList();
                        break;
                } */

                /*Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                Items.Sort();*/

                //throw new Exception(string.Join("#", Items.ToList()));
            }
            catch (Exception ex)
            {
                throw ex;
                //Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private DataCode CheckData()
        {
            if (!string.IsNullOrEmpty(App.UserID))
            {
                try
                {
                    DateTime FirebaseLatestLog = App.Client.Child(App.UserID).Child("LatestLog").OnceSingleAsync<DateTime>().Result;
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
            await App.Current.MainPage.Navigation.ShowPopupAsync(new NewItemPopup(null));
            IsBusy = true;
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            //throw new Exception(item.Id.ToString());
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}