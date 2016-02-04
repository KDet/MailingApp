using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailApp.Core.Service
{
    public interface IMailSender
    {
        Task EmailSendAsync(string aFrom, string aTo, string aSubject, string aMessage,
            IEnumerable<string> aAttachments = null, bool aReadConfirmation = false, bool aReceiptConfirmation = false);
        bool IsEmailValid(string aEmail);
    }
}