using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using global::Gtk;

namespace Colours
{
	public partial class PropertiesDialog : global::Gtk.Dialog
	{
		public string PaletteTitle
		{
			get
			{
				return titleBox.Text;
			}
			set
			{
				titleBox.Text = value;
			}
		}

		public int PaletteColumns
		{
			get
			{
				return columnsBox.ValueAsInt;
			}
			set
			{
				columnsBox.Value = value;
			}
		}

		public List<string> PaletteComments
		{
			get
			{
				return Regex.Split (commentsBox.Buffer.Text, "\\r?\\n").ToList();
			}
			set
			{
				commentsBox.Buffer.Text = String.Join(Environment.NewLine, value);
			}
		}

		public PropertiesDialog ()
		{
			this.Build ();
		}
	}
}

