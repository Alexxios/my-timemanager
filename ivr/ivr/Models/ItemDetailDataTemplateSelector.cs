using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ivr.Models
{
    public class ItemDetailDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Item)item).Type == 2 ? ValidTemplate : InvalidTemplate;
        }
    }
}
