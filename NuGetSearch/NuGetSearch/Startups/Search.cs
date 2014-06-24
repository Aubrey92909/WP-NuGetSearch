using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using NuGetSearch.ViewModels;
using System;
using System.Net;
using System.Windows;


namespace NuGetSearch.Startups
{
    public static partial class StartupFunctions
    {
        public static void ConfigSearch()
        {
            ViewModelLocator<Search_Model>
                .Instance
                .Register(context =>
                    new Search_Model())
                .GetViewMapper()
                .MapToDefault<Search>();

        }
    }
}
