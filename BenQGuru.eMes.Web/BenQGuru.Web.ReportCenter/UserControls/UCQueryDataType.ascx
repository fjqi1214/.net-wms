<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCQueryDataType.ascx.cs" Inherits="BenQGuru.Web.ReportCenter.UserControls.UCQueryDataType" %>


<asp:Panel ID="PanelReportQueryDataType" runat="server">
    <div style="height:30px;clear:both;width:300px">
        <asp:Panel ID="PanelTitle" runat="server">            
            <div style="border:0px;float:left;width:80px;height:22px;line-height:22px;text-align:left;font-weight:bold;">
                <asp:Label ID="lblQueryDataType" runat="server" Text="µ¥Î»"></asp:Label>
            </div>
        </asp:Panel>
    </div>        
    <div style="height:25px;clear:both;width:300px">
        <asp:Panel ID="PanelReprotQueryDataType" runat="server" >            
            <div style="border:0px;float:left;width:300px;text-align:left;">                
                <asp:radiobuttonlist id="rblQueryDataType" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
            </div>
        </asp:Panel>    
    </div>
</asp:Panel>