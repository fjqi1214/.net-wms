using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Domain.Warehouse;


namespace BenQGuru.eMES.Client
{
    public partial class FMaterialMSD : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public FMaterialMSD()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private void FMATERIALMSD_Load(object sender, System.EventArgs e)
        {
            //this.txtMLot.TextFocus(false, true);
            this.txtMLot.Select();
            //this.InitPageLanguage();
        }

        private void txtMLot_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string mLot = txtMLot.Value.ToUpper().Trim();
                if (mLot == String.Empty)
                {
                    this.ErrorMessage("$CS_MLot_Is_Empty");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;

                }
                //A.检查物料代码是否为湿敏物料。
                InventoryFacade _facade = new InventoryFacade(this.DataProvider);

                int Count = _facade.CheckMaterialByLotNO(mLot);
                if (Count == 0)
                {
                    this.ErrorMessage("$CS_MCode_IsNot_MSD");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }

                //B.检查物料批号是否存在。
                ItemLotForQuery itemLotForQuery = (ItemLotForQuery)_facade.GetItemLotDesc(mLot);
                if (itemLotForQuery == null)
                {
                    this.ErrorMessage("$CS_MLotInfo_IsNot_Exit");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }

                //C.带出物料批号的相关信息,第一次做拆封时TBLMSDLOT不存在数据。
                object objs = _facade.QueryMaterialMSD(mLot);

                this.txtMCode.Text = objs == null ? itemLotForQuery.Mcode : ((MSDLOTLExc)objs).MCODE.ToString();
                this.txtMName.Text = objs == null ? itemLotForQuery.MaterialName : ((MSDLOTLExc)objs).MNAME.ToString();
                this.txtMDesc.Text = objs == null ? itemLotForQuery.MaterialDescription : ((MSDLOTLExc)objs).MDESC.ToString();
                this.txtStatus.Text = objs == null ? MutiLanguages.ParserString("$CS_Package") : MSDStatus(((MSDLOTLExc)objs).Status.ToString());
                this.txtOverFloorlife.Text = string.Empty;
                if (objs == null)
                {
                    object msdLevel = _facade.GetMSDLevelByLotNo(mLot.Trim().ToUpper());
                    if (msdLevel != null)
                    {
                        this.txtOverFloorlife.Text = ((MSDLevel)msdLevel).FloorLife.ToString();
                    }
                }
                else
                {
                    this.txtOverFloorlife.Text = ((MSDLOTLExc)objs).OverFloorlife.ToString();
                }
                this.txtMUser.Text = ApplicationService.Current().UserCode;
                this.txtMDate.Text = objs == null ? "" : FormatHelper.ToDateString(((MSDLOTLExc)objs).MaintainDate);
                this.txtMtime.Text = objs == null ? "" : FormatHelper.ToTimeString(((MSDLOTLExc)objs).MaintainTime);

                #region IniultraOptionSetOperation

                if (objs == null)
                {
                    this.ultraOptionSetOperation.CheckedIndex = 1;
                }
                else if (((MSDLOTLExc)objs).Status.ToString() == "MSD_PACKAGE")
                {
                    this.ultraOptionSetOperation.CheckedIndex = 0;
                }
                else if (((MSDLOTLExc)objs).Status.ToString() == "MSD_OPENED")
                {
                    this.ultraOptionSetOperation.CheckedIndex = 1;
                }
                else if (((MSDLOTLExc)objs).Status.ToString() == "MSD_ALLUSED")
                {
                    this.ultraOptionSetOperation.CheckedIndex = 2;
                    this.ErrorMessage("$CS_MSD_ALLUSED_Info");  //提示“该批湿敏原料已全部使用，不可操作。”
                }
                //else if (((MSDLOTLExc)objs).Status.ToString() == "MSD_USING")
                //{
                //    this.ultraOptionSetOperation.CheckedIndex = 2;
                //}
                else if (((MSDLOTLExc)objs).Status.ToString() == "MSD_DRYING")
                {
                    this.ultraOptionSetOperation.CheckedIndex = 3;
                }
                else if (((MSDLOTLExc)objs).Status.ToString() == "MSD_BAKING")
                {
                    this.ultraOptionSetOperation.CheckedIndex = 4;
                }

                #endregion

