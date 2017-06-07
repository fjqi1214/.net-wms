<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FInvPeriodMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FInvPeriodMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FInvPeriodMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>

<script language="javascript">
	function CheckNumber()
	{   
        var dateFromPeriod = document.getElementById("txtDateFromPeriodEdit");
        var dateFromPeriodValue = dateFromPeriod.value;
        
        var dateToPeriod = document.getElementById("txtDateToPeriodEdit");
        var dateToPeriodValue = dateToPeriod.value;
        
        for(var i = 0; i < dateFromPeriodValue.length; i ++)   
        {   
            if(isNaN(parseInt(dateFromPeriodValue.substr(i,1))))
            {   
                alert("起止天必须为数字格式, 并且大于等于零");
                document.getElementById("txtDateFromPeriodEdit").focus();
                return false;  
            }
        }
        
        for(var j = 0; j < dateToPeriodValue.length; j ++)   
        {   
            if(isNaN(parseInt(dateToPeriodValue.substr(j,1))))
            {   
                alert("截止天必须为数字格式, 并且大于等于零");
                document.getElementById("txtDateToPeriodEdit").focus();
                return false;  
            }
        }
        
        return true;

    }
</script>

<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table height="100%" width="100%" runat="server">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">账龄设定</asp:Label></td>
            </tr>
            <tr height="100%">
               <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
            </tr>
            <tr class="normal">
                <td>
                    <table height="100%" cellpadding="0" width="100%">
                        <tr>
                            
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                    runat="server">
                                
                            </td>
                            <td class="smallImgButton">
                                <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                    runat="server">
                                
                            </td>
                            <td>
                                <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                            </td>
                            <td align="right">
                                <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="normal">
                <td>
                    <table class="edit" height="100%" cellpadding="0" width="100%">
                        <tr>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblPeriodCodeEdit" runat="server">账龄代码</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtPeriodCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblPeiodGroupEdit" runat="server">账龄组</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtPeiodGroupEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                            <td colspan="2" style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblDateFromPeriodEdit" runat="server">起止天</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtDateFromPeriodEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblDateToPeriodEdit" runat="server">截止天</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtDateToPeriodEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="toolBar">
                <td>
                    <table class="toolBar">
                        <tr>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                    runat="server" onclick="return CheckNumber();" /></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                    runat="server" onclick="return CheckNumber();" /></td>
                            <td>
                                <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                    runat="server"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
