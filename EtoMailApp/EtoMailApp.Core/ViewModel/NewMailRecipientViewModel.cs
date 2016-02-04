using System;
using GalaSoft.MvvmLight.Command;
using MailApp.Core.Service;

namespace MailApp.Core.ViewModel
{
    public class NewMailRecipientViewModel : MailRecipientViewModel
    {
        private readonly IMailSender _mailSender;
        private RelayCommand _addRecipientCommand;

        private void AddEMail()
        {
            IsValidMail = _mailSender.IsEmailValid(EMail);
            OnAddEmail?.Invoke();
        }

        public bool IsValidMail { get; set; }
        public override string EMail
        {
            get { return base.EMail; }
            set
            {
				if (!string.Equals (EMail, value)) 
				{
					base.EMail = value;
					AddEmailCommand.RaiseCanExecuteChanged ();
				}
            }
        }
        public Action OnAddEmail { get; set; }

        public RelayCommand AddEmailCommand
        {
            get
            {
                return _addRecipientCommand ??
                       (_addRecipientCommand = new RelayCommand(AddEMail, () => _mailSender.IsEmailValid(EMail)));
            }
        }

        public NewMailRecipientViewModel(Action<MailRecipientViewModel> removeAction, IMailSender mailSender)
            : base(removeAction)
        {
            _mailSender = mailSender;
        }
        public NewMailRecipientViewModel(IMailSender mailSender) : this(model => { }, mailSender){} 
    }
}