using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace NuGetSearch.WinRT.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; RaisePropertyChanged(); }
        }

        public RelayCommand CommandSearch { get; set; }

        public SearchViewModel()
        {
            if (IsInDesignMode)
            {
                Keyword = "Edi.";
            }

            CommandSearch = new RelayCommand(DoSearch);
        }

        void DoSearch()
        {
            
        }
    }
}
