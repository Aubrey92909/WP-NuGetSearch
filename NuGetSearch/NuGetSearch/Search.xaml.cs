using System;
using MVVMSidekick.Views;
using NuGetSearch.ViewModels;

namespace NuGetSearch
{
    public partial class Search : MVVMPage
    {
        public Search()
            : base(null)
        {
            InitializeComponent();
        }

        public Search(Search_Model model)
            : base(model)
        {
            InitializeComponent();
        }


        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            var vm = ViewModel as Search_Model;
            if (null != vm)
            {
                await vm.DoSearch();
            }
        }

        private void SearchResultPagedPackageList_OnSelectedPackageChanged(object sender, string stationname)
        {

        }

        private async void SearchResultPagedPackageList_OnLoadNextPage(object sender, int pageindex)
        {
            var vm = ViewModel as Search_Model;
            if (null != vm)
            {
                await vm.DoSearch(pageindex);
            }
        }
    }
}