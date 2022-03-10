using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
namespace ivr.Models
{

    public class Grouping : ObservableCollection<Item>
    {
        public string Name { get; private set; }
        public Grouping(string name, IEnumerable<Item> items)
        {
            Name = name;
            foreach (var item in items)
                Items.Add(item);
        }
    }
}
