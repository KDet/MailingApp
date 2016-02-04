using System;
using Eto;
using GalaSoft.MvvmLight.Ioc;
using EtoMailApp.Desktop;
using EtoMailApp.Desktop.Service;
using EtoMailApp.UI.EtoPortable;
using EtoMailApp.UI.EtoPortable.View;
using MailApp.Core.Service;

namespace EtoMailApp.Gtk2
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
			var app = new Startup (Platforms.Gtk2);
			var dialogService = new EtoDialogService ();
			SimpleIoc.Default.Register<IDialogService> (() => dialogService);
			var mainForm = new MainForm ();
			dialogService.Owner = mainForm;

			app.Run (mainForm);
        }
    }
}
