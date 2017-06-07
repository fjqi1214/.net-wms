<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FTDCartonPrint.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FTDCartonPrint" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FTDCartonPrint</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-barcode-2.0.1.js" type="text/javascript"></script>
    <script src="js/jquery.PrintArea.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Print() {
            var jsonText = "";
           // var gird2 = document.getElementById("gridWebGrid");
            var gird = $find("gridWebGrid");
            if (gird != null) {
                var rows=gird.get_rows();
                if(rows!=null)
                    {
                      if(rows._rows!=null)
                         {
                          if(rows._rows.length>0)
                              {
                              for (var i = 0; i < rows._rows.length; i++) {
                          if (rows._rows[i].get_cell(2)._element.firstChild.alt == "Checked") {
                                var cartonno = "'"+rows._rows[i]._element.cells[3].innerText+"'";
                                jsonText = jsonText + "{'barcode':" + cartonno + ",'itemcode': " + cartonno + " },";
                              //  jsonText =jsonText+"\/" ;
                           }
                        }
                        jsonText = jsonText.substring(0, jsonText.length - 1);
                      }
                   }
               }
            }
            if (jsonText != "") {
                    jsonText = "[" +jsonText +"]";
            }

            var jsonObj = eval(jsonText);
            if (jsonObj!=null) {
            for (var i = 0; i < jsonObj.length; i++) {
                if (i != jsonObj.length - 1) {
                    jc("#bcTarget").append("<div id=\"" + "bcTarget" + i + "\" style=\"page-break-after: always;\" ><\/div>");
                } else {
                    jc("#bcTarget").append("<div id=\"" + "bcTarget" + i + "\" ><\/div>");
                }
                var appendhtml = "<br>" + "œ‰∫≈:" + jsonObj[i].itemcode + "<br>";
                jc("#bcTarget" + i).barcode(jsonObj[i].barcode, "code128", { barWidth: 1, barHeight: 20, showHRI: false });
                jc("#bcTarget" + i).html(appendhtml + $("#bcTarget" + i).html());

                 //  jc("#bcTarget" + i).append("œ‰∫≈:" + jsonObj[i].itemcode + "<br>");
              }
                jc("#bcTarget").printArea();
            }
        
        }

      
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" width="100%" runat="server" class="noprint">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblFTDCartonPrint" runat="server" CssClass="labeltopic">∂¶«≈œ‰∫≈¥Ú”°</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCartonNoQurey" runat="server">œ‰∫≈</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCartonNoQurey" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td colspan="2" nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="≤È —Ø" name="btnQuery"
                                   runat="server"/>
                        </td>
                    </tr>
                    <tr style="display: none">
                          <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtBarCodeListQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid" >
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%" >
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
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolbar id="pagerToolBar" runat="server">
                            </cc1:PagerToolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblQTY" runat="server"> ˝¡ø</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtQtyEdit" runat="server" CssClass="require" Width="130px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                MaxLength="6"></asp:TextBox>
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCreateBarCode" type="submit" value="…˙≥…Ãı¬Î" name="cmdCreateBarCode"
                                runat="server" onserverclick="cmdCreateBarCode_ServerClick" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdPrintCode" type="submit" value="¥Ú”°" name="cmdPrintCode"
                                runat="server" onserverclick="cmdPrint_ServerClick" onclick="Print() "  />
                        </td>
                           <td style="display: none">
                            <input id="biuuu_button" type="button" onclick="Print() " value="¥Ú”°" />
                        </td> 
                       <td style="display: none">
                        <div id="bcTarget"  style="width:60px;height:40px"  >
                        </div>
                       </td> 
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
