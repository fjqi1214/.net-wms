using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.Web.ReportView
{
    public partial class ReportSecurity : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.imgSelect.Attributes.Add("onclick", "MoveUserGroup('" + this.lstUnSelectedUserGroup.ClientID + "','" + this.lstSelectedUserGroup.ClientID + "')");
            this.imgUnSelect.Attributes.Add("onclick", "MoveUserGroup('" + this.lstSelectedUserGroup.ClientID + "','" + this.lstUnSelectedUserGroup.ClientID + "')");
        }

        /// <summary>
        /// 初始化用户组数据
        /// </summary>
        /// <param name="userGroupList">所有的用户组列表，UserGroup数组</param>
        /// <param name="selectedList">已选择的用户组列表，RptViewReportSecurity数组</param>
        public void InitData(object[] userGroupList, object[] selectedList)
        {
            this.lstUnSelectedUserGroup.Items.Clear();
            this.lstSelectedUserGroup.Items.Clear();

            if (userGroupList == null || userGroupList.Length == 0)
                return;
            List<string> selectedUserGroup = new List<string>();
            if (selectedList != null && selectedList.Length > 0)
            {
                for (int i = 0; i < selectedList.Length; i++)
                {
                    RptViewReportSecurity rptSecurity = (RptViewReportSecurity)selectedList[i];
                    selectedUserGroup.Add(rptSecurity.UserGroupCode);
                }
            }

            for (int i = 0; i < userGroupList.Length; i++)
            {
                UserGroup ugroup = (UserGroup)userGroupList[i];
                if (selectedUserGroup.Contains(ugroup.UserGroupCode) == false)
                {
                    this.lstUnSelectedUserGroup.Items.Add(new ListItem(ugroup.UserGroupDescription, ugroup.UserGroupCode));
                }
                else
                {
                    this.lstSelectedUserGroup.Items.Add(new ListItem(ugroup.UserGroupDescription, ugroup.UserGroupCode));
                }
            }
            this.hidSelectedValue.Value = string.Join(";", selectedUserGroup.ToArray());
        }

        /// <summary>
        /// 初始化功能组数据
        /// </summary>
        /// <param name="functionGroupList">所有的功能组列表，FunctionGroup数组</param>
        /// <param name="selectedList">已选择的功能组列表，RptViewReportSecurity数组</param>
        public void InitFunctionGroupData(object[] functionGroupList, object[] selectedList)
        {
            this.lstUnSelectedUserGroup.Items.Clear();
            this.lstSelectedUserGroup.Items.Clear();

            if (functionGroupList == null || functionGroupList.Length == 0)
                return;
            List<string> selectedFunctionGroup = new List<string>();
            if (selectedList != null && selectedList.Length > 0)
            {
                for (int i = 0; i < selectedList.Length; i++)
                {
                    RptViewReportSecurity rptSecurity = (RptViewReportSecurity)selectedList[i];
                    selectedFunctionGroup.Add(rptSecurity.FunctionGroupCode);
                }
            }

            for (int i = 0; i < functionGroupList.Length; i++)
            {
                FunctionGroup ugroup = (FunctionGroup)functionGroupList[i];
                if (selectedFunctionGroup.Contains(ugroup.FunctionGroupCode) == false)
                {
                    this.lstUnSelectedUserGroup.Items.Add(new ListItem(ugroup.FunctionGroupDescription, ugroup.FunctionGroupCode));
                }
                else
                {
                    this.lstSelectedUserGroup.Items.Add(new ListItem(ugroup.FunctionGroupDescription, ugroup.FunctionGroupCode));
                }
            }
            this.hidSelectedValue.Value = string.Join(";", selectedFunctionGroup.ToArray());
        }

        /// <summary>
        /// 选择的用户组列表，返回用户组代码数组
        /// </summary>
        public string[] SelectedUserGroup
        {
            get
            {
                string strSel = hidSelectedValue.Value;
                if (strSel.EndsWith(";") == true)
                    strSel = strSel.Substring(0, strSel.Length - 1);
                if (strSel == "")
                    return new string[0];
                return strSel.Split(';');
            }
        }

        public void SetSelectedItem(string[] selectedItem)
        {
            if (selectedItem == null || selectedItem.Length == 0)
                return;
            string strVal = this.hidSelectedValue.Value;
            if (strVal.EndsWith(",") == false)
                strVal += ",";
            for (int i = 0; i < selectedItem.Length; i++)
            {
                ListItem item = this.lstUnSelectedUserGroup.Items.FindByValue(selectedItem[i]);
                if (item != null)
                {
                    this.lstSelectedUserGroup.Items.Add(new ListItem(item.Text, item.Value));
                    this.lstUnSelectedUserGroup.Items.Remove(item);
                    strVal += selectedItem[i] + ",";
                }
            }
            this.hidSelectedValue.Value = strVal;
        }

    }
}