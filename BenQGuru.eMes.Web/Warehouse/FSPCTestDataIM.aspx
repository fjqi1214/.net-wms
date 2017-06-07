<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FSPCTestDataIM.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FSPCTestDataIM" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>SPC测试数据导入</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            try {
                window.location.href = "./ImportData/FExcelDataImp.aspx?itype=TESTDATA";
            } catch (err) {
                return true;
            }
        });

    </script>

</head>
<body ms_positioning="GridLayout" >
    <form id="form1" runat="server">
       
    </form>
    
</body>
</html>
