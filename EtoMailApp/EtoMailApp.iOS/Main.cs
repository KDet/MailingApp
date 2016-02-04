using Eto;
using EtoMailApp.Mobile;
using EtoMailApp.UI.EtoPortable;
using EtoMailApp.UI.EtoPortable.View;

namespace EtoMailApp.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			var app = new Startup(Platforms.Ios);
			var mainForm = new MainForm();
			app.Run(mainForm);
		}
	}
}
