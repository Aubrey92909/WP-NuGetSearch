using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMSidekick.ViewModels;

namespace NuGetSearch.ViewModels
{
    public class NuGetSearchVMBase<TViewModel> : ViewModelBase<TViewModel> where TViewModel : NuGetSearchVMBase<TViewModel>
    {
        public string Message
        {
            get { return _MessageLocator(this).Value; }
            set { _MessageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Message Setup
        protected Property<string> _Message = new Property<string> { LocatorFunc = _MessageLocator };
        static Func<BindableBase, ValueContainer<string>> _MessageLocator = RegisterContainerLocator<string>("Message", model => model.Initialize("Message", ref model._Message, ref _MessageLocator, _MessageDefaultValueFactory));
        static Func<string> _MessageDefaultValueFactory = () => { return default(string); };
        #endregion

    }
}
