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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NuGetSearch.ViewModels;

namespace NuGetSearch
{
    public partial class MainPage : MVVMPage
    {
        public MainPage()
            : base(null)
        {
            InitializeComponent();
        }

        public MainPage(MainPage_Model model)
            : base(model)
        {
            InitializeComponent();
        }

        private void MostPopularPagedPackageList_OnSelectedPackageChanged(object sender, string stationname)
        {

        }

        private async void MostPopularPagedPackageList_OnLoadNextPage(object sender, int pageindex)
        {
            var vm = ViewModel as MainPage_Model;
            if (vm != null) await vm.GetMostPopularPackages(pageindex);
        }

        private void MicrosoftDotNetPagedPackageList_OnSelectedPackageChanged(object sender, string stationname)
        {
            
        }

        private void MicrosoftDotNetPagedPackageList_OnLoadNextPage(object sender, int pageindex)
        {
            
        }
    }
}
