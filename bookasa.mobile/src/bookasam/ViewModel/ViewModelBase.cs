using System;
using System.Net;
using System.Windows;
using System.ComponentModel;

namespace bookasam.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnNotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
