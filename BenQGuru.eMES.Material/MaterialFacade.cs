using System;
using System.Reflection;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using UserControl;

namespace BenQGuru.eMES.Material
{
    /// <summary>
    /// MaterialFacade 的摘要说明。
    /// 文件名:		MaterialFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// 创建日期:	2005-05-17 11:23:20
    /// 修改人:
    /// 修改日期:
    /// 描 述:	
    /// 版 本:	
    /// </summary>
    public class MaterialFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public MaterialFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public MaterialFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        public object GetOPBOMMain(string itemCode, string opBOMCode, string opBOMVersion, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(OPBOM), new object[] { itemCode, opBOMCode, opBOMVersion, orgID });
        }

        #region MINNO
        /// <summary>
        /// 
        /// </summary>
        public MINNO CreateNewMINNO()
        {
            return new MINNO();
        }

        /// <summary>
        /// ** 功能描述:	将mINNOs用新的集成上料号inno添加保存成一组集成上料
        ///						保存前的判断包括：
        ///							工单状态须为Release或Open
        ///							新的集成上料号不存在;
        ///							物料属于指定的Operation;
        ///							物料已设为lot管控;
        ///							Operation下所有的物料都已维护;
        ///							替代料和对应的主料只能在一个集成上料资料中出现一个;
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-06-01
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno">新的集成上料号</param>
        /// <param name="moCode"></param>
        /// <param name="routeCode"></param>
        /// <param name="operationCode"></param>
        /// <param name="resourceCode"></param>
        /// <param name="mINNOs"></param>
        public Messages AddMINNOs(string inno, string moCode, string routeCode, string operationCode, string resourceCode, object[] mINNOs)
        {
            Messages messages = new Messages();

            object mo = new MOFacade(this.DataProvider).GetMO(moCode);
            if (mo == null)
            {
                messages.Add(new Message(new Exception("$Error_CS_MO_Not_Exist")));
                return messages;
            }

            // 工单状态检查
            if (((MO)mo).MOStatus != MOManufactureStatus.MOSTATUS_RELEASE && ((MO)mo).MOStatus != MOManufactureStatus.MOSTATUS_OPEN)
            {
                messages.Add(new Message(new Exception("$Error_CS_MO_Should_be_Release_or_Open")));
                return messages;
            }

            /*
             * 注释 Karron Qiu,2005-9-20 ,这里不需要再次判断
             * 改变集成上料备料维护逻辑：修改集成上料资料后执行保持时检查集成上料号是否被使用过，
             * 如果没被使用过则直接保持更改后的资料，不需要输入新的集成上料号
             * 
            // 判断集成上料号不存在
            if ( this.IsINNOExist(inno) )
            {
                messages.Add( new Message( new Exception("$Error_CS_INNO_Has_Already_Exist")));
                return messages;
            }*/

            decimal sequence = this.GetUniqueMINNOSequence();
            ArrayList srcItemList = new ArrayList();
            OPBOMDetail opbomDetail = null;

            if (mINNOs != null)
            {
                foreach (MINNO mINNO in mINNOs)
                {
                    // OPBOM属于指定的Operation
                    opbomDetail = this.GetOPBOM(moCode, routeCode, operationCode, resourceCode, mINNO.MItemCode, messages);

                    if (opbomDetail == null)
                    {
                        continue;
                    }

                    OPBOMDetail detail = opbomDetail as OPBOMDetail;
                    object objOPBOMMain = this.GetOPBOMMain(detail.OPBOMCode, detail.ItemCode, detail.OPBOMVersion, ((MO)mo).OrganizationID);
                    if (objOPBOMMain != null)
                    {
                        OPBOM opBOM = objOPBOMMain as OPBOM;

                        /*
						if(opBOM.Avialable == 0)
						{
							messages.Add(new Message(new Exception("$CS_OPBOM_IS_NOT_AVIALABLE")));
							return messages;
						}
                        */
                    }

                    // 替代料和对应的主料只能在一个集成上料资料中出现一个
                    if (srcItemList.Contains(opbomDetail.OPBOMItemCode.ToUpper()))
                    {
                        messages.Add(new Message(new Exception(string.Format("$Error_CS_Item_Is_Already_Exist [$OPBOMItemcode={0}]", mINNO.MItemCode))));
                        continue;
                    }

                    srcItemList.Add(opbomDetail.OPBOMItemCode.ToUpper());

                    string srcItemCode = opbomDetail.OPBOMSourceItemCode.ToUpper();

                    if ((srcItemCode != string.Empty) && (srcItemCode != opbomDetail.OPBOMItemCode.ToUpper()))
                    {
                        if (srcItemList.Contains(srcItemCode))
                        {
                            messages.Add(new Message(new Exception(string.Format("$Error_CS_SourceItem_Is_Already_Exist [$OPBOMItemcode={0}, $OPBOMSourceItemCode={1}]", mINNO.MItemCode, srcItemCode))));
                            continue;
                        }

                        srcItemList.Add(srcItemCode);
                    }

                    mINNO.INNO = inno;
                    mINNO.ItemCode = opbomDetail.ItemCode;
                    mINNO.OPBOMCode = opbomDetail.OPBOMCode;
                    mINNO.OPBOMVersion = opbomDetail.OPBOMVersion;
                    mINNO.MSourceItemCode = opbomDetail.OPBOMSourceItemCode;
                    mINNO.Qty = opbomDetail.OPBOMItemQty;

                    mINNO.MOCode = moCode;
                    mINNO.RouteCode = routeCode;
                    mINNO.OPCode = operationCode;
                    mINNO.ResourceCode = resourceCode;

                    mINNO.Sequence = sequence;
                    mINNO.IsLast = "Y";
                    //2006/11/17,Laws Lu add get DateTime from db Server
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    mINNO.MaintainDate = FormatHelper.TODateInt(dtNow);
                    mINNO.MaintainTime = FormatHelper.TOTimeInt(dtNow);

                    sequence++;
                }
            }

            // 判断是否所有料都已维护
            object[] objs = new OPBOMFacade(this.DataProvider).GetLotControlOPBOMDetails(moCode, routeCode, operationCode, ((MO)mo).OrganizationID);

            if (objs == null || objs.Length <= 0)
            {
                messages.Add(new Message(new Exception("$Error_CS_No_OPBOMDetail")));
                return messages;
            }

            foreach (OPBOMDetail opbom in objs)
            {
                bool found = false;

                if (mINNOs != null)
                {
                    foreach (MINNO mINNO in mINNOs)
                    {
                        if (mINNO.MItemCode.ToUpper() == opbom.OPBOMItemCode.ToUpper() || mINNO.MSourceItemCode.ToUpper() == opbom.OPBOMItemCode.ToUpper())
                        {
                            found = true;
                            break;
                        }

                        if (opbom.OPBOMSourceItemCode != "")
                        {
                            if (mINNO.MItemCode.ToUpper() == opbom.OPBOMSourceItemCode.ToUpper() || mINNO.MSourceItemCode.ToUpper() == opbom.OPBOMSourceItemCode.ToUpper())
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }

                if (!found)
                {
                    messages.Add(new Message(new Exception(string.Format("$Error_Lack_of_OPBOMItem {0}", opbom.OPBOMItemCode))));
                }
            }

            if (!messages.IsSuccess())
            {
                return messages;
            }

            this.DataProvider.BeginTransaction();

            try
            {
                // 将所有之间设置的集成上料资料IsLast设为N
                this.DataProvider.CustomExecute(new SQLParamCondition("update tblminno set islast='N' where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and ISLAST='Y'",
                    new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
										  new SQLParameter("routecode", typeof(string), routeCode), 
										  new SQLParameter("opcode", typeof(string), operationCode), 
										  new SQLParameter("rescode", typeof(string), resourceCode)}));

                foreach (MINNO mINNO in mINNOs)
                {
                    this.DataProvider.Insert(mINNO);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                messages.Add(new Message(ex));
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }

            return messages;
        }

        public Messages ChangeLastMINNOStatus(string INNO, string moCode, string routeCode, string operationCode, string resourceCode)
        {
            Messages messages = new Messages();

            this.DataProvider.BeginTransaction();

            try
            {
                // 将最近设置的集成上料资料IsLast设为Y
                this.DataProvider.CustomExecute(new SQLParamCondition("update tblminno set islast='Y' where inno=$inno and mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and ISLAST='N'",
                    new SQLParameter[]{new SQLParameter("inno", typeof(string), INNO),
										  new SQLParameter("mocode", typeof(string), moCode),
										  new SQLParameter("routecode", typeof(string), routeCode), 
										  new SQLParameter("opcode", typeof(string), operationCode), 
										  new SQLParameter("rescode", typeof(string), resourceCode)}));

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                messages.Add(new Message(ex));
            }

            return messages;
        }

        /// <summary>
        /// ** 功能描述:	由集成上料号获得集成上料信息
        ///					用于上料检查
        /// ** 作 者:		Mark Lee
        /// ** 日 期:		2005-06-20
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno">集成上料号</param>
        /// <returns></returns>
        public object GetMINNO(string inno)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO),
                new SQLParamCondition(
                string.Format("select {0} from tblminno where inno=$inno ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{
									  new SQLParameter("inno", typeof(string), inno)
									   }));

            if (objs != null)
            {
                if (objs.Length > 0)
                    return objs[0];
            }

            return null;
        }

        public object GetMINNO(string moCode, string routeCode, string opCode, string resCode, string opBOMVersion, string opBOMItemCode)
        {
            string sql = "select {0} from tblminno where 1=1 ";
            sql += "and mocode = $mocode ";
            sql += "and routecode = $routecode ";
            sql += "and opcode =$opcode ";
            sql += "and rescode = $rescode ";
            sql += "and opbomver = $opbomver ";
            sql += "and mitemcode = $mitemcode ";

            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO),
                new SQLParamCondition(
                string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode),
                    new SQLParameter("opcode", typeof(string), opCode),
                    new SQLParameter("rescode", typeof(string), resCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion),
                    new SQLParameter("mitemcode", typeof(string), opBOMItemCode)
					}));

            if (objs != null)
            {
                if (objs.Length > 0)
                    return objs[0];
            }

            return null;
        }

        /// <summary>
        /// ** 功能描述:	删除所有的集成上料信息
        /// ** 作 者:		Karron Qiu
        /// ** 日 期:		2005-09-20
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno"></param>
        /// <returns></returns>
        public Messages DeleteINNO(string INNO)
        {
            Messages messages = new Messages();
            try
            {
                this._domainDataProvider.BeginTransaction();

                this.DataProvider.CustomExecute(new SQLParamCondition("DELETE FROM tblminno where inno=$inno ",
                    new SQLParameter[] { new SQLParameter("inno", typeof(string), INNO) }));

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                messages.Add(new Message(ex));
            }

            return messages;
        }

        /// <summary>
        /// ** 功能描述:	有集成上料号获得所有的集成上料信息
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-06-27
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno"></param>
        /// <returns></returns>
        public object[] GetLastMINNOs(string inno)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO),
                                new SQLParamCondition(
                                string.Format("select {0} from tblminno where inno=$inno and islast='Y'",
                                                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                                                new SQLParameter[] { new SQLParameter("inno", typeof(string), inno) }));
        }

        /// <summary>
        /// ** 功能描述:	有集成上料号获得所有的集成上料信息
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-06-27
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno"></param>
        /// <returns></returns>
        public object[] GetMINNOs(string inno)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO),
                new SQLParamCondition(
                string.Format("select {0} from tblminno where inno=$inno",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[] { new SQLParameter("inno", typeof(string), inno) }));
        }


        public OPBOMDetail GetOPBOM(string moCode, string routeCode, string operationCode, string resourceCode, string mItemCode, Messages messages)
        {
            OPBOMFacade opbomFacade = new OPBOMFacade(this.DataProvider);

            //  OPBOM属于指定的Operation
            OPBOMDetail opbom = (OPBOMDetail)opbomFacade.GetOPBOMDetail(moCode, routeCode, operationCode, mItemCode);

            if (opbom == null)
            {
                messages.Add(new Message(new Exception(string.Format("$Error_CS_OPBOMDetail_Not_Exist [$OPBOMItemcode={0}]", mItemCode))));
                return null;
            }
            //AMOI  MARK  START  20050803  设置为不管控也允许
            // 没有设置为Lot管控，不能添加
            if ((opbom.OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_LOT)
                && (opbom.OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_NOCONTROL))
            //AMOI  MARK  END
            {
                messages.Add(new Message(new Exception(string.Format("$Error_CS_OPBOMItem_Is_Not_LotControl [$OPBOMItemcode={0}]", mItemCode))));
                return null;
            }


            return opbom;
        }

        /// <summary>
        /// ** 功能描述:	将一个Resource下最新一笔的集成上料Copy至另一个Resource
        ///					前提条件：拷贝的Resource和源Resource要位于同一Operation下
        ///					
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-06-01
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno">新的集成上料号</param>
        /// <param name="moCode"></param>
        /// <param name="routeCode"></param>
        /// <param name="operationCode"></param>
        /// <param name="srcResourceCode"></param>
        /// <param name="desResourceCode"></param>
        public void CopyMINNOToResource(string inno, string moCode, string routeCode, string operationCode, string srcResourceCode, string desResourceCode)
        {
            if (srcResourceCode == desResourceCode)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_Canot_Copy_To_Self");
                return;
            }

            object[] objs = this.QueryLastMINNO(moCode, routeCode, operationCode, srcResourceCode);

            if (objs == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_No_Material_Below_Source_Resource");
                return;
            }

            this.AddMINNOs(inno, moCode, routeCode, operationCode, desResourceCode, objs);
        }

        /// <summary>
        /// ** 功能描述:	由集成上料号及物料号获得集成上料信息
        ///					由于一个集成上料号下，一个物料只能出现一次，结果集最多为1个，所以返回object
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-05-30
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="inno">集成上料号</param>
        /// <param name="mItemCode">物料号</param>
        /// <returns></returns>
        public object GetMINNO(string inno, string mItemCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO),
                new SQLParamCondition(
                string.Format("select {0} from tblminno where inno=$inno and mitemcode=$mitemcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{
									  new SQLParameter("inno", typeof(string), inno),
									  new SQLParameter("mitemcode", typeof(string), mItemCode) }));

            if (objs != null)
            {
                return objs[0];
            }

            return null;
        }

        public object GetMINNO(string inno, decimal sequence)
        {
            return this.DataProvider.CustomSearch(typeof(MINNO), new object[] { inno, sequence });
        }

        /// <summary>
        /// ** 功能描述:	分页查询MINNO
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-18 9:43:27
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="moCode">精确查询</param>
        /// <param name="routeCode">精确查询</param>
        /// <param name="operationCode">精确查询</param>
        /// <param name="resourceCode">精确查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> MINNO数组</returns>
        public object[] QueryLastMINNO(string moCode, string routeCode, string operationCode, string resourceCode)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and ISLAST='Y'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
									  new SQLParameter("routecode", typeof(string), routeCode), 
									  new SQLParameter("opcode", typeof(string), operationCode), 
									  new SQLParameter("rescode", typeof(string), resourceCode)}));
        }

        public object[] QueryMINNO(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion) }));
        }

        //支持堆栈上料调整，Jarvis 20120321
        public object[] QueryMINNO_New(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO outer where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and seq=(select min(seq) from tblminno inner where inner.mobsitemcode=outer.mobsitemcode and inner.mocode=outer.mocode and inner.routecode=outer.routecode and inner.opcode=outer.opcode and inner.rescode=outer.rescode and inner.opbomver=outer.opbomver)", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion) }));
        }

        public object[] QueryMINNO(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string materialCode)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mitemcode=$mitemcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion), 
                    new SQLParameter("mitemcode", typeof(string), materialCode) }));
        }
        public object[] QueryMINNO(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string materialCode, string obsitemcode)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mitemcode=$mitemcode and mobsitemcode=$mobsitemcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion), 
                    new SQLParameter("mitemcode", typeof(string), materialCode),
                    new SQLParameter("mobsitemcode", typeof(string), obsitemcode)}));
        }

        public object[] QueryMINNO(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string materialCode, string obsitemcode, string mItemPackedNo)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mitemcode=$mitemcode and mobsitemcode=$mobsitemcode and MITEMPACKEDNO=$mItemPackedNo", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion), 
                    new SQLParameter("mitemcode", typeof(string), materialCode),
                    new SQLParameter("mobsitemcode", typeof(string), obsitemcode),
                    new SQLParameter("mItemPackedNo", typeof(string), mItemPackedNo)}));
        }

        public object[] QueryMINNO_New(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string obsitemcode)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mobsitemcode=$mobsitemcode order by seq", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion), 
                    new SQLParameter("mobsitemcode", typeof(string), obsitemcode)}));
        }

        public object[] QueryMINNOByMSItemcode(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string obsitemcode)
        {
            return this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition(string.Format("select {0} from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mobsitemcode=$mobsitemcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                new SQLParameter[]{	new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion), 
                    new SQLParameter("mobsitemcode", typeof(string), obsitemcode) }));
        }

        /// <summary>
        /// ** 功能描述:	获得最新的集成上料号
        /// ** 作 者:		Karron Qiu
        /// ** 日 期:		2005-05-18 9:43:27
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="routeCode"></param>
        /// <param name="operationCode"></param>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public string GetLastINNO(string moCode, string routeCode, string operationCode, string resourceCode)
        {
            string sql = string.Format("select INNO from TBLMINNO where mocode='{0}' and routecode='{1}' and opcode='{2}' and rescode='{3}' /* and islast='N' */",
                moCode, routeCode, operationCode, resourceCode);

            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO), new PagerCondition(sql, " MDATE,MTIME desc", 1, 1));

            if (objs == null || objs.Length < 1)
            {
                return "";
            }

            return ((MINNO)objs[0]).INNO;
        }

        public decimal GetUniqueMINNOSequence()
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO),
                new PagerCondition(
                string.Format("select seq from TBLMINNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MINNO))),
                "seq desc", 1, 1));

            if (objs == null || objs.Length < 1)
            {
                return 0;
            }

            return ((MINNO)objs[0]).Sequence + 1;
        }

        public decimal GetUniqueMINNOSequence(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string materialCode, string obsitemcode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition("select nvl(max(seq),-1) as seq from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mitemcode=$mitemcode and mobsitemcode=$mobsitemcode",
                new SQLParameter[]{	
                    new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion), 
                    new SQLParameter("mitemcode", typeof(string), materialCode),
                    new SQLParameter("mobsitemcode", typeof(string), obsitemcode)}));
            if (objs == null || objs.Length < 1)
            {
                return 0;
            }

            return ((MINNO)objs[0]).Sequence + 1;
        }

        public decimal GetUniqueMINNOSequence(string moCode, string routeCode, string operationCode, string resourceCode, string opBOMVersion, string obsitemcode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(MINNO), new SQLParamCondition("select nvl(max(seq),-1) as seq from TBLMINNO where mocode=$mocode and routecode=$routecode and opcode=$opcode and rescode=$rescode and opbomver=$opbomver and mobsitemcode=$mobsitemcode",
                new SQLParameter[]{	
                    new SQLParameter("mocode", typeof(string), moCode),
                    new SQLParameter("routecode", typeof(string), routeCode), 
                    new SQLParameter("opcode", typeof(string), operationCode), 
                    new SQLParameter("rescode", typeof(string), resourceCode),
                    new SQLParameter("opbomver", typeof(string), opBOMVersion),                     
                    new SQLParameter("mobsitemcode", typeof(string), obsitemcode)}));
            if (objs == null || objs.Length < 1)
            {
                return 0;
            }

            return ((MINNO)objs[0]).Sequence + 1;
        }

        public bool IsINNOExist(string inno)
        {
            if (this.DataProvider.GetCount(new SQLParamCondition("select count(*) from tblminno where inno=$inno", new SQLParameter[] { new SQLParameter("inno", typeof(string), inno) })) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断INNO是否被使用过
        /// </summary>
        /// <param name="inno"></param>
        /// <param name="MOCODE"></param>
        /// <returns></returns>
        public bool IsINNOHasBeenUsed(string inno, string MOCODE)
        {
            if (this.DataProvider.GetCount(new SQLParamCondition("select count(*) from tblonwipitem where MCARD =$inno AND MOCODE=$MOCODE",
                new SQLParameter[]{ new SQLParameter("inno", typeof(string), inno)
								  ,new SQLParameter("MOCODE", typeof(string), MOCODE) })) > 0)
            {
                return true;
            }

            return false;
        }

        public void AddMINNO(MINNO minno)
        {
            this._helper.AddDomainObject(minno);
        }

        public void UpdateMINNO(MINNO minno)
        {
            this._helper.UpdateDomainObject(minno);
        }

        public void DeleteMINNO(MINNO minno)
        {
            this._helper.DeleteDomainObject(minno);
        }


        public void AddOrUpdateMINNO(MINNO minno)
        {
            object[] obj = QueryMINNOByMSItemcode(minno.MOCode, minno.RouteCode, minno.OPCode, minno.ResourceCode, minno.OPBOMVersion, minno.MSourceItemCode);
            if (obj == null)
                AddMINNO(minno);
            else
            {
                minno.INNO = ((MINNO)obj[0]).INNO;
                minno.Sequence = ((MINNO)obj[0]).Sequence;
                for (int i = 0; i < obj.Length; i++)
                {
                    this.DeleteMINNO((MINNO)obj[i]);
                }
                AddMINNO(minno);
            }
        }

        #endregion

        #region MKeyPart
        /// <summary>
        /// 
        /// </summary>
        public MKeyPart CreateNewMKeyPart()
        {
            return new MKeyPart();
        }

        public void AddNewMKeyPart(MKeyPart mKeyPart)
        {
            this.DataProvider.Insert(mKeyPart);
        }

        public void AddMKeyPart(MKeyPart mKeyPart)
        {
            //  OPBOMItemCode存在
            OPBOMFacade opbomFacade = new OPBOMFacade(this.DataProvider);
            if (!opbomFacade.IsOPBOMItemExist(mKeyPart.MItemCode.ToUpper()))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_OPBOMItem_Not_Exist");
                return;
            }

            mKeyPart.Sequence = Convert.ToInt32(this.GetUniqueMKeyPartSequence());

            if (this.DataProvider.CustomSearch(mKeyPart.GetType(), DomainObjectUtility.GetDomainObjectKeyScheme(mKeyPart.GetType()), DomainObjectUtility.GetDomainObjectKeyValues(mKeyPart)) != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_Primary_Key_Overlap");
                return;
            }
            // RunningCard范围不允许交叉
            //if (RunningCardRangeCheck(mKeyPart))
            {
                //2006/11/17,Laws Lu add get DateTime from db Server
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                mKeyPart.MaintainDate = FormatHelper.TODateInt(dtNow);
                mKeyPart.MaintainTime = FormatHelper.TOTimeInt(dtNow);

                try
                {
                    this.DataProvider.Insert(mKeyPart);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Raise(mKeyPart.GetType(), "$Error_CS_Add_MKeyPart", ex);
                }
            }
        }

        public void AddMKeyPartTrace(MKeyPart mKeyPart)
        {

            mKeyPart.Sequence = Convert.ToInt32(this.GetUniqueMKeyPartSequence());

            if (this.DataProvider.CustomSearch(mKeyPart.GetType(), DomainObjectUtility.GetDomainObjectKeyScheme(mKeyPart.GetType()), DomainObjectUtility.GetDomainObjectKeyValues(mKeyPart)) != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_Primary_Key_Overlap");
                return;
            }

            {
                //2006/11/17,Laws Lu add get DateTime from db Server
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                mKeyPart.MaintainDate = FormatHelper.TODateInt(dtNow);
                mKeyPart.MaintainTime = FormatHelper.TOTimeInt(dtNow);

                try
                {
                    this.DataProvider.Insert(mKeyPart);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Raise(mKeyPart.GetType(), "$Error_CS_Add_MKeyPart", ex);
                }
            }
        }

        public void UpdateMKeyPart(MKeyPart mKeyPart)
        {
            this.DataProvider.Update(mKeyPart);
        }

        public void UpdateMKeyPart(MKeyPart oldMKeyPart, MKeyPart newMKeyPart)
        {
            this.CheckOPBOMItemIsKeyparts(newMKeyPart.MItemCode);

            bool result = true;

            //Modified By Karron Qiu,2005-9-19
            //保存修改前的起始序列号然后和修改后的做比对，
            //如果都没有更改那么就不用做范围重叠的Check而直接Update数据库；
            //如果任意一个有更改则需要做原有所有逻辑Check。
            if (oldMKeyPart.RunningCardStart != newMKeyPart.RunningCardStart ||
                oldMKeyPart.RunningCardEnd != newMKeyPart.RunningCardEnd)
            {
                oldMKeyPart.RunningCardStart = newMKeyPart.RunningCardStart;
                oldMKeyPart.RunningCardEnd = newMKeyPart.RunningCardEnd;

                result = RunningCardRangeCheck(oldMKeyPart);
            }

            // RunningCard范围不允许交叉
            if (result)
            {
                //2006/11/17,Laws Lu add get DateTime from db Server
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                newMKeyPart.MaintainDate = FormatHelper.TODateInt(dtNow);
                newMKeyPart.MaintainTime = FormatHelper.TOTimeInt(dtNow);

                this.DataProvider.BeginTransaction();
                try
                {
                    // 先删除，后添加
                    this.DataProvider.Delete(oldMKeyPart);
                    this.DataProvider.Insert(newMKeyPart);

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ExceptionManager.Raise(newMKeyPart.GetType(), "$Error_CS_Update_MKeyPart", ex);
                }
            }

        }

        public void DeleteMKeyPart(MKeyPart mKeyPart)
        {
            try
            {
                this.DataProvider.Delete(mKeyPart);

                DeleteMKeyPartDetail(mKeyPart.MItemCode, mKeyPart.Sequence, string.Empty);
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(mKeyPart.GetType(), "$Error_CS_Delete_MKeyPart", ex);
            }
        }

        public void DeleteMKeyPartBySql(string materialCode, string lotNo)
        {
            if (materialCode.Trim().Length == 0 || lotNo.Trim().Length == 0)
            {
                return;
            }
            string sql = "Delete  FROM TBLMKEYPART WHERE MITEMCODE = '" + materialCode.Trim().ToUpper() + "'   AND LOTNO = '" + lotNo.Trim().ToUpper() + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        public object GetMKeyPart(decimal sequence, string mItemCode)
        {
            return this.DataProvider.CustomSearch(typeof(MKeyPart), new object[] { sequence, mItemCode });
        }

        /// <summary>
        /// ** 功能描述:	获得所有的MKeyPart
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-18 9:43:27
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>MKeyPart的总记录数</returns>
        public object[] GetAllMKeyPart()
        {
            return this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLCondition(string.Format("select {0} from TBLMKEYPART order by MITEMCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)))));
        }


        public object GetMKeyPart(string materialCode, string lotNo)
        {
            string sql = "SELECT *  FROM TBLMKEYPART WHERE MITEMCODE = '" + materialCode.Trim().ToUpper() + "'   AND LOTNO = '" + lotNo.Trim().ToUpper() + "'";
            object[] returnValue = this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLCondition(sql));
            if (returnValue != null)
            {
                return returnValue[0];
            }

            return null;
        }

        /// <summary>
        /// ** 功能描述:	分页查询MKeyPart
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-05-17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">料号，精确查询</param>
        /// <param name="lotNo">keyParts序列号，精确查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> MKeyPart数组</returns>
        public object[] QueryMKeyPart(string itemCode, string runningCard, int inclusive, int exclusive)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard.Trim().Length > 0)
            {
                condition += " and exists (select * from TBLMKEYPARTDETAIL ";
                condition += " where MITEMCODE = TBLMKEYPART.MITEMCODE ";
                condition += " and SEQ = TBLMKEYPART.SEQ ";
                condition += " and SERIALNO = $RCARD ) ";
                paramList.Add(new SQLParameter("RCARD", typeof(string), runningCard));
            }

            return this.DataProvider.CustomQuery(typeof(MKeyPart), new PagerParamCondition(
                string.Format("select {0} from TBLMKEYPART where 1=1 {1}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)),
                condition),
                "MITEMCODE, SEQ",
                inclusive,
                exclusive,
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }
        //Laws Lu,2005/12/16,新增	夏新二期GAP
        public object[] QueryMKeyPartPagedByMo(string itemCode, string runningCard, string moCode, int mdate, int inclusive, int exclusive)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and RCARDSTART <= $RCARDS and RCARDEND >= $RCARDE";
                paramList.Add(new SQLParameter("RCARDS", typeof(string), runningCard));
                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }

            if (moCode != null && moCode != string.Empty)
            {
                condition += " and mocode = $mocode";
                paramList.Add(new SQLParameter("mocode", typeof(string), moCode));
            }

            if (mdate != 0)
            {
                condition += " and mdate = $mdate";
                paramList.Add(new SQLParameter("mdate", typeof(int), mdate));
            }

            return this.DataProvider.CustomQuery(typeof(MKeyPart), new PagerParamCondition(
                string.Format("select {0} from TBLMKEYPART where 1=1 {1}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)),
                condition),
                inclusive,
                exclusive,
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }

        //bighai.wang 2009/02/18

        public object[] QueryMKeyPartByMo(string itemCode, string runningCard, string moCode, int mdate, string muser, int inclusive, int exclusive)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += string.Format(" and MITEMCODE  = ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and EXISTS (";
                condition += string.Format(" SELECT * FROM tblmkeypartdetail WHERE serialno  = '{0}'", runningCard.Trim());
                condition += " AND mitemcode = tblmkeypart.mitemcode ";
                condition += " AND seq = tblmkeypart.seq ";
                condition += " ) ";
            }

            if (moCode != null && moCode != string.Empty)
            {
                condition += string.Format(" and mocode  in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            if (mdate != 0 && mdate.ToString() != null)
            {
                condition += string.Format(" and TBLMKEYPART.mdate  in ({0})", FormatHelper.ProcessQueryValues(mdate.ToString()));
            }

            if (muser != null && muser != string.Empty)
            {
                condition += " and TBLMKEYPART.muser = $muser";
                paramList.Add(new SQLParameter("muser", typeof(string), muser));
            }

            string Columns = "PCBA,RCARDEND,TBLMKEYPART.MUSER,LOTNO,SNSCALE,TBLMKEYPART.VENDORCODE,RCARDSTART,SEQ,tblmaterial.mname AS MITEMNAME,";
            Columns += "BIOS,RCARDPREFIX,TEMPLATENAME,VENDORITEMCODE,TBLMKEYPART.MDATE,TBLMKEYPART.MTIME,VERSION,MITEMCODE,MOCODE,TBLMKEYPART.EATTRIBUTE1,DATECODE";


            return this.DataProvider.CustomQuery(typeof(MKeyPart), new PagerParamCondition(
                string.Format("select {0} from TBLMKEYPART  LEFT OUTER JOIN tblmaterial ON TBLMKEYPART .Mitemcode=tblmaterial.mcode where 1=1 {1}  ",
                Columns,
                condition), "TBLMKEYPART.mdate desc,TBLMKEYPART.mtime desc",
                inclusive,
                exclusive,
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));

        }

        public int QueryMKeyPartByMoCount(string itemCode, string runningCard, string moCode, int mdate, string muser)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += string.Format(" and MITEMCODE  in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and EXISTS (";
                condition += " SELECT * FROM tblmkeypartdetail WHERE serialno = $RCARDE ";
                condition += " AND mitemcode = tblmkeypart.mitemcode ";
                condition += " AND seq = tblmkeypart.seq ";
                condition += " ) ";

                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }

            if (moCode != null && moCode != string.Empty)
            {
                condition += string.Format(" and mocode  in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            if (mdate != 0 && mdate.ToString() != string.Empty)
            {
                condition += string.Format(" and mdate  in ({0})", FormatHelper.ProcessQueryValues(mdate.ToString()));
            }

            if (muser != null && muser != string.Empty)
            {
                condition += " and muser = $muser";
                paramList.Add(new SQLParameter("muser", typeof(string), muser));
            }


            return this.DataProvider.GetCount(new SQLParamCondition(
                string.Format("select count(*) from TBLMKEYPART where 1=1 {0}", condition),
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }


        //end
        //Laws Lu,2005/12/16,新增	夏新二期GAP
        public object[] QueryMKeyPartByMo(string itemCode, string runningCard, string moCode, int mdate)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = '" + itemCode + "'";
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and EXISTS (";
                condition += " SELECT * FROM tblmkeypartdetail WHERE serialno = '" + runningCard.Trim().ToUpper() + "' ";
                condition += " AND mitemcode = tblmkeypart.mitemcode ";
                condition += " AND seq = tblmkeypart.seq ";
                condition += " ) ";
            }

            if (moCode != null && moCode != string.Empty)
            {
                condition += " and mocode = '" + moCode + "'";
            }

            if (mdate != 0)
            {
                condition += " and tblmkeypart.mdate = " + mdate;
            }

            string Columns = "PCBA,RCARDEND,TBLMKEYPART.MUSER,LOTNO,SNSCALE,TBLMKEYPART.VENDORCODE,RCARDSTART,SEQ,tblmaterial.mname AS MITEMNAME,";
            Columns += "BIOS,RCARDPREFIX,TEMPLATENAME,VENDORITEMCODE,TBLMKEYPART.MDATE,TBLMKEYPART.MTIME,VERSION,MITEMCODE,MOCODE,TBLMKEYPART.EATTRIBUTE1,DATECODE";

            return this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLCondition(
                string.Format("select {0} from TBLMKEYPART LEFT OUTER JOIN tblmaterial ON TBLMKEYPART .Mitemcode=tblmaterial.mcode where 1=1 {1} ORDER BY MITEMCODE, SEQ",
                Columns,
                condition)));
        }
        /// <summary>
        /// ** 功能描述:	分页查询MKeyPart
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-05-17
        /// ** 修 改:       jessie lee
        /// ** 日 期:       2005-12-14
        /// 增加日期查询条件
        /// </summary>
        /// <param name="itemCode">料号，精确查询</param>
        /// <param name="lotNo">keyParts序列号，精确查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <param name="mdate">操作日期</param>
        /// <returns> MKeyPart数组</returns>
        public object[] QueryMKeyPart(string itemCode, string runningCard, int mdate, int inclusive, int exclusive)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and RCARDSTART <= $RCARDS and RCARDEND >= $RCARDE";
                paramList.Add(new SQLParameter("RCARDS", typeof(string), runningCard));
                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }
            condition += " and mdate = $MDATE ";
            paramList.Add(new SQLParameter("MDATE", typeof(int), mdate));

            return this.DataProvider.CustomQuery(typeof(MKeyPart), new PagerParamCondition(
                string.Format("select {0} from TBLMKEYPART where 1=1 {1} order by mdate*1000000+mtime desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)),
                condition),
                inclusive,
                exclusive,
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }

        /// <summary>
        /// 不分页的查询
        /// Karron Qiu ,2005-10-11
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] QueryMKeyPart(string itemCode, string runningCard)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and RCARDSTART <= $RCARDS and RCARDEND >= $RCARDE";
                paramList.Add(new SQLParameter("RCARDS", typeof(string), runningCard));
                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }

            return this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLParamCondition(
                        string.Format("select {0} from TBLMKEYPART where 1=1 {1}",
                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)), condition)
                        , (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }


        //public object[] QueryMKeyPart(string mItemCode, int sequence, string rcardStart, string rcardEnd)
        //{
        //    string sql = string.Empty;
        //    sql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)) + " from TBLMKEYPART where 1=1 ";

        //    if (mItemCode != null && mItemCode.Trim().Length > 0)
        //    {
        //        sql += " and MITEMCODE = '" + mItemCode.Trim().ToUpper() + "' ";
        //    }

        //    if (sequence >= 0)
        //    {
        //        sql += " and SEQ = " + sequence.ToString() + " ";
        //    }

        //    if (rcardStart != null && rcardStart.Trim().Length > 0 && rcardEnd != null && rcardEnd.Trim().Length > 0)
        //    {
        //        sql += "and length(rcardstart) = " + rcardStart.Trim().Length.ToString() + " ";
        //        sql += "and length(rcardend) = " + rcardEnd.Trim().Length.ToString() + " ";
        //        sql += " and ((upper(rcardstart) >= '" + FormatHelper.CleanString(rcardStart.Trim().ToUpper()) + "' ";
        //        sql += " and upper(rcardend) <= '" + FormatHelper.CleanString(rcardEnd.Trim().ToUpper()) + "') ";

        //        sql += " or (upper(rcardstart) <= '" + FormatHelper.CleanString(rcardStart.Trim().ToUpper()) + " '";
        //        sql += " and upper(rcardend) >= '" + FormatHelper.CleanString(rcardStart.Trim().ToUpper()) + "') ";

        //        sql += " or (upper(rcardstart) <= '" + FormatHelper.CleanString(rcardEnd.Trim().ToUpper()) + "' ";
        //        sql += " and upper(rcardend) >= '" + FormatHelper.CleanString(rcardEnd.Trim().ToUpper()) + "')) ";
        //    }

        //    return this.DataProvider.CustomQuery(typeof(MKeyPart),
        //        new SQLCondition(sql));
        //}

        public object GetLatestMKeyPart(string itemCode)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE ";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            object[] onjs = this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLParamCondition(
                        string.Format("select {0} from TBLMKEYPART where 1=1 {1} order by MDATE desc, MTIME desc ",
                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)), condition)
                        , (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));

            if (onjs != null && onjs.Length > 0)
            {
                return onjs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 不分页的查询
        /// added by jessie lee, 2005/12/14
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] QueryMKeyPart(string itemCode, string runningCard, int mdate)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and RCARDSTART <= $RCARDS and RCARDEND >= $RCARDE";
                paramList.Add(new SQLParameter("RCARDS", typeof(string), runningCard));
                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }
            condition += " and mdate = $MDATE ";
            paramList.Add(new SQLParameter("MDATE", typeof(int), mdate));

            return this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLParamCondition(
                string.Format("select {0} from TBLMKEYPART where 1=1 {1} order by mdate*1000000+mtime desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)), condition)
                , (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }

        /// <summary>
        /// ** 功能描述:	查询MKeyPart的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-18 9:43:27
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">料号，精确查询</param>
        /// <param name="lotNo">keyParts序列号，精确查询</param>
        /// <returns> MKeyPart的总记录数</returns>
        public int QueryMKeyPartCount(string itemCode, string runningCard)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and RCARDSTART <= $RCARDS and RCARDEND >= $RCARDE";
                paramList.Add(new SQLParameter("RCARDS", typeof(string), runningCard));
                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }


            return this.DataProvider.GetCount(new SQLParamCondition(
                string.Format("select count(*) from TBLMKEYPART where 1=1 {0}", condition),
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }

        /// <summary>
        /// ** 功能描述:	查询MKeyPart的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-18 9:43:27
        /// ** 修 改:       jessie lee
        /// ** 日 期:       2005/12/14
        /// </summary>
        /// <param name="itemCode">料号，精确查询</param>
        /// <param name="lotNo">keyParts序列号，精确查询</param>
        /// <param name="mdate">操作日期</param>
        /// <returns> MKeyPart的总记录数</returns>
        public int QueryMKeyPartCount(string itemCode, string runningCard, int mdate)
        {
            string condition = string.Empty;
            ArrayList paramList = new ArrayList();

            if (itemCode != null && itemCode != string.Empty)
            {
                condition += " and MITEMCODE = $MITEMCODE";
                paramList.Add(new SQLParameter("MITEMCODE", typeof(string), itemCode));
            }

            if (runningCard != null && runningCard != string.Empty)
            {
                condition += " and RCARDSTART <= $RCARDS and RCARDEND >= $RCARDE";
                paramList.Add(new SQLParameter("RCARDS", typeof(string), runningCard));
                paramList.Add(new SQLParameter("RCARDE", typeof(string), runningCard));
            }

            condition += " and mdate = $MDATE ";
            paramList.Add(new SQLParameter("MDATE", typeof(int), mdate));

            return this.DataProvider.GetCount(new SQLParamCondition(
                string.Format("select count(*) from TBLMKEYPART where 1=1 {0}", condition),
                (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));
        }

        public bool RunningCardRangeCheck(MKeyPart mKeyPart)
        {
            if (mKeyPart == null)
            {
                return true;
            }

            if (string.Compare(mKeyPart.RunningCardStart, mKeyPart.RunningCardEnd) > 0)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd");
                return false;
            }
            //Laws Lu,2005/08/30,修改	
            //没有考虑料号相同、SEQ相同的情况
            /*AM0146 KeyParts上料采集时，在判断用户输入的KeyParts序列号存在于用户维护的KeyParts备料资料中以后，
             * 增加判断用户输入的KeyParts序列号长度是否与该笔备料资料的KeyParts起止序列号长度一致，
             * 如果不一致则直接提示用户KeyParts序列号不存在，例如这个问题当中，
             * 尽管P144401188通过字符串大小比较找到备料记录P14440101~P14440150，
             * 但是其长度与备料记录不相等，也判断为KeyParts序列号P144401188不存在，
             * 除非备料资料中还存在着KeyParts岂止序列号P144401120~P144401600这样的资料。
             * 另外在KeyParts备料维护中允许用户输入KeyParts岂止序列号P14440101~P14440150这样的备料资料后，
             * 还能够输入KeyParts岂止序列号P144401120~P144401600这样的资料
             */
            //				if ( this.DataProvider.GetCount( new SQLParamCondition(
            //				"select count(*) from TBLMKEYPART where (($RCARDS1 between RCARDSTART and RCARDEND) or ($RCARDE1 between RCARDSTART and RCARDEND) or ($RCARDS2 <= RCARDSTART and $RCARDE2 >= RCARDEND)) and (MITEMCODE <> $MITEMCODE or SEQ <> $SEQ)", 
            //				new SQLParameter[]{
            //									  new SQLParameter("RCARDS1", typeof(string), mKeyPart.RunningCardStart),
            //									  new SQLParameter("RCARDE1", typeof(string), mKeyPart.RunningCardEnd),
            //									  new SQLParameter("RCARDS2", typeof(string), mKeyPart.RunningCardStart),
            //									  new SQLParameter("RCARDE2", typeof(string), mKeyPart.RunningCardEnd),
            //									  new SQLParameter("MITEMCODE", typeof(string), mKeyPart.MItemCode),
            //									  new SQLParameter("SEQ", typeof(decimal), mKeyPart.Sequence)})) > 0 )
            if (this.DataProvider.GetCount(new SQLCondition(
                "select count(*) from TBLMKEYPART where "
                + " (('" + mKeyPart.RunningCardStart + "' between RCARDSTART and RCARDEND) and length(RCARDSTART) = length('" + mKeyPart.RunningCardStart + "')) "
                + "or (('" + mKeyPart.RunningCardEnd + "' between RCARDSTART and RCARDEND) and length(RCARDEND) = length('" + mKeyPart.RunningCardEnd + "')) or "
                + " ('" + mKeyPart.RunningCardStart + "' <= RCARDSTART and length(RCARDEND) = length('"
                + mKeyPart.RunningCardEnd + "') and length(RCARDSTART) = length('"
                + mKeyPart.RunningCardStart + "') and '" + mKeyPart.RunningCardEnd + "' >= RCARDEND) "
                + " and (MITEMCODE <> '" + mKeyPart.MItemCode + "' or SEQ <> '" + mKeyPart.Sequence + "')")) > 0)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_CS_RunningCard_Range_Overlap");
                return false;
            }

            return true;
        }
        //Laws Lu,2005/08/31,新增	检查RunningCard的长度和范围是否符合条件
        public bool RunningCardRangeCheck(string mItemCode, string runningCard, string sq)
        {
            if (this.DataProvider.GetCount(new SQLCondition(
                "select count(*) from TBLMKEYPART where "
                + " ('" + runningCard + "' between RCARDSTART and RCARDEND) and length(RCARDSTART) = length('" + runningCard + "') "
                + " and MITEMCODE = '" + mItemCode + "' and SEQ = '" + sq + "'")) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal GetUniqueMKeyPartSequence()
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(MKeyPart),
                new PagerCondition(string.Format("select seq from TBLMKEYPART", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart))),
                "seq desc", 1, 1));

            if (objs == null || objs.Length < 1)
            {
                return 0;
            }

            return ((MKeyPart)objs[0]).Sequence + 1;
        }

        public Messages CheckOPBOMItemIsKeyparts(string opBomItemCode)
        {
            Messages messages = new Messages();
            OPBOMFacade opbomFacade = new OPBOMFacade(this.DataProvider);

            //  OPBOMItemCode存在
            if (!opbomFacade.IsOPBOMItemExist(opBomItemCode))
            {
                //				this.ShowMessage("料号不存在，请输入新的料号");
                messages.Add(new Message(new Exception("$Error_CS_OPBOMItem_Not_Exist")));
                return messages;
            }

            // 是否Keyparts料
            if (!opbomFacade.OPBOMItemIsKeyPartsControl(opBomItemCode))
            {
                //				this.ShowMessage("料号不是工序Bom中的KeyParts管控料，请输入新的料号");
                messages.Add(new Message(new Exception("$Error_CS_OPBOMItem_Is_Not_Keyparts_Control")));
                return messages;
            }

            return messages;
        }

        /// <summary>
        /// 查询已经在序列号上实际存在的KeyPart列表
        /// </summary>
        /// <param name="runningCard"></param>
        /// <param name="moCode"></param>
        /// <returns></returns>
        public BenQGuru.eMES.Domain.DataCollect.OnWIPItem[] QueryLoadedKeyPartByRCard(string runningCard, string moCode)
        {
            string strSql = "SELECT * FROM tblOnWIPItem WHERE RCard='" + runningCard + "' AND MOCode='" + moCode + "' AND MCardType='" + MCardType.MCardType_Keyparts + "' AND ActionType='" + ((int)MaterialType.CollectMaterial).ToString() + "' ORDER BY MDate,MTime";
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            BenQGuru.eMES.Domain.DataCollect.OnWIPItem[] items = new BenQGuru.eMES.Domain.DataCollect.OnWIPItem[objs.Length];
            objs.CopyTo(items, 0);
            return items;
        }

        /// <summary>
        /// 查询已经在序列号上实际存在的Part列表
        /// </summary>
        /// <param name="runningCard"></param>
        /// <param name="moCode"></param>
        /// <returns></returns>
        public BenQGuru.eMES.Domain.DataCollect.OnWIPItem[] QueryLoadedPartByRCard(string runningCard, string moCode)
        {
            string strSql = " SELECT * FROM tblOnWIPItem WHERE RCard='" + runningCard + "' ";
            if (moCode.Trim().Length > 0)
            {
                strSql += " AND MOCode='" + moCode + "' ";
            }
            strSql += " AND ActionType='" + ((int)MaterialType.CollectMaterial).ToString() + "' ";
            strSql += " ORDER BY MDate,MTime ";
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            BenQGuru.eMES.Domain.DataCollect.OnWIPItem[] items = new BenQGuru.eMES.Domain.DataCollect.OnWIPItem[objs.Length];
            objs.CopyTo(items, 0);
            return items;
        }

        /// <summary>
        /// 检查KeyPart是否经过下料
        /// 用在下料采集界面的KeyPart采不良检查
        /// </summary>
        /// <param name="keyPart"></param>
        /// <returns></returns>
        public Messages CheckKeyPartUnLoaded(string keyPartNo)
        {
            Messages msg = new Messages();
            string strSql = "SELECT * FROM tblOnWIPItem WHERE MCard='" + keyPartNo + "' ORDER BY MDate DESC,MTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
            {
                msg.Add(new Message(MessageType.Error, "$KeyPart_NG_ErrorKeyPart"));
                return msg;
            }
            BenQGuru.eMES.Domain.DataCollect.OnWIPItem item = (BenQGuru.eMES.Domain.DataCollect.OnWIPItem)objs[0];
            if (item.ActionType != (int)MaterialType.DropMaterial)
            {
                msg.Add(new Message(MessageType.Error, "$KeyPart_NG_KeyPart_NotUnLoaded"));
                return msg;
            }
            return msg;
        }

        public bool CheckMKeyPartUsed(MKeyPart mKeyPart)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) AS qty ";
            sql += "FROM tblonwipitem ";
            sql += "WHERE EXISTS (SELECT * ";
            sql += "       FROM tblmkeypartdetail ";
            sql += "       WHERE mitemcode = '{0}' ";
            sql += "       AND seq = {1} ";
            sql += "       AND mcard = serialno) ";
            sql = string.Format(sql, mKeyPart.MItemCode, mKeyPart.Sequence.ToString());

            object[] list = this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(sql));
            if (list == null)
            {
                return false;
            }
            else
            {
                return ((OnWIPItem)list[0]).Qty > 0;
            }
        }


        //bighai.wang

        public long CheckMKeyPartDetail(string snSeqValue, string itemcode, string startsn, string endsn, string snPrefix)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) AS qty ";
            sql += "FROM Tblmkeypartdetail ";
            sql += "WHERE 1=1";
            sql += " and seq='{0}' and mitemcode='{1}'";
            sql += " and serialno>='{2}' and serialno<='{3}' and LENGTH(serialno)=length('{2}') and  LENGTH(serialno)=length('{3}') ";

            sql = string.Format(sql, snSeqValue.Trim(), itemcode.Trim(), snPrefix.Trim() + startsn, snPrefix.Trim() + endsn);

            object[] list = this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(sql));
            if (list == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(((OnWIPItem)list[0]).Qty);
            }

        }
        #endregion

        #region MKeyPartDetail

        public MKeyPartDetail CreateNewMKeyPartDetail()
        {
            return new MKeyPartDetail();
        }

        public void AddMKeyPartDetail(MKeyPartDetail mKeyPartDetail)
        {
            this.DataProvider.Insert(mKeyPartDetail);
        }

        public void UpdateMKeyPartDetail(MKeyPartDetail mKeyPartDetail)
        {
            this.DataProvider.Update(mKeyPartDetail);
        }

        public void DeleteMKeyPartDetail(MKeyPartDetail mKeyPartDetail)
        {
            this.DataProvider.Delete(mKeyPartDetail);
        }

        public void DeleteMKeyPartDetail(string mItemCode, int sequence, string eAttribute1)
        {
            string sql = string.Empty;
            sql += "DELETE FROM tblmkeypartdetail ";
            sql += "WHERE 1 = 1 ";
            sql += "AND mitemcode = '" + mItemCode.Trim().ToUpper() + "' ";
            sql += "AND seq = " + sequence.ToString() + " ";
            if (eAttribute1 != null && eAttribute1.Trim().Length > 0)
            {
                sql += "AND eattribute1 = '" + eAttribute1.Trim().ToUpper() + "' ";
            }

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeleteMKeyPartDetail(string mItemCode, string serialNo)
        {
            if (mItemCode == null || mItemCode.Trim().Length <= 0 || serialNo == null || serialNo.Trim().Length <= 0)
            {
                return;
            }

            string sql = string.Empty;
            sql += "DELETE FROM tblmkeypartdetail ";
            sql += "WHERE 1 = 1 ";
            sql += "AND mitemcode = '" + mItemCode.Trim().ToUpper() + "' ";
            sql += "AND serialNo = '" + serialNo.Trim().ToUpper() + "' ";


            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object GetMKeyPartDetail(string mItemCode, string serialNo)
        {
            return this.DataProvider.CustomSearch(typeof(MKeyPartDetail), new object[] { mItemCode, serialNo });
        }

        public object[] QueryMKeyPartDetail(string mItemCode, int sequence, string startSN, string endSN)
        {
            string sql = string.Empty;
            sql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPartDetail)) + " from TBLMKEYPARTDETAIL where 1=1 ";

            if (mItemCode != null && mItemCode.Trim().Length > 0)
            {
                sql += " and MITEMCODE = '" + mItemCode.Trim().ToUpper() + "' ";
            }

            if (sequence >= 0)
            {
                sql += " and SEQ = " + sequence.ToString() + " ";
            }

            if (startSN != null && startSN.Trim().Length > 0 && endSN != null && endSN.Trim().Length > 0 && startSN.Trim().Length == endSN.Trim().Length)
            {
                sql += " and length(SERIALNO) = " + startSN.Trim().Length.ToString() + " ";
                sql += " and SERIALNO >= '" + startSN.Trim().ToUpper() + "' ";
                sql += " and SERIALNO <= '" + endSN.Trim().ToUpper() + "' ";
            }
            sql += " ORDER BY serialno ";

            return this.DataProvider.CustomQuery(typeof(MKeyPartDetail),
                new SQLCondition(sql));
        }

        public object[] QueryMKeyPartDetailWithHead(string mItemCode, int sequence, string startSN, string endSN)
        {
            string sql = string.Empty;
            sql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPartDetail)) + " from TBLMKEYPARTDETAIL where 1=1 ";

            if (mItemCode != null && mItemCode.Trim().Length > 0)
            {
                sql += " and MITEMCODE = '" + mItemCode.Trim().ToUpper() + "' ";
            }

            if (sequence >= 0)
            {
                sql += " and SEQ = " + sequence.ToString() + " ";
            }

            if (startSN != null && startSN.Trim().Length > 0 && endSN != null && endSN.Trim().Length > 0 && startSN.Trim().Length == endSN.Trim().Length)
            {
                sql += " and length(SERIALNO) = " + startSN.Trim().Length.ToString() + " ";
                sql += " and SERIALNO >= '" + startSN.Trim().ToUpper() + "' ";
                sql += " and SERIALNO <= '" + endSN.Trim().ToUpper() + "' ";
            }

            sql += " and eattribute1 <> 'N' ";

            return this.DataProvider.CustomQuery(typeof(MKeyPartDetail),
                new SQLCondition(string.Format(sql)));
        }

        public int QueryMKeyPartDetailCountNotInBOMRelation(string mItemCode, string serialNo)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblmkeypartdetail ";
            sql += "WHERE serialno = '{1}' ";
            sql += "AND mitemcode NOT IN ( ";
            sql += "    SELECT DISTINCT itemcode ";
            sql += "    FROM tblsbom ";
            sql += "    CONNECT BY PRIOR itemcode = sbitemcode ";
            sql += "    START WITH sbitemcode = '{0}' ";
            sql += ") ";
            sql += "AND mitemcode <> '{0}' ";
            sql = string.Format(sql, mItemCode, serialNo);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void UpdateMKeyPartDetialEAttribute1(string mItemCode, int seq, string eAttribute1)
        {
            string sql = "";
            sql += "UPDATE tblmkeypartdetail ";
            sql += "SET eattribute1 = '" + eAttribute1 + "' ";
            sql += "WHERE mitemcode = '" + mItemCode.Trim().ToUpper() + "' ";
            sql += "AND seq = " + seq.ToString() + " ";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region MaterialReqStd
        /// <summary>
        /// 
        /// </summary>
        public MaterialReqStd CreateNewMaterialReqStd()
        {
            return new MaterialReqStd();
        }

        public void AddMaterialReqStd(MaterialReqStd materialReqStd)
        {
            this._helper.AddDomainObject(materialReqStd);
        }

        public void UpdateMaterialReqStd(MaterialReqStd materialReqStd)
        {
            this._helper.UpdateDomainObject(materialReqStd);
        }

        public void DeleteMaterialReqStd(MaterialReqStd materialReqStd)
        {
            this._helper.DeleteDomainObject(materialReqStd);
        }

        public void DeleteMaterialReqStd(MaterialReqStd[] materialReqStd)
        {
            this._helper.DeleteDomainObject(materialReqStd);
        }

        public object GetMaterialReqStd(string iTEMCODE, int oRGID)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialReqStd), new object[] { iTEMCODE, oRGID });
        }

        public object GetMaterialReqStdWithItemDesc(string iTEMCODE, int oRGID)
        {
            //return this.DataProvider.CustomSearch(typeof(MaterialReqStdWithItemDesc), new object[] { iTEMCODE, oRGID });

            string sql = string.Empty;
            sql = " SELECT tblMaterialreqstd.itemcode,tblMaterial.mdesc itemdesc, tblMaterialreqstd.orgid ";
            sql += ", tblMaterialreqstd.requestqty,tblMaterialreqstd.muser, tblMaterialreqstd.mdate, tblMaterialreqstd.mtime ";
            sql += " FROM tblMaterial,tblMaterialreqstd  where 1=1 ";
            sql += string.Format(@" AND tblMaterialreqstd.itemcode = '{0}'", iTEMCODE);
            sql += string.Format(@" AND tblMaterialreqstd.orgid = {0}", oRGID);
            sql += " AND tblMaterialreqstd.itemcode=tblMaterial.mcode(+)";
            return this._domainDataProvider.CustomQuery(typeof(Domain.Material.MaterialReqStdWithItemDesc), new SQLCondition(sql));


        }



        /// <summary>
        /// ** 功能描述:	查询MaterialReqStd的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2009-3-18 12:45:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="iTEMCODE">ITEMCODE，模糊查询</param>
        /// <param name="oRGID">ORGID，模糊查询</param>
        /// <returns> MaterialReqStd的总记录数</returns>
        public int QueryMaterialReqStdCount(string iTEMCODE, int oRGID)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblMaterialReqStd where 1=1 and ITEMCODE like '{0}%'  and ORGID like '{1}%' ", iTEMCODE, oRGID)));
        }

        #region 产品要料标准维护

        public string GetMaterialReqStdWhere(string itemCode, string firstClass, string twoClass, string threeClass)
        {
            string sqlwhere = string.Empty;

            sqlwhere += " FROM tblMaterialreqstd ,tblMaterial,tblItemclass where 1=1 ";
            sqlwhere += " AND tblMaterialreqstd.itemcode=tblMaterial.mcode AND tblMaterial.mgroup = tblItemclass.itemgroup";

            if (itemCode != null && itemCode != string.Empty)
                sqlwhere += string.Format(@" AND tblMaterialreqstd.itemcode IN ({0})", FormatHelper.ProcessQueryValues(itemCode));
            if (firstClass != null && firstClass != string.Empty)
                sqlwhere += string.Format(@" AND tblitemclass.firstclass = '{0}'", firstClass);
            if (twoClass != null && twoClass != string.Empty)
                sqlwhere += string.Format(@" AND tblitemclass.secondclass = '{0}'", twoClass);
            if (threeClass != null && threeClass != string.Empty)
                sqlwhere += string.Format(@" AND tblitemclass.thirdclass = '{0}'", threeClass);

            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sqlwhere += "  AND tblMaterialreqstd.orgid IN (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
            }





            return sqlwhere;
        }
        public object[] QueryMaterialReqStd(string itemCode, string firstClass, string twoClass, string threeClass, int inclusive, int exclusive)
        {
            string sqlwhere = GetMaterialReqStdWhere(itemCode, firstClass, twoClass, threeClass);

            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialReqStd)) + " ,tblMaterial.mdesc itemdesc {0}";
            sql = string.Format(sql, sqlwhere);

            return this.DataProvider.CustomQuery(typeof(MaterialReqStdWithItemDesc), new PagerCondition(
                sql, "tblMaterialreqstd.itemcode ", inclusive, exclusive));
        }

        public int QueryMaterialReqStdCount(string itemCode, string firstClass, string twoClass, string threeClass)
        {
            string sqlwhere = GetMaterialReqStdWhere(itemCode, firstClass, twoClass, threeClass);
            return this.DataProvider.GetCount(new SQLCondition(string.Format("SELECT  COUNT(*) {0} ", sqlwhere)));
        }

        #endregion

        /// <summary>
        /// ** 功能描述:	分页查询MaterialReqStd
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2009-3-18 12:45:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="iTEMCODE">ITEMCODE，模糊查询</param>
        /// <param name="oRGID">ORGID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> MaterialReqStd数组</returns>
        public object[] QueryMaterialReqStd(string iTEMCODE, int oRGID, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(MaterialReqStd), new PagerCondition(string.Format("select {0} from tblMaterialReqStd where 1=1 and ITEMCODE like '{1}%'  and ORGID like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialReqStd)), iTEMCODE, oRGID), "ITEMCODE,ORGID", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的MaterialReqStd
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2009-3-18 12:45:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>MaterialReqStd的总记录数</returns>
        public object[] GetAllMaterialReqStd()
        {
            return this.DataProvider.CustomQuery(typeof(MaterialReqStd), new SQLCondition(string.Format("select {0} from tblMaterialReqStd order by ITEMCODE,ORGID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialReqStd)))));
        }


        #endregion

        #region WorkPlan
        /// <summary>
        /// 
        /// </summary>
        public WorkPlan CreateNewWorkPlan()
        {
            return new WorkPlan();
        }

        public void AddWorkPlan(WorkPlan workPlan)
        {
            this._helper.AddDomainObject(workPlan);
        }

        public void UpdateWorkPlan(WorkPlan workPlan)
        {
            this._helper.UpdateDomainObject(workPlan);
        }

        public void DeleteWorkPlan(WorkPlan workPlan)
        {
            this._helper.DeleteDomainObject(workPlan);
        }

        public void DeleteWorkPlan(WorkPlan[] workPlan)
        {
            this._helper.DeleteDomainObject(workPlan);
        }

        public object GetWorkPlan(string bigSSCode, int planDate, string mCode, decimal moSeq)
        {
            return this.DataProvider.CustomSearch(typeof(WorkPlan), new object[] { bigSSCode, planDate, mCode, moSeq });
        }

        public int QueryWorkPlanCount(string bigSSCode, int planDate, string mCode, decimal moSeq)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWorkPlan where 1=1 and BIGSSCODE like '{0}%'  and PLANDATE like '{1}%'  and MOCODE like '{2}%'  and MOSEQ like '{3}%' ", bigSSCode, planDate, mCode, moSeq)));
        }

        public object[] QueryWorkPlan(string bigSSCode, int planDate, string mCode, decimal moSeq, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(WorkPlanWithQty), new PagerCondition(string.Format("select {0} from TBLWorkPlan where 1=1 and BIGSSCODE like '{1}%'  and PLANDATE like '{2}%'  and MOCODE like '{3}%'  and MOSEQ like '{4}%' ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)), bigSSCode, planDate, mCode, moSeq), "BIGSSCODE,PLANDATE,MOCODE,MOSEQ", inclusive, exclusive));
        }

        public object[] QueryWorkPlan(string bigSSCode, int planStartDate, int planEndDate, string mCode, string actionStatus, string materialStatus, int inclusive, int exclusive)
        {
            string sql = " SELECT TBLWORKPLAN.BIGSSCODE,TBLWORKPLAN.PLANDATE,TBLWORKPLAN.MOCODE,TBLWORKPLAN.MOSEQ,";
            sql += "            TBLWORKPLAN.PLANSEQ,TBLWORKPLAN.ITEMCODE || ' - ' || TBLMATERIAL.MDESC AS ITEMCODE,TBLWORKPLAN.PLANQTY,TBLWORKPLAN.ACTQTY,";
            sql += "            TBLWORKPLAN.MATERIALQTY,TBLWORKPLAN.PLANSTARTTIME,TBLWORKPLAN.PLANENDTIME,TBLWORKPLAN.LASTRECEIVETIME,TBLWORKPLAN.LASTREQTIME,";
            sql += "            TBLWORKPLAN.PROMISETIME,TBLWORKPLAN.ACTIONSTATUS,TBLWORKPLAN.MATERIALSTATUS,TBLWORKPLAN.MUSER,TBLWORKPLAN.MDATE,";
            sql += "            TBLWORKPLAN.MTIME,TBLMATERIAL.MMODELCODE   FROM TBLWORKPLAN";
            sql += "    LEFT JOIN TBLMATERIAL ON TBLWORKPLAN.ITEMCODE = TBLMATERIAL.MCODE  WHERE 1=1";

            sql += GetWorkPlanWhere(bigSSCode, planStartDate, planEndDate, mCode, actionStatus, materialStatus);
            sql += " ORDER BY TBLWorkPlan.PLANDATE,TBLWorkPlan.BIGSSCODE,TBLWorkPlan.PlanSEQ ";
            return this.DataProvider.CustomQuery(typeof(WorkPlanWithQty), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryWorkPlanCount(string bigSSCode, int planStartDate, int planEndDate, string mCode, string actionStatus, string materialStatus)
        {
            string sql = GetWorkPlanWhere(bigSSCode, planStartDate, planEndDate, mCode, actionStatus, materialStatus);
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWorkPlan WHERE 1=1  {0} ", sql)));
        }

        public object[] QueryWorkPlan(string bigSSCode, string moCode)
        {
            string sql = " SELECT * FROM TBLWorkPlan WHERE 1=1 ";

            if (bigSSCode.Trim() != string.Empty)
            {
                sql += " AND BIGSSCODE='" + bigSSCode.Trim().ToUpper() + "'";
            }

            if (moCode.Trim() != string.Empty)
            {
                sql += " AND MOCODE='" + moCode.Trim().ToUpper() + "'";
            }

            return this.DataProvider.CustomQuery(typeof(WorkPlan), new SQLCondition(sql));
        }

        public string GetWorkPlanWhere(string bigSSCode, int planStartDate, int planEndDate, string mCode, string actionStatus, string materialStatus)
        {
            string sqlwhere = string.Empty;

            if (bigSSCode != null && bigSSCode != string.Empty)
            {
                if (bigSSCode.IndexOf(",") >= 0)
                {
                    bigSSCode = FormatHelper.ProcessQueryValues(bigSSCode);
                    sqlwhere += string.Format(@" AND TBLWorkPlan.bigSSCode IN ({0})", bigSSCode);
                }
                else
                {
                    sqlwhere += string.Format(@" AND TBLWorkPlan.bigSSCode like ('%{0}%')", bigSSCode);
                }
            }

            if (planStartDate != 0)
                sqlwhere += string.Format(@" AND TBLWorkPlan.planDate >= {0}", planStartDate);
            if (planEndDate != 0)
                sqlwhere += string.Format(@" AND TBLWorkPlan.planDate< = {0}", planEndDate);

            if (mCode != null && mCode != string.Empty)
                sqlwhere += string.Format(@" AND TBLWorkPlan.moCode LIKE ('%{0}%')", mCode);
            if (actionStatus != null && actionStatus != string.Empty)
                sqlwhere += string.Format(@" AND TBLWorkPlan.actionStatus = '{0}'", actionStatus);
            if (materialStatus != null && materialStatus != string.Empty)
                sqlwhere += string.Format(@" AND TBLWorkPlan.materialStatus = '{0}'", materialStatus);
            return sqlwhere;
        }

        public object[] QueryMaterialTrans(int planStartDate, int planEndDate, int inclusive, int exclusive)
        {
            string sql = GetMaterialTransWhere(planStartDate, planEndDate);
            return this.DataProvider.CustomQuery(typeof(WorkPlanWithQty), new PagerCondition(string.Format("SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)) + ",(PlanQty-MaterialQty) LackQTY  FROM TBLWorkPlan WHERE 1=1 {0}", sql), "PLANDATE,BIGSSCODE,PlanSEQ", inclusive, exclusive));

        }

        public int QueryMaterialTransCount(int planStartDate, int planEndDate)
        {
            string sql = GetMaterialTransWhere(planStartDate, planEndDate);
            return this.DataProvider.GetCount(new SQLCondition(string.Format("SELECT  COUNT(*) FROM TBLWorkPlan WHERE 1=1  {0} ", sql)));
        }

        public string GetMaterialTransWhere(int planStartDate, int planEndDate)
        {
            string sqlwhere = string.Empty;

            if (planStartDate != 0)
                sqlwhere += string.Format(@" AND planDate >= {0}", planStartDate);
            if (planEndDate != 0)
                sqlwhere += string.Format(@" AND planDate< = {0}", planEndDate);

            return sqlwhere;
        }

        public object[] QueryWorkPlanByItemCode(string itemcode, string bigSSCode, int planDate, string mCode,
                                                decimal moSeq, int inclusive, int exclusive)
        {
            string sql = " select a.*  from (select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)) + " ";
            sql += " from  TBLWorkPlan where 1=1 ";
            if (itemcode.Trim() != string.Empty)
            {
                sql += " and  itemcode='" + itemcode.Trim().ToUpper() + "'";
            }

            sql += " and materialqty-actqty>0";
            sql += "  MINUS (SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)) + "  ";
            sql += " FROM TBLWORKPLAN WHERE   (PLANDATE = " + planDate + " AND MOCODE  = '" + mCode.Trim().ToUpper() + "' ";
            sql += "  AND BIGSSCODE  = '" + bigSSCode.Trim().ToUpper() + "' AND MOSEQ = " + moSeq + "))) a";
            sql += " order by planDate,bigSSCode,planseq";

            return this.DataProvider.CustomQuery(typeof(WorkPlan), new PagerCondition(sql, inclusive, exclusive));

        }

        public int GetWorkPlanCountByItemCode(string itemcode, string bigSSCode, int planDate, string mCode, decimal moSeq)
        {
            string sql = " select count(*) from (select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)) + "";
            sql += " from TBLWorkPlan where 1=1 ";

            if (itemcode.Trim() != string.Empty)
            {
                sql += " and  itemcode='" + itemcode.Trim().ToUpper() + "'";
            }

            sql += " and materialqty-actqty>0";
            sql += "  MINUS (SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)) + " ";
            sql += " FROM TBLWORKPLAN WHERE   (PLANDATE = " + planDate + " AND MOCODE  = '" + mCode.Trim().ToUpper() + "' ";
            sql += "  AND BIGSSCODE  = '" + bigSSCode.Trim().ToUpper() + "' AND MOSEQ = " + moSeq + ")))";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object GetWorkPlan(string bigSSCode, int planDate, int planSeq)
        {
            string sql = " select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(WorkPlan)) + "  from tblworkplan where 1=1 ";

            if (bigSSCode.Trim() != string.Empty)
            {
                sql += " and  bigSSCode='" + bigSSCode.Trim().ToUpper() + "'";
            }

            if (planDate > 0)
            {
                sql += " and  PLANDATE=" + planDate + "";
            }

            if (planSeq > 0)
            {
                sql += " and  planSeq=" + planSeq + "";
            }

            if (this.DataProvider.CustomQuery(typeof(WorkPlan), new SQLCondition(sql)) != null)
            {
                return this.DataProvider.CustomQuery(typeof(WorkPlan), new SQLCondition(sql))[0];
            }

            return null;
        }


        /// <summary>
        /// ** 功能描述:	获得所有的WorkPlan
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2009-3-20 9:21:15
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WorkPlan的总记录数</returns>
        public object[] GetAllWorkPlan()
        {
            return this.DataProvider.CustomQuery(typeof(WorkPlan), new SQLCondition(string.Format("select {0} from TBLWorkPlan order by BIGSSCODE,PLANDATE,MOCODE,MOSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WorkPlan)))));
        }


        #endregion

        #region MaterialIssue
        /// <summary>
        /// 
        /// </summary>
        public MaterialIssue CreateNewMaterialIssue()
        {
            return new MaterialIssue();
        }

        public void AddMaterialIssue(MaterialIssue materialIssue)
        {
            this._helper.AddDomainObject(materialIssue);
        }

        public void UpdateMaterialIssue(MaterialIssue materialIssue)
        {
            this._helper.UpdateDomainObject(materialIssue);
        }

        public void DeleteMaterialIssue(MaterialIssue materialIssue)
        {
            this._helper.DeleteDomainObject(materialIssue);
        }

        public void DeleteMaterialIssue(MaterialIssue[] materialIssue)
        {
            this._helper.DeleteDomainObject(materialIssue);
        }

        public object GetMaterialIssue(string bigSSCode, int planDate, string mCode, decimal moSeq, decimal issueSEQ)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialIssue), new object[] { bigSSCode, planDate, mCode, moSeq, issueSEQ });
        }

        public object[] QueryMaterialIssue(string bigSSCode, int planDate, string moCode, decimal moSeq, string materialIssueType, int inclusive, int exclusive)
        {

            string sql = " SELECT a.BIGSSCODE,a.PLANDATE,a.MOCODE,a.MOSEQ,a.ISSUESEQ,a.ISSUEQTY,a.ISSUETYPE,a.ISSUESTATUS, ";
            sql += " a.MUSER || '-' || b.username AS MUSER,a.MDATE,a.MTIME  FROM TBLMATERIALISSUE a  LEFT OUTER JOIN tbluser b ON a.muser=b.usercode WHERE 1=1";
            sql += GetMaterialIssueWhereSql(bigSSCode, planDate, moCode, moSeq, materialIssueType);
            sql += " order by a.ISSUESEQ ";
            return this.DataProvider.CustomQuery(typeof(MaterialIssue), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetMaterialIssueCount(string bigSSCode, int planDate, string moCode, decimal moSeq, string materialIssueType)
        {
            string sql = "SELECT Count(*) FROM tblmaterialIssue WHERE 1=1 ";
            sql += GetMaterialIssueWhereSql(bigSSCode, planDate, moCode, moSeq, materialIssueType);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetMaterialIssueMaxIssueSEQ(string bigSSCode, int planDate, string moCode, decimal moSeq)
        {
            string sql = "SELECT Max(IssueSEQ) FROM tblmaterialIssue WHERE 1=1 ";
            sql += GetMaterialIssueWhereSql(bigSSCode, planDate, moCode, moSeq, string.Empty);

            try
            {
                int seq = this.DataProvider.GetCount(new SQLCondition(sql));
                return seq + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void UpdateMaterialIssueIssueStatus(MaterialIssue materialIssue)
        {
            string sql = "   UPDATE TBLMaterialIssue SET issuestatus='materialissuestatus_close' ";
            sql += " WHERE BIGSSCODE='" + materialIssue.BigSSCode + "' AND plandate=" + materialIssue.PlanDate + "  AND MOCODE='" + materialIssue.MoCode + "'";
            sql += " AND MOSEQ=" + materialIssue.MoSeq + " AND ISSUESEQ=" + materialIssue.IssueSEQ + "";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        private string GetMaterialIssueWhereSql(string bigSSCode, int planDate, string moCode, decimal moSeq, string materialIssueType)
        {
            string sql = string.Empty;
            if (bigSSCode.Trim() != string.Empty)
            {
                sql += "  AND BIGSSCODE='" + bigSSCode.Trim().ToUpper() + "'";
            }

            if (planDate > 0)
            {
                sql += " AND planDate>=" + planDate + "";
            }

            if (moCode.Trim() != string.Empty)
            {
                sql += " AND MOCODE='" + moCode.Trim().ToUpper() + "'";
            }

            if (moSeq >= 0)
            {
                sql += " AND MOSEQ=" + moSeq + "";
            }

            if (materialIssueType.Trim() != string.Empty)
            {
                sql += " AND  ISSUETYPE='" + materialIssueType + "'";
            }
            return sql;
        }

        public object[] QueryMaterialIssueAndPlanSeq(string bigSSCode, int planDate, string moCode, string issueType, string issueStatus)
        {
            string sql = " SELECT a.BIGSSCODE,a.PLANDATE,a.MOCODE,a.MOSEQ,a.ISSUESEQ,a.ISSUEQTY,a.ISSUETYPE,a.ISSUESTATUS,a.MUSER || '-' || c.username AS MUSER,a.MDATE,a.MTIME, B.PLANSEQ  ";
            sql += " FROM TBLMATERIALISSUE A, TBLWORKPLAN B,tbluser c ";
            sql += " WHERE A.BIGSSCODE = B.BIGSSCODE   AND A.PLANDATE = B.PLANDATE  AND  A.MOCODE = B.MOCODE   AND A.MOSEQ = B.MOSEQ   AND a.muser=c.usercode";


            if (bigSSCode.Trim() != string.Empty)
            {
                if (bigSSCode.IndexOf(",") >= 0)
                {
                    string[] list = bigSSCode.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i].Trim().ToUpper() + "'";
                    }
                    bigSSCode = string.Join(",", list);

                    sql += "  AND a.bigsscode IN (" + bigSSCode + ")";
                }
                else
                {
                    sql += "  AND a.bigsscode like  '" + bigSSCode + "%'";
                }
            }

            if (planDate > 0)
            {
                sql += " AND a.plandate>=" + planDate + "";
            }

            if (moCode.Trim() != string.Empty)
            {
                sql += " AND a.mocode='" + moCode.Trim().ToUpper() + "'";
            }

            if (issueType.Trim() != string.Empty)
            {
                sql += " AND a.issuetype='" + issueType + "'";
            }

            if (issueStatus.Trim() != string.Empty)
            {
                sql += " AND a.issuestatus='" + issueStatus + "'";
            }

            sql += " ORDER BY a.issueseq ";
            return this.DataProvider.CustomQuery(typeof(MaterialIssueAndPlanSeq), new SQLCondition(sql));
        }

        #endregion

        #region MaterialReqInfo
        /// <summary>
        /// 
        /// </summary>
        public MaterialReqInfo CreateNewMaterialReqInfo()
        {
            return new MaterialReqInfo();
        }

        public void AddMaterialReqInfo(MaterialReqInfo materialReqInfo)
        {
            this._helper.AddDomainObject(materialReqInfo);
        }

        public void UpdateMaterialReqInfo(MaterialReqInfo materialReqInfo)
        {
            this._helper.UpdateDomainObject(materialReqInfo);
        }

        public void DeleteMaterialReqInfo(MaterialReqInfo materialReqInfo)
        {
            this._helper.DeleteDomainObject(materialReqInfo);
        }

        public void DeleteMaterialReqInfo(MaterialReqInfo[] materialReqInfo)
        {
            this._helper.DeleteDomainObject(materialReqInfo);
        }

        public object GetMaterialReqInfo(string bigSSCode, int planDate, string mCode, decimal moSeq, decimal requestSEQ)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialReqInfo), new object[] { bigSSCode, planDate, mCode, moSeq, requestSEQ });
        }

        public void UpdateMaterialReqInfo(string bigSSCode, string moCode)
        {
            string sql = "UPDATE TBLMaterialReqInfo SET status='" + MaterialReqStatus.MaterialReqStatus_Responsed + "' ";
            sql += " WHERE BIGSSCODE='" + bigSSCode.Trim().ToUpper() + "' AND MOCODE='" + moCode.Trim().ToUpper() + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object[] QueryMaterialReqInfo(string bisSSCode, int date, string status, int inclusive, int exclusive)
        {
            string sql = "SELECT TBLMATERIALREQINFO.BIGSSCODE,TBLMATERIALREQINFO.PLANDATE,TBLMATERIALREQINFO.MOCODE,TBLMATERIALREQINFO.MOSEQ,";
            sql += "        TBLMATERIALREQINFO.REQUESTSEQ,TBLMATERIALREQINFO.PLANSEQ,TBLMATERIALREQINFO.ITEMCODE,TBLMATERIALREQINFO.REQUESTQTY,";
            sql += "        TBLMATERIALREQINFO.MAYBEQTY,TBLMATERIALREQINFO.STATUS,TBLMATERIALREQINFO.REQTYPE,TBLMATERIALREQINFO.MUSER,";
            sql += "        TBLMATERIALREQINFO.MDATE,TBLMATERIALREQINFO.MTIME,tblmaterial.mmodelcode";
            sql += "     FROM TBLMATERIALREQINFO LEFT JOIN tblmaterial ON TBLMATERIALREQINFO.Itemcode=tblmaterial.mcode  ";
            sql += QueryMaterialReqInfoWhereCondition(bisSSCode, date, status);
            sql += " ORDER BY TBLMATERIALREQINFO.bigsscode, TBLMATERIALREQINFO.plandate, TBLMATERIALREQINFO.planseq";
            return this.DataProvider.CustomQuery(typeof(MaterialReqInfoWithMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryMaterialReqInfoCount(string bisSSCode, int date, string status)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblmaterialreqinfo ";
            sql += QueryMaterialReqInfoWhereCondition(bisSSCode, date, status);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public string QueryMaterialReqInfoWhereCondition(string bisSSCode, int date, string status)
        {
            string returnValue = string.Empty;

            returnValue += " WHERE 1 = 1 ";
            if (bisSSCode.Trim().Length > 0)
            {
                if (bisSSCode.IndexOf(",") < 0)
                {
                    returnValue += string.Format("AND tblmaterialreqinfo.bigsscode LIKE '%{0}%' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(bisSSCode)));
                }
                else
                {
                    returnValue += string.Format("AND tblmaterialreqinfo.bigsscode IN ({0}) ", FormatHelper.ProcessQueryValues(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(bisSSCode))));
                }
            }

            if (date > 0)
            {
                returnValue += string.Format("AND tblmaterialreqinfo.plandate = {0} ", date.ToString());
            }

            if (status.Trim().Length > 0)
            {
                returnValue += string.Format("AND tblmaterialreqinfo.status = '{0}' ", status);
            }

            return returnValue;
        }

        #endregion

        #region GetMOCode

        public bool GetMOCode(string moCode)
        {
            string sql = "SELECT Count(*) FROM tblmo WHERE 1=1 ";

            if (moCode.Trim() != string.Empty)
            {
                sql += " AND moCode='" + FormatHelper.CleanString(moCode).ToUpper() + "'";
            }

            int iCount = this.DataProvider.GetCount(new SQLCondition(sql));

            if (iCount == 0)
            {
                return false;
            }

            return true;

        }
        #endregion
        #region
        /// <summary>
        /// ** 修 改:		jack	2012-04-01
        ///					改传参SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object GetLastSimulationReport(string runningCard)
        {
            object[] simulationreports = this.DataProvider.CustomQuery(typeof(SimulationReport),
                //new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD order by MDATE desc,MTIME desc",
                new SQLParamCondition(string.Format(
                @"select {0} from tblsimulationreport where
					(rcard,mocode) in (
					select rcard, mocode
					from (select rcard, mocode
							from tblsimulationreport
							where RCARD = $RCARD
							order by MDATE desc, MTIME desc)
							where rownum = 1)",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport))),
                new SQLParameter[] { new SQLParameter("RCARD", typeof(string), runningCard.ToUpper()) }));

            if (simulationreports == null)
                return null;
            if (simulationreports.Length > 0)
                return simulationreports[0];
            else
                return null;

        }
        #endregion


         
    }
}

