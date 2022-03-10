using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ivr.Models
{
    public class EisenhowerMatrix
    {
        private ObservableCollection<Item> iu, inu, niu, ninu, undistributed;
        public ObservableCollection<Item> IU { get { return iu; } }
        public ObservableCollection<Item> INU { get { return inu; } }
        public ObservableCollection<Item> NIU { get { return niu; } }
        public ObservableCollection<Item> NINU { get { return ninu; } }
        public ObservableCollection<Item> Undistributed { get { return undistributed; } }
        public EisenhowerMatrix()
        {
            iu = new ObservableCollection<Item>();
            inu = new ObservableCollection<Item>();
            niu = new ObservableCollection<Item>();
            ninu = new ObservableCollection<Item>();
            undistributed = new ObservableCollection<Item>();
        }

        public void Add(Item item)
        {
            if (item.Type != 2) return;
            switch (item.EClass)
            {
                case 1:
                    iu.Add(item);
                    break;
                case 2:
                    inu.Add(item);
                    break;
                case 3:
                    niu.Add(item);
                    break;
                case 4:
                    ninu.Add(item);
                    break;
                default:
                    undistributed.Add(item);
                    break;
            }
        }
        public void Clear()
        {
            iu.Clear();
            inu.Clear();
            niu.Clear();
            ninu.Clear();
            undistributed.Clear();
        }
        public void Remove(Item item)
        {
            if (item.Type != 2) return;
            switch (item.EClass)
            {
                case 1:
                    iu.Remove(item);
                    break;
                case 2:
                    inu.Remove(item);
                    break;
                case 3:
                    niu.Remove(item);
                    break;
                case 4:
                    ninu.Remove(item);
                    break;
                default:
                    undistributed.Remove(item);
                    break;
            }
        }
    }
}
