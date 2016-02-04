using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MailApp.Core.ViewModel
{
    public class MailRecipientViewModel: ViewModelBase
    {
        private sealed class EMailEqualityComparer : IEqualityComparer<MailRecipientViewModel>
        {
            public bool Equals(MailRecipientViewModel x, MailRecipientViewModel y)
            {
                if (ReferenceEquals(x, y))
                    return true;
                if (ReferenceEquals(x, null))
                    return false;
                if (ReferenceEquals(y, null))
                    return false;
                if (x.GetType() != y.GetType())
                    return false;
                return string.Equals(x.EMail, y.EMail);
            }
            public int GetHashCode(MailRecipientViewModel obj)
            {
                return obj.EMail?.GetHashCode() ?? 0;
            }
        }

        private string _eMail;

        public static IEqualityComparer<MailRecipientViewModel> EMailComparer { get; } = new EMailEqualityComparer();
        public virtual string EMail
        {
            get { return _eMail; }
            set { Set(() => EMail, ref _eMail, value); }
        }

        public RelayCommand<MailRecipientViewModel> RemoveRecipientCommand { get; private set; }

        public MailRecipientViewModel(Action<MailRecipientViewModel> removeAction)
        {
            RemoveRecipientCommand = new RelayCommand<MailRecipientViewModel>(removeAction);
        }
        public MailRecipientViewModel():this(model => {}) {}

        public override string ToString()
        {
            return _eMail;
        }
    }
}