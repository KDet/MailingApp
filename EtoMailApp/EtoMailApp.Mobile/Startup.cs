using System;
using System.ComponentModel;
using Eto.Forms;
using EtoMailApp.Mobile.Service;
using EtoMailApp.UI.EtoPortable;
using EtoMailApp.UI.EtoPortable.View;
using GalaSoft.MvvmLight.Ioc;
using MailApp.Core.Service;

namespace EtoMailApp.Mobile
{
    public class Startup : Application
    {
        public Startup(string platformId)
            : base(platformId)
        {
            SimpleIoc.Default.Register<IMailSender, MailSender>();
            SimpleIoc.Default.Register<IFileSystemRepository, FileSystemRepository>();
            SimpleIoc.Default.Register<IDialogService, EtoDialogService>();
        }

        protected override void OnTerminating(CancelEventArgs e)
        {
            base.OnTerminating(e);
            //var form = this.MainForm as MainForm;
            //if (!form.PromptSave())
            //    e.Cancel = true;
        }
        protected override void OnInitialized(EventArgs e)
        {
            this.Name = "EtoMail";
            this.MainForm = new MainForm();
            base.OnInitialized(e);
            this.MainForm.Show();
        }
    }
}
