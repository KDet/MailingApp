using Eto;
using GalaSoft.MvvmLight.Ioc;
using EtoMailApp.Desktop;
using EtoMailApp.Desktop.Service;
using EtoMailApp.UI.EtoPortable.View;
using MailApp.Core.Service;


namespace EtoMailApp.MacUnified
{
	static class MainClass
	{
		static void Main (string[] args)
		{
			var app = new Startup(Platforms.XamMac2);
			var dialogService = new EtoDialogService();
			SimpleIoc.Default.Register<IDialogService>(() => dialogService);
			using (var mainForm = new MainForm ()) {
				dialogService.Owner = mainForm;
				app.Run (mainForm);
			}
		}
	}
}
