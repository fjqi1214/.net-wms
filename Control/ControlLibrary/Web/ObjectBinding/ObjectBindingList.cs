using System;
using System.Collections; 
using System.Web; 
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection; 
using BenQGuru.eMES.Common.Domain;  

namespace ControlLibrary.Web.ObjectBinding
{
	/// <summary>
	/// WebCustomControl1 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ObjectBindingList runat=server></{0}:ObjectBindingList>")]
	public class ObjectBindingList : System.Web.UI.WebControls.WebControl
	{
		[Browsable(false)]
		public object  BindingObjects
		{
			get
			{
				return this.ViewState["BindingObjects"];
			}
			set
			{
				this.ViewState["BindingObjects"] = value;
			}
		}

		
		public void RenderBindingObjects(HtmlTextWriter output)
		{
			if (this.BindingObjects ==null)
			{
				return;
			}
			object[] objects = (object[])this.BindingObjects;
			if (objects.Length<1) 
			{
				return;
			}

			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(objects[0]);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(objects[0]); 
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();

			output.RenderBeginTag(HtmlTextWriterTag.Tr); 
			int perTd = 100/hs.Count;

			while ( myEnumerator.MoveNext())
			{
				output.AddStyleAttribute(HtmlTextWriterStyle.Width, perTd.ToString() + "%");
				output.AddAttribute(HtmlTextWriterAttribute.Align, "right");
				output.RenderBeginTag(HtmlTextWriterTag.Td); 
				output.Write( ((MemberInfo)myEnumerator.Value).Name); 
				output.RenderEndTag(); 
			}
			output.RenderEndTag(); 

			for(int j=0;j<objects.Length;j++)
			{		
				IDictionaryEnumerator myEnumerator2 = hs.GetEnumerator();
				output.RenderBeginTag(HtmlTextWriterTag.Tr); 
				while ( myEnumerator2.MoveNext())
				{
					Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;
					output.AddStyleAttribute(HtmlTextWriterStyle.Width, perTd.ToString() + "%");
					output.AddAttribute(HtmlTextWriterAttribute.Align, "right");
					output.RenderBeginTag(HtmlTextWriterTag.Td); 
					//output.Write(DomainObjectUtility.XMLEncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType, type1, DomainObjectUtility.GetValue(objects[j] , ((MemberInfo)myEnumerator2.Value), null))); 
					output.RenderEndTag(); 
				}
				output.RenderEndTag(); 
			}
		}

		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
			output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
			output.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
			output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			output.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, this.Font.Name);
			output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, this.Font.Size.ToString());
			output.RenderBeginTag(HtmlTextWriterTag.Table);
			this.RenderBindingObjects(output);
			output.RenderEndTag();
		}
	}
}
