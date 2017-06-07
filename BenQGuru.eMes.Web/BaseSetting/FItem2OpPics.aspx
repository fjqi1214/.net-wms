<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FItem2OpPics.aspx.cs" Inherits="BenQGuru.eMES.Web.BaseSetting.FItem2OpPics" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FModelMP</title>
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script type="text/javascript" language="javascript">
        function display(abc) {
            var fileName = abc.value;
            if (fileName == "")
                return;
            var exName = fileName.substr(fileName.lastIndexOf(".") + 1).toUpperCase();
            if (exName == "JPG") {
                var imgView = document.getElementById("imgView");
                //            id1.style.background = "url(" + abc.value + ")";
                imgView.innerHTML = '<div style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=\'scale\',src=\'' + fileName + '\'); width:250px; height:170px; border="0" id="zbc" aid="dd" alt="" />';
                //            id1.innerHTML='<img src='+abc.value+' />'
            }
            else {
                alert("请选择正确的图片文件");
                document.getElementById("fileUpload").value = " ";
            }
        }
        function ViewDetail() {
            if (document.getElementById("fileUpload").value != "")
                window.open("FItem2OpPicsViewLocal.htm");
        }
        function PreviewImage(fileObj, imgPreviewId, divPreviewId) {
            var allowExtention = ".jpg"; //允许上传文件的后缀名document.getElementById("hfAllowPicSuffix").value;
            var extention = fileObj.value.substring(fileObj.value.lastIndexOf(".") + 1).toLowerCase();
            var browserVersion = window.navigator.userAgent.toUpperCase();
            if (allowExtention.indexOf(extention) > -1) {
                if (fileObj.files) {//兼容chrome、火狐7+、360浏览器5.5+等，应该也兼容ie10，HTML5实现预览
                    if (window.FileReader) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById(imgPreviewId).setAttribute("src", e.target.result);
                        }
                        reader.readAsDataURL(fileObj.files[0]);
                    } else if (browserVersion.indexOf("SAFARI") > -1) {
                        alert("不支持Safari浏览器6.0以下版本的图片预览!");
                    }
                } else if (browserVersion.indexOf("MSIE") > -1) {//ie、360低版本预览
                    if (browserVersion.indexOf("MSIE 6") > -1) {//ie6
                        document.getElementById(imgPreviewId).setAttribute("src", fileObj.value);
                    } else {//ie[7-9]
                        fileObj.select();
                        if (browserVersion.indexOf("MSIE 9") > -1)
                            fileObj.blur(); //不加上document.selection.createRange().text在ie9会拒绝访问
                        var newPreview = document.getElementById(divPreviewId + "New");
                        if (newPreview == null) {
                            newPreview = document.createElement("div");
                            newPreview.setAttribute("id", divPreviewId + "New");
                            newPreview.style.width = document.getElementById(imgPreviewId).width + "px";
                            newPreview.style.height = document.getElementById(imgPreviewId).height + "px";
                            newPreview.style.border = "solid 1px #d2e2e2";
                            newPreview.style.cursor = "pointer"; 
                            newPreview.setAttribute("onclick", "ViewDetail();");
                        }
                        newPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src='" + fileObj.value + "')";
                        var tempDivPreview = document.getElementById(divPreviewId);
                        tempDivPreview.parentNode.insertBefore(newPreview, tempDivPreview);
                        tempDivPreview.style.display = "none";

                    }
                } else if (browserVersion.indexOf("FIREFOX") > -1) {//firefox
                    var firefoxVersion = parseFloat(browserVersion.toLowerCase().match(/firefox\/([\d.]+)/)[1]);
                    if (firefoxVersion < 7) {//firefox7以下版本
                        document.getElementById(imgPreviewId).setAttribute("src", fileObj.files[0].getAsDataURL());
                    } else {//firefox7.0+                    
                        document.getElementById(imgPreviewId).setAttribute("src", window.URL.createObjectURL(fileObj.files[0]));
                    }
                } else {
                    document.getElementById(imgPreviewId).setAttribute("src", fileObj.value);
                }
            } else {
                alert("仅支持" + allowExtention + "为后缀名的文件!");
                fileObj.value = ""; //清空选中文件
                if (browserVersion.indexOf("MSIE") > -1) {
                    fileObj.select();
                    document.selection.clear();
                }
                fileObj.outerHTML = fileObj.outerHTML;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">文档查询</asp:Label>
            </td>
        </tr>
        <tr height="20px">
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemlistQuery" runat="server">产品编号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 120px">
                            <cc2:SelectableTextBox ID="txtItemlistQuery" runat="server" CssClass="textbox" Width="120px"
                                Type="item"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOplistQuery" runat="server">工序</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 120px">
                            <cc2:SelectableTextBox ID="txtOplistQuery" runat="server" CssClass="textbox" Width="120px"
                                Type="operation"></cc2:SelectableTextBox>
                        </td>
                        <td width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
                                value="查 询" name="cmdQuery" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
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
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeEdit" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 120px">
                            <cc2:SelectableTextBox ID="txtItemCode" runat="server" CssClass="textbox" Width="120px"
                                Type="singleitem"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOPCodeEdit" runat="server">工序</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 120px">
                            <cc2:SelectableTextBox ID="txtOplist" runat="server" CssClass="textbox" Width="120px"
                                Type="singleFopsp"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPICOrderEdit" runat="server">图片顺序</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPICOrderEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td rowspan="4" width="25%">
                            <a id="imgPicLink" runat="server" >
                                <asp:Image ID="imgPic" Width="250px" Height="170px" runat="server"/></a>
                        </td>
                        <td rowspan="4" width="25%">
                         
                                <div id="divImgView" style="cursor:pointer;">
                                    <img ID="imageView" Width="250px" Height="170px" onclick="ViewDetail();" style="cursor:pointer;"/>
                                </div>
                      
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblPICTypeEdit" runat="server">图片类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropdownlistPICType" runat="server" Width="130px" CssClass="require"
                                DESIGNTIMEDRAGDROP="252" OnLoad="drpesopPicTypeQuery_Load" Visible="false">
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkOInstruction" runat="server" Text="操作说明"></asp:CheckBox>
                            <asp:CheckBox ID="chkMInstruction" runat="server" Text="维修说明"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPICTitle" runat="server">图片描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPICTitleEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFormFileEdit" runat="server">上传图片</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <input id="fileUpload" type="file" size="45" name="File" runat="server" onchange="PreviewImage(this,'imageView','divImgView')"
                                runat="server" />
                        </td>
                        <td>
                            <asp:TextBox Visible="false" runat="server" ID="HiddenCheckedstatus" />
                        </td>
                        <td>
                            <asp:TextBox Visible="false" runat="server" ID="HiddenPicFullName" />
                        </td>
                        <td>
                            <asp:TextBox Visible="false" runat="server" ID="HiddenPicSerial" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPICMemo" runat="server">图片备注</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="5">
                            <asp:TextBox Wrap="true" ID="txtPICMemoEdit" runat="server" CssClass="textbox" Width="390px"
                                Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td style="height: 15px">
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td width="10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </contenttemplate>
        <triggers>
        <asp:PostBackTrigger ControlID="cmdAdd"/>
        <asp:PostBackTrigger ControlID="cmdSave"/>
        </triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
