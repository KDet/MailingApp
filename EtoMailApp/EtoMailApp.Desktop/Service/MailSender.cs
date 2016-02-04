using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MailApp.Core.Service;

namespace EtoMailApp.Desktop.Service
{
    public class MailSender : IMailSender
    {
        public async Task EmailSendAsync(string aFrom, string aTo, string aSubject, string aMessage,
            IEnumerable<string> aAttachments = null, bool aReadConfirmation = false, bool aReceiptConfirmation = false)
        {
            using (var mail = new MailMessage())
            {
                if (aReadConfirmation)
                    mail.Headers.Add("Disposition-Notification-To", aFrom);
                if (!IsEmailValid(aFrom))
                    throw new ArgumentException("Sender address \"{aFrom}\" is not valid");
                mail.From = new MailAddress(aFrom.Trim());
                if (!IsEmailValid(aTo))
                    throw new ArgumentException("Recipient address \"{aTo}\" is not valid");
                mail.To.Add(aTo.Trim());
                mail.Subject = aSubject;
                mail.Body = aMessage;
                if (aAttachments != null)
                    foreach (var attachment in aAttachments)
                        mail.Attachments.Add(new Attachment(attachment));
                if (aReceiptConfirmation)
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
                                                       DeliveryNotificationOptions.OnFailure |
                                                       DeliveryNotificationOptions.OnSuccess;

                    using (var smtpClient = new SmtpClient())
						await smtpClient.SendMailExAsync(mail);
            }
        }
        public bool IsEmailValid(string aEmail)
        {
            const string regExpEmail =
                @"(((([a-zA-Z0-9' ]+ ?)|(\([a-zA-Z0-9' ]*\) ?)*)?<(?<email>[a-zA-Z]+[a-zA-Z0-9'\._\-\+]*@[a-zA-Z0-9'\._\-\+]+\.[a-zA-Z]{2,})>)|((([a-zA-Z0-9']+ )|(\([a-zA-Z0-9' ]*\) ?)*)?[a-zA-Z]+[a-zA-Z0-9'\._\-\+]*@[a-zA-Z0-9'\._\-\+]+\.[a-zA-Z]{2,}))[ ]*(([a-zA-Z0-9' ]+ ?)|(\([a-zA-Z0-9' ]*\) ?)*)?";
            return !string.IsNullOrWhiteSpace(aEmail) && Regex.IsMatch(aEmail, regExpEmail);
        }
    }

	public static class SendMailEx
	{
		public static Task SendMailExAsync(
			this System.Net.Mail.SmtpClient smtpClient,
			System.Net.Mail.MailMessage message,
			CancellationToken token = default(CancellationToken))
		{
			// use Task.Run to negate SynchronizationContext
			return Task.Run(() => SendMailExImplAsync(smtpClient, message, token), token);
		}

		private static async Task SendMailExImplAsync(
			SmtpClient client, 
			MailMessage message, 
			CancellationToken token)
		{
			token.ThrowIfCancellationRequested();

			var tcs = new TaskCompletionSource<bool>();
			SendCompletedEventHandler[] handler = {null};
			Action unsubscribe = () => client.SendCompleted -= handler[0];
			handler[0] = async (s, e) =>
			{
				unsubscribe();

				// a hack to complete the handler asynchronously
				await Task.Yield(); 

				if (e.UserState != tcs)
					tcs.TrySetException(new InvalidOperationException("Unexpected UserState"));
				else if (e.Cancelled)
					tcs.TrySetCanceled();
				else if (e.Error != null)
					tcs.TrySetException(e.Error);
				else
					tcs.TrySetResult(true);
			};
			client.SendCompleted += handler[0];
			try
			{
				client.SendAsync(message, tcs);
			    using (token.Register(client.SendAsyncCancel, useSynchronizationContext: false))
			        await tcs.Task;
			}
			finally
			{
				unsubscribe();
			}
		}
	}
}