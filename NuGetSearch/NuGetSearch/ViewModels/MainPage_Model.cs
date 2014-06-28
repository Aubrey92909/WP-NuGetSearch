using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.Utilities;
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
using NuGetApiClientLib;
using NuGetApiClientLib.NuGetService;

namespace NuGetSearch.ViewModels
{
    public class MainPage_Model : NuGetSearchVMBase<MainPage_Model>
    {
        public Task InitDataTask { get; set; }

        public NuGetOrgSearcher NuGetSearcher { get; private set; }

        public bool IsMsDotNetDataInitialized
        {
            get { return _IsMsDotNetDataInitializedLocator(this).Value; }
            set { _IsMsDotNetDataInitializedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsMsDotNetDataInitialized Setup
        protected Property<bool> _IsMsDotNetDataInitialized = new Property<bool> { LocatorFunc = _IsMsDotNetDataInitializedLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsMsDotNetDataInitializedLocator = RegisterContainerLocator<bool>("IsMsDotNetDataInitialized", model => model.Initialize("IsMsDotNetDataInitialized", ref model._IsMsDotNetDataInitialized, ref _IsMsDotNetDataInitializedLocator, _IsMsDotNetDataInitializedDefaultValueFactory));
        static Func<bool> _IsMsDotNetDataInitializedDefaultValueFactory = () => { return default(bool); };
        #endregion

        public ObservableCollection<V2FeedPackageEx> MostPopularPackages
        {
            get { return _MostPopularPackagesLocator(this).Value; }
            set { _MostPopularPackagesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<V2FeedPackageEx> MostPopularPackages Setup
        protected Property<ObservableCollection<V2FeedPackageEx>> _MostPopularPackages = new Property<ObservableCollection<V2FeedPackageEx>> { LocatorFunc = _MostPopularPackagesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<V2FeedPackageEx>>> _MostPopularPackagesLocator = RegisterContainerLocator<ObservableCollection<V2FeedPackageEx>>("MostPopularPackages", model => model.Initialize("MostPopularPackages", ref model._MostPopularPackages, ref _MostPopularPackagesLocator, _MostPopularPackagesDefaultValueFactory));
        static Func<ObservableCollection<V2FeedPackageEx>> _MostPopularPackagesDefaultValueFactory = () => { return default(ObservableCollection<V2FeedPackageEx>); };
        #endregion

        public ObservableCollection<V2FeedPackageEx> MicrosoftDotNetPackages
        {
            get { return _MicrosoftDotNetPackagesLocator(this).Value; }
            set { _MicrosoftDotNetPackagesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<V2FeedPackageEx> MicrosoftDotNetPackages Setup
        protected Property<ObservableCollection<V2FeedPackageEx>> _MicrosoftDotNetPackages = new Property<ObservableCollection<V2FeedPackageEx>> { LocatorFunc = _MicrosoftDotNetPackagesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<V2FeedPackageEx>>> _MicrosoftDotNetPackagesLocator = RegisterContainerLocator<ObservableCollection<V2FeedPackageEx>>("MicrosoftDotNetPackages", model => model.Initialize("MicrosoftDotNetPackages", ref model._MicrosoftDotNetPackages, ref _MicrosoftDotNetPackagesLocator, _MicrosoftDotNetPackagesDefaultValueFactory));
        static Func<ObservableCollection<V2FeedPackageEx>> _MicrosoftDotNetPackagesDefaultValueFactory = () => { return default(ObservableCollection<V2FeedPackageEx>); };
        #endregion

        public MainPage_Model()
        {
            MostPopularPackages = new ObservableCollection<V2FeedPackageEx>();
            MicrosoftDotNetPackages = new ObservableCollection<V2FeedPackageEx>();

            if (IsInDesignMode)
            {
                MostPopularPackages.Add(new V2FeedPackageEx()
                {
                    Summary = "Microsoft Hope Of Human! A quick brown fox jumped over the lazy dog.",
                    Title = "Microsoft.Humanity.Hope",
                    LastUpdated = DateTime.Now,
                    DownloadCount = 8888
                });

                MostPopularPackages.Add(new V2FeedPackageEx()
                {
                    Summary = "Fuck the fucking fuckers.",
                    Title = "Linustd.Is.Adobe",
                    LastUpdated = DateTime.Now.AddDays(-1),
                    DownloadCount = 250
                });

                MostPopularPackages.Add(new V2FeedPackageEx()
                {
                    Summary = "This is a fucking long text which is very long and you can not see it within one single fucking line. Why the text is this diao yang? Because I need to test if the UI is good when given such a long fucking text.",
                    Title = "Text.Is.So.Long",
                    LastUpdated = DateTime.Now.AddDays(-2),
                    DownloadCount = 250
                });

                MostPopularPackages.Add(new V2FeedPackageEx()
                {
                    Summary = "hello world!",
                    Title = "Why.This.Package.Has.A.Long.Title",
                    LastUpdated = DateTime.Now.AddDays(-3),
                    DownloadCount = 250
                });
            }
            else
            {
                NuGetSearcher = new NuGetOrgSearcher()
                {
                    IncludePreRelease = new AppSettings().IsIncludePreReleaseSetting
                };
                InitDataTask = GetMostPopularPackages();
            }
        }

        public void ClearData()
        {
            MostPopularPackages = new ObservableCollection<V2FeedPackageEx>();
            MicrosoftDotNetPackages = new ObservableCollection<V2FeedPackageEx>();
        }

        public async Task GetMostPopularPackages(int pageIndex = 1)
        {
            IsUIBusy = true;
            Message = "Getting Data...";
            var apiResponse = await NuGetSearcher.GetMostPopularPackagesAsync(pageIndex);
            var response = apiResponse as Response<IEnumerable<V2FeedPackageEx>>;
            if (response != null && response.IsSuccess)
            {
                foreach (var v2FeedPackage in response.Item)
                {
                    MostPopularPackages.Add(v2FeedPackage);
                }
            }
            Message = string.Empty;
            IsUIBusy = false;
        }

        public async Task GetMicrosoftDotNetPackages(int pageIndex = 1)
        {
            IsUIBusy = true;
            Message = "Getting Data...";
            var apiResponse = await NuGetSearcher.GetTopMicrosoftDotNetPackagesAsync(pageIndex);
            var response = apiResponse as Response<IEnumerable<V2FeedPackageEx>>;
            if (response != null && response.IsSuccess)
            {
                foreach (var v2FeedPackage in response.Item)
                {
                    MicrosoftDotNetPackages.Add(v2FeedPackage);
                }

                IsMsDotNetDataInitialized = true;
            }
            Message = string.Empty;
            IsUIBusy = false;
        }
    }

}

