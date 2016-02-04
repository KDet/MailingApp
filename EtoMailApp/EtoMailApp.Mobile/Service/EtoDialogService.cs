using System.Collections.Generic;
using System.Threading.Tasks;
using MailApp.Core.Service;

namespace EtoMailApp.Mobile.Service
{
    public class EtoDialogService : IDialogService
    {
        public async Task<IEnumerable<string>> OpenFilesAsync()
        {
            return null;
        }
        public async Task<string> SaveFileAsync()
        {
            return "";
        }
        public async Task ErrorAlarmAsync(string text, string caption = "")
        {
            
        }
        public async Task<string> OpenRecipientAsync()
        {
            return "";
        }
    }
}