<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDisplayConditions.ascx.cs" Inherits="BenQGuru.Web.ReportCenter.UserControls.UCDisplayConditions" %>
                    		
<asp:Panel ID="PanelDisplayConditions" runat="server">
    <div style="height:30px;clear:both;width:300px">
        <asp:Panel ID="PanelTitle" runat="server">            
            <div style="border:0px;float:left;width:80px;height:22px;line-height:22px;text-align:left;font-weight:bold;">
                <asp:Label ID="lblDisplayConditions" runat="server" Text="ÏÔÊ¾¸ñÊ½"></asp:Label>
            </div>
        </asp:Panel>
    </div>        
    <div style="height:25px;clear:both;width:300px">
        <asp:Panel ID="PanelReportDisplayType" runat="server" >            
            <div style="border:0px;float:left;width:300px;text-align:left;">                
                <asp:radiobuttonlist id="rblReportDisplayType" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
            </div>
        </asp:Panel>    
    </div>
</asp:Panel>