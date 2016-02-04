using System;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MailApp.Core.ViewModel
{
    public class MailAttachmentViewModel : ViewModelBase
    {
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set { Set(() => FilePath, ref _filePath, value); }
        }
        public string FileName => Path.GetFileName(FilePath);

        public RelayCommand<MailAttachmentViewModel> RemoveAttachmentCommand { get; private set; }

        public MailAttachmentViewModel(string filePath, Action<MailAttachmentViewModel> removeAction)
        {
            _filePath = filePath;
            RemoveAttachmentCommand = new RelayCommand<MailAttachmentViewModel>(removeAction);
        }

        public override string ToString()
        {
            return _filePath;
        }
    }
}