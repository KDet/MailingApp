using GalaSoft.MvvmLight;

namespace MailApp.Core.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(() => IsBusy, ref _isBusy, value); }
        }
    }
}