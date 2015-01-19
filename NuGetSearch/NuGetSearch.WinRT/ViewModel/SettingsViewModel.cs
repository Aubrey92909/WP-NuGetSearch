using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using NuGetSearch.WinRT.Core;

namespace NuGetSearch.WinRT.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private AppSettings _appSettings;

        public AppSettings AppSettings
        {
            get { return _appSettings; }
            set { _appSettings = value; RaisePropertyChanged(); }
        }
        

        public SettingsViewModel()
        {
            AppSettings = new AppSettings();
        }
    }
}
