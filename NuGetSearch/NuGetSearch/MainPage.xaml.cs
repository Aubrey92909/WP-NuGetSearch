using Microsoft.Phone.Controls;
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

        private void MainPivot_OnLoadedPivotItem(object sender, PivotItemEventArgs e)
        {
            
        }
    }
}
