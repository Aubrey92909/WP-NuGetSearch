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

        public ObservableCollection<V2FeedPackage> MostPopularPackages
        {
            get { return _MostPopularPackagesLocator(this).Value; }
            set { _MostPopularPackagesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<V2FeedPackage> MostPopularPackages Setup
        protected Property<ObservableCollection<V2FeedPackage>> _MostPopularPackages = new Property<ObservableCollection<V2FeedPackage>> { LocatorFunc = _MostPopularPackagesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<V2FeedPackage>>> _MostPopularPackagesLocator = RegisterContainerLocator<ObservableCollection<V2FeedPackage>>("MostPopularPackages", model => model.Initialize("MostPopularPackages", ref model._MostPopularPackages, ref _MostPopularPackagesLocator, _MostPopularPackagesDefaultValueFactory));
        static Func<ObservableCollection<V2FeedPackage>> _MostPopularPackagesDefaultValueFactory = () => { return default(ObservableCollection<V2FeedPackage>); };
        #endregion

        public ObservableCollection<V2FeedPackage> MicrosoftDotNetPackages
        {
            get { return _MicrosoftDotNetPackagesLocator(this).Value; }
            set { _MicrosoftDotNetPackagesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<V2FeedPackage> MicrosoftDotNetPackages Setup
        protected Property<ObservableCollection<V2FeedPackage>> _MicrosoftDotNetPackages = new Property<ObservableCollection<V2FeedPackage>> { LocatorFunc = _MicrosoftDotNetPackagesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<V2FeedPackage>>> _MicrosoftDotNetPackagesLocator = RegisterContainerLocator<ObservableCollection<V2FeedPackage>>("MicrosoftDotNetPackages", model => model.Initialize("MicrosoftDotNetPackages", ref model._MicrosoftDotNetPackages, ref _MicrosoftDotNetPackagesLocator, _MicrosoftDotNetPackagesDefaultValueFactory));
        static Func<ObservableCollection<V2FeedPackage>> _MicrosoftDotNetPackagesDefaultValueFactory = () => { return default(ObservableCollection<V2FeedPackage>); };
        #endregion


        public MainPage_Model()
        {
            if (IsInDesignMode)
            {
                
            }

            NuGetSearcher = new NuGetOrgSearcher();
            InitDataTask = GetMostPopularPackages();

            MostPopularPackages = new ObservableCollection<V2FeedPackage>();
            MicrosoftDotNetPackages = new ObservableCollection<V2FeedPackage>();
        }

        public async Task GetMostPopularPackages(int pageIndex = 1)
        {
            IsUIBusy = true;
            Message = "Getting Data...";
            var apiResponse = await NuGetSearcher.GetMostPopularPackagesAsync(pageIndex);
            var response = apiResponse as Response<IEnumerable<V2FeedPackage>>;
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
            var response = apiResponse as Response<IEnumerable<V2FeedPackage>>;
            if (response != null && response.IsSuccess)
            {
                foreach (var v2FeedPackage in response.Item)
                {
                    MicrosoftDotNetPackages.Add(v2FeedPackage);
                }
            }
            Message = string.Empty;
            IsUIBusy = false;
        }
    }

}

