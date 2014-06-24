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
        public static void ConfigSettings()
        {
            ViewModelLocator<Settings_Model>
                .Instance
                .Register(context =>
                    new Settings_Model())
                .GetViewMapper()
                .MapToDefault<Settings>();

        }
    }
}
