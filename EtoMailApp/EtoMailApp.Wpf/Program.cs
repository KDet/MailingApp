using System;
using Eto;
using EtoMailApp.Desktop;
using EtoMailApp.Desktop.Service;
using EtoMailApp.UI.EtoPortable;
using EtoMailApp.UI.EtoPortable.View;
using GalaSoft.MvvmLight.Ioc;
using MailApp.Core.Service;

namespace EtoMailApp.Wpf
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Startup(Platforms.Wpf);
            var dialogService = new EtoDialogService();
            SimpleIoc.Default.Register<IDialogService>(() => dialogService);
            var mainForm = new MainForm();
            dialogService.Owner = mainForm;

            app.Run(mainForm);
        }
    }
}