                this.ultraOptionSetOperation.Focus();
            }
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            decimal floorlife = 0;
            decimal decRealFloorlife = 0;
            decimal overFloorlife = 0;
            int dryingTime = 0;
            string status = string.Empty;
            int dateInWip = 0;
            int timeInWip = 0;
            bool blnexit = false;

            string mLot = txtMLot.Value.ToUpper().Trim();
            if (mLot == String.Empty)
            {
                this.ErrorMessage("$CS_MLot_Is_Empty");
                this.txtMLot.TextFocus(false, true);
                ClearInfo();
                return;
            }

            //得到时间
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            int time = dbDateTime.DBTime;


            //查询TBLMSDLOT表状态
            InventoryFacade _facade = new InventoryFacade(this.DataProvider);
            object obj = _facade.GetMSDLot(mLot);
            if (obj != null)
            {
                status = ((MSDLOT)obj).Status;
                floorlife = ((MSDLOT)obj).Floorlife;
                overFloorlife = ((MSDLOT)obj).OverFloorlife;
                blnexit = true;
            }


            #region MSD_PACKAGE：封装
            //B.	选择封装：只有状态为“烘烤箱烘烤”才能进行此操作，TBLMSDLOT表中剩余时间等于有效车间寿命，
            //状态更新为“封装”。TBLMSDWIP需要插入两笔数据，状态分别为“出烘烤箱”和“封装”。

            if (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_PACKAGE"))
            {

                if (status == "MSD_BAKING")
                {
                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_PACKAGE";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = floorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);


                    //添加TBLMSDWIP--MSD_OUTBAKING:出烘烤箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_OUTBAKING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    //添加TBLMSDWIP--MSD_PACKAGE：封装
                    MSDWIP _msdwip = new MSDWIP();
                    _msdwip.serial = 0;
                    _msdwip.LotNo = mLot;
                    _msdwip.Status = "MSD_PACKAGE";
                    _msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    _msdwip.MaintainDate = date;
                    _msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(_msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }
                else
                {
                    this.ErrorMessage("$CS_Status_IsNot_MSD_BAKING");   //只有状态为“烘烤箱烘烤”才能进行此操作
                    this.txtMLot.TextFocus(false, true);
                    // ClearInfo();
                    return;
                }

            }
            #endregion

            #region MSD_OPENED:拆封
            //C.拆封未使用：状态必须为“干燥箱干燥”、“烘烤箱烘烤”或者在表TBLMSDLOT未存在记录（默认为封装）。  
            else if (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_OPENED"))
            {
                #region 干燥箱干燥
                //检查（当前时间减去TBLMSDWIP中最近一笔“进干燥箱”的时间差）与物料湿敏等级中维护的干燥时间比较，
                //如果小于维护值，则不允许出干燥箱。大于等于则TBLMSDLOT更新状态即可，剩余车间寿命不变。
                //TBLMSDWIP需要插入两笔数据，状态分别为“出干燥箱”和“拆封未使用”。
                if (status == "MSD_DRYING")
                {
                    object msdWip = _facade.GetMSDWIP(mLot, "MSD_INDRYING");
                    if (msdWip != null)
                    {
                        dateInWip = ((MSDWIP)msdWip).MaintainDate;
                        timeInWip = ((MSDWIP)msdWip).MaintainTime;
                    }
                    string dtInWip = FormatHelper.ToDateString(dateInWip) + " " + FormatHelper.ToTimeString(timeInWip);


                    object msdLever = _facade.GetMSDLevelByLotNo(mLot);
                    if (msdLever != null)
                    {
                        dryingTime = ((MSDLevel)msdLever).DryingTime;
                        //int timeInWip = ((MSDLevel)msdLever).FloorLife;
                    }

                    DateTime dt1 = DateTime.Parse(FormatHelper.ToDateString(date) + " " + FormatHelper.ToTimeString(time));
                    DateTime dt2 = DateTime.Parse(dtInWip);
                    TimeSpan ts = dt1 - dt2;
                    if (ts.TotalHours >= dryingTime)
                    {
                        //TBLMSDLOT更新状态MSD_OPENED:拆封即可，剩余车间寿命不变。TBLMSDWIP需要插入两笔数据，状态分别为“出干燥箱”和“拆封未使用”。

                        //更新TBLMSDLOT
                        MSDLOT msdLot = new MSDLOT();
                        msdLot.LotNo = mLot;
                        msdLot.Status = "MSD_OPENED";
                        msdLot.Floorlife = floorlife;
                        msdLot.OverFloorlife = overFloorlife;
                        msdLot.MaintainUser = ApplicationService.Current().UserCode;
                        msdLot.MaintainDate = date;
                        msdLot.MaintainTime = time;

                        _facade.UpdateMSDLOT(msdLot);


                        //添加TBLMSDWIP--MSD_OUTDRYING：出干燥箱
                        MSDWIP msdwip = new MSDWIP();
                        msdwip.serial = 0;
                        msdwip.LotNo = mLot;
                        msdwip.Status = "MSD_OUTDRYING";
                        msdwip.MaintainUser = ApplicationService.Current().UserCode;
                        msdwip.MaintainDate = date;
                        msdwip.MaintainTime = time;

                        _facade.AddMSDWIP(msdwip);

                        //添加TBLMSDWIP--MSD_OPENED:拆封未使用
                        MSDWIP _msdwip = new MSDWIP();
                        _msdwip.serial = 0;
                        _msdwip.LotNo = mLot;
                        _msdwip.Status = "MSD_OPENED";
                        _msdwip.MaintainUser = ApplicationService.Current().UserCode;
                        _msdwip.MaintainDate = date;
                        _msdwip.MaintainTime = time;

                        _facade.AddMSDWIP(_msdwip);

                        this.SuccessMessage("$CS_Save_Success");
                        this.txtMLot.TextFocus(false, true);
                        ClearInfo();
                        return;

                    }
                    else
                    {
                        //如果小于维护值，则不允许出干燥箱
                        this.ErrorMessage("$CS_Time_Is_Less_Than_DryingTime");
                        this.txtMLot.TextFocus(false, true);
                        //ClearInfo();
                        return;
                    }

                }
                #endregion

                #region 烘烤箱烘烤
                //2)	“烘烤箱烘烤”：TBLMSDLOT更新状态即可，剩余车间寿命不变。
                //TBLMSDWIP需要插入两笔数据，状态分别为“出烘烤箱”和“拆封未使用”。
                else if (status == "MSD_BAKING")
                {
                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_OPENED";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = overFloorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);


                    //添加TBLMSDWIP--MSD_OUTBAKING:出烘烤箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_OUTBAKING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    //添加TBLMSDWIP--MSD_OPENED:拆封未使用
                    MSDWIP _msdwip = new MSDWIP();
                    _msdwip.serial = 0;
                    _msdwip.LotNo = mLot;
                    _msdwip.Status = "MSD_OPENED";
                    _msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    _msdwip.MaintainDate = date;
                    _msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(_msdwip);


                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;

                }
                #endregion

                #region 封装
                else if (status == "MSD_PACKAGE")
                {
                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_OPENED";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = overFloorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);


                    //添加TBLMSDWIP--MSD_OPENED:拆封未使用
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_OPENED";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }
                #endregion

                #region 在表TBLMSDLOT未存在记录
                // 3)	在表TBLMSDLOT未存在记录（默认为封装未使用）：TBLMSDLOT新增一笔数据，状态为“拆封未使用”，
                // 有效车间寿命根据物料湿敏等级从TBLMSDLEVEL获取，剩余车间寿命=有效车间寿命。TBLMSDWIP插入一笔数据，状态为“拆封未使用”。
                else if (blnexit == false)
                {
                    //有效车间寿命根据物料湿敏等级从TBLMSDLEVEL获取
                    object msdLever = _facade.GetMSDLevelByLotNo(mLot);
                    if (msdLever != null)
                    {
                        floorlife = ((MSDLevel)msdLever).FloorLife;
                    }


                    //新增TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_OPENED";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = floorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.AddMSDLOT(msdLot);


                    //添加TBLMSDWIP--MSD_OPENED:拆封未使用
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_OPENED";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }
                #endregion

                else
                {
                    this.ErrorMessage("$CS_Status_IsNot_DRYING_AND_BAKING_AND_PACKAGE_NONE");   //拆封未使用：状态必须为“干燥箱干燥”、“烘烤箱烘烤”或者在表TBLMSDLOT未存在记录（默认为封装）。 
                    this.txtMLot.TextFocus(false, true);
                    // ClearInfo();
                    return;
                }

            }
            #endregion

            #region MSD_ALLUSED：全部使用
            //全部使用：原状态为“拆封”或者“超时”。 
            else if (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_ALLUSED"))
            {
                //1)“拆封未使用”，需要检查当前时间-拆封时间与有效车间寿命比较，如果大于有效车间寿命，则不允许使用。
                //如果不是，TBLMSDLOT更新状态，剩余车间寿命=剩余车间寿命-（当前时间-使用的维护时间），TBLMSDWIP插入一笔数据，状态为“使用”。
                if (status == "MSD_OPENED" || status == "MSD_OVERTIME")
                {
                    object msdWip;
                    if (status == "MSD_OPENED")
                    {
                        msdWip = _facade.GetMSDWIP(mLot, "MSD_OPENED");
                    }
                    else
                    {
                        msdWip = _facade.GetMSDWIP(mLot, "MSD_OVERTIME");
                    }

                    if (msdWip != null)
                    {
                        dateInWip = ((MSDWIP)msdWip).MaintainDate;
                        timeInWip = ((MSDWIP)msdWip).MaintainTime;
                    }
                    string dtInWip = FormatHelper.ToDateString(dateInWip) + " " + FormatHelper.ToTimeString(timeInWip);


                    object msdLever = _facade.GetMSDLevelByLotNo(mLot);
                    if (msdLever != null)
                    {
                        decRealFloorlife = ((MSDLevel)msdLever).FloorLife;
                    }

                    DateTime dt1 = DateTime.Parse(FormatHelper.ToDateString(date) + " " + FormatHelper.ToTimeString(time));
                    DateTime dt2 = DateTime.Parse(dtInWip);
                    TimeSpan ts = dt1 - dt2;

                    //如果大于有效车间寿命，则不允许使用。
                    //if (decimal.Parse(ts.TotalHours.ToString()) >= decRealFloorlife)
                    //{
                    //    this.ErrorMessage("$CS_Time_Over_Floorlife");
                    //    this.txtMLot.TextFocus(false, true);
                    //  //  ClearInfo();
                    //    return;

                    //}

                    //如果原状态是“拆封”，剩余车间寿命=剩余车间寿命-（当前时间-使用的维护时间）；如果原状态是超时，剩余车间寿命=0
                    decimal overFloorLifeNew = 0;
                    if (status == "MSD_OPENED")
                    {
                        overFloorLifeNew = floorlife - decimal.Parse(ts.TotalHours.ToString());
                    }

                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_ALLUSED";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = overFloorLifeNew;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);


                    //添加TBLMSDWIP--MSD_ALLUSED：全部使用
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_ALLUSED";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;

                }
                else
                {
                    this.ErrorMessage("$CS_Status_IsNot_MSD_OPENED_And_MSD_OVERTIME");   //只有状态为“拆封”或“超时”才能进行此操作
                    this.txtMLot.TextFocus(false, true);
                    // ClearInfo();
                    return;
                }

            }
            #endregion

            #region MSD_USING：使用
            ////D.使用：原状态为“拆封未使用”、“干燥箱干燥”、“烘烤箱烘烤”或者在表TBLMSDLOT未存在记录（默认为封装）。 
            //else if (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_USING"))
            //{
            //    //1)“拆封未使用”，需要检查当前时间-拆封时间与有效车间寿命比较，如果大于有效车间寿命，则不允许使用。
            //    //如果不是，TBLMSDLOT更新状态，剩余车间寿命=剩余车间寿命-（当前时间-使用的维护时间），TBLMSDWIP插入一笔数据，状态为“使用”。
            //    if (status == "MSD_OPENED")
            //    {
            //        object msdWip = _facade.GetMSDWIP(mLot, "MSD_OPENED");
            //        if (msdWip != null)
            //        {
            //            dateInWip = ((MSDWIP)msdWip).MaintainDate;
            //            timeInWip = ((MSDWIP)msdWip).MaintainTime;
            //        }
            //        string dtInWip = FormatHelper.ToDateString(dateInWip) + " " + FormatHelper.ToTimeString(timeInWip);


            //        object msdLever = _facade.GetMSDLevelByLotNo(mLot);
            //        if (msdLever != null)
            //        {
            //            decRealFloorlife = ((MSDLevel)msdLever).FloorLife;
            //        }

            //        DateTime dt1 = DateTime.Parse(FormatHelper.ToDateString(date) + " " + FormatHelper.ToTimeString(time));
            //        DateTime dt2 = DateTime.Parse(dtInWip);
            //        TimeSpan ts = dt1 - dt2;

            //        //如果大于有效车间寿命，则不允许使用。
            //        if (decimal.Parse(ts.TotalHours.ToString()) >= decRealFloorlife)
            //        {
            //            this.ErrorMessage("$CS_Time_Over_Floorlife");
            //            this.txtMLot.TextFocus(false, true);
            //          //  ClearInfo();
            //            return;

            //        }

            //        //更新TBLMSDLOT
            //        MSDLOT msdLot = new MSDLOT();
            //        msdLot.LotNo = mLot;
            //        msdLot.Status = "MSD_USING";
            //        msdLot.Floorlife = floorlife;
            //        msdLot.OverFloorlife = floorlife - decimal.Parse(ts.TotalHours.ToString());
            //        msdLot.MaintainUser = ApplicationService.Current().UserCode;
            //        msdLot.MaintainDate = date;
            //        msdLot.MaintainTime = time;

            //        _facade.UpdateMSDLOT(msdLot);


            //        //添加TBLMSDWIP--MSD_USING：使用
            //        MSDWIP msdwip = new MSDWIP();
            //        msdwip.serial = 0;
            //        msdwip.LotNo = mLot;
            //        msdwip.Status = "MSD_USING";
            //        msdwip.MaintainUser = ApplicationService.Current().UserCode;
            //        msdwip.MaintainDate = date;
            //        msdwip.MaintainTime = time;

            //        _facade.AddMSDWIP(msdwip);

            //        this.SuccessMessage("$CS_Save_Success");
            //        this.txtMLot.TextFocus(false, true);
            //        ClearInfo();
            //        return;


            //    }
            //    //干燥箱干燥”，TBLMSDLOT更新状态，剩余车间寿命不变，TBLMSDWIP插入两笔数据，状态为“出干燥箱”，“使用”。
            //    //“烘烤箱烘烤”，TBLMSDLOT更新状态，剩余车间寿命不变，TBLMSDWIP插入两笔数据，状态为“出烘烤箱”，“使用”。
            //    else if ((status == "MSD_DRYING") || (status == "MSD_BAKING"))
            //    {
            //        //更新TBLMSDLOT
            //        MSDLOT msdLot = new MSDLOT();
            //        msdLot.LotNo = mLot;
            //        msdLot.Status = "MSD_USING";
            //        msdLot.Floorlife = floorlife;
            //        msdLot.OverFloorlife = overFloorlife;
            //        msdLot.MaintainUser = ApplicationService.Current().UserCode;
            //        msdLot.MaintainDate = date;
            //        msdLot.MaintainTime = time;

            //        _facade.UpdateMSDLOT(msdLot);

            //        //添加TBLMSDWIP--MSD_OUTDRYING：出干燥箱
            //        MSDWIP msdwip = new MSDWIP();
            //        msdwip.serial = 0;
            //        msdwip.LotNo = mLot;
            //        msdwip.Status = "MSD_OUTDRYING";
            //        msdwip.MaintainUser = ApplicationService.Current().UserCode;
            //        msdwip.MaintainDate = date;
            //        msdwip.MaintainTime = time;

            //        _facade.AddMSDWIP(msdwip);

            //        //添加TBLMSDWIP--MSD_USING：使用
            //        MSDWIP _msdwip = new MSDWIP();
            //        _msdwip.serial = 0;
            //        _msdwip.LotNo = mLot;
            //        _msdwip.Status = "MSD_USING";
            //        _msdwip.MaintainUser = ApplicationService.Current().UserCode;
            //        _msdwip.MaintainDate = date;
            //        _msdwip.MaintainTime = time;

            //        _facade.AddMSDWIP(_msdwip);

            //        this.SuccessMessage("$CS_Save_Success");
            //        this.txtMLot.TextFocus(false, true);
            //        ClearInfo();
            //        return;
            //    }

            //    //4)在表TBLMSDLOT未存在记录（默认为封装）。TBLMSDLOT新增一笔数据，状态为“使用”，
            //    // 有效车间寿命根据物料湿敏等级从TBLMSDLEVEL获取，剩余车间寿命=有效车间寿命。
            //    // TBLMSDWIP插入一笔数据，状态为“使用”。
            //    else if (blnexit == false)
            //    {
            //        //有效车间寿命根据物料湿敏等级从TBLMSDLEVEL获取
            //        object msdLever = _facade.GetMSDLevelByLotNo(mLot);
            //        if (msdLever != null)
            //        {
            //            decRealFloorlife = ((MSDLevel)msdLever).FloorLife;
            //        }

            //        //新增TBLMSDLOT
            //        MSDLOT msdLot = new MSDLOT();
            //        msdLot.LotNo = mLot;
            //        msdLot.Status = "MSD_USING";
            //        msdLot.Floorlife = decRealFloorlife;
            //        msdLot.OverFloorlife = decRealFloorlife;
            //        msdLot.MaintainUser = ApplicationService.Current().UserCode;
            //        msdLot.MaintainDate = date;
            //        msdLot.MaintainTime = time;

            //        _facade.AddMSDLOT(msdLot);


            //        //添加TBLMSDWIP--MSD_USING：使用
            //        MSDWIP msdwip = new MSDWIP();
            //        msdwip.serial = 0;
            //        msdwip.LotNo = mLot;
            //        msdwip.Status = "MSD_USING";
            //        msdwip.MaintainUser = ApplicationService.Current().UserCode;
            //        msdwip.MaintainDate = date;
            //        msdwip.MaintainTime = time;

            //        _facade.AddMSDWIP(msdwip);

            //        this.SuccessMessage("$CS_Save_Success");
            //        this.txtMLot.TextFocus(false, true);
            //        ClearInfo();
            //        return;
            //    }

            //}
            #endregion

            #region MSD_DRYING：干燥箱干燥

            //E.干燥箱干燥：只有状态为“封装”、“拆封未使用”、“烘烤箱烘烤”才可进行干燥。
            else if (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_DRYING"))
            {
                #region 封装
                //TBLMSDLOT更新状态即可。TBLMSDWIP需要插入一笔数据，状态分别为“进干燥箱”。
                if (status == "MSD_PACKAGE")
                {
                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_DRYING";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = overFloorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);

                    //添加TBLMSDWIP--MSD_INDRYING：进干燥箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_INDRYING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;

                }
                #endregion

                #region 拆封未使用
                //TBLMSDLOT更新状态，剩余车间寿命=剩余车间寿命-（当前时间-使用的维护时间），TBLMSDWIP插入一笔数据，状态为“进干燥箱”。
                else if (status == "MSD_OPENED")
                {
                    object msdWip = _facade.GetMSDWIP(mLot, "MSD_OPENED");
                    if (msdWip != null)
                    {
                        dateInWip = ((MSDWIP)msdWip).MaintainDate;
                        timeInWip = ((MSDWIP)msdWip).MaintainTime;
                    }
                    string dtInWip = FormatHelper.ToDateString(dateInWip) + " " + FormatHelper.ToTimeString(timeInWip);


                    DateTime dt1 = DateTime.Parse(FormatHelper.ToDateString(date) + " " + FormatHelper.ToTimeString(time));
                    DateTime dt2 = DateTime.Parse(dtInWip);
                    TimeSpan ts = dt1 - dt2;

                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_DRYING";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = floorlife - decimal.Parse(ts.TotalHours.ToString());
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);

                    //添加TBLMSDWIP-MSD_INDRYING：进干燥箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_INDRYING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }
                #endregion

                #region 烘烤箱烘烤
                //3)“烘烤箱烘烤”，TBLMSDLOT更新状态即可。TBLMSDWIP需要插入两笔笔数据，状态分别为“出烘烤箱”，“进干燥箱”。
                else if (status == "MSD_BAKING")
                {
                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = mLot;
                    msdLot.Status = "MSD_DRYING";
                    msdLot.Floorlife = floorlife;
                    msdLot.OverFloorlife = overFloorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);

                    //添加TBLMSDWIP-MSD_OUTBAKING:出烘烤箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_OUTBAKING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    //添加TBLMSDWIP--MSD_INDRYING：进干燥箱
                    MSDWIP _msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_INDRYING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);

                    this.SuccessMessage("$CS_Save_Success");
                    this.txtMLot.TextFocus(false, true);
                    ClearInfo();
                    return;
                }
                else
                {
                    this.ErrorMessage("$CS_Status_IsNot_MSD_PACKAGE_And_MSD_OPENED_And_MSD_BAKING");
                    this.txtMLot.TextFocus(false, true);
                    // ClearInfo();
                    return;
                }
                #endregion
            }
            #endregion

            #region MSD_BAKING:烘烤箱烘烤
            //F.烘烤箱烘烤：只要状态不为“烘烤箱烘烤”、“全部使用”即可。
            //TBLMSDLOT更新状态，剩余车间寿命=有效车间寿命。TBLMSDWIP插入一笔数据，状态为“进烘烤箱”，
            //如果原状态为“干燥箱干燥”，需要先插入一笔“出干燥箱”。
            else
            {
                if (status == "MSD_BAKING" || status == "MSD_ALLUSED")
                {
                    this.ErrorMessage("$CS_Status_Is_MSD_BAKING_And_MSD_USIN");
                    this.txtMLot.TextFocus(false, true);
                    // ClearInfo();
                    return;
                }

                //更新TBLMSDLOT
                MSDLOT msdLot = new MSDLOT();
                msdLot.LotNo = mLot;
                msdLot.Status = "MSD_BAKING";
                msdLot.Floorlife = floorlife;
                msdLot.OverFloorlife = floorlife;
                msdLot.MaintainUser = ApplicationService.Current().UserCode;
                msdLot.MaintainDate = date;
                msdLot.MaintainTime = time;

                _facade.UpdateMSDLOT(msdLot);

                //MSD_DRYING：干燥箱干燥
                if (status == "MSD_DRYING")
                {
                    //添加TBLMSDWIP--MSD_OUTDRYING：出干燥箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_OUTDRYING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);


                    //添加TBLMSDWIP--MSD_INBAKING:进烘烤箱
                    MSDWIP _msdwip = new MSDWIP();
                    _msdwip.serial = 0;
                    _msdwip.LotNo = mLot;
                    _msdwip.Status = "MSD_INBAKING";
                    _msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    _msdwip.MaintainDate = date;
                    _msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(_msdwip);
                }
                else
                {
                    //添加TBLMSDWIP--MSD_INBAKING:进烘烤箱
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = mLot;
                    msdwip.Status = "MSD_INBAKING";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);
                }

                this.SuccessMessage("$CS_Save_Success");
                this.txtMLot.TextFocus(false, true);
                ClearInfo();
                return;
            }
            #endregion
        }

        #region ClearInfo
        private void ClearInfo()
        {
            this.txtMLot.Value = string.Empty;
            this.txtMCode.Text = string.Empty;
            this.txtMName.Text = string.Empty;
            this.txtMDesc.Text = string.Empty;
            this.txtStatus.Text = string.Empty;
            this.txtOverFloorlife.Text = string.Empty;
            this.txtMUser.Text = string.Empty;
            this.txtMDate.Text = string.Empty;
            this.txtMtime.Text = string.Empty;
        }
        #endregion

        #region Message
        protected void ErrorMessage(string msg)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, msg));
        }

        protected void SuccessMessage(string msg)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Success, msg));
        }
        #endregion


        private void ultraOptionSetOperation_ValueChanged(object sender, EventArgs e)
        {
            string status = string.Empty;
            decimal floorlife = 0;
            decimal overFloorlife = 0;
            int dateInMSDdLot = 0;
            int timeInMSDdLot = 0;

            string mLot = txtMLot.Value.ToUpper().Trim();
            if (mLot == String.Empty)
            {
                this.ErrorMessage("$CS_MLot_Is_Empty");
                ClearInfo();
                return;
            }

            //得到时间
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            int time = dbDateTime.DBTime;


            //查询TBLMSDLOT表状态
            InventoryFacade _facade = new InventoryFacade(this.DataProvider);
            object obj = _facade.GetMSDLot(mLot);
            if (obj != null)
            {
                status = ((MSDLOT)obj).Status;
                //floorlife = ((MSDLOT)obj).Floorlife;
                overFloorlife = ((MSDLOT)obj).OverFloorlife;
            }

            // 封装->拆封未使用
            if (status == "MSD_PACKAGE")
            {
                if ((ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_OPENED")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_DRYING")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_BAKING")))
                {

                    //有效车间寿命根据物料湿敏等级从TBLMSDLEVEL获取
                    object msdLever = _facade.GetMSDLevelByLotNo(mLot);
                    if (msdLever != null)
                    {
                        floorlife = ((MSDLevel)msdLever).FloorLife;
                        this.txtOverFloorlife.Text = floorlife.ToString();
                    }

                }
            }
            //拆封未使用->使用：剩余车间寿命=剩余车间寿命-（当前日期时间-维护日期时间）。
            else if (status == "MSD_OPENED")
            {
                if (((ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_DRYING")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_BAKING")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_ALLUSED"))))
                {
                    object msdLot = _facade.GetMSDLot(mLot);
                    if (msdLot != null)
                    {
                        dateInMSDdLot = ((MSDLOT)msdLot).MaintainDate;
                        timeInMSDdLot = ((MSDLOT)msdLot).MaintainTime;
                    }
                    string dtInMSDdLot = FormatHelper.ToDateString(dateInMSDdLot) + " " + FormatHelper.ToTimeString(timeInMSDdLot);

                    DateTime dt1 = DateTime.Parse(FormatHelper.ToDateString(date) + " " + FormatHelper.ToTimeString(time));
                    DateTime dt2 = DateTime.Parse(dtInMSDdLot);
                    TimeSpan ts = dt1 - dt2;

                    this.txtOverFloorlife.Text = (double.Parse(overFloorlife.ToString()) - ts.TotalHours).ToString();
                }
                else
                {
                    this.txtOverFloorlife.Text = string.Empty;
                }

            }

            //干燥箱干燥->使用、拆封未使用
            else if (status == "MSD_DRYING")
            {
                if ((ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_OPENED")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_BAKING")))
                {

                    this.txtOverFloorlife.Text = overFloorlife.ToString();
                }
                else
                {
                    this.txtOverFloorlife.Text = string.Empty;
                }
            }

           // 烘烤箱烘烤->拆封未使用、使用、干燥箱干燥、封装，剩余车间寿命=剩余车间寿命
            else if (status == "MSD_BAKING")
            {
                if ((ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_OPENED")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_DRYING")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_PACKAGE")))
                {

                    this.txtOverFloorlife.Text = overFloorlife.ToString();
                }
                else
                {
                    this.txtOverFloorlife.Text = string.Empty;
                }
            }

            else if (status == "MSD_OVERTIME")
            {
                if ((ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_BAKING")) || (ultraOptionSetOperation.CheckedItem.DataValue.ToString().Equals("MSD_ALLUSED")))
                {

                    this.txtOverFloorlife.Text = "0";
                }
                else
                {
                    this.txtOverFloorlife.Text = string.Empty;
                }
            }
        }

        private string MSDStatus(string status)
        {
            if (status == "MSD_PACKAGE")
            {
                return MutiLanguages.ParserString("$CS_Package");
            }
            else if (status == "MSD_OPENED")
            {
                return MutiLanguages.ParserString("$CS_MSD_OPENED");
            }
            else if (status == "MSD_ALLUSED")
            {
                return MutiLanguages.ParserString("$CS_MSD_ALLUSED");
            }
            //else if (status == "MSD_USING")
            //{
            //    return "使用";
            //}
            else if (status == "MSD_DRYING")
            {
                return MutiLanguages.ParserString("$CS_MSD_DRYING");
            }
            else if (status == "MSD_BAKING")
            {
                return MutiLanguages.ParserString("$CS_MSD_BAKING");
            }
            else
            {
                return MutiLanguages.ParserString("$CS_TimeOver");
            }
        }

        //private void lblOverFloorlife_Click(object sender, EventArgs e)
        //{

        //}


        //private void txtMtime1_TextChanged(object sender, EventArgs e)
        //{

        //}
    }
}
