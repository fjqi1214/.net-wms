using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Material;


namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QStockDetail 的摘要说明。
	/// </summary>

	public class QStockInDetail : MaterialStockIn
	{
		public QStockInDetail() : base()
		{
		}

		/// <summary>
		/// 入库数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(int), 10, true)]
		public int Quantity;
	}

	public class QStockOutDetail : MaterialStockOut
	{
		public QStockOutDetail() : base()
		{
		}

		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string RunningCard;
	}

	public class QStockContrast : DomainObject
	{
		public QStockContrast() : base()
		{
		}

		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string RunningCard;
		
		/// <summary>
		/// 入库单号
		/// </summary>
		[FieldMapAttribute("INTKTNO", typeof(string), 40, true)]
		public string StockInTicketNo;
		
		/// <summary>
		/// 出货单号
		/// </summary>
		[FieldMapAttribute("OUTTKTNO", typeof(string), 40, true)]
		public string StockOutTicketNo;
		
		/// <summary>
		/// 入库日期
		/// </summary>
		[FieldMapAttribute("INDATE", typeof(int), 8, true)]
		public int StockInDate;
		
		/// <summary>
		/// 出货日期
		/// </summary>
		[FieldMapAttribute("OUTDATE", typeof(int), 8, true)]
		public int StockOutDate;
		
		/// <summary>
		/// 入库用户
		/// </summary>
		[FieldMapAttribute("INUSER", typeof(string), 40, true)]
		public string StockInUser;
		
		/// <summary>
		/// 出货用户
		/// </summary>
		[FieldMapAttribute("OUTUSER", typeof(string), 40, true)]
		public string StockOutUser;

	}

	

}
