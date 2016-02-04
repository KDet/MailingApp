using System;
using Eto.Forms;
using Eto.Serialization.Xaml;
using GalaSoft.MvvmLight.Ioc;
using MailApp.Core.Service;
using MailApp.Core.ViewModel;

namespace EtoMailApp.UI.EtoPortable.View
{
    public class RecepientForm : Dialog
    {
        protected void OnTextChanged(object sender, EventArgs e)
        {
            Email = ((TextBox)sender).Text;
        }

        public RecepientForm()
        {
			var seneder = SimpleIoc.Default.GetInstance<IMailSender> ();
			var vM = new NewMailRecipientViewModel(seneder) { OnAddEmail = Close };
			DataContext = vM;
            XamlReader.Load(this);
        }

        public bool OkDialogResult => ((NewMailRecipientViewModel)DataContext).IsValidMail;
        public string Email
        {
            get { return ((NewMailRecipientViewModel) DataContext).EMail; }
            private set { ((NewMailRecipientViewModel) DataContext).EMail = value; } 
        }
    }
}
