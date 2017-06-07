using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for DataLink 数据连线
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2006-05-18 9:29:18
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.DataLink
{

	#region FT
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLFT", "RCard,TestSeq")]
	public class FT : DomainObject
	{
		public FT()
		{
		}
 
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCard", typeof(string), 40, true)]
		public string  RCard;

		/// <summary>
		/// 测试序号（针对多次测试设置的字段）
		/// </summary>
		[FieldMapAttribute("TestSeq", typeof(int), 10, true)]
		public int  TestSeq;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("Itemcode", typeof(string), 40, true)]
		public string  Itemcode;

		/// <summary>
		/// 资源(设备)
		/// </summary>
		[FieldMapAttribute("Rescode", typeof(string), 40, true)]
		public string  Rescode;

		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("LineCode", typeof(string), 40, true)]
		public string  LineCode;

		/// <summary>
		/// 制具
		/// </summary>
		[FieldMapAttribute("Machinetool", typeof(string), 40, true)]
		public string  Machinetool;
		

		/// <summary>
		/// 采集人员
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 采集日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 采集时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;


		/// <summary>
		/// 测试结果
		/// </summary>
		[FieldMapAttribute("TestResult", typeof(string), 1, true)]
		public string  TestResult;

	}
	#endregion

	#region FTDetail
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLFTDetail", "RCard,TestSeq,TGroup")]
	public class FTDetail : DomainObject
	{
		public FTDetail()
		{
		}
 
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCard", typeof(string), 40, true)]
		public string  RCard;

		/// <summary>
		/// 测试序号（针对多次测试设置的字段）
		/// </summary>
		[FieldMapAttribute("TestSeq", typeof(int), 10, true)]
		public int  TestSeq;

		/// <summary>
		/// 测试组
		/// </summary>
		[FieldMapAttribute("TGroup", typeof(int), 10, true)]
		public int  TGroup;

		/// <summary>
		/// 频率(上限)
		/// </summary>
		[FieldMapAttribute("FreqUpSpec", typeof(decimal), 15, true)]
		public decimal  FreqUpSpec;

		/// <summary>
		/// 频率(下限)
		/// </summary>
		[FieldMapAttribute("FreqLowSpec", typeof(decimal), 15, true)]
		public decimal  FreqLowSpec;

		/// <summary>
		/// 频率(测试值)
		/// </summary>
		[FieldMapAttribute("Freq", typeof(decimal), 15, true)]
		public decimal  Freq;

		#region Duty_RT

		/// <summary>
		/// DUTY_RATO(上限)
		/// </summary>
		[FieldMapAttribute("DutyUpSpec", typeof(decimal), 15, false)]
		public decimal  DutyUpSpec;

		/// <summary>
		/// DUTY_RATO(下限)
		/// </summary>
		[FieldMapAttribute("DutyLowSpec", typeof(decimal), 15, false)]
		public decimal  DutyLowSpec;

		/// <summary>
		/// DUTY_RATO(测试值)
		/// </summary>
		[FieldMapAttribute("Duty_Rt", typeof(decimal), 15, false)]
		public decimal  Duty_Rt;
		#endregion

		#region Burst_MD
		/// <summary>
		/// BURST_MD(上限)
		/// </summary>
		[FieldMapAttribute("BurstUpSpec", typeof(decimal), 15, false)]
		public decimal  BurstUpSpec;

		/// <summary>
		/// BURST_MD(下限)
		/// </summary>
		[FieldMapAttribute("BurstLowSpec", typeof(decimal), 15, false)]
		public decimal  BurstLowSpec;

		/// <summary>
		/// BURST_MD(测试值)
		/// </summary>
		[FieldMapAttribute("Burst_Md", typeof(decimal), 15, false)]
		public decimal  Burst_Md;
		#endregion


		/// <summary>
		/// 电流(上限)
		/// </summary>
		[FieldMapAttribute("ACUpSpec", typeof(decimal), 15, true)]
		public decimal  ACUpSpec;

		/// <summary>
		/// 电流(下限)
		/// </summary>
		[FieldMapAttribute("ACLowSpec", typeof(decimal), 15, true)]
		public decimal  ACLowSpec;

		/// <summary>
		/// 电流1(测试值)
		/// </summary>
		[FieldMapAttribute("AC1", typeof(decimal), 15, true)]
		public decimal  AC1;

		/// <summary>
		/// 电流2(测试值)
		/// </summary>
		[FieldMapAttribute("AC2", typeof(decimal), 15, true)]
		public decimal  AC2;

		/// <summary>
		/// 电流3(测试值)
		/// </summary>
		[FieldMapAttribute("AC3", typeof(decimal), 15, true)]
		public decimal  AC3;

		/// <summary>
		/// 电流4(测试值)
		/// </summary>
		[FieldMapAttribute("AC4", typeof(decimal), 15, true)]
		public decimal  AC4;

		/// <summary>
		/// 电流5(测试值)
		/// </summary>
		[FieldMapAttribute("AC5", typeof(decimal), 15, true)]
		public decimal  AC5;

		/// <summary>
		/// 电流6(测试值)
		/// </summary>
		[FieldMapAttribute("AC6", typeof(decimal), 15, true)]
		public decimal  AC6;

		/// <summary>
		/// 电流7(测试值)
		/// </summary>
		[FieldMapAttribute("AC7", typeof(decimal), 15, true)]
		public decimal  AC7;

		/// <summary>
		/// 电流8(测试值)
		/// </summary>
		[FieldMapAttribute("AC8", typeof(decimal), 15, true)]
		public decimal  AC8;

		/// <summary>
		/// 电流9(测试值)
		/// </summary>
		[FieldMapAttribute("AC9", typeof(decimal), 15, true)]
		public decimal  AC9;

		/// <summary>
		/// 电流9(测试值)
		/// </summary>
		[FieldMapAttribute("AC10", typeof(decimal), 15, true)]
		public decimal  AC10;

		/// <summary>
		/// 电流11(测试值)
		/// </summary>
		[FieldMapAttribute("AC11", typeof(decimal), 15, true)]
		public decimal  AC11;

		/// <summary>
		/// 电流12(测试值)
		/// </summary>
		[FieldMapAttribute("AC12", typeof(decimal), 15, true)]
		public decimal  AC12;

		/// <summary>
		/// 电流13(测试值)
		/// </summary>
		[FieldMapAttribute("AC13", typeof(decimal), 15, true)]
		public decimal  AC13;

		/// <summary>
		/// 电流14(测试值)
		/// </summary>
		[FieldMapAttribute("AC14", typeof(decimal), 15, true)]
		public decimal  AC14;

		/// <summary>
		/// 电流15(测试值)
		/// </summary>
		[FieldMapAttribute("AC15", typeof(decimal), 15, true)]
		public decimal  AC15;

		/// <summary>
		/// 电流16(测试值)
		/// </summary>
		[FieldMapAttribute("AC16", typeof(decimal), 15, true)]
		public decimal  AC16;

		/// <summary>
		/// 电流17(测试值)
		/// </summary>
		[FieldMapAttribute("AC17", typeof(decimal), 15, false)]
		public decimal  AC17;

		/// <summary>
		/// 电流18(测试值)
		/// </summary>
		[FieldMapAttribute("AC18", typeof(decimal), 15, false)]
		public decimal  AC18;

		/// <summary>
		/// 电流19(测试值)
		/// </summary>
		[FieldMapAttribute("AC19", typeof(decimal), 15, false)]
		public decimal  AC19;

		/// <summary>
		/// 电流20(测试值)
		/// </summary>
		[FieldMapAttribute("AC20", typeof(decimal), 15, false)]
		public decimal  AC20;

		/// <summary>
		/// 电流21(测试值)
		/// </summary>
		[FieldMapAttribute("AC21", typeof(decimal), 15, false)]
		public decimal  AC21;

		/// <summary>
		/// 电流22(测试值)
		/// </summary>
		[FieldMapAttribute("AC22", typeof(decimal), 15, false)]
		public decimal  AC22;

		/// <summary>
		/// 电流23(测试值)
		/// </summary>
		[FieldMapAttribute("AC23", typeof(decimal), 15, false)]
		public decimal  AC23;

		/// <summary>
		/// 电流24(测试值)
		/// </summary>
		[FieldMapAttribute("AC24", typeof(decimal), 15, false)]
		public decimal  AC24;
		
		/// <summary>
		/// 电流25(测试值)
		/// </summary>
		[FieldMapAttribute("AC25", typeof(decimal), 15, false)]
		public decimal  AC25;

		/// <summary>
		/// 电流26(测试值)
		/// </summary>
		[FieldMapAttribute("AC26", typeof(decimal), 15, false)]
		public decimal  AC26;

		/// <summary>
		/// 电流27(测试值)
		/// </summary>
		[FieldMapAttribute("AC27", typeof(decimal), 15, false)]
		public decimal  AC27;

		/// <summary>
		/// 电流28(测试值)
		/// </summary>
		[FieldMapAttribute("AC28", typeof(decimal), 15, false)]
		public decimal  AC28;

		/// <summary>
		/// 电流29(测试值)
		/// </summary>
		[FieldMapAttribute("AC29", typeof(decimal), 15, false)]
		public decimal  AC29;

		/// <summary>
		/// 电流30(测试值)
		/// </summary>
		[FieldMapAttribute("AC30", typeof(decimal), 15, false)]
		public decimal  AC30;

		/// <summary>
		/// 电流31(测试值)
		/// </summary>
		[FieldMapAttribute("AC31", typeof(decimal), 15, false)]
		public decimal  AC31;
	}
	#endregion

}

