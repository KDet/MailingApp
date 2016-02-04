using Eto;
using GalaSoft.MvvmLight.Ioc;
using EtoMailApp.Desktop;
using EtoMailApp.Desktop.Service;
using EtoMailApp.UI.EtoPortable.View;
using MailApp.Core.Service;

namespace EtoMailApp.MacClassic
{
	class MainClass
	{
		static void Main (string[] args)
		{
			var app = new Startup (Platforms.XamMac);
			var dialogService = new EtoDialogService ();

			SimpleIoc.Default.Register<IDialogService> (() => dialogService);
			var mainForm = new MainForm ();
			dialogService.Owner = mainForm;

			app.Run (mainForm);
		}
	}
}