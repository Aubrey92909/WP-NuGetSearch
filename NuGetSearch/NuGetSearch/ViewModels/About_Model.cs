using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NuGetSearch.ViewModels
{
    public class About_Model : ViewModelBase<About_Model>
    {
        public string Version
        {
            get
            {
                return GetType().Assembly.GetName().Version.ToString();
                //return _VersionLocator(this).Value;
            }
            set { _VersionLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Version Setup
        protected Property<string> _Version = new Property<string> { LocatorFunc = _VersionLocator };
        static Func<BindableBase, ValueContainer<string>> _VersionLocator = RegisterContainerLocator<string>("Version", model => model.Initialize("Version", ref model._Version, ref _VersionLocator, _VersionDefaultValueFactory));
        static Func<string> _VersionDefaultValueFactory = () => { return default(string); };
        #endregion

    }
}

