using System;
using global::Gtk;

namespace Colours
{
	public partial class SortDialog : Dialog
	{
		ListStore sortBy = new ListStore(typeof(PaletteSortBy));

		public SortDialog()
		{
			this.Build();

			sortByBox.Model = sortBy;
			foreach (var sb in Enum.GetValues(typeof(PaletteSortBy)))
				sortBy.AppendValues(sb);

			var sortRender = new CellRendererText();
			sortByBox.PackStart(sortRender, true);
			sortByBox.SetCellDataFunc(sortRender, new CellLayoutDataFunc((cl, c, m, i) =>
			{
				((CellRendererText)c).Text = ((PaletteSortBy)sortBy.GetValue(i, 0)).ToString();
			}));

			sortByBox.Active = 0;
		}

		// casting hell
		public PaletteSortBy SortBy => ((PaletteSortBy[])Enum.GetValues(typeof(PaletteSortBy)))[sortByBox.Active];

		public bool Ascending => orderBox.Active;
	}
}
