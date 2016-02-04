using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailApp.Core.Service;

namespace EtoMailApp.Mobile.Service
{
    public class MailSender : IMailSender
    {
        public async Task EmailSendAsync(string aFrom, string aTo, string aSubject, string aMessage,
            IEnumerable<string> aAttachments = null, bool aReadConfirmation = false, bool aReceiptConfirmation = false)
        {
            
        }
        public bool IsEmailValid(string aEmail)
        {
            const string regExpEmail =
                @"(((([a-zA-Z0-9' ]+ ?)|(\([a-zA-Z0-9' ]*\) ?)*)?<(?<email>[a-zA-Z]+[a-zA-Z0-9'\._\-\+]*@[a-zA-Z0-9'\._\-\+]+\.[a-zA-Z]{2,})>)|((([a-zA-Z0-9']+ )|(\([a-zA-Z0-9' ]*\) ?)*)?[a-zA-Z]+[a-zA-Z0-9'\._\-\+]*@[a-zA-Z0-9'\._\-\+]+\.[a-zA-Z]{2,}))[ ]*(([a-zA-Z0-9' ]+ ?)|(\([a-zA-Z0-9' ]*\) ?)*)?";
            return !string.IsNullOrWhiteSpace(aEmail) && Regex.IsMatch(aEmail, regExpEmail);
        }

        public string SendReport { get; set; }
    }
}