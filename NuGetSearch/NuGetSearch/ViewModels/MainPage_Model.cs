﻿using System.Reactive;
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


        public string DesignTitle
        {
            get { return _DesignTitleLocator(this).Value; }
            set { _DesignTitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string DesignTitle Setup
        protected Property<string> _DesignTitle = new Property<string> { LocatorFunc = _DesignTitleLocator };
        static Func<BindableBase, ValueContainer<string>> _DesignTitleLocator = RegisterContainerLocator<string>("DesignTitle", model => model.Initialize("DesignTitle", ref model._DesignTitle, ref _DesignTitleLocator, _DesignTitleDefaultValueFactory));
        static Func<string> _DesignTitleDefaultValueFactory = () => { return default(string); };
        #endregion


        public MainPage_Model()
        {
            MostPopularPackages = new ObservableCollection<V2FeedPackage>();
            MicrosoftDotNetPackages = new ObservableCollection<V2FeedPackage>();
            NuGetSearcher = new NuGetOrgSearcher();

            if (IsInDesignMode)
            {
                MostPopularPackages.Add(new V2FeedPackage()
                {
                    Summary = "Microsoft Hope Of Human! A quick brown fox jumped over the lazy dog.",
                    Title = "Microsoft.Humanity.Hope",
                    LastUpdated = DateTime.Now,
                    DownloadCount = 8888
                });

                MostPopularPackages.Add(new V2FeedPackage()
                {
                    Summary = "Fuck the fucking fuckers.",
                    Title = "Linustd.Is.Adobe",
                    LastUpdated = DateTime.Now.AddDays(-1),
                    DownloadCount = 250
                });

                MostPopularPackages.Add(new V2FeedPackage()
                {
                    Summary = "This is a fucking long text which is very long and you can not see it within one single fucking line. Why the text is this diao yang? Because I need to test if the UI is good when given such a long fucking text.",
                    Title = "Text.Is.So.Long",
                    LastUpdated = DateTime.Now.AddDays(-2),
                    DownloadCount = 250
                });

                MostPopularPackages.Add(new V2FeedPackage()
                {
                    Summary = "hello world!",
                    Title = "Why.This.Package.Has.A.Long.Title",
                    LastUpdated = DateTime.Now.AddDays(-3),
                    DownloadCount = 250
                });
            }
            else
            {
                InitDataTask = GetMostPopularPackages();
            }
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

