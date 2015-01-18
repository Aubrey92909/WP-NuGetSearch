using GalaSoft.MvvmLight;
using NuGetSearch.WinRT.Common;
using NuGetSearch.WinRT.Core;

namespace NuGetSearch.WinRT.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IncrementalSource<RandomNumbers, int> _items;

        public IncrementalSource<RandomNumbers, int> Items
        {
            get { return _items; }
            set { _items = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                Items = new IncrementalSource<RandomNumbers, int>(string.Empty, 15);
            }
        }
    }
}