using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using ivr.Models;
using Xamarin.Forms;

namespace ivr.ViewModels
{
    class MatrixViewModel : BaseViewModel
    {
        private Item item;
        public Item Item
        {
            get => item;
            set
            {
                SetProperty(ref item, value);
            }
        }
        public EisenhowerMatrix Matrix { get; }
        public ObservableCollection<Item> Undistributed { get => Matrix.Undistributed; }
        public Command LoadItemsCommand { get; }
        public Command ItemDragged { get; }
        public Command ItemDropped { get; }
        public MatrixViewModel()
        {
            Matrix = new EisenhowerMatrix();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemDragged = new Command<Item>(OnItemDragged);
            ItemDropped = new Command<string>(OnItemDropped);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Matrix.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Matrix.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnItemDragged(Item item)
        {
            if (item == null)
                return;
            Item = item;
        }

        private async void OnItemDropped(string s)
        {
            if (Item == null || s == null)
                return;
            IsBusy = true;
            try
            {
                Matrix.Remove(Item);
                int eclass = int.Parse(s);
                Item.EClass = eclass;
                Matrix.Add(Item);
                await DataStore.SaveItemAsync(Item);
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

        public void OnAppearing()
        {
            Item = null;
            ExecuteLoadItemsCommand();
        }

        

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            throw new NotImplementedException();
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
