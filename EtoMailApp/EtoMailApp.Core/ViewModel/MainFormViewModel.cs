using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using MailApp.Core.Service;

namespace MailApp.Core.ViewModel
{
    public class MainFormViewModel : BaseViewModel
    {
        private readonly IFileSystemRepository _fileSystemRepository;
        private readonly IDialogService _dialogService;
        private readonly IMailSender _mailSender;

        private RelayCommand _addAttachmentCommand;
        private RelayCommand _removAttachmentCommand;
        private RelayCommand _addRecipientCommand;
        private RelayCommand _removRecipientCommand;
        private RelayCommand _loadRecipientsCommand;
        private RelayCommand _saveRecipientsCommand;
        private RelayCommand _sendCommand;
        private RelayCommand _quitCommand;

        private string _from;
        private string _subject;
        private string _message;
        private bool _readConfirmation;
        private bool _receiptConfirmation;

        private MailAttachmentViewModel _selectedAttachment;
        private MailRecipientViewModel _selectedRecipient;
        private int _mailsSending;

        private async void AddAttachment()
        {
            var files = await _dialogService.OpenFilesAsync();
            if (files != null)
                foreach (var file in files)
                    Attachments.Add(new MailAttachmentViewModel(file, RemoveAttachment));
        }
        private void RemoveAttachment(MailAttachmentViewModel mailAttachment)
        {
            Attachments.Remove(mailAttachment);
        }
        private void RemoveAttachment()
        {
            RemoveAttachment(SelectedAttachment);
        }
        private async void AddRecipient()
        {
            var mail = await _dialogService.OpenRecipientAsync();
            if (!string.IsNullOrEmpty(mail) && _mailSender.IsEmailValid(mail))
                Recipients.Add(new MailRecipientViewModel(RemoveRecipient) {EMail = mail});
        }
        private void RemoveRecipient(MailRecipientViewModel mailRecipient)
        {
            Recipients.Remove(mailRecipient);
        }
        private void RemoveRecipient()
        {
            RemoveRecipient(SelectedRecipient);
        }
        private async void LoadRecipients()
        {
            
            var files = await _dialogService.OpenFilesAsync();
			if (files != null) 
			{
				Recipients.Clear();
				foreach (var path in files) 
				{
					var content = await _fileSystemRepository.ReadAllTextAsync (path);
					foreach (string line in content.Replace(Environment.NewLine, string.Empty).Split(';'))
						if (!string.IsNullOrEmpty (line))
							Recipients.Add (new MailRecipientViewModel (RemoveRecipient) { EMail = line.Trim () });
				}
			}
        }
        private async void SaveRecipients()
        {
            var path = await _dialogService.SaveFileAsync();
            if (!string.IsNullOrEmpty(path))
            {
                StringBuilder recs = new StringBuilder("");
                foreach (var mailRecipient in Recipients)
                    recs.AppendFormat("{0};", mailRecipient.EMail);
                await _fileSystemRepository.WriteAllTextAsync(path, recs.ToString());
            }
        }
        private async void Send()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            MailsSending = 0;
            SendCommand.RaiseCanExecuteChanged();
            try
            {
                var reipients = Recipients.Distinct(MailRecipientViewModel.EMailComparer).Select(recipient => recipient.EMail);
                var attaches = Attachments.Select(attachment => attachment.FilePath).ToArray();
                foreach (var reipient in reipients)
                {
                    await _mailSender.EmailSendAsync(From, reipient, Subject, Message, attaches, ReadConfirmation, ReceiptConfirmation);
                    ++MailsSending;
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ErrorAlarmAsync(ex.Message, "Sending Error");
            }
            finally
            {
                IsBusy = false;
                MailsSending = 0;
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<MailAttachmentViewModel> Attachments { get; set; } = new ObservableCollection<MailAttachmentViewModel>();
        public ObservableCollection<MailRecipientViewModel> Recipients { get; set; } = new ObservableCollection<MailRecipientViewModel>();

        public string From
        {
            get { return _from; }
            set { Set(() => From, ref _from, value); }
        }
        public string Subject
        {
            get { return _subject; }
            set { Set(() => Subject, ref _subject, value); }
        }
        public string Message
        {
            get { return _message; }
            set { Set(() => Message, ref _message, value); }
        }
        public bool ReadConfirmation
        {
            get { return _readConfirmation; }
            set { Set(() => ReadConfirmation, ref _readConfirmation, value); }
        }
        public bool ReceiptConfirmation
        {
            get { return _receiptConfirmation; }
            set { Set(() => ReceiptConfirmation, ref _receiptConfirmation, value); }
        }
        public int MailsTotal => Recipients.Count;
        public int MailsSending
        {
            get { return _mailsSending; }
            set { Set(() => MailsSending, ref _mailsSending, value); }
        }

        public MailAttachmentViewModel SelectedAttachment
        {
            get { return _selectedAttachment; }
            set
            {
                if (Set(() => SelectedAttachment, ref _selectedAttachment, value))
                    RemoveAttachmentCommand.RaiseCanExecuteChanged();
            }
        }
        public MailRecipientViewModel SelectedRecipient
        {
            get { return _selectedRecipient; }
            set
            {
                if (Set(() => SelectedRecipient, ref _selectedRecipient, value))
                    RemoveRecipientCommand.RaiseCanExecuteChanged();
            }
        }

        public static Action Quit { get; set; }

        public RelayCommand AddAttachmentCommand => _addAttachmentCommand ?? 
            (_addAttachmentCommand = new RelayCommand(AddAttachment));
        public RelayCommand RemoveAttachmentCommand => _removAttachmentCommand ??
                (_removAttachmentCommand = new RelayCommand(RemoveAttachment, () => SelectedAttachment != null));
        public RelayCommand AddRecipientCommand => _addRecipientCommand ?? 
            (_addRecipientCommand = new RelayCommand(AddRecipient));
        public RelayCommand RemoveRecipientCommand => _removRecipientCommand ?? 
            (_removRecipientCommand = new RelayCommand(RemoveRecipient, () => SelectedRecipient != null));
        public RelayCommand LoadRecipientsCommand => _loadRecipientsCommand ?? 
            (_loadRecipientsCommand = new RelayCommand(LoadRecipients));
        public RelayCommand SaveRecipientsCommand => _saveRecipientsCommand ?? 
            (_saveRecipientsCommand = new RelayCommand(SaveRecipients, () => Recipients.Count > 0));
        public RelayCommand SendCommand => _sendCommand ?? 
            (_sendCommand = new RelayCommand(Send, () => !IsBusy && MailsTotal > 0));
        public RelayCommand QuitCommand => _quitCommand ?? (_quitCommand = new RelayCommand(() => Quit?.Invoke()));

        public MainFormViewModel(IFileSystemRepository fileSystemRepository, IDialogService dialogService, IMailSender mailSender)
        {
            _fileSystemRepository = fileSystemRepository;
            _dialogService = dialogService;
            _mailSender = mailSender;
            Recipients.CollectionChanged += (o, args) =>
            {
                SaveRecipientsCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => MailsTotal);
                SendCommand.RaiseCanExecuteChanged();
            };
        }
    }
}