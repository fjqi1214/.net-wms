using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FImpItemRoute : FImpFormBase
    {
        public FImpItemRoute()
        {
            InitializeComponent();
        }

        protected override void ImportData(Excel.Worksheet sheet, string[] strHeader)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(ApplicationService.DataProvider);
            ImportDataEngine impEng = new ImportDataEngine(ApplicationService.DataProvider, this.configObjList, this.configMatchType, ApplicationService.LoginUserCode, dbDateTime);
            impEng.ImportDataMapping = Properties.Settings.Default.DataMappingType;
            impEng.UpdateRepeatData = Properties.Settings.Default.DataRepeat;
            ApplicationService.DataProvider.BeginTransaction();
            try
            {
                int iRow = 2;
                int iImportedCount = 0;
                while (true)
                {
                    string[] strRow = ReadExcelRow(sheet, iRow, strHeader.Length);
                    if (strRow == null)
                        break;
                    // 导入产品
                    try
                    {
                        // 产品途程主资料
                        BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(ApplicationService.DataProvider);
                        BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(ApplicationService.DataProvider);

                        BenQGuru.eMES.Domain.MOModel.Item2Route itemRoute = new BenQGuru.eMES.Domain.MOModel.Item2Route();
                        itemRoute.ItemCode = strRow[0].Trim().ToUpper();
                        itemRoute.RouteCode = strRow[1].Trim().ToUpper();
                        itemRoute.OrganizationID = int.Parse(strRow[2].Trim().ToUpper());
                        itemRoute.IsReference = "0";
                        itemRoute.MaintainUser = ApplicationService.LoginUserCode;
                        itemRoute.MaintainDate = dbDateTime.DBDate;
                        itemRoute.MaintainTime = dbDateTime.DBTime;

                        object ir = itemFacade.GetItem2Route(itemRoute.ItemCode, itemRoute.RouteCode, itemRoute.OrganizationID.ToString());
                        if (ir == null)
                        {
                            ApplicationService.DataProvider.Insert(itemRoute);
                        }
                        else
                        {
                            ApplicationService.DataProvider.Update(itemRoute);
                        }

                        // 产品默认途程
                        BenQGuru.eMES.Domain.MOModel.DefaultItem2Route defRoute = null;
                        BenQGuru.eMES.Domain.MOModel.DefaultItem2Route dr = moFacade.GetDefaultItem2Route(itemRoute.ItemCode) as BenQGuru.eMES.Domain.MOModel.DefaultItem2Route;
                        if (dr != null)
                        {
                            defRoute = dr;
                        }
                        else
                        {
                            defRoute = new BenQGuru.eMES.Domain.MOModel.DefaultItem2Route();
                        }

                        defRoute.ItemCode = itemRoute.ItemCode;
                       
                        if (strRow[3].Trim().ToUpper() == "YES")
                            defRoute.RouteCode = itemRoute.RouteCode;
                        if (defRoute.RouteCode != null && defRoute.RouteCode != "")
                        {
                            defRoute.MDate = dbDateTime.DBDate;
                            defRoute.MTime = dbDateTime.DBTime;
                            
                            if (dr == null)
                            {
                                ApplicationService.DataProvider.Insert(defRoute);
                            }
                            else
                            {
                                ApplicationService.DataProvider.Update(defRoute);
                            }
                        }

                        // 产品途程工序列表

                        //将原来的删除
                        string strSql = string.Format("delete tblitemroute2op where itemcode = '{0}' and routecode='{1}' and orgid = {2}", itemRoute.ItemCode, itemRoute.RouteCode, itemRoute.OrganizationID.ToString());
                        ApplicationService.DataProvider.CustomExecute(new SQLCondition(strSql));

                        strSql = "insert into tblitemroute2op (opid,routecode,opcode,opseq,opcontrol,idmergetype,idmergerule,muser,mdate,mtime,itemcode,orgid) ";
                        strSql += "select routecode||opcode||'{0}',routecode,opcode,opseq,opcontrol,'idmergetype_idmerge',1,muser,mdate,mtime,'{0}',{2} ";
                        strSql += "from tblroute2op where routecode='{1}' ";
                        strSql = string.Format(strSql, itemRoute.ItemCode, itemRoute.RouteCode, itemRoute.OrganizationID.ToString());
                        ApplicationService.DataProvider.CustomExecute(new SQLCondition(strSql));
                        iImportedCount++;
                    }
                    catch (Exception ex)
                    {
                        string strResult = ex.Message;
                        if (strResult == "$DATA_REPEAT_CANCEL")      // 如果是重复数据，并设置终止
                        {
                            throw new Exception(strResult);
                        }
                        else if (strResult != "$Import_Unique_Data_Exist")  // $Import_Unique_Data_Exist表示重复数据，设置忽略
                        {
                            if (Properties.Settings.Default.DataError == ImportDataEngine.OnErrorDeal.Cancel)
                                throw new Exception(strResult);
                        }
                    }
                    iRow++;
                }
                ApplicationService.DataProvider.CommitTransaction();
                MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CycleImport_Success [" + iImportedCount.ToString() + "]"), this.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ApplicationService.DataProvider.RollbackTransaction();
                MessageBox.Show(UserControl.MutiLanguages.ParserMessage(ex.Message), this.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}