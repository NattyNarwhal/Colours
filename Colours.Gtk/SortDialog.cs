using System;
using global::Gtk;

namespace Colours
{
	public partial class SortDialog : Dialog
	{
		public SortDialog()
		{
			this.Build();

			sortByBox.Model = new ListStore(typeof(PaletteSortBy));
			foreach (var sb in Enum.GetValues(typeof(PaletteSortBy)))
				sortByBox.Model.AppendValues(sb);

			var sortRender = new CellRendererText();
			sortByBox.PackStart(sortRender, true);
			sortByBox.SetCellDataFunc(sortRender, new CellLayoutDataFunc((cl, c, m, i) =>
			{
				((CellRendererText)c).Text = ((PaletteSortBy)sortByBox.Model.GetValue(i, 0)).ToString();
			}));
		}

		public PaletteSortBy SortBy => Enum.GetValues(typeof(PaletteSortBy))[sortByBox.Active];

		public bool Ascending => orderBox.Active;
	}
}
