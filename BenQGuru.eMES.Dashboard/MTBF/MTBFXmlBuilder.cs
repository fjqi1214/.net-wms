using System;

namespace BenQGuru.eMES.Dashboard
{
	public class MTBFXmlBuilder
	{
		public MTBFXmlBuilder()
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
		public void BeginBuildItem(string fieldcode,string mtbf,string totalmtbf,string ngqty)
		{
			XmlContent.Append(
				"<item fieldcode=\"" + fieldcode 
				+ "\" mtbf=\"" + mtbf 
				+ "\"  totalmtbf=\"" + totalmtbf 
				+ "\" ngqty=\"" + ngqty + "\">");
		}
		//日数据
		public void BeginBuildDateMTBF(string date,string fieldcode,string ngqty,string mtbf)
		{
			XmlContent.Append(
				"<datemtbf date=\"" + date 
				+ "\" fieldcode=\"" + fieldcode
				+ "\" ngqty=\"" + ngqty 
				+ "\" mtbf=\"" + mtbf 
				+ "\">");
		}
		//明细数据
		public void BuildDateMTBFDetail(string itemcode,string modelcode,string sscode,string sn,string ngdate,string begintime,string endtime,string mtime)
		{
			XmlContent.Append(
				"<mtbfdetail  " +
				" modelcode=\"" + modelcode +  "\"" +
				" itemcode=\"" + itemcode +  "\"" +
				" sscode=\"" + sscode +  "\"" +
				" sn=\"" + sn +  "\"" +
				" ngdate=\"" + ngdate +  "\"" +
				" begintime=\"" + begintime +  "\"" +
				" endtime=\"" + endtime +  "\"" +
				" mtime=\"" + mtime +  "\"/>");
		}

		public void EndBuildDateMTBF()
		{
			XmlContent.Append("</datemtbf>");
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
