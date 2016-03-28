using System;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace Colours
{
	public static class ConfigParser
	{
		static string configrc = Path.Combine(Environment.GetFolderPath
			(Environment.SpecialFolder.ApplicationData), ".colorsrc");

		public static AppState LoadConfig()
		{
			// set some initial options in case 
			SchemeType t = SchemeType.Complement;
			Color c = Color.Red;
			try {
				// TODO: a real config mechanism? the .NET one is poor in mono
				string[] lines = File.ReadAllLines(configrc);
				foreach (string l in lines) {
					string[] components = l.Split("=".ToCharArray(), 2);
					switch (components[0]) {
					case "color":
						c = ColorTranslator.FromHtml(components[1].Trim());
						break;
					case "scheme":
						Enum.TryParse(components[1], out t);
						break;
					default: break;
					}
				}
			} catch (Exception) { // just load some defaults

			}
			return new AppState (new HsvColor(c), t);
		}

		public static void SaveConfig(Color c, SchemeType t)
		{
			try {
				File.WriteAllLines (configrc, new string[] {
					"color=" + ColorTranslator.ToHtml(c),
					"scheme=" + t.ToString()
				});
			} catch (Exception) {

			}
		}
	}
}

