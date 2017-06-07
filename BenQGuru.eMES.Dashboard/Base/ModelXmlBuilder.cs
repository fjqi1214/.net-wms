using System;

namespace BenQGuru.eMES.Dashboard
{
	public class ModelXmlBuilder
	{
		public ModelXmlBuilder()
		{
			XmlContent.Insert(0,"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
		}

		public System.Text.StringBuilder XmlContent  = new System.Text.StringBuilder();
		//数据根节点
		public void BeginBuildRoot()
		{
			XmlContent.Append("<data>");
		}
		//机种数据项
		public void BeginBuildModel(string label,string data)
		{
			XmlContent.Append(
				"<model label=\"" + label 
				+ "\" data=\"" + data +	"\">");
		}
		//产品数据
		public void BeginBuildItem(string label,string data)
		{
			XmlContent.Append(
				"<item label=\"" + label 
				+ "\" data=\"" + data 
				+ "\"/>");
		}

		public void EndBuildModel()
		{
			XmlContent.Append("</model>");
		}

		public void EndBuildRoot()
		{
			XmlContent.Append("</data>");
		}
	}
}
