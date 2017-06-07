using System;

namespace BenQGuru.eMES.Dashboard
{
	public class MPOCRXmlBuilder
	{
		public MPOCRXmlBuilder()
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
		public void BeginBuildItem(string itemType,string moqty,string totalmoqty,string percent)
		{
			XmlContent.Append(
				"<item itemtype=\"" + itemType 
				+ "\" moqty=\"" + moqty 
				+ "\"  totalmoqty=\"" + totalmoqty 
				+ "\" scale=\"" + percent + "\">");
		}
		//日数据
		public void BeginBuildDateMo(string date,string qty)
		{
			XmlContent.Append(
				"<datemo date=\"" + date 
				+ "\" moqty=\"" + qty 
				+ "\">");
		}
		//明细数据
		public void BuildDateMoDetail(string itemcode,string mocode,string plancompletedate,string actcompletedate,string outputqty)
		{
			XmlContent.Append(
				"<shipdetail " +
				" itemcode=\"" + itemcode +  "\"" +
				" mocode=\"" + mocode +  "\"" +
				" plancompletedate=\"" + plancompletedate +  "\"" +
				" actcompletedate=\"" + actcompletedate +  "\"" +
				" outputqty=\"" + outputqty +  "\"/>");
		}

		public void EndBuildDateMo()
		{
			XmlContent.Append("</datemo>");
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
