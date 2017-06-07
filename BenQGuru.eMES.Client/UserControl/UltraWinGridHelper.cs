
using System;
namespace UserControl
{
	public class UltraWinGridHelper
	{
		private Infragistics.Win.UltraWinGrid.UltraGrid _ultraWinGrid = null;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource  _ultraDataSource = null;
		public UltraWinGridHelper(Infragistics.Win.UltraWinGrid.UltraGrid ultraWinGrid)
		{
			this.UltraWinGrid = ultraWinGrid;
			this.UltraWinGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
			this.UltraWinGrid.DataSource = this.UltraDataSource; 
			this.LoadStyle = Infragistics.Win.UltraWinGrid.LoadStyle.LoadOnDemand;
		}

		public Infragistics.Win.UltraWinDataSource.UltraDataColumnsCollection  Columns
		{
			get
			{
				return this.UltraDataSource.Band.Columns;
			}
		}

		public Infragistics.Win.UltraWinDataSource.UltraDataRow  AddRow(object[] cells)
		{
			if (cells == null)
			{
				return null;
			}

			if (this.Columns.Count !=  cells.Length)
			{
				return null;
			}
		
			Infragistics.Win.UltraWinDataSource.UltraDataRow  row = this.Rows.Add();
			for (int i=0;i< this.Columns.Count; i++)
			{
				row[i] = cells[i];
			}

			return row;
		}

		public Infragistics.Win.UltraWinDataSource.UltraDataRowsCollection  Rows
		{
			get
			{
				return this.UltraDataSource.Rows;
			}
		}

		public Infragistics.Win.UltraWinGrid.LoadStyle LoadStyle
		{
			get
			{
				return this.UltraWinGrid.DisplayLayout.LoadStyle;
			}
			set
			{
				this.UltraWinGrid.DisplayLayout.LoadStyle = value;
			}
		}

		public Infragistics.Win.UltraWinGrid.UltraGrid UltraWinGrid
		{
			get
			{
				return _ultraWinGrid;
			}
			set
			{
				this._ultraWinGrid = value;
			}
		}

		public Infragistics.Win.UltraWinDataSource.UltraDataSource UltraDataSource
		{
			get
			{
				if(_ultraDataSource == null)
				{
					_ultraDataSource = new Infragistics.Win.UltraWinDataSource.UltraDataSource();
				}
				return _ultraDataSource;
			}
			set
			{
				this._ultraDataSource = value;
			}
		}
	}
}