using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NuGetSearch.WinRT.Core
{
    public class AppSettings : INotifyPropertyChanged
    {
        public bool IncludePrerelease
        {
            get
            {
                return ReadSettings<bool>("IncludePrerelease");
            }
            set
            {
                SaveSettings("IncludePrerelease", value);
                NotifyPropertyChanged("IncludePrerelease");
            }
        }

        public ApplicationDataContainer LocalSettings { get; set; }

        public AppSettings()
        {
            LocalSettings = ApplicationData.Current.LocalSettings;
        }

        private void SaveSettings(string key, object value)
        {
            LocalSettings.Values[key] = value;
        }

        private T ReadSettings<T>(string key)
        {
            if (LocalSettings.Values.ContainsKey(key))
            {
                return (T)LocalSettings.Values[key];
            }
            return default(T);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
