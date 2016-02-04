using System;
using Eto;
using Eto.Forms;
using EtoMailApp.Desktop;
using GalaSoft.MvvmLight.Ioc;
using EtoMailApp.UI.EtoPortable;
using EtoMailApp.Core.Service;
using EtoMailApp.Desktop.Service;


namespace EtoMailApp.MacTemplate
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //new Application(Platforms.Mac).Run(new MainForm());
			var app = new Startup (Platforms.Mac);
			var dialogService = new EtoDialogService ();
			SimpleIoc.Default.Register<IDialogService> (() => dialogService);
			var mainForm = new MainForm ();
			dialogService.Owner = mainForm;

			app.Run (mainForm);
        }
    }
}
