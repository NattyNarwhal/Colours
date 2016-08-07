using System;
using Gtk;
using Colours.App;

namespace Colours.Gtk
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var initialState = ConfigParser.LoadConfig ();
			var parsed = AppArgParser.ParseArgs (args,
				initialState.Color, initialState.SchemeType);

			Application.Init ();
			MainWindow win = new MainWindow (parsed);
			win.Show ();
			Application.Run ();
		}
	}
}
