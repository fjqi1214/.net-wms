
using System;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace BenQGuru.eMES.PDAClient
{
	public class UltraWinGridHelper
	{
		private Infragistics.Win.UltraWinGrid.UltraGrid _ultraWinGrid = null;


		public UltraWinGridHelper(Infragistics.Win.UltraWinGrid.UltraGrid ultraWinGrid)
		{
			this.UltraWinGrid = ultraWinGrid;
			//this.UltraWinGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
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


		public void AddDrpDownListColumn(string columnName,string caption, ValueList valueList)
		{
			if(valueList != null)
			{
				this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].ValueList = valueList;
			}
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName ].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].Width = 100;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[columnName ].Header.Caption = caption;

		}

		public void AddReadOnlyColumn(string columnName,string caption)
		{
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName ].Width = 100;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].Header.Caption = caption;
		}

		public void AddCommonColumn(string columnName,string caption)
		{
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName ].Width = 100;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName ].Header.Caption = caption;
		}

		public void AddCheckColumn(string columnName,string caption)
		{
		    DefaultEditorOwnerSettings editorSettings = new DefaultEditorOwnerSettings( );
			editorSettings.DataType = typeof( System.Boolean );
			CheckEditor checkEditor =  new CheckEditor( new DefaultEditorOwner( editorSettings ) );
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[columnName].Editor = checkEditor;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[columnName].Width = 20;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[columnName].Header.Caption = caption;

//			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
//			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeBoth;
//			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].MaskDataMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;
		}

		

		public void AddRadioButtonColumn(string columnName,string caption, ValueList valueList)
		{
			if(valueList != null)
			{
				DefaultEditorOwnerSettings editorSettings = new DefaultEditorOwnerSettings( );
				editorSettings.DataType = typeof( bool );
				editorSettings.ValueList = valueList;
				this.UltraWinGrid.DisplayLayout.Bands[0].Columns[columnName].Editor =  new OptionSetEditor( new DefaultEditorOwner( editorSettings ) );
			}
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].Width = 100;
			this.UltraWinGrid.DisplayLayout.Bands[0].Columns[ columnName].Header.Caption = caption;
		}


		
	}
}