using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace NuGetSearch.WinRT.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        private string _version;

        public string Version
        {
            get { return GetType().GetTypeInfo().Assembly.GetName().Version.ToString(); }
            set { _version = value; }
        }

        public AboutViewModel()
        {
            
        }
    }
}
