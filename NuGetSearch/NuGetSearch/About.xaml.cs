using System;
using MVVMSidekick.Views;
using NuGetSearch.ViewModels;

namespace NuGetSearch
{
    public partial class About : MVVMPage
    {
        public About()
            : base(null)
        {
            InitializeComponent();
        }

        public About(About_Model model)
            : base(model)
        {
            InitializeComponent();
        }


        private void BtnReview_Click(object sender, EventArgs e)
        {
            Utils.GoToReview();
        }
    }
}