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
using System.Windows.Navigation;
using NuGetApiClientLib.NuGetService;
using NuGetSearch.ViewModels;
namespace NuGetSearch
{
    public partial class PackageDetail : MVVMPage
    {



        public PackageDetail()
            : base(null)
        {
            this.InitializeComponent();
        }
        public PackageDetail(PackageDetail_Model model)
            : base(model)
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string packageId;
            if (NavigationContext.QueryString.TryGetValue("id", out packageId))
            {
                var person = (V2FeedPackageEx)NavigationContext.GetNavigationData(packageId);
                MessageBox.Show(person.DisplayTitle);
                // todo: init data in VM

                NavigationContext.Remove(packageId);
            }
        }

        private void BtnNuGetPage_Click(object sender, EventArgs e)
        {
            

        }

        private void MenuReportAbuse_Click(object sender, EventArgs e)
        {
            
        }

        private void MenuContactOwners_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnLicense_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnProjectSite_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnShareQR_Click(object sender, EventArgs e)
        {
            
        }

        private void MenuShareEmail_Click(object sender, EventArgs e)
        {
            
        }
    }
}




