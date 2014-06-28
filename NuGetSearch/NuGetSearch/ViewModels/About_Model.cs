using MVVMSidekick.ViewModels;
using System;

namespace NuGetSearch.ViewModels
{
    public class About_Model : ViewModelBase<About_Model>
    {
        public string Version
        {
            get { return Utils.GetAppVersion(); }
            set { _VersionLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Version Setup
        protected Property<string> _Version = new Property<string> { LocatorFunc = _VersionLocator };
        static Func<BindableBase, ValueContainer<string>> _VersionLocator = RegisterContainerLocator<string>("Version", model => model.Initialize("Version", ref model._Version, ref _VersionLocator, _VersionDefaultValueFactory));
        static Func<string> _VersionDefaultValueFactory = () => { return default(string); };
        #endregion
    }
}

