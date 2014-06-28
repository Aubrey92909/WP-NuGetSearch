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
        public V2FeedPackageEx CurrentPackage
        {
            get { return _CurrentPackageLocator(this).Value; }
            set { _CurrentPackageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property V2FeedPackageEx CurrentPackage Setup
        protected Property<V2FeedPackageEx> _CurrentPackage = new Property<V2FeedPackageEx> { LocatorFunc = _CurrentPackageLocator };
        static Func<BindableBase, ValueContainer<V2FeedPackageEx>> _CurrentPackageLocator = RegisterContainerLocator<V2FeedPackageEx>("CurrentPackage", model => model.Initialize("CurrentPackage", ref model._CurrentPackage, ref _CurrentPackageLocator, _CurrentPackageDefaultValueFactory));
        static Func<V2FeedPackageEx> _CurrentPackageDefaultValueFactory = () => { return default(V2FeedPackageEx); };
        #endregion

        public PackageDetail_Model()
        {
            if (IsInDesignMode)
            {
                CurrentPackage = new V2FeedPackageEx()
                {
                    Summary = "Microsoft Hope Of Human! A quick brown fox jumped over the lazy dog.",
                    Title = "Microsoft's Humanity Hope",
                    Id = "Microsoft.Humanity.Hope",
                    LastUpdated = DateTime.Now,
                    DownloadCount = 8888,
                    NormalizedVersion = "4.0.1231.30853",
                    Dependencies = "Microsoft.AspNet.WebPages:[3.1.2, 3.2.0):|Microsoft.AspNet.Razor:[3.1.2, 3.2.0):"
                };
            }
        }

        public void InitData(V2FeedPackageEx package)
        {
            CurrentPackage = package;
        }
    }
}

