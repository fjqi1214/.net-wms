<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGroupConditions.ascx.cs" Inherits="BenQGuru.Web.ReportCenter.UserControls.UCGroupConditions" %>

<!-- script type="text/javascript">   
	function onCheckChange(thisControl)
	{
	    if (document.getElementById(thisControl).checked)
	    {
		    document.all.divCompareTypeGroup.style.display = "block";
		}
		else
	    {
	        document.all.divCompareTypeGroup.style.display = "none";
	    }
	}
	function loadCheckStatus(thisControl)
	{
	    if (document.getElementById(thisControl).checked)
	    {
		    document.all.divCompareTypeGroup.style.display = "block";
		}
		else
	    {
	        document.all.divCompareTypeGroup.style.display = "none";
	    }		    
	}
</script -->
		
<asp:Panel ID="PanelGroupConditions" runat="server">
    <div style="height:30px;clear:both;width:960px">
        <asp:Panel ID="PanelTitle" runat="server">            
            <div style="border:0px;float:left;width:80px;height:22px;line-height:22px;text-align:left;font-weight:bold;">
                <asp:Label ID="lblGroupConditions" runat="server" Text="统计维度"></asp:Label>
            </div>
        </asp:Panel>
    </div>        
    <div style="overflow:auto;height:auto;clear:both;width:960px">
        <asp:Panel ID="PanelByTime" runat="server" >    
            <div style="border:0px;float:left;width:80px;height:22px;line-height:22px;text-align:left;">
                <asp:Label ID="lblByTime" runat="server" Text="按时间"></asp:Label>
            </div>            
            <div style="border:0px;float:left;width:400px;height:22px;text-align:left;">                
                <asp:radiobuttonlist id="rblByTimeTypeGroup" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelByCompareType" runat="server" >       
            <div style="border:0px;float:left;width:60px;height:22px;text-align:left;margin-top:3px;">                
                <asp:CheckBox id="chbCompareGroup" runat="server" Text="比较" AutoPostBack="true"></asp:CheckBox> 
            </div>
            <div id="divCompareTypeGroup" style="border:0px;float:left;width:180px;height:22px;text-align:left;"> 
                <asp:radiobuttonlist id="rblCompareTypeGroup" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
            </div>
            <div style="border:0px;float:left;width:40px;height:22px;text-align:left;">                
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelByCompleteType" runat="server" >              
            <div style="border:0px;float:left;width:180px;height:22px;text-align:left;">                
                <asp:radiobuttonlist id="rblCompleteTypeGroup" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
            </div> 
        </asp:Panel> 
    </div>   
    <div style="overflow:auto;height:auto;clear:both;width:970px">
        <asp:Panel ID="PanelByConditions0" runat="server" >    
            <div style="border:0px;float:left;width:70px;height:25px;line-height:22px;text-align:left;">
                <asp:Label ID="lblByConditionsRequired" runat="server" Text="按条件（必选）"></asp:Label>
            </div>                        
            <div style="border:0px;float:left;width:880px;height:25px;line-height:22px;text-align:left;"> 
                <asp:CheckBox id="chbBigSSCodeGroupRequired" runat="server" Text="大线"></asp:CheckBox>
                <asp:CheckBox id="chbSegCodeGroupRequired" runat="server" Text="工段"></asp:CheckBox>
                <asp:CheckBox id="chbSSCodeGroupRequired" runat="server" Text="生产线"></asp:CheckBox>
                <asp:CheckBox id="chbOPCodeGroupRequired" runat="server" Text="工序"></asp:CheckBox>
                <asp:CheckBox id="chbResCodeGroupRequired" runat="server" Text="资源"></asp:CheckBox>
            </div>
        </asp:Panel>    
    </div>    
    <div style="height:25px;clear:both;width:970px">
        <asp:Panel ID="PanelByConditions1" runat="server" >    
            <div style="border:0px;float:left;width:70px;height:22px;line-height:22px;text-align:left;">
                <asp:Label ID="lblByConditions" runat="server" Text="按条件"></asp:Label>
            </div>                
            <div style="border:0px;float:left;width:900px;height:auto;line-height:22px;text-align:left;">                
                <asp:CheckBox id="chbGoodSemiGoodGroup" runat="server" Text="成品/半成品"></asp:CheckBox>
                <asp:CheckBox id="chbInspectorGroup" runat="server" Text="人员"></asp:CheckBox>
                <asp:CheckBox id="chbItemCodeGroup" runat="server" Text="产品代码"></asp:CheckBox>
                <asp:CheckBox id="chbItemTypeGroup" runat="server" Text="产品别"></asp:CheckBox>
                <asp:CheckBox id="chbProjectName" runat="server" Text="项目代码"></asp:CheckBox>
                <asp:CheckBox id="chbMaterialModelCodeGroup" runat="server" Text="整机机型"></asp:CheckBox>
                <asp:CheckBox id="chbMaterialMachineTypeGroup" runat="server" Text="整机机芯"></asp:CheckBox>
                <asp:CheckBox id="chbMaterialExportImportGroup" runat="server" Text="内销/出口"></asp:CheckBox>   
                <asp:CheckBox id="chbLotNoGroup" runat="server" Text="批号"></asp:CheckBox>    
                <asp:CheckBox id="chbProductionTypeGroup" runat="server" Text="生产类型"></asp:CheckBox> 
                <asp:CheckBox id="chbOQCLotTypeGroup" runat="server" Text="批类型"></asp:CheckBox>          
                <asp:CheckBox id="chbMOCodeGroup" runat="server" Text="工单"></asp:CheckBox>
                <asp:CheckBox id="chbMOMemoGroup" runat="server" Text="工单别"></asp:CheckBox>
                <asp:CheckBox id="chbNewMassGroup" runat="server" Text="新品/量产品"></asp:CheckBox>                
                <asp:CheckBox id="chbCrewCodeGroup" runat="server" Text="班组"></asp:CheckBox>                
                <asp:CheckBox id="chbFirstClassGroup" runat="server" Text="一级分类"></asp:CheckBox>
                <asp:CheckBox id="chbSecondClassGroup" runat="server" Text="二级分类"></asp:CheckBox>
                <asp:CheckBox id="chbThirdClassGroup" runat="server" Text="三级分类"></asp:CheckBox>
                <asp:CheckBox id="chbFacCodeGroup" runat="server" Text="车间" AutoPostBack="true"></asp:CheckBox>
                <asp:Label ID="lblSplitter1" runat="server" Text="|" ForeColor="#999999"></asp:Label>
                <asp:CheckBox id="chbBigSSCodeGroup" runat="server" Text="大线"></asp:CheckBox>
                <asp:Label ID="lblSplitter2" runat="server" Text="|" ForeColor="#999999"></asp:Label>
                <asp:CheckBox id="chbSegCodeGroup" runat="server" Text="工段"></asp:CheckBox>
                <asp:CheckBox id="chbSSCodeGroup" runat="server" Text="生产线"></asp:CheckBox>
                <asp:Label ID="lblSplitter3" runat="server" Text="|" ForeColor="#999999"></asp:Label>
                <asp:CheckBox id="chbOPCodeGroup" runat="server" Text="工序"></asp:CheckBox>
                <asp:CheckBox id="chbResCodeGroup" runat="server" Text="资源"></asp:CheckBox>
                <asp:CheckBox id="chbExceptionCodeGroup" runat="server" Text="异常事件代码"></asp:CheckBox>
                <asp:CheckBox id="chbIQCItemTypeWhere" runat="server" Text="单物料类型"></asp:CheckBox>
                <asp:CheckBox id="chbIQCLineItemTypeWhere" runat="server" Text="行项目物料类型"></asp:CheckBox>                
                <asp:CheckBox id="chbRoHSWhere" runat="server" Text="免检/非免检RoHS"></asp:CheckBox>
                <asp:CheckBox id="chbConcessionWhere" runat="server" Text="让步接收"></asp:CheckBox>
                <asp:CheckBox id="chbVendorCodeWhere" runat="server" Text="供应商代码"></asp:CheckBox>
                <asp:CheckBox id="chbMaterialCodeGroup" runat="server" Text="物料代码"></asp:CheckBox> 
                <asp:radiobuttonlist id="rblExceptionOrDuty" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
            </div>
        </asp:Panel> 
        <asp:Panel ID="PanelManHour" runat="server" >  
            <div style="border:0px;float:left;width:70px;height:22px;line-height:22px;text-align:left;">
            </div>    
            <div style="border:0px;float:left;width:160px;height:22px;text-align:left;margin-top:3px;">                
                <asp:CheckBox id="chbExcludeReworkOutput" runat="server" Text="去除返工产出" Checked="false"></asp:CheckBox> 
            </div>          
            <div style="border:0px;float:left;width:160px;height:22px;text-align:left;margin-top:3px;">                
                <asp:CheckBox id="chbExcludeLostManHour" runat="server" Text="去除无效工时" Checked="false"></asp:CheckBox> 
            </div>
            <div style="border:0px;float:left;width:160px;height:22px;text-align:left;margin-top:3px;">                
                <asp:CheckBox id="chbIncludeIndirectManHour" runat="server" Text="包含间接人力工时" Checked="true"></asp:CheckBox> 
            </div>
        </asp:Panel> 
    </div>
</asp:Panel>