using System;

namespace BenQGuru.eMES.Dashboard
{
	public class MTTRXmlBuilder
	{
		public MTTRXmlBuilder()
		{
			XmlContent.Insert(0,"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
		}

		public System.Text.StringBuilder XmlContent  = new System.Text.StringBuilder();
		//数据根节点
		public void BeginBuildRoot()
		{
			XmlContent.Append("<data>");
		}
		//数据项
		public void BeginBuildItem(string fieldcode,string mttr,string totalmttr,string tsqty)
		{
			XmlContent.Append(
				"<item fieldcode=\"" + fieldcode 
				+ "\" mttr=\"" + mttr 
				+ "\"  totalmttr=\"" + totalmttr 
				+ "\" tsqty=\"" + tsqty + "\">");
		}
		//日数据
		public void BeginBuildDateMTTR(string date,string fieldcode,string tsqty,string mttr)
		{
			XmlContent.Append(
				"<datemttr date=\"" + date 
				+ "\" fieldcode=\"" + fieldcode
				+ "\" tsqty=\"" + tsqty 
				+ "\" mttr=\"" + mttr 
				+ "\">");
		}
		//明细数据
		public void BuildDateMTTRDetail(string itemcode,string modelcode,string resourcecode,string sn,string confirmdate,string completedate,string ttr)
		{
			XmlContent.Append(
				"<mttrdetail  " +
				" modelcode=\"" + modelcode +  "\"" +
				" itemcode=\"" + itemcode +  "\"" +
				" resourcecode=\"" + resourcecode +  "\"" +
				" sn=\"" + sn +  "\"" +
				" confirmdate=\"" + confirmdate +  "\"" +
				" completedate=\"" + completedate +  "\"" +
				" ttr=\"" + ttr +  "\"/>");
		}

		public void EndBuildDateMTTR()
		{
			XmlContent.Append("</datemttr>");
		}

		public void EndBuildItem()
		{
			XmlContent.Append("</item>");
		}

		public void EndBuildRoot()
		{
			XmlContent.Append("</data>");
		}
	}
}
