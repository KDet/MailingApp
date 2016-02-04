using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailApp.Core.Service
{
    public interface IDialogService
    {
        Task<IEnumerable<string>> OpenFilesAsync();
        Task<string> OpenRecipientAsync();
        Task<string> SaveFileAsync();
        Task ErrorAlarmAsync(string text, string caption = "");
    }
}