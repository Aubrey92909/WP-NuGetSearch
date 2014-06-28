using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using MVVMSidekick.ViewModels;
using NuGetApiClientLib.NuGetService;

namespace NuGetSearch.UserControls
{
    public partial class PagedPackageList : UserControl
    {
        #region Bindable Item Source

        public static readonly DependencyProperty PackageItemsSourceProperty;

        static PagedPackageList()
        {
            PackageItemsSourceProperty = DependencyProperty.Register("PackageItemsSource", typeof(IEnumerable), typeof(PagedPackageList), null);
        }

        public IEnumerable PackageItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(PackageItemsSourceProperty);
            }
            set
            {
                SetValue(PackageItemsSourceProperty, value);
            }
        }

        #endregion

        private object o = new object();

        public int PageIndex { get; set; }

        public IViewModel VM { get; set; }

        #region Events

        public event LoadNextPageEventHandler LoadNextPage;
        public delegate void LoadNextPageEventHandler(object sender, int pageIndex);

        public delegate void SelectedPackageChangedEventHandler(object sender, string stationName);
        public event SelectedPackageChangedEventHandler SelectedPackageChanged;

        #endregion

        public PagedPackageList()
        {
            InitializeComponent();
            PageIndex = 1;
        }

        private void SelectedPackage(object sender, SelectionChangedEventArgs e)
        {
            object selected = ((LongListSelector)(sender)).SelectedItem;
            if (null != selected)
            {
                string pid = ((V2FeedPackageEx)(selected)).Id;

                if (SelectedPackageChanged != null)
                {
                    SelectedPackageChanged(this, pid);
                }
            }

            PackageListSelector.SelectedItem = null;
        }

        private void PackageListSelector_OnItemRealized(object sender, ItemRealizationEventArgs e)
        {
            lock (o)
            {
                IList source = PackageListSelector.ItemsSource;

                if (!VM.IsUIBusy && source != null && source.Count >= 1)
                {
                    if (e.ItemKind == LongListSelectorItemKind.Item)
                    {
                        var package = e.Container.Content as V2FeedPackageEx;
                        if (null != package && package.Equals(source[source.Count -1]))
                        {
                            var nextPageIndex = ++PageIndex;
                            Debug.WriteLine("Loading Page {0}", nextPageIndex);

                            if (LoadNextPage != null)
                            {
                                LoadNextPage(this, nextPageIndex);
                            }
                        }
                    }
                }
            }
        }
    }
}
