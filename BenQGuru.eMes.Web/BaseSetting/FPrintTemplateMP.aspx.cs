using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FPrintTemplateMP 的摘要说明。
    /// </summary>
    public partial class FPrintTemplateMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private PrintTemplateFacade _PrintTemplateFacade;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }
        #endregion


        #region form events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region private method

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_PrintTemplateFacade == null)
            {
                _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
            }

            return this._PrintTemplateFacade.QueryPrintTemplate(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTemplateNameQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTemplateDescQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_PrintTemplateFacade == null)
            {
                _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
            }
            return this._PrintTemplateFacade.QueryPrintTemplateCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTemplateNameQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTemplateDescQuery.Text)));
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("TemplateName", "模板名称", null);
            this.gridHelper.AddColumn("TemplateDesc", "模板描述", null);
            this.gridHelper.AddColumn("TemplatePath", "模板路径", null);            
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_PrintTemplateFacade == null)
                {
                    _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
                }

                PrintTemplate printTemplate = this._PrintTemplateFacade.CreateNewPrintTemplate();

                printTemplate.TemplateName = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTemplateNameEdit.Text, 40));
                printTemplate.TemplateDesc = FormatHelper.CleanString(this.txtTemplateDescEdit.Text);
                printTemplate.TemplatePath = FormatHelper.CleanString(this.txtTemplatePathEdit.Text);
                printTemplate.MaintainUser = this.GetUserCode();
                printTemplate.EAttribute1 = " ";
                return printTemplate;
            }
            else
            {
                return null;
            }
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_PrintTemplateFacade == null)
            {
                _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("TemplateName").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _PrintTemplateFacade.GetPrintTemplate(strCode);
            if (obj != null)
            {
                return (PrintTemplate)obj;
            }
            return null;

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblTemplateNameEdit, txtTemplateNameEdit, 40, true));
            manager.Add(new LengthCheck(lblTemplateDescEdit, txtTemplateDescEdit, 100, true));
            manager.Add(new LengthCheck(lblTemplatePathEdit, txtTemplatePathEdit, 300, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (_PrintTemplateFacade == null)
            {
                _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
            }
            this._PrintTemplateFacade.AddPrintTemplate((PrintTemplate)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_PrintTemplateFacade == null)
            {
                _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
            }
            this._PrintTemplateFacade.DeletePrintTemplate((PrintTemplate[])domainObjects.ToArray(typeof(PrintTemplate)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_PrintTemplateFacade == null)
            {
                _PrintTemplateFacade = new PrintTemplateFacade(base.DataProvider);
            }
            this._PrintTemplateFacade.UpdatePrintTemplate((PrintTemplate)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtTemplateNameEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtTemplateNameEdit.ReadOnly = true;
            }
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtTemplateNameEdit.Text = string.Empty;
                this.txtTemplateDescEdit.Text = string.Empty;
                this.txtTemplatePathEdit.Text = string.Empty;

                return;
            }

            this.txtTemplateNameEdit.Text = ((PrintTemplate)obj).TemplateName;
            this.txtTemplateDescEdit.Text = ((PrintTemplate)obj).TemplateDesc;
            this.txtTemplatePathEdit.Text = ((PrintTemplate)obj).TemplatePath;
        }

        protected override DataRow GetGridRow(object obj)
        {           
            DataRow row = this.DtSource.NewRow();
            row["TemplateName"] = ((PrintTemplate)obj).TemplateName;
            row["TemplateDesc"] = ((PrintTemplate)obj).TemplateDesc;
            row["TemplatePath"] = ((PrintTemplate)obj).TemplatePath;
            row["MaintainDate"] = FormatHelper.ToDateString(((PrintTemplate)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((PrintTemplate)obj).MaintainTime);
            row["MaintainUser"] = ((PrintTemplate)obj).GetDisplayText("MaintainUser");
            return row;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {
                "TemplateName",
                "TemplateDesc",
                "TemplatePath",
                "MaintainDate",
                "MaintainTime",
                "MaintainUser"	  
            };
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{                      
                ((PrintTemplate)obj).TemplateName,
                ((PrintTemplate)obj).TemplateDesc,
                ((PrintTemplate)obj).TemplatePath,
                FormatHelper.ToDateString(((PrintTemplate)obj).MaintainDate),
                FormatHelper.ToTimeString(((PrintTemplate)obj).MaintainTime),
                //((PrintTemplate)obj).MaintainUser.ToString()
                 ((PrintTemplate)obj).GetDisplayText("MaintainUser")
            };
        }

        #endregion
    }
}
