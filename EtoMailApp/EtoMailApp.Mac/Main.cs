using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Eto.Forms;
using Eto;
using EtoControlsDemo;

namespace EtoMailApp.Mac
{
	class MainClass
	{
		static void Main (string[] args)
		{
			var platform = Eto.Platform.Detect;
			var main = new MainForm ();
			var app = new Application (platform);
			app.Run(main);
		}
	}
}

