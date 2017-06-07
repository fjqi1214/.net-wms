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
	/// ObjectBinding 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"),
		ToolboxData("<{0}:ObjectBinding runat=server></{0}:ObjectBinding>")]
	public class ObjectBinding : System.Web.UI.WebControls.WebControl
	{
		public ObjectBinding()
		{
		}

		internal ObjectBinding(HtmlTextWriterTag tag) : base(tag)
		{
		}

		[Browsable(false)]
		public object  BindingObject
		{
			get
			{
				return this.ViewState["BindingObject"];
			}
			set
			{
				this.ViewState["BindingObject"] = value;
			}
		}
		
		[Browsable(false)]
		public virtual int Columns
		{
			get
			{
				object obj1 = this.ViewState["Columns"];
				if (obj1 != null)
				{
					return (int) obj1;
				}
				return 1;
			}
			set
			{
				this.ViewState["Columns"] = value;
			}
		}

		public void RenderBindingObject(HtmlTextWriter output)
		{
			if (this.BindingObject ==null)
			{
				return;
			}
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(this.BindingObject);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(this.BindingObject); 
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			int cols = 0;
			int perTd = 100/(this.Columns * 2);
			while ( myEnumerator.MoveNext())
			{
				if ((cols % this.Columns) ==0)
				{
					if (cols != 0)
					{
						output.RenderEndTag(); 
					}
					output.RenderBeginTag(HtmlTextWriterTag.Tr);
				}

				output.AddStyleAttribute(HtmlTextWriterStyle.Width, perTd.ToString() + "%");
				output.AddAttribute(HtmlTextWriterAttribute.Align, "right");
				output.RenderBeginTag(HtmlTextWriterTag.Td); 

				System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
				lbl.ID		= "lbl" + ((FieldMapAttribute)myEnumerator.Key).FieldName;
				lbl.Text	= ((MemberInfo)myEnumerator.Value).Name;
				lbl.RenderControl(output);    
				output.RenderEndTag(); 

				output.AddStyleAttribute(HtmlTextWriterStyle.Width, perTd.ToString() + "%");
				output.AddAttribute(HtmlTextWriterAttribute.Align, "left");
				output.RenderBeginTag(HtmlTextWriterTag.Td); 
				
				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

				System.Web.UI.WebControls.TextBox  txtBox = new System.Web.UI.WebControls.TextBox();
				txtBox.ID		= "txt" + ((FieldMapAttribute)myEnumerator.Key).FieldName;
				//txtBox.Text		= DomainObjectUtility.XMLEncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(this.BindingObject , ((MemberInfo)myEnumerator.Value), null));
				txtBox.RenderControl(output); 
				output.RenderEndTag(); 
				cols = cols +1;
			}

			if (((cols-1) % this.Columns) !=0)
			{
				if(cols>0)
				{
					output.RenderEndTag(); 
				}
			}
		}

		public object GetViewBindingObject()
		{
			object obj = this.BindingObject;
			if (obj  ==null)
			{
				return null;
			}

			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(obj);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(obj); 
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				string controlText = null;  
				controlText = this.Page.Request.Form["txt" + ((FieldMapAttribute)myEnumerator.Key).FieldName]; 
				if (controlText!=null)
				{
					MemberInfo info1 = (MemberInfo)myEnumerator.Value;
					Type type1 = (info1 is FieldInfo) ? ((FieldInfo) info1).FieldType : ((PropertyInfo) info1).PropertyType;
					DomainObjectUtility.SetValue(obj, info1, controlText, null);
				}
			}
			return obj;
		}

		public void UpdateBindingViewObject()
		{
			this.BindingObject = this.GetViewBindingObject(); 
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
			this.RenderBindingObject(output);
			output.RenderEndTag();
		}
	}
}
