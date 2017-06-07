using System;
using System.Collections;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.WebQuery;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// DimensionChange 的摘要说明。
	/// </summary>
	public class DimensionChange
	{
		private object[] _originalSource = null;
		private string _rowCol = "";
		private string _colRow = "";
		private string[] _cols = null;
		private ExportGrid _grid = null;

		public DimensionChange(object[] source,string rowCol,string colRow,string[] cols)
		{
			this._originalSource = source;
			this._rowCol = rowCol;
			this._colRow = colRow;
			this._cols = cols;

			this._grid = new ExportGrid();
		}

		public ExportGrid Change()
		{
			this._getColumnList();

			ExportRow row = null;
			foreach(object obj in this._originalSource)
			{
				row = new ExportRow( this._grid.OriginalColumnList.Count );
				foreach(string col in this._cols)
				{
					if( col.ToUpper() != this._rowCol.ToUpper() &&
						col.ToUpper() != this._colRow.ToUpper() )
					{
						object objValue = DomainObjectUtility.GetValue(obj,col,null);
						if( objValue != null )
						{
							row.AddCellText( objValue.ToString() );
						}
						else
						{
							row.AddCellText( ExportCell.CellEmpty );
						}
					}
					else if( col.ToUpper() == this._colRow.ToUpper() )
					{
						object objColRow = DomainObjectUtility.GetValue(obj,col,null);
						if( objColRow != null )
						{
							object objRowCol = DomainObjectUtility.GetValue(obj,this._rowCol,null);						
							if( objRowCol != null )
							{
								for(int i=0;i<this._grid.OriginalColumnList.Count;i++)
								{
									if( (this._grid.OriginalColumnList[i] as ExportColumn).Key.ToUpper() ==
										objRowCol.ToString().ToUpper() )
									{
										row.CellList[i] = objColRow;
									}
								}
							}
						}
					}
				}				
			}
			
			return this._grid;
		}

		private void _getColumnList()
		{
			foreach(string col in this._cols)
			{
				if( col.ToUpper() != this._colRow.ToUpper()  &&
					col.ToUpper() != this._rowCol.ToUpper() )
				{
					this._grid.AddColumn( new ExportColumn( col.ToUpper(),col.ToUpper() ));
				}
			}

			foreach(object obj in this._originalSource)
			{
				object objValue = DomainObjectUtility.GetValue(obj,this._rowCol,null);
				if( objValue != null )
				{
					this._grid.AddColumn( new ExportColumn( objValue.ToString().ToUpper(),objValue.ToString().ToUpper() ));
				}
			}	
		}
	}
}
