using ivr.Models;
using ivr.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Firebase.Auth;

namespace ivr.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected List<int> GetItemTree(ref IEnumerable<Item> list, int id)
        {
            var onErase = new List<int>();
            onErase.Add(id);
            foreach (var item in list.Where(i => i.ParentId == id))
            {
                onErase.AddRange(GetItemTree(ref list, item.Id));
            }
            return onErase;
        }

        protected List<Item> Merge(ref List<Item> a, ref List<Item> b)
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
                }
                else if (a[i].Id < b[j].Id)
                {
                    merged.Add(a[i]);
                    i++;
                }
                else
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
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
