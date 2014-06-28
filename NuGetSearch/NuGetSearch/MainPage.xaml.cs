using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Tasks;
using MVVMSidekick.Views;
using System;
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
            CheckNetwork();
        }

        private void CheckNetwork()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                var result = MessageBox.Show(
                    NetworkInterface.NetworkInterfaceType + "Your network blow up.",
                    "NO CONNECTION",
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var connectionSettingsTask = new ConnectionSettingsTask
                    {
                        ConnectionSettingsType = ConnectionSettingsType.WiFi
                    };
                    connectionSettingsTask.Show();
                }
            }
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

        private async void MicrosoftDotNetPagedPackageList_OnLoadNextPage(object sender, int pageindex)
        {
            var vm = ViewModel as MainPage_Model;
            if (vm != null) await vm.GetMicrosoftDotNetPackages(pageindex);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Search.xaml", UriKind.Relative));
        }

        private void MenuReview_Click(object sender, EventArgs e)
        {
            Utils.GoToReview();
        }

        private async void Pivot_OnLoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item == PvtMsDotNet)
            {
                var vm = ViewModel as MainPage_Model;
                if (null != vm && !vm.IsMsDotNetDataInitialized)
                {
                    await vm.GetMicrosoftDotNetPackages();
                }
            }
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as MainPage_Model;
            if (vm != null)
            {
                vm.IsMsDotNetDataInitialized = false;
                vm.ClearData();

                var tasks = new List<Task>
                {
                    vm.GetMostPopularPackages(), 
                    vm.GetMicrosoftDotNetPackages()
                };

                await Task.WhenAll(tasks);
            }
        }
    }
}
