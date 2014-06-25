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
using NuGetApiClientLib.NuGetService;

namespace NuGetSearch.ViewModels
{
    public class PackageDetail_Model : ViewModelBase<PackageDetail_Model>
    {
        public V2FeedPackage CurrentPackage
        {
            get { return _CurrentPackageLocator(this).Value; }
            set { _CurrentPackageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property V2FeedPackage CurrentPackage Setup
        protected Property<V2FeedPackage> _CurrentPackage = new Property<V2FeedPackage> { LocatorFunc = _CurrentPackageLocator };
        static Func<BindableBase, ValueContainer<V2FeedPackage>> _CurrentPackageLocator = RegisterContainerLocator<V2FeedPackage>("CurrentPackage", model => model.Initialize("CurrentPackage", ref model._CurrentPackage, ref _CurrentPackageLocator, _CurrentPackageDefaultValueFactory));
        static Func<V2FeedPackage> _CurrentPackageDefaultValueFactory = () => { return default(V2FeedPackage); };
        #endregion

        public PackageDetail_Model()
        {
            if (IsInDesignMode)
            {
                CurrentPackage = new V2FeedPackage()
                {
                    Summary = "Microsoft Hope Of Human! A quick brown fox jumped over the lazy dog.",
                    Title = "Microsoft's Humanity Hope",
                    Id = "Microsoft.Humanity.Hope",
                    LastUpdated = DateTime.Now,
                    DownloadCount = 8888,
                    Version = "4.0.1231.30853"
                };
            }
        }

        public void InitData(V2FeedPackage package)
        {
            CurrentPackage = package;
        }
    }
}

