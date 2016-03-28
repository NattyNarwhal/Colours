using System;
using Gtk;

namespace Colours.Gtk
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			AppState initialState = ConfigParser.LoadConfig ();
			AppState parsed = AppArgParser.ParseArgs (args,
				initialState.Color, initialState.SchemeType);

			Application.Init ();
			MainWindow win = new MainWindow (parsed);
			win.Show ();
			Application.Run ();
		}
	}
}
