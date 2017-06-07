using System;
using System.Collections;

namespace BenQGuru.eMES.Web.WebQuery
{
	public class ExportCell 
	{
		public static string CellEmpty = "";

		public ExportCell()
		{
		}
	}

	public enum ExportRowType
	{
		Title,Common,Head,Summary,GrandSummary
	}

	public class ExportRow
	{		
		public ArrayList CellList = new ArrayList();
		public ExportRowType RowType = ExportRowType.Common;

		public ExportRow() : this( ExportRowType.Common )
		{
		}

		public ExportRow(int length) : this( ExportRowType.Common )
		{
			this.CellList.Clear();

			for(int i=0;i<length;i++)
			{
				this.CellList.Add( ExportCell.CellEmpty );
			}
		}

		public ExportRow( ExportRowType rowType )
		{
			this.RowType = rowType;
		}

		public ExportRow( ExportRow row )
		{
			if( row != null )
			{
				this.RowType = row.RowType;
				foreach(object text in row.CellList)
				{
					this.CellList.Add( text.ToString() );
				}
			}
		}

		public ExportRow(string[] objs)
		{
			if( objs != null )
			{
				foreach(string obj in objs)
				{
					this.CellList.Add( obj );
				}
			}
		}

		public void AddCellText(string text)
		{
			this.CellList.Add( text );
		}

		public static void PaddingEnd(ExportRow row,int length)
		{
			//make each length of row equal
			int paddingLength = length - row.CellList.Count;
			if( paddingLength > 0 )
			{
				for(int i=0;i<paddingLength;i++)
				{
					row.CellList.Add(ExportCell.CellEmpty);
				}
			}
		}
	}

	public class ExportColumn
	{
		public ExportColumn(string text) : this("",text)
		{			
		}

		public ExportColumn(string key,string text)
		{
			this.Key = key;
			this.Text = text;
		}

		public bool CellMerge = false;

		public bool CellSummary = false;

		public string Key = "";

		public string Text = "";
	}

	public class ExportGrid
	{
		public ArrayList OriginalColumnList = new ArrayList();
		public ArrayList OriginalRowList = new ArrayList();
		public ArrayList FormattedRowList = new ArrayList();

		public ExportGrid()
		{
		}

		public void AddColumn(ExportColumn column)
		{
			if( column.Key == "" )
			{
				this.OriginalColumnList.Add( column );
			}
			else
			{
				bool isIn = false;
				foreach(ExportColumn col in this.OriginalColumnList)
				{
					if( col.Key.ToUpper() == column.Key.ToUpper() )
					{
						isIn = true;
						break;
					}
				}
				if( !isIn )
				{
					this.OriginalColumnList.Add( column );
				}
			}
		}

		public void AddExportRow(ExportRow row)
		{
			this.OriginalRowList.Add( row );
		}

		public void AddExportGrid(ExportGrid grid)
		{
		}

		public void Format()
		{
			int rowNum = 0;
			ExportRow newRow = null;
			ExportRow sumRow = null;
			foreach(ExportRow row in this.OriginalRowList)
			{					
				if(row.RowType == ExportRowType.Title )
				{	
					newRow = new ExportRow();
					foreach(string text in row.CellList)
					{
						if( text != ExportCell.CellEmpty )
						{
							newRow.CellList[ this.OriginalColumnList.Count / 2 ] = text;
							break;
						}
					}
				}
				else if( row.RowType == ExportRowType.Common )
				{
					newRow = new ExportRow(row);
					if( rowNum > 0  )						
					{
						if( (this.OriginalRowList[rowNum-1] as ExportRow).RowType != ExportRowType.Common )
						{
							sumRow = new ExportRow(newRow);
						}
						else if( sumRow != null )
						{
							int colNum = 0;
							foreach(ExportColumn col in this.OriginalColumnList)
							{
								if( col.CellMerge == true )
								{
									if( (this.OriginalRowList[rowNum-1] as ExportRow).CellList[colNum].ToString().ToUpper() ==
										newRow.CellList[colNum].ToString().ToUpper() )
									{
										newRow.CellList[colNum] = "";
										sumRow.CellList[colNum] = (this.OriginalRowList[rowNum-1] as ExportRow).CellList[colNum] + "total";
									}
								}
								else if( col.CellSummary == true )
								{
									decimal sum = System.Decimal.Parse( sumRow.CellList[colNum].ToString() );
									decimal add = System.Decimal.Parse( newRow.CellList[colNum].ToString() );

									sumRow.CellList[colNum] = sum + add;
								}
								colNum += 1;
							}
						}
					}
				}
				else if( row.RowType == ExportRowType.Head )
				{
					newRow = new ExportRow( row );
				}
				else if( row.RowType == ExportRowType.Summary )
				{
					newRow = new ExportRow(sumRow);
					newRow.RowType = ExportRowType.Summary;
					sumRow = null;
				}
				else if( row.RowType == ExportRowType.GrandSummary )
				{
					newRow = new ExportRow();
					int colNum = 0;
					foreach(ExportColumn col in this.OriginalColumnList)
					{
						if( col.CellMerge == true )
						{							
							newRow.AddCellText( "grand total" );
						}
						else if( col.CellSummary == true )
						{
							decimal sum = 0;
							foreach(ExportRow sRow in this.FormattedRowList)
							{
								if( sRow.RowType == ExportRowType.Summary )
								{
									sum += System.Decimal.Parse( sRow.CellList[colNum].ToString() );									
								}
							}
							newRow.AddCellText( sum.ToString() ) ;
						}
						else
						{
							newRow.AddCellText( ExportCell.CellEmpty );
						}
						colNum += 1;
					}
				}
				this.FormattedRowList.Add( newRow );
				rowNum += 1;
			}
		}
	}
	
	/// <summary>
	/// ExportTool 的摘要说明。
	/// </summary>
	public class ExportTool
	{
		public ExportTool()
		{			
		}
	}
}
