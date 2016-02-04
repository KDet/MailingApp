using Eto;
using EtoMailApp.Desktop.Service;
using EtoMailApp.Desktop;
using EtoMailApp.UI.EtoPortable.View;
using MailApp.Core.Service;
using GalaSoft.MvvmLight.Ioc;

namespace EtoMailApp.MacMono
{
	class MainClass
	{
		static void Main (string[] args)
		{
			var app = new Startup(Platforms.Mac);
			var dialogService = new EtoDialogService();
			SimpleIoc.Default.Register<IDialogService>(() => dialogService);
			var mainForm = new MainForm();
			dialogService.Owner = mainForm;

			app.Run(mainForm);
		}
	}
}

