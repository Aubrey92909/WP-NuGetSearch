using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
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
    public class Search_Model : NuGetSearchVMBase<Search_Model>
    {
        public string Keyword
        {
            get { return _KeywordLocator(this).Value; }
            set { _KeywordLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Keyword Setup
        protected Property<string> _Keyword = new Property<string> { LocatorFunc = _KeywordLocator };
        static Func<BindableBase, ValueContainer<string>> _KeywordLocator = RegisterContainerLocator<string>("Keyword", model => model.Initialize("Keyword", ref model._Keyword, ref _KeywordLocator, _KeywordDefaultValueFactory));
        static Func<string> _KeywordDefaultValueFactory = () => { return default(string); };
        #endregion

        public ObservableCollection<V2FeedPackage> SearchResults
        {
            get { return _SearchResultsLocator(this).Value; }
            set { _SearchResultsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<V2FeedPackage> SearchResults Setup
        protected Property<ObservableCollection<V2FeedPackage>> _SearchResults = new Property<ObservableCollection<V2FeedPackage>> { LocatorFunc = _SearchResultsLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<V2FeedPackage>>> _SearchResultsLocator = RegisterContainerLocator<ObservableCollection<V2FeedPackage>>("SearchResults", model => model.Initialize("SearchResults", ref model._SearchResults, ref _SearchResultsLocator, _SearchResultsDefaultValueFactory));
        static Func<ObservableCollection<V2FeedPackage>> _SearchResultsDefaultValueFactory = () => { return default(ObservableCollection<V2FeedPackage>); };
        #endregion

        public Search_Model()
        {
            SearchResults = new ObservableCollection<V2FeedPackage>();

            if (IsInDesignMode)
            {
                Keyword = "Edi.";

                SearchResults.Add(new V2FeedPackage()
                {
                    Summary = "Microsoft Hope Of Human! A quick brown fox jumped over the lazy dog.",
                    Title = "Microsoft.Humanity.Hope",
                    LastUpdated = DateTime.Now,
                    DownloadCount = 8888
                });

                SearchResults.Add(new V2FeedPackage()
                {
                    Summary = "Fuck the fucking fuckers.",
                    Title = "Linustd.Is.Adobe",
                    LastUpdated = DateTime.Now.AddDays(-1),
                    DownloadCount = 250
                });

                SearchResults.Add(new V2FeedPackage()
                {
                    Summary = "This is a fucking long text which is very long and you can not see it within one single fucking line. Why the text is this diao yang? Because I need to test if the UI is good when given such a long fucking text.",
                    Title = "Text.Is.So.Long",
                    LastUpdated = DateTime.Now.AddDays(-2),
                    DownloadCount = 250
                });

                SearchResults.Add(new V2FeedPackage()
                {
                    Summary = "hello world!",
                    Title = "Why.This.Package.Has.A.Long.Title",
                    LastUpdated = DateTime.Now.AddDays(-3),
                    DownloadCount = 250
                });
            }
        }

        public CommandModel<ReactiveCommand, string> CommandSearch
        {
            get { return _CommandSearchLocator(this).Value; }
            set { _CommandSearchLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, string> CommandSearch Setup
        protected Property<CommandModel<ReactiveCommand, string>> _CommandSearch = new Property<CommandModel<ReactiveCommand, string>> { LocatorFunc = _CommandSearchLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, string>>> _CommandSearchLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, string>>("CommandSearch", model => model.Initialize("CommandSearch", ref model._CommandSearch, ref _CommandSearchLocator, _CommandSearchDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, string>> _CommandSearchDefaultValueFactory =
            model =>
            {
                var resource = "Search";           // Command resource  
                var commandId = "Search";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            await vm.DoSearch();
                        }
                    )
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        public async Task DoSearch(int pageIndex = 1)
        {
            var settings = new AppSettings();
            var opt = new NuGetOrgSearcher()
            {
                IncludePreRelease = settings.IsIncludePreReleaseSetting
            };

            if (!string.IsNullOrEmpty(Keyword))
            {
                IsUIBusy = true;
                Message = "Getting Data...";
                var apiResponse = await opt.SearchPackagesByTermAsync(Keyword, pageIndex);
                var response = apiResponse as Response<IEnumerable<V2FeedPackageEx>>;
                if (response != null && response.IsSuccess)
                {
                    foreach (var v2FeedPackage in response.Item)
                    {
                        SearchResults.Add(v2FeedPackage);
                    }
                }
                Message = string.Empty;
                IsUIBusy = false;
            }
        }
    }
}

