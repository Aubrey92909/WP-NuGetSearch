using System;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using MVVMSidekick.Views;
using NuGetApiClientLib.NuGetService;
using NuGetSearch.ViewModels;

namespace NuGetSearch
{
    public partial class PackageDetail : MVVMPage
    {
        public PackageDetail()
            : base(null)
        {
            InitializeComponent();
        }

        public PackageDetail(PackageDetail_Model model)
            : base(model)
        {
            InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string packageId;
            if (NavigationContext.QueryString.TryGetValue("id", out packageId))
            {
                var pkg = (V2FeedPackageEx)NavigationContext.GetNavigationData(packageId);

                var vm = ViewModel as PackageDetail_Model;
                if (null != vm && null != pkg)
                {
                    vm.InitData(pkg);
                }

                NavigationContext.Remove(packageId);
            }
        }

        private void BtnNuGetPage_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as PackageDetail_Model;
            if (null != vm)
            {
                var url = vm.CurrentPackage.GalleryDetailsUrl;
                if (!string.IsNullOrEmpty(url))
                {
                    var t = new WebBrowserTask
                    {
                        Uri = new Uri(url)
                    };
                    t.Show();
                }
            }
        }

        private void MenuReportAbuse_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as PackageDetail_Model;
            if (null != vm)
            {
                var url = vm.CurrentPackage.ReportAbuseUrl;
                if (!string.IsNullOrEmpty(url))
                {
                    var t = new WebBrowserTask
                    {
                        Uri = new Uri(url)
                    };
                    t.Show();
                }
            }
        }

        private void MenuContactOwners_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as PackageDetail_Model;
            if (null != vm)
            {
                var url = vm.CurrentPackage.GalleryDetailsUrl + "/ContactOwners";
                if (!string.IsNullOrEmpty(url))
                {
                    var t = new WebBrowserTask
                    {
                        Uri = new Uri(url)
                    };
                    t.Show();
                }
            }
        }

        private void BtnLicense_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as PackageDetail_Model;
            if (null != vm)
            {
                var url = vm.CurrentPackage.LicenseUrl;
                if (!string.IsNullOrEmpty(url))
                {
                    var t = new WebBrowserTask
                    {
                        Uri = new Uri(url)
                    };
                    t.Show();
                }
            }
        }

        private void BtnProjectSite_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as PackageDetail_Model;
            if (null != vm)
            {
                var url = vm.CurrentPackage.ProjectUrl;
                if (!string.IsNullOrEmpty(url))
                {
                    var t = new WebBrowserTask
                    {
                        Uri = new Uri(url)
                    };
                    t.Show();
                }
            }
        }

        private void BtnShareQR_Click(object sender, EventArgs e)
        {
        }

        private void MenuShareEmail_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as PackageDetail_Model;
            if (null != vm)
            {
                var t = new EmailComposeTask()
                {
                    Subject = vm.CurrentPackage.DisplayTitle,
                    Body = vm.CurrentPackage.ToString()
                };
                t.Show();
            }
        }
    }
}