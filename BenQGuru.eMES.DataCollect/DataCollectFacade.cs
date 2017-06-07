using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.DataCollect
{
    /// <summary>
    /// DataCollectFacade ��ժҪ˵����
    /// �ļ���:		DataCollectFacade.cs
    /// Copyright (c) 1999 -2003 ������¹��BenQGuru��������˾�з���
    /// ������:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// ��������:	2005-03-31 14:21:26
    /// �޸���:Mark Lee
    /// �޸�����:20050331
    /// �� ��:	
    /// �� ��:	
    /// </summary>
    public class DataCollectFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        //Laws Lu,2006/06/27 add support variant factory
        public const string PID = "PID";
        public const string HID = "HID";
        public const string POWER = "POWER";

        public DataCollectFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                //				if (_domainDataProvider == null)
                //				{
                //					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                //				}

                return _domainDataProvider;
            }
        }



        #region ���
        /// <summary>
        /// ֻ֧�����ϵĲɼ���TS��REWORK������վӦ���Լ���顢��д
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="actionType"></param>
        /// <param name="resourceCode"></param>
        /// <param name="userCode"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public Messages CheckID(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            return CheckID(iD, actionType, resourceCode, userCode, product, null);
        }
        // Added by Icyer 2005/10/28
        // ��չһ����ActionCheckStatus�����ķ���
        public Messages CheckID(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, ActionCheckStatus actionCheckStatus)
        {
            // Added by Icyer 2005/11/11
            // ���actionCheckStatus�е�MOCode�Ƿ���product�е�һ��
            if (actionCheckStatus != null)
            {
                if (actionCheckStatus.ProductInfo != null)
                {
                    //Laws Lu��2005/11/11������	�ж�LastSimulationΪNull�����
                    if (actionCheckStatus.ProductInfo.LastSimulation != null)
                    {
                        if (actionCheckStatus.ProductInfo.LastSimulation.MOCode != product.LastSimulation.MOCode)
                        {
                            actionCheckStatus = new ActionCheckStatus();
                        }
                    }
                }
            }
            // Added end

            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                //����ȫ�������������ǡ����ޡ�״̬��û�о�������ȷ�ϣ�����ʱ��������
                //���˺�Ĳ�������ά��Ҳ������ά�ޣ�ϵͳʵ�ַ�ʽ�������������ǡ�

                BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
                //Laws Lu,��Ҫ�޸�	�����߹�TS����,û�п��Ƕ��TS�����,MOCode��SequenceҲӦ����Ϊ�ؼ�����,
                //Laws Lu,2005/10/26,�޸�	��������

                BenQGuru.eMES.Domain.TS.TS ts = null;
                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_NG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_SMTNG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_OQCNG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineNG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_BurnOutNG)   //Add by sandy on 20140530
                {

                    ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(product.LastSimulation.RunningCard);
                    product.LastTS = ts;
                }
                //				if(product.LastSimulation.ISTS == 1)
                //				{
                //					ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(product.LastSimulation.RunningCard);
                //					product.LastTS = ts;
                //				}
                //2005/08/29,�޸�	������FQC����ʱ������û��ȷ�ϵ����
                if (actionType == ActionType.DataCollectAction_OQCReject
                    && ts != null
                    && ts.FromInputType == TS.TSFacade.TSSource_OnWIP
                    && ts.TSStatus == BenQGuru.eMES.Web.Helper.TSStatus.TSStatus_New
                    && ts.RunningCardSequence == product.LastSimulation.RunningCardSequence)
                {
                    //ʲô������return messages;
                }
                else
                {
                    if (ts != null
                        && ts.FromInputType == TS.TSFacade.TSSource_OnWIP
                        && ts.TSStatus == BenQGuru.eMES.Web.Helper.TSStatus.TSStatus_New
                        && actionType != ActionType.DataCollectAction_OffMo)/*2005/12/21,Laws Lu,�޸�	�������빤��*/
                    {
                        //Laws Lu,2005/09/09,����	���NG�ظ������

                        switch (actionType)
                        {
                            case ActionType.DataCollectAction_OQCNG:
                            case ActionType.DataCollectAction_SMTNG:
                            case ActionType.DataCollectAction_NG:
                            case ActionType.DataCollectAction_OutLineNG:
                            case ActionType.DataCollectAction_OutLineReject:
                            case ActionType.DataCollectAction_BurnOutNG:   //Add by sandy on 20140530
                                {
                                    if (product.LastSimulation.LastAction == actionType)
                                    {
                                        throw new Exception("$CS_NG_PLEASE_SEND_TS");
                                    }
                                    break;
                                }
                            //Karron Qiu,2005-10-25
                            //����û��ɼ���Ʒ��Ϣʱ�����ɼ��Ĳ�Ʒ�Ѿ��ǲ���Ʒ��
                            //�뽫��ʾ��Ϣ�������벻��Ʒ�ɼ�ʱһ�£����ò�Ʒ�ǲ���Ʒ������ά�޴�����
                            case ActionType.DataCollectAction_GOOD:
                            case ActionType.DataCollectAction_Carton:   // Added By Hi1/venus.Feng on 20080814 for Hisense Version
                            case ActionType.DataCollectAction_BurnIn:   //Add by sandy on 20140530
                            case ActionType.DataCollectAction_BurnOutGood:   //Add by sandy on 20140530
                                {
                                    if (product.LastSimulation.LastAction == ActionType.DataCollectAction_NG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OQCNG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_SMTNG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineNG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineReject ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_BurnOutNG)   //Modify by sandy on 20140530
                                    {
                                        throw new Exception("$CS_NG_PLEASE_SEND_TS");
                                    }
                                    break;
                                }
                        }
                    }

                    //End Laws Lu

                    // Changed by Icyer 2005/10/28
                    //DoCheck(iD,actionType,resourceCode,userCode, product);
                    DoCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                    // Changed end

                    //						this.CheckMO(product.LastSimulation.MOCode);
                    //						this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product); 
                    //						this.CheckActionOnlineAndOutline(actionType, product); 	
                    //						this.CheckCardStatus(product); 
                    //						this.CheckRepeatCollect(actionType, product); 
                }

                //				#region ��д��SIMULATION
                //				if (messages.IsSuccess())
                //				{
                //					//����
                //					if (!(product.LastSimulation.OPCode == product.NowSimulation.OPCode))
                //					{
                //						product.NowSimulation.ActionList= ";"+actionType+";";
                //					}
                //				}
                //				#endregion
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }
        // Added end


        //Laws Lu,2005/09/09,����	���
        private void DoCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            if (product.CurrentMO == null)
            {
                this.CheckMO(product.LastSimulation.MOCode, product);
            }
            else
            {
                this.CheckMO(product.CurrentMO);
            }

            this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product);
            this.CheckActionOnlineAndOutline(actionType, product);
            this.CheckCardStatus(product);
            this.CheckRepeatCollect(actionType, product);

            //Laws Lu,2006/05/30	if first op,break off carton information
            if (GetMORouteFirstOP(product.NowSimulation.MOCode, product.NowSimulation.RouteCode).OPCode
                == product.CurrentItemRoute2OP.OPCode)
            {
                if (product.NowSimulation.CartonCode != String.Empty)
                {
                    Package.PackageFacade pf = new Package.PackageFacade(DataProvider);
                    pf.SubtractCollected(((ExtendSimulation)product.LastSimulation).CartonCode);
                }

                ((ExtendSimulation)product.LastSimulation).CartonCode = String.Empty;

                product.NowSimulation.CartonCode = String.Empty;


            }
        }
        // Added by Icyer 2005/10/28
        private void DoCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, ActionCheckStatus actionCheckStatus)
        {
            if (actionCheckStatus == null || actionCheckStatus.CheckedID == false)
            {
                if (actionCheckStatus == null || actionCheckStatus.CheckedMO == false)
                {
                    this.CheckMO(product.LastSimulation.MOCode, product);
                    if (actionCheckStatus != null)
                    {
                        actionCheckStatus.CheckedMO = true;
                    }
                }
                this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                this.CheckActionOnlineAndOutline(actionType, product);

                // Move the follow code after if
                //this.CheckRepeatCollect(actionType, product); 

                if (actionCheckStatus != null)
                {
                    actionCheckStatus.CheckedID = true;
                }
            }
            else
            {
                this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
            }
            // �Ƿ��ظ��ɼ�
            this.CheckCardStatus(product);
            this.CheckRepeatCollect(actionType, product);

        }
        // Added end

        public Messages CheckIDOutline(string iD, string actionType, string resourceCode, string opCode, string userCode, ProductInfo product)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                /* check the status of mo whether is Release or open */
                this.CheckMO(product.LastSimulation.MOCode, product);
                this.GetRouteOPOutline(iD, actionType, resourceCode, opCode, userCode, product);
                this.CheckActionOnlineAndOutline(actionType, product);
                this.CheckCardStatus(product);
                this.CheckRepeatCollect(actionType, product);

                //				#region ��д��SIMULATION
                //				if (messages.IsSuccess())
                //				{
                //					//����
                //					if (!(product.LastSimulation.OPCode == product.NowSimulation.OPCode))
                //					{
                //						product.NowSimulation.ActionList= ";"+actionType+";";
                //					}
                //				}
                //				#endregion
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        //����ǰһ��Simulation ���, Ԥ���� �µ� Simulation ��¼

        public Messages WriteSimulation(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                product.NowSimulation.MOCode = product.LastSimulation.MOCode;
                product.NowSimulation.ItemCode = product.LastSimulation.ItemCode;
                product.NowSimulation.ModelCode = product.LastSimulation.ModelCode;
                product.NowSimulation.IDMergeRule = product.LastSimulation.IDMergeRule;

                product.NowSimulation.RunningCard = iD;
                product.NowSimulation.RunningCardSequence = product.LastSimulation.RunningCardSequence + 1;
                product.NowSimulation.TranslateCard = product.LastSimulation.TranslateCard;
                product.NowSimulation.TranslateCardSequence = product.LastSimulation.TranslateCardSequence;
                product.NowSimulation.SourceCard = product.LastSimulation.SourceCard;
                product.NowSimulation.SourceCardSequence = product.LastSimulation.SourceCardSequence;

                product.NowSimulation.FromRoute = ActionOnLineHelper.StringNull;
                product.NowSimulation.FromOP = ActionOnLineHelper.StringNull;

                product.NowSimulation.RouteCode = product.LastSimulation.RouteCode;
                product.NowSimulation.OPCode = product.LastSimulation.OPCode;
                product.NowSimulation.ResourceCode = resourceCode;

                product.NowSimulation.ProductStatus = ProductStatus.GOOD;
                product.NowSimulation.LastAction = actionType;
                product.NowSimulation.ActionList = string.Format("{0}{1};", product.LastSimulation.ActionList, actionType);

                //����Ƿ��깤  TODO
                product.NowSimulation.IsComplete = ProductComplete.NoComplete;

                product.NowSimulation.CartonCode = product.LastSimulation.CartonCode;
                product.NowSimulation.LOTNO = product.LastSimulation.LOTNO;
                product.NowSimulation.PalletCode = product.LastSimulation.PalletCode;
                product.NowSimulation.NGTimes = product.LastSimulation.NGTimes;
                //Laws Lu,2006/07/05 support RMA
                product.NowSimulation.RMABillCode = product.LastSimulation.RMABillCode;
                product.NowSimulation.MOSeq = product.LastSimulation.MOSeq;     // Added by icyer 2007/07/03

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        public Simulation CloneSimulation(Simulation simulation)
        {
            Simulation returnValue = CreateNewSimulation();

            Type simulationType = typeof(Simulation);
            FieldInfo[] fieldInfoList = simulationType.GetFields();
            if (fieldInfoList != null)
            {
                foreach (FieldInfo fieldInfo in fieldInfoList)
                {
                    fieldInfo.SetValue(returnValue, fieldInfo.GetValue(simulation));
                }
            }

            return returnValue;
        }

        /// <summary>
        /// ���ID��һ���������Ƿ��Ѿ���ʹ��
        /// �� ��:		Jane Shu	2005-07-21
        ///					�Ĵ���SQL
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="mOCode"></param>
        /// <returns></returns>
        public OnWIP CheckIDIsUsed(string id, string moCode)
        {
            object[] onWips = this.DataProvider.CustomQuery(typeof(OnWIP),
                new SQLParamCondition(
                string.Format("select {0} from TBLONWIP where RCARD = $RCARD and MOCODE = $MOCODE order by rcardseq desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIP))),
                new SQLParameter[]{ new SQLParameter( "RCARD", typeof(string), id ) ,
									  new SQLParameter( "MOCODE", typeof(string), moCode) }));

            if (onWips == null)
                return null;
            if (onWips.Length > 0)
                return (OnWIP)onWips[0];
            return null;
        }

        /// <summary>
        /// ���ID��һ���������Ƿ��Ѿ���ʹ��
        /// �� ��:		Jane Shu	2005-07-21
        ///					�Ĵ���SQL
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="mOCode"></param>
        /// <returns></returns>
        public OnWIPCardTransfer CheckIDIsSNTransfered(string id, string moCode)
        {
            object[] onWips = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer),
                new SQLParamCondition(
                string.Format("select {0} from tblonwipcardtrans where SCARD = $SCARD and MOCODE=$MOCODE",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer))),
                new SQLParameter[]{ new SQLParameter( "SCARD", typeof(string), id )
									  , new SQLParameter( "MOCODE", typeof(string), moCode )}));

            if (onWips == null)
            {
                return null;
            }
            if (onWips.Length > 0)
            {
                return (OnWIPCardTransfer)onWips[0];
            }

            return null;
        }

        #region ���
        //ID״̬���
        public bool CheckCardStatus(ProductInfo productInfo)
        {
            if (productInfo.LastSimulation.ProductStatus != ProductStatus.GOOD)
            {
                if (((ExtendSimulation)productInfo.LastSimulation).AdjustProductStatus != ProductStatus.GOOD)
                /*&& productInfo.NowSimulation.LastAction != ActionType.DataCollectAction_OffMo)/*2005/12/21,Laws Lu,���� �������빤��*/
                {
                    throw new Exception("$CS_ProductStatusError $CS_Param_ProductStatus=" + MutiLanguages.ParserString(productInfo.LastSimulation.ProductStatus)
                        + " $CS_Param_ID=" + productInfo.LastSimulation.RunningCard);
                }
                //				else
                //				{
                //					if( productInfo.NowSimulation.LastAction == ActionType.DataCollectAction_OQCReject)
                //					{
                //					}
                //				}
            }


            return true;
        }

        //		//����״̬���
        //		public bool CheckMO(string moCode)
        //		{
        //			MOFacade moFacade=new MOFacade(this.DataProvider);	
        //			MO mo=(MO)moFacade.GetMO(moCode);
        //			//����״̬���
        //			bool moStatus = this.CheckMO(mo);
        //			if (!moStatus)
        //			{
        //				throw new Exception("$CS_MOStatus_Error $CS_Param_MOStatus=$"+mo.MOStatus);
        //			}		
        //			
        //			return moStatus;
        //		}

        public bool CheckMO(string moCode, ProductInfo product)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);

            MO mo = null;
            if (product.CurrentMO == null)
            {
                mo = (MO)moFacade.GetMO(moCode);
            }
            else
            {
                mo = product.CurrentMO;
            }
            //����״̬���
            bool moStatus = this.CheckMO(mo);
            if (!moStatus)
            {
                throw new Exception("$CS_MOStatus_Error $CS_Param_MOStatus=$" + mo.MOStatus);
            }

            product.CurrentMO = mo;

            return moStatus;
        }

        //����״̬���
        public bool CheckMO(MO mo)
        {
            if ((mo.MOStatus == MOManufactureStatus.MOSTATUS_RELEASE) ||
                (mo.MOStatus == MOManufactureStatus.MOSTATUS_OPEN))
                return true;
            else
                return false;
        }

        private const char isSelected = '1';

        //���ݹ����趨��鶯���ɷ��ڸù�����
        public bool CheckAction(ProductInfo productInfo, object op, string actionType)
        {
            string opCode = string.Empty;
            string opControl = "0000000000000000000000";

            if (op is Operation)
            {
                opCode = ((Operation)op).OPCode;
                opControl = ((Operation)op).OPControl;
            }

            if (op is ItemRoute2OP)
            {
                opCode = ((ItemRoute2OP)op).OPCode;
                opControl = ((ItemRoute2OP)op).OPControl;
            }

            return this.CheckActionOnlineAndOutline(actionType, opCode, opControl);
        }

        public bool CheckActionOnlineAndOutline(string actionType, ProductInfo productInfo)
        {
            string opCode = productInfo.NowSimulation.OPCode;
            string opControl = "0000000000000000000000";

            if ((actionType == ActionType.DataCollectAction_OutLineGood)
                || (actionType == ActionType.DataCollectAction_OutLineNG)
                || (actionType == ActionType.DataCollectAction_OutLineReject))
            {
                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                Operation op = (Operation)dataModel.GetOperation(opCode);
                if (op != null)
                {
                    opControl = op.OPControl;
                }
            }
            else
            {
                //Laws Lu,2005/12/27���޸�	�������湤��;����Ϣ
                ItemRoute2OP itemRoute2OP = (ItemRoute2OP)this.GetMORouteOP(productInfo.NowSimulation.ItemCode, productInfo.NowSimulation.MOCode, productInfo.NowSimulation.RouteCode, productInfo.NowSimulation.OPCode, productInfo);

                if (itemRoute2OP != null)
                {
                    opControl = itemRoute2OP.OPControl;
                }
            }
            return this.CheckActionOnlineAndOutline(actionType, opCode, opControl);
        }
        public bool CheckActionOnlineAndOutline(string actionType, string opCode, string opControl)
        {
            switch (actionType)
            {
                case ActionType.DataCollectAction_GOOD:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTGOOD:
                case ActionType.DataCollectAction_SMTNG:
                    if (opControl[(int)OperationList.Testing] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_TestOP $CS_Param_OPCode =" + opCode);
                    }
                    break;

                case ActionType.DataCollectAction_GoMO:
                /*2005/12/15��Laws Lu���������빤���ɼ�*/
                case ActionType.DataCollectAction_OffMo:
                    break;
                // Added by hi1/Venus.Feng on 20080716 for Hisense Version
                case ActionType.DataCollectAction_Carton:
                    if (opControl[(int)OperationList.Packing] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_PackOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                case ActionType.DataCollectAction_DropMaterial:
                    break;
                case ActionType.DataCollectAction_ECN:
                    break;
                case ActionType.DataCollectAction_TRY:
                    break;
                case ActionType.DataCollectAction_SoftINFO:
                    break;
                case ActionType.DataCollectAction_Reject:
                    break;

                case ActionType.DataCollectAction_CollectINNO:
                case ActionType.DataCollectAction_CollectKeyParts:
                    if (opControl[(int)OperationList.ComponentLoading] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_INNOOP $CS_Param_OPCode =" + opCode);
                    }
                    break;

                case ActionType.DataCollectAction_Split:
                case ActionType.DataCollectAction_Convert:
                case ActionType.DataCollectAction_IDTran:
                    if (opControl[(int)OperationList.IDTranslation] != isSelected)
                        throw new Exception("$CS_OP_Not_SplitOP $CS_Param_OPCode =" + opCode);
                    break;

                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    if (opControl[(int)OperationList.OutsideRoute] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_OutLineOP $CS_Param_OPCode = " + opCode);
                    }
                    break;

                case ActionType.DataCollectAction_LOT:
                case ActionType.DataCollectAction_OQCLotAddID:
                case ActionType.DataCollectAction_OQCLotRemoveID:
                    if (opControl[(int)OperationList.Packing] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_PackOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                case ActionType.DataCollectAction_OQCPass:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_OQCGood:
                case ActionType.DataCollectAction_OQCNG:
                    // Marked By Hi1/venus.feng on 20080721 for Hisense Version : Don't check op for OQCNG��OQCPass��OQCReject
                    /*
                    if (opControl[(int) OperationList.OQC]!=isSelected)
					{
						throw new Exception("$CS_OP_Not_OQCOP $CS_Param_OPCode =" + opCode);
					}
                    */
                    break;

                /* added by jessie lee, 2006-5-30, ����burn in action */
                case ActionType.DataCollectAction_BurnIn:
                    if (opControl[(int)OperationList.BurnIn] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_BurnInOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                /* added by jessie lee, 2006-5-30, ����burn out action */
                case ActionType.DataCollectAction_BurnOutGood:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530
                    if (opControl[(int)OperationList.BurnOut] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_BurnOutOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                default:
                    throw new Exception("$CS_SystemError_CheckID_Not_SupportAction:" + actionType);
            }
            return true;
        }


        public void CheckRepeatCollect(string actionType, ProductInfo product)
        {
            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                            //Laws Lu,2006/06/08	���⹤���������βɼ�NG
                            if (product.LastTS != null && product.LastTS.RunningCardSequence != product.LastSimulation.RunningCardSequence)
                            {
                                this.CheckRepeatCollect(product.LastSimulation.RunningCard, product.LastSimulation.ActionList, actionType);
                            }
                            product.NowSimulation.ActionList = string.Format("{0}{1};", product.LastSimulation.ActionList, actionType);
                            break;
                        default:
                            product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                            break;
                    }
                    break;

                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530����һվΪNGʱ��Ҫ��վ��action�滻ActionList
                    product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                    break;

                default:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                            product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                            break;

                        default:
                            if (product.LastSimulation.OPCode == product.NowSimulation.OPCode)
                            {
                                this.CheckRepeatCollect(product.LastSimulation.RunningCard, product.LastSimulation.ActionList, actionType);
                                product.NowSimulation.ActionList = string.Format("{0}{1};", product.LastSimulation.ActionList, actionType);
                            }
                            else
                            {
                                product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                            }
                            break;
                    }
                    break;
            }
        }
        public void CheckRepeatCollect(string rcard, string actionList, string action)
        {
            actionList = ";" + actionList + ";";

            switch (action)
            {
                case ActionType.DataCollectAction_GoMO:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_GoMO + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                /*�������빤��*/
                case ActionType.DataCollectAction_OffMo:
                    {
                        if (actionList.IndexOf(";" + ActionType.DataCollectAction_OffMo + ";") >= 0)
                        {
                            throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                        }
                        break;
                    }
                case ActionType.DataCollectAction_DropMaterial:
                    {
                        if (actionList.IndexOf(";" + ActionType.DataCollectAction_DropMaterial + ";") >= 0)
                        {
                            throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                        }
                        break;
                    }
                case ActionType.DataCollectAction_SMTGOOD:
                case ActionType.DataCollectAction_GOOD:
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OQCGood:
                    string[] actions = actionList.Split(new char[] { ';' });

                    //if (actions.Length > 2) //��Ϊ������actionList��ǰ�󶼼��˷ֺ�
                    if (actions.Length > 3)
                    {
                        //string lastAction = actions[actions.Length - 2]; //��Ϊ������actionList��ǰ�󶼼��˷ֺ�
                        string lastAction = actions[actions.Length - 3];
                        if (lastAction == ActionType.DataCollectAction_GOOD
                            || lastAction == ActionType.DataCollectAction_GOOD
                            || lastAction == ActionType.DataCollectAction_SMTGOOD
                            || lastAction == ActionType.DataCollectAction_OutLineGood
                            || lastAction == ActionType.DataCollectAction_OQCGood)
                        {
                            throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                        }
                    }
                    //					if (actionList.IndexOf(";"+ActionType.DataCollectAction_GOOD +";")>=0)
                    //					{
                    //						throw new Exception("$CS_RepeatCollect_OnOneOP");
                    //					}
                    //					
                    //					if (actionList.IndexOf(";"+ActionType.DataCollectAction_SMTGOOD +";")>=0)
                    //					{
                    //						throw new Exception("$CS_RepeatCollect_OnOneOP");
                    //					}
                    //
                    //					if (actionList.IndexOf(";"+ActionType.DataCollectAction_OutLineGood +";")>=0)
                    //					{
                    //						throw new Exception("$CS_RepeatCollect_OnOneOP");
                    //					}
                    //
                    //					if (actionList.IndexOf(";"+ActionType.DataCollectAction_OQCGood +";")>=0)
                    //					{
                    //						throw new Exception("$CS_RepeatCollect_OnOneOP");
                    //					}
                    break;

                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_OutLineReject:
                case ActionType.DataCollectAction_BurnOutNG:   //Add by sandy on 20140530
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_NG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_SMTNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OutLineNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCReject + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }

                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    //Add by sandy on 20140530
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_BurnOutNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_CollectINNO:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_CollectINNO + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_CollectKeyParts:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_CollectKeyParts + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_ECN:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_ECN + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_OQCLotAddID:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCLotAddID + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_OQCLotRemoveID:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCLotRemoveID + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;


                case ActionType.DataCollectAction_TRY:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_TRY + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_SoftINFO:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_SoftINFO + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_Split:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Split + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_Convert:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Convert + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_IDTran:
                    //					if (actionList.IndexOf(";"+ActionType.DataCollectAction_IDTran +";")>=0)
                    //					{
                    //						throw new Exception("$CS_RepeatCollect_OnOneOP");
                    //					}
                    break;

                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Reject + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }

                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCReject + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }

                    break;
                case ActionType.DataCollectAction_OQCPass:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCPass + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_BurnIn:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_BurnIn + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_BurnOutGood:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_BurnOutGood + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_Carton:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Carton + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                default:
                    throw new Exception("$CS_Sys_Error_CheckRepeatCollect_Failed $CS_Param_Action=" + action + "  $CS_Param_ID :" + rcard);
            }
        }


        //Online ���;��OP�Ƿ����
        public void GetRouteOPOnline(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            GetRouteOPOnline(iD, actionType, resourceCode, userCode, product, null);
        }
        public void GetRouteOPOnline(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            //			BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);  
            //			//Laws Lu,��Ҫ�޸�	�����߹�TS����,û�п��Ƕ��TS�����,MOCode��SequenceҲӦ����Ϊ�ؼ�����,
            //			//Laws Lu,2005/10/26,�޸�	��������
            //			BenQGuru.eMES.Domain.TS.TS ts =  null;
            //			if(product.LastSimulation.ISTS == 1)
            //			{
            //				ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(product.LastSimulation.RunningCard);
            //			}

            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOutline");
                        default:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            //this.CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product); 
                            this.CheckOnlineOP(iD, actionType, resourceCode, userCode, product);
                            break;
                    }
                    break;
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_BurnOutNG:   //Add by sandy on 20140530
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOutline");
                        case ActionType.DataCollectAction_OQCReject:
                            {

                                //								if(ts != null 
                                //									&& (ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap
                                //									|| ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split))
                                //								{
                                //									return;
                                //								}

                                this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                                this.CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product);

                                break;

                            }
                        default:
                            //								if(ts != null 
                            //									&& (ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap
                            //									|| ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split))
                            //								{
                            //									return;
                            //								}

                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                            break;
                    }
                    break;

                default:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOutline");

                        default:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOnlineOP(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                            break;
                    }
                    break;
            }
        }

        public void AdjustRouteOPOnline(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            //BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider); 
            //Laws Lu,��Ҫ�޸�	�����߹�TS����,û�п��Ƕ��TS�����,MOCode��SequenceҲӦ����Ϊ�ؼ�����,
            //Laws Lu,2005/10/26,�޸�	��������
            BenQGuru.eMES.Domain.TS.TS ts = null;
            if (product.LastTS != null)
            {
                ts = product.LastTS;
            }
            else
            {
                object obj = (new TS.TSFacade(DataProvider)).GetCardLastTSRecord(iD, product.LastSimulation.MOCode);
                if (obj != null)
                {
                    ts = (BenQGuru.eMES.Domain.TS.TS)obj;
                }
            }


            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                    //					//�������Action�Ͳ���Action�Ƿ���ͬһ������
                    //					string route = product.LastSimulation.RouteCode;
                    //					string op =  product.LastSimulation.OPCode;
                    //					//��ʱ��ֵ
                    //					product.LastSimulation.RouteCode = product.LastSimulation.FromRoute;
                    //					product.LastSimulation.OPCode = product.LastSimulation.FromOP;
                    //
                    //					CheckMaterialAndTest(iD,actionType,resourceCode,userCode, product);
                    //			
                    //					//�ָ�����ֵ
                    //					product.LastSimulation.RouteCode = route;
                    //					product.LastSimulation.OPCode = op;

                    ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.FromRoute;
                    ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.FromOP;
                    break;

                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530


                    //					//�������Action�Ͳ���Action�Ƿ���ͬһ������
                    //					CheckMaterialAndTest(iD,actionType,resourceCode,userCode, product);



                    //Laws Lu,2005/08/22,ȡNG TS �Ļ�����Ϣ, ��д 	NextRouteCode, NextOPCode
                    //Laws Lu,2005/08/25,���ǻ������������
                    //Laws Lu,2005/08/29	����δȷ��
                    #region	ά������������⡢����״̬�Ĵ���

                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_New)
                    {
                        if (actionType == ActionType.DataCollectAction_OffMo)
                        {
                            //						((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.FromRouteCode;
                            ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.FromOPCode;
                            if (product.LastSimulation.ProductStatus != ProductStatus.OffMo)
                            {
                                ((ExtendSimulation)product.LastSimulation).ProductStatus = ProductStatus.GOOD;
                            }

                            return;
                        }

                        else if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            /*ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&*/
                            ts.RunningCard == product.LastSimulation.RunningCard)
                        {
                            //Laws Lu,2006/06/12
                            //							if(actionType == ActionType.DataCollectAction_OutLineGood ||
                            //								actionType == ActionType.DataCollectAction_OutLineNG)
                            //							{
                            //								((ExtendSimulation)product.LastSimulation).NextRouteCode  = ts.FromRouteCode;
                            //								((ExtendSimulation)product.LastSimulation).NextOPCode		 = ts.FromOPCode;	
                            //							}
                            return;
                        }
                    }

                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            /*ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&*/
                            ts.RunningCard == product.LastSimulation.RunningCard)
                        {
                            //Laws Lu,2005/09/27,ע��	���ϺͲ�ⲻ�ܹ��ٴν������
                            //							((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            //							((ExtendSimulation)product.LastSimulation).NextRouteCode  = ts.FromRouteCode;
                            //							((ExtendSimulation)product.LastSimulation).NextOPCode		 = ts.FromOPCode;
                            //
                            //							return;
                            throw new Exception("$CS_Error_Product_Already_Scrap");
                        }


                    }
                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&
                            ts.RunningCard == product.LastSimulation.RunningCard)
                        {
                            //Laws Lu,2005/09/27,ע��	���ϺͲ�ⲻ�ܹ��ٴν������
                            //							((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            //							((ExtendSimulation)product.LastSimulation).NextRouteCode  = ts.FromRouteCode;
                            //							((ExtendSimulation)product.LastSimulation).NextOPCode		 = ts.FromOPCode;
                            //
                            //							return;
                            throw new Exception("$CS_Error_Product_Already_Split");
                        }


                    }
                    #endregion
                    //End Laws Lu

                    /*2005/12/21��Laws Lu������	�������빤��*/
                    if (actionType == ActionType.DataCollectAction_OffMo)
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.FromRouteCode;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.FromOPCode;

                    }
                    else if (ts != null && ts.TSStatus == TSStatus.TSStatus_RepeatNG)	// Added by Icyer 2007/03/15	���Ӳ���Ʒ�ظ���������
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.RouteCode;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.OPCode;
                    }
                    else if (ts == null || ts.TSStatus != TSStatus.TSStatus_Reflow)
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.NG;
                        //add by hiro 08/09/26
                        if (ts == null || ts.ConfirmResourceCode == string.Empty)
                            throw new Exception("$CS_REPAIR_NOT_READY:" + product.LastSimulation.RunningCard);
                        else
                            throw new Exception("$CS_REPAIR_NOT_READY:" + product.LastSimulation.RunningCard + "\n" + "TS" + "$ALERT_Resource:" + ts.ConfirmResourceCode);
                        //end by hiro 
                    }
                    else
                    {
                        if (ts.RunningCardSequence == product.LastSimulation.RunningCardSequence)
                        {
                            ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.ReflowRouteCode;
                            ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.ReflowOPCode;
                            if (ts.ReflowOPCode.Length == 0)
                            {
                                ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.LastSimulation.MOCode, ts.ReflowRouteCode);
                                if (itemOP != null)
                                {
                                    if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                                        || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                                    {
                                        ((ExtendSimulation)product.LastSimulation).NextOPCode = itemOP.OPCode;
                                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                                    }
                                }
                                else
                                {
                                    throw new Exception("$CS_Route_Lost_First_OP_Of_Route" + " : " + ts.ReflowRouteCode);
                                }
                            }
                        }
                        else
                        {
                            if (((ExtendSimulation)product.LastSimulation).NextRouteCode == null
                                || ((ExtendSimulation)product.LastSimulation).NextRouteCode.Trim().Length <= 0)
                            {
                                ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.RouteCode;
                            }
                            if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                                || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                            {
                                ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.OPCode;
                            }
                        }
                    }
                    break;
                //TODO: TS Pan duan
                //					((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                //					((ExtendSimulation)product.LastSimulation).NextRouteCode  = product.LastSimulation.RouteCode;
                //					((ExtendSimulation)product.LastSimulation).NextOPCode		 = product.LastSimulation.OPCode;	
                //					break;

                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_OutLineReject:
                    BenQGuru.eMES.Rework.ReworkFacade reworkFacade = new BenQGuru.eMES.Rework.ReworkFacade(this.DataProvider);
                    BenQGuru.eMES.Domain.Rework.ReworkSheet rSheet =
                        (BenQGuru.eMES.Domain.Rework.ReworkSheet)reworkFacade.GetReworkSheetByReject(
                        product.LastSimulation.RunningCard,
                        product.LastSimulation.RunningCardSequence,
                        product.LastSimulation.MOCode);
                    if (rSheet == null)
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.NG;
                        throw new Exception("$CS_Card_Has_Reject" + " : " + product.LastSimulation.RunningCard);
                    }
                    else
                    {
                        #region code not no use

                        //						//��ʱroute op
                        //						string tmpRoute = product.LastSimulation.RouteCode;
                        //						string tmpOP =  product.LastSimulation.OPCode;

                        //						//��ʱ��ֵ
                        //						product.LastSimulation.RouteCode = rSheet.NewRouteCode;
                        //						if(rSheet.NewOPCode.Length == 0)
                        //						{
                        //							ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.LastSimulation.MOCode, rSheet.NewRouteCode);
                        //							if(itemOP != null)
                        //							{
                        //								product.LastSimulation.OPCode	 = itemOP.OPCode;
                        //							}
                        //							else
                        //							{
                        //								throw new Exception("$CS_Route_Lost_First_OP_Of_Route"  + " : " + rSheet.NewRouteCode);
                        //							}
                        //						}
                        //						else
                        //						{
                        //							product.LastSimulation.OPCode = rSheet.NewOPCode;
                        //						}
                        //
                        //						//�������Action�Ͳ���Action�Ƿ���ͬһ������
                        ////						CheckMaterialAndTest(iD,actionType,resourceCode,userCode, product);
                        //
                        //						//�ָ�����ֵ
                        //						product.LastSimulation.RouteCode = tmpRoute;
                        //						product.LastSimulation.OPCode = tmpOP;
                        #endregion

                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = rSheet.NewRouteCode;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = rSheet.NewOPCode;
                        //Laws Lu,2006/06/01	��ϰ�װ��ϵ
                        if (product.NowSimulation.CartonCode != String.Empty)
                        {
                            Package.PackageFacade pf = new Package.PackageFacade(DataProvider);
                            pf.SubtractCollected(((ExtendSimulation)product.LastSimulation).CartonCode);
                        }

                        ((ExtendSimulation)product.LastSimulation).CartonCode = String.Empty;
                        if (rSheet.NewOPCode.Length == 0)
                        {
                            ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.LastSimulation.MOCode, rSheet.NewRouteCode);
                            if (itemOP != null)
                            {
                                ((ExtendSimulation)product.LastSimulation).NextOPCode = itemOP.OPCode;
                                ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            }
                            else
                            {
                                throw new Exception("$CS_Route_Lost_First_OP_Of_Route" + " : " + rSheet.NewRouteCode);
                            }
                        }
                    }
                    //TODO: ȡRework �Ļ�����Ϣ, ��д 	NextRouteCode, NextOPCode
                    //((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.NG; 
                    break;
                //throw new Exception("$CS_Will_Support_Reject_Back_Later");		
                default:
                    //					//�������Action�Ͳ���Action�Ƿ���ͬһ������
                    //					CheckMaterialAndTest(iD,actionType,resourceCode,userCode, product);

                    #region �������ϡ��������
                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            /*ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&*/
                            ts.RunningCard == product.LastSimulation.RunningCard)
                        {
                            //Laws Lu,2005/09/27,ע��	���ϺͲ�ⲻ�ܹ��ٴν������
                            //							((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            //							((ExtendSimulation)product.LastSimulation).NextRouteCode  = ts.FromRouteCode;
                            //							((ExtendSimulation)product.LastSimulation).NextOPCode		 = ts.FromOPCode;
                            //
                            //							
                            //							return;
                            throw new Exception("$CS_Error_Product_Already_Scrap");
                        }

                    }
                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            /*ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&*/
                            ts.RunningCard == product.LastSimulation.RunningCard)
                        {
                            //Laws Lu,2005/09/27,ע��	���ϺͲ�ⲻ�ܹ��ٴν������
                            //							((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            //							((ExtendSimulation)product.LastSimulation).NextRouteCode  = ts.FromRouteCode;
                            //							((ExtendSimulation)product.LastSimulation).NextOPCode		 = ts.FromOPCode;
                            //
                            //							return;
                            throw new Exception("$CS_Error_Product_Already_Split");
                        }

                    }
                    #endregion

                    #region �������������
                    if (ts != null && ts.TSStatus == TSStatus.TSStatus_Reflow)
                    {
                        if (ts.RunningCardSequence == product.LastSimulation.RunningCardSequence)
                        {
                            ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.ReflowRouteCode;
                            ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.ReflowOPCode;
                            if (ts.ReflowOPCode.Length == 0)
                            {
                                ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.LastSimulation.MOCode, ts.ReflowRouteCode);
                                if (itemOP != null)
                                {
                                    if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                                        || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                                    {
                                        ((ExtendSimulation)product.LastSimulation).NextOPCode = itemOP.OPCode;
                                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                                    }
                                }
                                else
                                {
                                    throw new Exception("$CS_Route_Lost_First_OP_Of_Route" + " : " + ts.ReflowRouteCode);
                                }
                            }
                            //���践��
                            //return;
                        }
                    }
                    #endregion

                    if (((ExtendSimulation)product.LastSimulation).NextRouteCode == null
                        || ((ExtendSimulation)product.LastSimulation).NextRouteCode.Trim().Length <= 0)
                    {
                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.RouteCode;
                    }

                    if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                        || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                    {
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.OPCode;
                    }
                    break;
            }
            //
            //			if(product.CurrentItemRoute2OP == null)
            //			{
            //				product.CurrentItemRoute2OP = (ItemRoute2OP)(new ItemFacade(DataProvider)).GetItemRoute2Operation(
            //						product.LastSimulation.ItemCode
            //					,((ExtendSimulation)product.LastSimulation).NextRouteCode
            //					,((ExtendSimulation)product.LastSimulation).NextOPCode);
            //				
            //			}//Laws Lu,2005/12/27������	���ǵ�;�̺͹���任��case
            //			else if(product.CurrentItemRoute2OP != null 
            //				&& (product.CurrentItemRoute2OP.OPCode != ((ExtendSimulation)product.LastSimulation).NextOPCode 
            //				|| product.CurrentItemRoute2OP.RouteCode != ((ExtendSimulation)product.LastSimulation).NextRouteCode))
            //			{
            //				product.CurrentItemRoute2OP = (ItemRoute2OP)(new ItemFacade(DataProvider)).GetItemRoute2Operation(
            //					product.LastSimulation.ItemCode
            //					,((ExtendSimulation)product.LastSimulation).NextRouteCode
            //					,((ExtendSimulation)product.LastSimulation).NextOPCode);
            //			}
        }

        private void CheckMaterialAndTest(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, null);
        }
        private void CheckMaterialAndTest(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            #region �������Ժ�������ͬһ������
            //Laws Lu,AM0133
            //Ĭ�ϻ��ڵ�ǰվ����һ������{hight}
            //Laws Lu,2005/09/01,���Ժ�����Action �ڽ�����һվǰ�����������
            //Laws Lu,2005/09/27,�޸�	FQC��GOOD����Pass�������ų�����
            /*�����߼������һ�������Ƿ����û������������˼�������Action��KeyParts����Action�ļ�飻
                     * �����ϲ����пͻ���ҵ���ϱ�֤��
                     * ������߼������������ݲɼ�ģ���Spec�У���RD�������Ϸ�ʽ��
                     * Ŀǰû����Rework�ļ��
                     */
            if (
                ActionType.DataCollectAction_SMTGOOD == actionType
                || ActionType.DataCollectAction_SMTNG == actionType
                || ActionType.DataCollectAction_GOOD == actionType
                || ActionType.DataCollectAction_NG == actionType
                || ActionType.DataCollectAction_OQCGood == actionType
                || ActionType.DataCollectAction_OQCNG == actionType
                || ActionType.DataCollectAction_OQCPass == actionType
                /*|| ActionType.DataCollectAction_OQCReject == actionType*/)/* && product.NowSimulation.FromRoute != ""*/
            {
                //(new BaseSetting.BaseModelFacade()).getop
                //Laws Lu,2005/11/09,�޸�	��������

                object currentOP = null;

                if (product.CurrentItemRoute2OP != null)
                {
                    currentOP = product.CurrentItemRoute2OP;
                }
                else
                {
                    currentOP = (new ItemFacade(this._domainDataProvider)).GetItemRoute2Operation(product.NowSimulation.ItemCode, product.NowSimulation.RouteCode, product.NowSimulation.OPCode);
                    product.CurrentItemRoute2OP = currentOP as ItemRoute2OP;
                }

                if (currentOP == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In ©ɨʱ���������FT����ʾ���ò�Ʒ�Ѿ��깤���߱��滻�������ʾҪ�޸ģ��Ա���USER֪�����������깤�ˣ�����ʾҪ�ӵ�һվͶ�롣
                    �������������ʾ��ʱ�򣬼����ָò�Ʒ�Ѿ��깤���Ȳ�Ҫֱ�ӱ��������ں����ټ�һ����飬������깤���򣬲����������Ͼ�����������Ǻܶ࣬�������ܷ���Ĺ��ǿ����ų�����
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }


                string opControls = ((ItemRoute2OP)currentOP).OPControl;

                if (opControls != null && opControls.Length > 2)
                {
                    if (opControls.Substring(0, 2).Trim() == "11"
                        || (FormatHelper.StringToBoolean(opControls, (int)OperationList.Testing)
                        && FormatHelper.StringToBoolean(opControls, (int)OperationList.ComponentDown)))
                    {
                        BenQGuru.eMES.Rework.ReworkFacade reworkFacade = new BenQGuru.eMES.Rework.ReworkFacade(this.DataProvider);
                        BenQGuru.eMES.Domain.Rework.ReworkSheet rSheet = (BenQGuru.eMES.Domain.Rework.ReworkSheet)reworkFacade.GetReworkSheetByReject(product.LastSimulation.RunningCard, product.LastSimulation.RunningCardSequence);

                        //ͳ��KeyParts�ĸ���
                        int keypartTimes = 0;
                        //ͳ��Inno�ĸ���
                        int innoTimes = 0;

                        object[] objs = null;
                        ArrayList listTmp = new ArrayList();
                        if (actionCheckStatus != null)
                        {
                            for (int iaction = 0; iaction < actionCheckStatus.ActionList.Count; iaction++)
                            {
                                ActionEventArgs actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[iaction];
                                if (actionEventArgs is CINNOActionEventArgs || actionEventArgs is CKeypartsActionEventArgs)
                                {
                                    for (int iwip = 0; iwip < actionEventArgs.OnWIP.Count; iwip++)
                                    {
                                        if (actionEventArgs.OnWIP[iwip] is OnWIPItem)
                                        {
                                            OnWIPItem wipItem = (OnWIPItem)actionEventArgs.OnWIP[iwip];
                                            ONWIPItemObject wipObj = new ONWIPItemObject();
                                            wipObj.MCARD = wipItem.MCARD;
                                            wipObj.MCardType = wipItem.MCardType;
                                            wipObj.OPCODE = wipItem.OPCode;
                                            wipObj.RunningCard = wipItem.RunningCard;
                                            wipObj.RunningCardSequence = wipItem.RunningCardSequence.ToString();
                                            listTmp.Add(wipObj);
                                        }
                                    }
                                }
                            }
                        }
                        if (listTmp.Count > 0)
                        {
                            objs = new object[listTmp.Count];
                            listTmp.CopyTo(objs);
                        }
                        if (objs == null || objs.Length == 0)
                        {
                            objs = this.ExtraQuery(product.NowSimulation.RunningCard
                                , product.NowSimulation.OPCode
                                , product.NowSimulation.MOCode
                                , product.LastSimulation.LastAction, product);
                        }
                        string opBOMType = actionCheckStatus == null ? String.Empty : actionCheckStatus.opBOMType;
                        if (opBOMType == string.Empty)
                        {
                            //��ȡOPBOM��ά��������Ϣ
                            OPBOMFacade opFac = new OPBOMFacade(this._domainDataProvider);
                            if (FormatHelper.StringToBoolean(opControls, (int)OperationList.ComponentDown))
                            {
                                opBOMType = opFac.GetOPDropBOMDetailType(
                                    product.NowSimulation.MOCode
                                    , product.NowSimulation.RouteCode
                                    , ((ItemRoute2OP)currentOP).OPCode
                                    , out keypartTimes
                                    , out innoTimes);
                                //Laws Lu,2006/03/13 ���Ƿְ����
                                keypartTimes = Convert.ToInt32(keypartTimes * product.LastSimulation.IDMergeRule);
                            }
                            else
                            {
                                opBOMType = opFac.GetOPBOMDetailType(
                                    product.NowSimulation.MOCode
                                    , product.NowSimulation.RouteCode
                                    , ((ItemRoute2OP)currentOP).OPCode
                                    , out keypartTimes
                                    , out innoTimes);
                                //Laws Lu,2006/03/13 ���Ƿְ����
                                keypartTimes = Convert.ToInt32(keypartTimes * product.LastSimulation.IDMergeRule);
                            }

                            if (actionCheckStatus != null)
                            {
                                actionCheckStatus.opBOMType = opBOMType;
                                actionCheckStatus.keypartTimes = keypartTimes;
                                actionCheckStatus.innoTimes = innoTimes;
                            }
                        }
                        else
                        {
                            if (actionCheckStatus != null)
                            {
                                keypartTimes = actionCheckStatus.keypartTimes;
                                innoTimes = actionCheckStatus.innoTimes;
                            }
                        }
                        //�����ǰ��Rework�����Ļ�,ͳ�ƴ���
                        int iCount = 1;
                        if (rSheet != null && rSheet.ReworkType == Web.Helper.ReworkType.REWORKTYPE_ONLINE)
                        {
                            object[] objReworks = reworkFacade.QueryReworkSheet(rSheet.ReworkCode);

                            if (objReworks != null && objReworks.Length > 0)
                            {
                                for (int i = 0; i < objReworks.Length; i++)
                                {
                                    BenQGuru.eMES.Domain.Rework.ReworkSheet rs = (BenQGuru.eMES.Domain.Rework.ReworkSheet)objReworks[i];

                                    if (rs.ReworkType == Web.Helper.ReworkType.REWORKTYPE_ONLINE)
                                    {
                                        object[] objOPS = (new MOModel.ItemFacade(this._domainDataProvider)).QueryItem2Operation(rs.ItemCode, rs.NewRouteCode);
                                        for (int j = 0; j < objOPS.Length; j++)
                                        {
                                            ItemRoute2OP i2op = (ItemRoute2OP)objOPS[j];
                                            if (i2op.OPCode == product.LastSimulation.OPCode)
                                            {
                                                iCount += 1;
                                            }
                                        }
                                    }
                                }
                            }
                            //product.LastSimulation.FromRoute
                        }

                        keypartTimes = keypartTimes * iCount;
                        innoTimes = innoTimes * iCount;

                        if (objs == null || (objs != null && objs.Length < 1))
                        {
                            if (BenQGuru.eMES.Web.Helper.FormatHelper.StringToBoolean(product.CurrentItemRoute2OP.OPControl, (int)OperationList.ComponentLoading))
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                            }
                            if (BenQGuru.eMES.Web.Helper.FormatHelper.StringToBoolean(product.CurrentItemRoute2OP.OPControl, (int)OperationList.ComponentDown))
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_DROPMATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                            }
                            if (product.LastSimulation.LastAction == ActionType.DataCollectAction_DropMaterial)
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_DROPMATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                            }
                            else
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                            }
                        }


                        string actionList = String.Empty;
                        //ʵ������KeyParts����
                        int actualKeyParts = 0;
                        //ʵ������Inno����
                        int actualInnos = 0;

                        string keyPartsString = ActionType.DataCollectAction_CollectKeyParts;
                        string INNOString = ActionType.DataCollectAction_CollectINNO;

                        string bomKeyPartsString = BOMItemControlType.ITEM_CONTROL_KEYPARTS;
                        string bomINNOString = BOMItemControlType.ITEM_CONTROL_LOT;

                        if (objs != null)
                        {
                            foreach (ONWIPItemObject wip in objs)
                            {
                                if (wip.MCardType == MCardType.MCardType_Keyparts)
                                {

                                    actionList = actionList + keyPartsString + ";";
                                    actualKeyParts += 1;
                                }

                                if (wip.MCardType == MCardType.MCardType_INNO)
                                {

                                    actionList = actionList + INNOString + ";";
                                    //Laws Lu��2006/03/10���޸�	ֻ����һ�μ�������
                                    actualInnos = 1;
                                }
                            }
                        }

                        if (actionList.IndexOf(INNOString) < 0
                            && actionList.IndexOf(keyPartsString) < 0)
                        {
                            if (product.LastSimulation.LastAction == ActionType.DataCollectAction_DropMaterial)
                            {
                                throw new Exception("$CS_PLEASE_DROP_INNO_KEYPARTS $CS_Param_ID =" + product.LastSimulation.RunningCard);
                            }
                            else
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                            }
                            return;
                        }
                        //���KeyParts���ϵ����
                        if (opBOMType.IndexOf(bomKeyPartsString) >= 0
                            && opBOMType.IndexOf(bomINNOString) < 0)
                        {
                            if (actionList.IndexOf(keyPartsString) < 0
                                || actualKeyParts < keypartTimes)
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_DropMaterial)
                                {
                                    throw new Exception("$CS_PLEASE_DROP_INNO_KEYPARTS $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }
                                else
                                {
                                    throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }
                            }
                            if (actionList.IndexOf(keyPartsString) >= 0 && actualKeyParts > keypartTimes && actualKeyParts != 0)
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_CollectKeyParts)
                                {
                                    throw new Exception("$CS_ALREADY_COLLECTMATIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }

                            }
                        }
                        //��鼯�����ϵ����
                        if (opBOMType.IndexOf(bomKeyPartsString) < 0
                            && opBOMType.IndexOf(bomINNOString) >= 0)
                        {
                            if (actionList.IndexOf(INNOString) < 0
                                || actualInnos < innoTimes)
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_DropMaterial)
                                {
                                    throw new Exception("$CS_PLEASE_DROP_INNO_KEYPARTS $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }
                                else
                                {
                                    throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }
                            }

                            if (actionList.IndexOf(INNOString) >= 0 && actualInnos > innoTimes && actualInnos != 0)
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_CollectINNO)
                                {
                                    throw new Exception("$CS_ALREADY_COLLECTMATIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }

                            }
                        }
                        //���߶����ڵ�״��
                        if (opBOMType.IndexOf(bomKeyPartsString) >= 0
                            && opBOMType.IndexOf(bomINNOString) >= 0)
                        {
                            if (actionList.IndexOf(INNOString) < 0
                                || actionList.IndexOf(keyPartsString) < 0
                                || actualInnos < innoTimes
                                || actualKeyParts < keypartTimes)
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_DropMaterial)
                                {
                                    throw new Exception("$CS_PLEASE_DROP_INNO_KEYPARTS $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }
                                else
                                {
                                    throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }
                            }

                            if ((actionList.IndexOf(INNOString) >= 0 && actualInnos > innoTimes && actualInnos != 0)
                                || (actionList.IndexOf(keyPartsString) >= 0 && actualKeyParts > keypartTimes && actualKeyParts != 0))
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_CollectKeyParts
                                    || product.LastSimulation.LastAction == ActionType.DataCollectAction_CollectINNO)
                                {
                                    throw new Exception("$CS_ALREADY_COLLECTMATIAL $CS_Param_ID =" + product.LastSimulation.RunningCard);
                                }

                            }
                        }
                    }
                }
            }
            #endregion
        }
        //OUT_Line	-> OQC  OUT_Line	-> Normal �����߱任��ָ�� Route + OP
        public ItemRoute2OP CheckOnlineOPSingle(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            return CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product, null);
        }
        // Added by Icyer 2005/11/01
        // ��չActionCheckStatus
        public ItemRoute2OP CheckOnlineOPSingle(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            // Added by Icyer 2005/11/02
            if (actionCheckStatus != null)
            {
                if (actionCheckStatus.CheckedNextOP == true && actionCheckStatus.OP != null)
                {
                    bool bPass = false;
                    //��ǰվ
                    if (product.LastSimulation.NextOPCode == actionCheckStatus.OP.OPCode &&
                        product.LastSimulation.NextRouteCode == actionCheckStatus.OP.RouteCode)
                    {
                        bPass = true;
                    }
                    if (bPass == true)
                    {
                        this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);
                        OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                        return actionCheckStatus.OP;
                    }
                }
            }
            // Added end

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            //��һվ����ɼ�
            if (dataModel.GetOperation2Resource(((ExtendSimulation)product.LastSimulation).NextOPCode, resourceCode) != null)
            {
                ItemRoute2OP op = this.GetMORouteOP(product.LastSimulation.ItemCode, product.LastSimulation.MOCode, ((ExtendSimulation)product.LastSimulation).NextRouteCode, ((ExtendSimulation)product.LastSimulation).NextOPCode, product);
                if (op == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In ©ɨʱ���������FT����ʾ���ò�Ʒ�Ѿ��깤���߱��滻�������ʾҪ�޸ģ��Ա���USER֪�����������깤�ˣ�����ʾҪ�ӵ�һվͶ�롣
                    �������������ʾ��ʱ�򣬼����ָò�Ʒ�Ѿ��깤���Ȳ�Ҫֱ�ӱ��������ں����ټ�һ����飬������깤���򣬲����������Ͼ�����������Ǻܶ࣬�������ܷ���Ĺ��ǿ����ų�����
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }
                this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);

                OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                // Added by Icyer 2005/11/02
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.OP = op;
                    actionCheckStatus.CheckedNextOP = true;
                    actionCheckStatus.CheckedNextOPCode = product.LastSimulation.OPCode;
                    actionCheckStatus.CheckedNextRouteCode = product.LastSimulation.NextRouteCode;
                }
                // Added end

                return op;
            }
            else
            {
                //Laws Lu,2005/11/22,����	Checkά�޻��������
                if (product.LastTS != null
                    && product.LastTS.TSStatus == TSStatus.TSStatus_Reflow
                    && product.LastTS.RunningCardSequence == product.LastSimulation.RunningCardSequence)
                {
                    throw new Exception("$CS_Route_Failed $CS_Param_OPCode =" + product.LastTS.ReflowOPCode
                        + " $CS_Param_ID =" + product.LastSimulation.RunningCard);
                }
                else if (product.LastSimulation.ProductStatus == ProductStatus.NG)
                {
                    throw new Exception("$CS_Product_NG_Please_TS");
                }
                else
                {//Laws Lu,2005/12/27,ע��	�����Ժ����Ҫ����ȷ�Ĺ�����������
                    throw new Exception("$CS_Route_Failed_GetNextOP_Online_Line $CS_Param_ID =" + product.LastSimulation.RunningCard);
                }
            }
        }
        // Added end

        public void OtherCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            OtherCheck(iD, actionType, resourceCode, userCode, product, null);
        }
        public void OtherCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            //Laws Lu,2005/09/13,����	���ϺͲ���ͬʱ�������
            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                    //�������Action�Ͳ���Action�Ƿ���ͬһ������
                    string route = product.LastSimulation.RouteCode;
                    string opcode = product.LastSimulation.OPCode;
                    //��ʱ��ֵ
                    product.LastSimulation.RouteCode = product.LastSimulation.FromRoute;
                    product.LastSimulation.OPCode = product.LastSimulation.FromOP;

                    CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                    //�ָ�����ֵ
                    product.LastSimulation.RouteCode = route;
                    product.LastSimulation.OPCode = opcode;

                    break;

                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_OQCNG:

                    //�������Action�Ͳ���Action�Ƿ���ͬһ������
                    CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                    break;


                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                    BenQGuru.eMES.Rework.ReworkFacade reworkFacade = new BenQGuru.eMES.Rework.ReworkFacade(this.DataProvider);
                    BenQGuru.eMES.Domain.Rework.ReworkSheet rSheet = (BenQGuru.eMES.Domain.Rework.ReworkSheet)reworkFacade.GetReworkSheetByReject(product.LastSimulation.RunningCard, product.LastSimulation.RunningCardSequence);
                    if (rSheet == null)
                    {
                        //((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.NG;
                        throw new Exception("$CS_Card_Has_Reject" + " : " + product.LastSimulation.RunningCard);
                    }
                    else
                    {

                        //��ʱroute op
                        string tmpRoute = product.NowSimulation.RouteCode;
                        string tmpOP = product.NowSimulation.OPCode;

                        //��ʱ��ֵ
                        product.NowSimulation.RouteCode = rSheet.NewRouteCode;
                        if (rSheet.NewOPCode.Length == 0)
                        {
                            ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.NowSimulation.MOCode, rSheet.NewRouteCode);
                            if (itemOP != null)
                            {
                                product.NowSimulation.OPCode = itemOP.OPCode;
                            }
                            else
                            {
                                throw new Exception("$CS_Route_Lost_First_OP_Of_Route" + " : " + rSheet.NewRouteCode);
                            }
                        }
                        else
                        {
                            product.NowSimulation.OPCode = rSheet.NewOPCode;
                        }

                        //�������Action�Ͳ���Action�Ƿ���ͬһ������
                        CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                        //�ָ�����ֵ
                        product.NowSimulation.RouteCode = tmpRoute;
                        product.NowSimulation.OPCode = tmpOP;


                    }
                    break;
                default:
                    //�������Action�Ͳ���Action�Ƿ���ͬһ������
                    CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);


                    break;
            }

        }

        public ItemRoute2OP CheckOnlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            return CheckOnlineOP(iD, actionType, resourceCode, userCode, product, null);
        }
        public ItemRoute2OP CheckOnlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            // Added by Icyer 2005/11/02
            if (actionCheckStatus != null)
            {
                if (actionCheckStatus.CheckedNextOP == true && actionCheckStatus.OP != null)
                {
                    bool bPass = false;
                    //��ǰվ
                    if (product.LastSimulation.NextOPCode == actionCheckStatus.OP.OPCode &&
                        product.LastSimulation.NextRouteCode == actionCheckStatus.OP.RouteCode)
                    {
                        bPass = true;
                    }
                    else if (product.LastSimulation.NextOPCode == actionCheckStatus.CheckedNextOPCode &&
                        product.LastSimulation.NextRouteCode == actionCheckStatus.CheckedNextRouteCode)
                    {
                        //��һվ
                        bPass = true;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = actionCheckStatus.OP.OPCode;
                    }
                    if (bPass == true)
                    {
                        this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);
                        OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                        return actionCheckStatus.OP;
                    }
                }
            }
            // Added end

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            ItemRoute2OP op;


            //��һվ,��ǰվ������
            if (dataModel.GetOperation2Resource(((ExtendSimulation)product.LastSimulation).NextOPCode, resourceCode) != null)
            {
                op = this.GetMORouteOP(product.LastSimulation.ItemCode, product.LastSimulation.MOCode, ((ExtendSimulation)product.LastSimulation).NextRouteCode, ((ExtendSimulation)product.LastSimulation).NextOPCode, product);
                if (op == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In ©ɨʱ���������FT����ʾ���ò�Ʒ�Ѿ��깤���߱��滻�������ʾҪ�޸ģ��Ա���USER֪�����������깤�ˣ�����ʾҪ�ӵ�һվͶ�롣
                    �������������ʾ��ʱ�򣬼����ָò�Ʒ�Ѿ��깤���Ȳ�Ҫֱ�ӱ��������ں����ټ�һ����飬������깤���򣬲����������Ͼ�����������Ǻܶ࣬�������ܷ���Ĺ��ǿ����ų�����
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }

                this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);

                //Laws Lu,2005/09/13,����
                OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                // Added by Icyer 2005/11/02
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.OP = op;
                    actionCheckStatus.CheckedNextOP = true;
                    actionCheckStatus.CheckedNextOPCode = product.LastSimulation.OPCode;
                    actionCheckStatus.CheckedNextRouteCode = product.LastSimulation.NextRouteCode;
                }
                // Added end

                return op;
            }
            else
            {

                op = this.GetMORouteNextOP(product.LastSimulation.MOCode, ((ExtendSimulation)product.LastSimulation).NextRouteCode, ((ExtendSimulation)product.LastSimulation).NextOPCode);
                if (op == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In ©ɨʱ���������FT����ʾ���ò�Ʒ�Ѿ��깤���߱��滻�������ʾҪ�޸ģ��Ա���USER֪�����������깤�ˣ�����ʾҪ�ӵ�һվͶ�롣
                    �������������ʾ��ʱ�򣬼����ָò�Ʒ�Ѿ��깤���Ȳ�Ҫֱ�ӱ��������ں����ټ�һ����飬������깤���򣬲����������Ͼ�����������Ǻܶ࣬�������ܷ���Ĺ��ǿ����ų�����
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }
                //Laws Lu,2005/11/05,�޸�	ֱ������������������������һվ�ɼ�
                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_GoMO
                    && product.LastSimulation.OPCode != op.OPCode)
                {
                    throw new Exception("$CS_Route_Failed_GetNotNextOP $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                }

                if (product.LastTS != null && product.LastTS.TSStatus == TSStatus.TSStatus_Reflow)
                {
                    throw new Exception("$CS_Route_Failed_GetNotNextOP $CS_Param_OPCode =" + ((ExtendSimulation)product.LastSimulation).NextOPCode);
                }

                // Edited By Hi1/Venus.Feng on 20080721 for Hisense Version
                if (actionType != ActionType.DataCollectAction_OQCNG
                    && actionType != ActionType.DataCollectAction_OQCPass
                    && actionType != ActionType.DataCollectAction_OQCReject
                    && actionType != ActionType.DataCollectAction_OQCGood)
                {
                    if (dataModel.GetOperation2Resource(op.OPCode, resourceCode) == null)
                    {
                        throw new Exception("$CS_Route_Failed $CS_Param_OPCode  =" + op.OPCode + "[" + op.EAttribute1 + "]");
                    }
                }
                // End Added

                // Added by Icyer 2005/11/02
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.OP = op;
                    actionCheckStatus.CheckedNextOP = true;
                    actionCheckStatus.CheckedNextOPCode = product.LastSimulation.OPCode;
                    actionCheckStatus.CheckedNextRouteCode = product.LastSimulation.NextRouteCode;
                }
                // Added end

                //����:
                ((ExtendSimulation)product.LastSimulation).NextOPCode = op.OPCode;
                this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);

                //Laws Lu,2005/09/13,����	
                OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);


                return op;
            }
        }

        public void WriteSimulationCheckOnlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            this.WriteSimulation(iD, actionType, resourceCode, userCode, product);
            product.NowSimulation.RouteCode = ((ExtendSimulation)product.LastSimulation).NextRouteCode;
            product.NowSimulation.OPCode = ((ExtendSimulation)product.LastSimulation).NextOPCode;
        }



        //OutLine
        public void GetRouteOPOutline(string iD, string actionType, string resourceCode, string opCode, string userCode, ProductInfo product)
        {
            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOutlineOP(iD, actionType, resourceCode, opCode, userCode, product, false);
                            break;
                        default:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOnline");
                    }
                    break;

                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOutlineOP(iD, actionType, resourceCode, opCode, userCode, product, true);
                            break;
                        //throw new Exception("$CS_Route_Failed_Not_Support_NG(TS)2Outline");
                        default:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOnline");
                    }
                    break;

                default:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOutlineOP(iD, actionType, resourceCode, opCode, userCode, product, true);
                            break;
                        default:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOnline");
                    }
                    break;
            }
        }

        public void CheckOutlineOP(string iD, string actionType, string resourceCode, string opCode, string userCode, ProductInfo product, bool fristIn)
        {
            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            //��һվ��OutLine ����Ҫ���Route
            if (dataModel.GetOperation2Resource(opCode, resourceCode) != null)
            {
                //((ExtendSimulation)product.LastSimulation).NextRouteCode = string.Empty ;
                ((ExtendSimulation)product.LastSimulation).NextOPCode = opCode;
                this.WriteSimulationCheckOutlineOP(iD, actionType, resourceCode, userCode, product, fristIn);
            }
            else
            {
                //Laws Lu,2005/11/22,����	Checkά�޻��������
                if (product.LastTS != null
                    && product.LastTS.TSStatus == TSStatus.TSStatus_Reflow
                    && product.LastTS.RunningCardSequence == product.NowSimulation.RunningCardSequence)
                {
                    throw new Exception("$CS_Route_Failed_GetNextOP_OUT_Line $CS_Param_OPCode = " + product.LastTS.ReflowOPCode);
                }
                else
                {
                    throw new Exception("$CS_Route_Failed_GetNextOP_OUT_Line");
                }
                //throw new Exception("$CS_Route_Failed_GetNextOP_OUT_Line");
            }
        }

        public void WriteSimulationCheckOutlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, bool fristIn)
        {
            this.WriteSimulation(iD, actionType, resourceCode, userCode, product);

            if (fristIn)
            {
                product.NowSimulation.FromRoute = product.LastSimulation.RouteCode;
                product.NowSimulation.FromOP = product.LastSimulation.OPCode;
            }
            else
            {
                product.NowSimulation.FromRoute = product.LastSimulation.FromRoute;
                product.NowSimulation.FromOP = product.LastSimulation.FromOP;
            }

            product.NowSimulation.RouteCode = string.Empty;
            //Laws Lu,2006/06/12 modify support ts 
            if (((ExtendSimulation)product.LastSimulation).NextOPCode != String.Empty)
            {
                product.NowSimulation.OPCode = ((ExtendSimulation)product.LastSimulation).NextOPCode;
            }
            else
            {
                product.NowSimulation.OPCode = product.LastSimulation.OPCode;//((ExtendSimulation)product.LastSimulation).NextOPCode;
            }
        }


        #endregion ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="outPutQty">ֻ��Ҫ��д������</param>
        public void UpdateMOOutPut(string moCode, int outPutQty)
        {
            string updateSql = string.Format("update TBLMO set MOACTQTY=MOACTQTY+{0} where MOCODE=$moCode1", outPutQty.ToString());
            this.DataProvider.CustomExecute(new SQLParamCondition(updateSql, new SQLParameter[] { new SQLParameter("moCode1", typeof(string), moCode.ToUpper()) }));
        }
        #region ����;����Ϣ
        //��ȡ����;�̵�һ������
        public ItemRoute2OP GetMORouteFirstOP(string moCode, string routeCode)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            ItemRoute2OP op = itemFacade.GetMORouteFirstOperation(moCode, routeCode);
            return op;
        }
        //��ȡ����;����һ������
        public ItemRoute2OP GetMORouteNextOP(string moCode, string routeCode, string curOp)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            ItemRoute2OP op = itemFacade.GetMORouteNextOperation(moCode, routeCode, curOp);
            return op;
        }
        //�����Ƿ�Ϊ;�����һ������
        public bool OPIsMORouteLastOP(string moCode, string routeCode, string opCode)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            return itemFacade.OperationIsRouteLastOperation(moCode, routeCode, opCode);
        }
        //����;�̹�����Ϣ
        public ItemRoute2OP GetMORouteOP(string itemCode, string moCode, string routeCode, string curOp, ProductInfo e)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            if (e.CurrentItemRoute2OP == null)
            {
                e.CurrentItemRoute2OP = (ItemRoute2OP)itemFacade.GetItemRoute2Operation(itemCode, routeCode, curOp);

            }//Laws Lu,2005/12/27������	���ǵ�;�̺͹���任��case
            else if (e.CurrentItemRoute2OP != null
                && (e.CurrentItemRoute2OP.OPCode != curOp
                || e.CurrentItemRoute2OP.RouteCode != routeCode))
            {
                e.CurrentItemRoute2OP = (ItemRoute2OP)itemFacade.GetItemRoute2Operation(itemCode, routeCode, curOp);
            }
            return e.CurrentItemRoute2OP;

        }
        #endregion ����;����Ϣ

        #endregion

        #region ���к�ת���ɼ�
        //ID��ת��ǰ���кţ�����
        //Resrouce����ǰ��̨��Դ���ƣ�����
        //UserCode�������û�������
        // Language�����Կ��أ�0 Ӣ�ģ�1 ���ģ�Ŀǰֻ�ṩ����
        //����ֵ����OK�� ��ʾ���ͨ��������Ϊ������Ϣ
        public string CheckIDRoute(string ID, string Resrouce, string UserCode, int Language)
        {
            ID = ID.Trim().ToUpper();
            Resrouce = Resrouce.Trim().ToUpper();
            UserCode = UserCode.Trim().ToUpper();

            if (ID == string.Empty)
                return MutiLanguages.ParserMessage("$CS_IDisNull");
            if (Resrouce == string.Empty)
                return MutiLanguages.ParserMessage("$CS_ResrouceisNull");
            if (UserCode == string.Empty)
                return MutiLanguages.ParserMessage("$CS_UserCodeisNull");
            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            Messages messages1 = onLine.GetIDInfo(ID);
            if (messages1.IsSuccess())
            {
                ((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                try
                {
                    ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                    messages1.AddMessages(onLine.CheckID(new ActionEventArgs(ActionType.DataCollectAction_IDTran, ID,
                        UserCode, Resrouce,
                        product
                        )));
                    if (!messages1.IsSuccess())
                        return GetErrorMessage(messages1);
                    return "OK";
                }
                catch
                {
                }
                finally
                {
                    ((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }
                return "";
            }
            else
            {
                return GetErrorMessage(messages1);
            }

        }

        public object[] QuerySimulationT(string runningCard)
        {
            string sql = "SELECT {0} FROM tblsimulation WHERE tcard = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation)), runningCard.ToUpper());

            return this.DataProvider.CustomQuery(typeof(Simulation), new SQLCondition(sql));
        }

        public string GetErrorMessage(Messages messages)
        {
            for (int i = 0; i < messages.Count(); i++)
            {
                Message message = messages.Objects(i);
                if (message.Type == MessageType.Error)
                {
                    if (message.Body == string.Empty)
                        return MutiLanguages.ParserMessage(message.Exception.Message);
                    else
                        return MutiLanguages.ParserMessage(message.Body);
                }

            }
            //			return messages.Debug();
            return MutiLanguages.ParserMessage("$CS_System_unKnowError");
        }

        //�������к�ת����¼�ӿ�
        //ID��ת��ǰ���кţ�����
        //Resrouce����ǰ��̨��Դ���ƣ�����
        //UserCode�������û�������
        //newID���·����ID������
        //IMEI ��оƬ��ԭ��ID�����оƬԭ��û����Ϊ���ַ���
        // Language�����Կ��أ�0 Ӣ�ģ�1 ���ģ�Ŀǰֻ�ṩ����
        //����ֵ����OK�� ��ʾ����ɹ�������Ϊ������Ϣ
        public string SaveTransferInfo(string ID, string Resrouce, string UserCode, string newID, string IMEI, int Language)
        {
            ID = ID.Trim().ToUpper();
            Resrouce = Resrouce.Trim().ToUpper();
            UserCode = UserCode.Trim().ToUpper();
            newID = newID.Trim().ToUpper();
            IMEI = IMEI.Trim().ToUpper();
            if (ID == string.Empty)
                return MutiLanguages.ParserMessage("$CS_IDisNull");
            if (Resrouce == string.Empty)
                return MutiLanguages.ParserMessage("$CS_ResrouceisNull");
            if (UserCode == string.Empty)
                return MutiLanguages.ParserMessage("$CS_UserCodeisNull");
            if (newID == string.Empty)
                return MutiLanguages.ParserMessage("$CS_newIDisNull");


            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            ((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {
                ProductInfo product;
                Messages messages1 = onLine.GetIDInfo(ID);
                messages1.Add(new Message(MessageType.Debug, "GetIDInfo(ID)"));
                if (messages1.IsSuccess())
                {
                    messages1.Add(new Message(MessageType.Debug, "GetIDInfo(ID) OK"));
                    product = (ProductInfo)messages1.GetData().Values[0];
                    if (product.LastSimulation == null)
                    {
                        throw new Exception("$NoSimulation");
                    }
                }
                else
                {
                    return GetErrorMessage(messages1);
                }
                //AMOI  MARK  START  20050803 ����ظ� �����ԭ�������ϼ�һ
                bool IDIsUsed = false;
                decimal seq = ActionOnLineHelper.StartSeq;
                messages1.Add(new Message(MessageType.Debug, "seq"));
                //AMOI  MARK  END

                if (ID == newID)
                {
                    messages1.Add(new Message(MessageType.Debug, "ID ==newID"));
                }
                else
                {
                    OnWIP onWip = CheckIDIsUsed(newID, product.LastSimulation.MOCode);
                    if (onWip == null)
                    {
                        messages1.Add(new Message(MessageType.Debug, "onWip==null"));
                    }
                    else
                    {
                        //AMOI  MARK  START  20050803 ����ظ� �����ԭ�������ϼ�һ
                        //throw new Exception("$CS_IDRepeatCollect");
                        IDIsUsed = true;
                        seq = onWip.RunningCardSequence + 10;
                        messages1.Add(new Message(MessageType.Debug, "nWip.RunningCardSequence +10;" + onWip.RunningCardSequence.ToString()));
                        //AMOI  MARK  END
                    }
                }
                messages1.AddMessages(onLine.CheckID(new ActionEventArgs(ActionType.DataCollectAction_IDTran, ID,
                    UserCode, Resrouce, product)));
                if (messages1.IsSuccess())
                {
                    #region ����
                    this.DataProvider.BeginTransaction();
                    try
                    {

                        //�޸�SIMULATION
                        product.NowSimulation.RunningCard = newID;
                        if (ID == newID)
                        {
                            product.NowSimulation.RunningCardSequence = product.LastSimulation.RunningCardSequence + 1;
                            messages1.Add(new Message(MessageType.Debug, "ID ==newID"));
                        }
                        else
                        {
                            //AMOI  MARK  START  20050803 ����ظ� �����ԭ�������ϼ�һ
                            product.NowSimulation.RunningCardSequence = seq;
                            messages1.Add(new Message(MessageType.Debug, seq.ToString()));

                            //AMOI  MARK  END
                        }
                        product.NowSimulation.TranslateCard = product.LastSimulation.RunningCard;
                        product.NowSimulation.TranslateCardSequence = product.LastSimulation.RunningCardSequence;
                        //Laws Lu,2005/08/15,����	�깤�߼���������Check��ͨ��������£����е�RunningCardӦ����GOOD״̬
                        //��ʱ���������⹤��
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        if (product.NowSimulation.RouteCode != "" && dataCollectFacade.OPIsMORouteLastOP(
                            product.NowSimulation.MOCode
                            , product.NowSimulation.RouteCode
                            , product.NowSimulation.OPCode))
                        {
                            product.NowSimulation.IsComplete = "1";
                            product.NowSimulation.EAttribute1 = "GOOD";
                            //�깤�Զ����
                            dataCollectFacade.AutoInventory(product.NowSimulation, UserCode);
                        }
                        //���ID�Ѿ���ʹ�ù�����ԭ����SIMULATION��Ҫɾ��
                        if (IDIsUsed)
                        {
                            this.DeleteSimulation(product.NowSimulation);
                            SimulationReport simulationReport = new SimulationReport();
                            simulationReport.RunningCard = product.NowSimulation.RunningCard;
                            simulationReport.MOCode = product.NowSimulation.MOCode;
                            this.DeleteSimulationReport(simulationReport);
                            messages1.Add(new Message(MessageType.Debug, "IDIsUsed "
                                + product.NowSimulation.RunningCard + ";" + product.NowSimulation.MOCode));

                        }

                        messages1.AddMessages(onLine.Execute(new ActionEventArgs(ActionType.DataCollectAction_Split, ID,
                            UserCode, Resrouce, product)));


                        if (messages1.IsSuccess())
                        {
                            //��дIDת������ TODO
                            OnWIPCardTransfer transf = this.CreateNewOnWIPCardTransfer();

                            transf.RunningCard = product.NowSimulation.RunningCard;
                            transf.RunningCardSequence = product.NowSimulation.RunningCardSequence;
                            transf.TranslateCard = product.NowSimulation.TranslateCard;
                            transf.TranslateCardSequence = product.NowSimulation.TranslateCardSequence;
                            transf.SourceCard = product.NowSimulation.SourceCard;
                            transf.IDMergeType = IDMergeType.IDMERGETYPE_IDMERGE;
                            transf.ModelCode = product.NowSimulation.ModelCode;
                            transf.MOCode = product.NowSimulation.MOCode;
                            transf.ItemCode = product.NowSimulation.ItemCode;
                            transf.ResourceCode = product.NowSimulation.ResourceCode;
                            transf.OPCode = product.NowSimulation.OPCode;
                            transf.SourceCardSequence = product.NowSimulation.SourceCardSequence;
                            transf.RouteCode = product.NowSimulation.RouteCode;
                            transf.StepSequenceCode = product.NowSimulationReport.StepSequenceCode;
                            transf.SegmnetCode = product.NowSimulationReport.SegmentCode;
                            transf.TimePeriodCode = product.NowSimulationReport.TimePeriodCode;
                            transf.ShiftCode = product.NowSimulationReport.ShiftCode;
                            transf.ShiftTypeCode = product.NowSimulationReport.ShiftTypeCode;
                            transf.MaintainUser = product.NowSimulationReport.MaintainUser;
                            transf.MOSeq = product.NowSimulationReport.MOSeq;

                            this.AddOnWIPCardTransfer(transf);

                            // ��ID���ӵ�MO��Χ����
                            //Laws Lu,2005/10/31
                            MORunningCardFacade cardFacade = new MORunningCardFacade(this.DataProvider);
                            MORunningCard card = cardFacade.CreateNewMORunningCard();

                            card.MOCode = product.NowSimulation.MOCode;
                            card.MORunningCardStart = product.NowSimulation.RunningCard;
                            card.MORunningCardEnd = card.MORunningCardStart;
                            card.MaintainUser = UserCode;
                            card.MOSeq = product.NowSimulation.MOSeq;   // Added by Icyer 2007/07/02

                            cardFacade.AddMORunningCard(card);

                            #region ��дͳ�Ʊ��� ����Դͳ��
                            //ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                            //messages1.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, ActionType.DataCollectAction_IDTran, product));
                            //messages1.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, ActionType.DataCollectAction_IDTran, product));
                            //							if (!messages1.IsSuccess())
                            //								return GetErrorMessage(messages1);
                            #endregion

                            if (messages1.IsSuccess())
                            {
                                this.DataProvider.CommitTransaction();
                            }
                            else
                            {
                                this.DataProvider.RollbackTransaction();
                            }
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            return GetErrorMessage(messages1);
                        }
                        //AMOI  MARK  START  20050806 ���Ӱ���Դͳ�Ʋ���

                        //AMOI  MARK  END
                    }
                    catch (Exception e)
                    {
                        this.DataProvider.RollbackTransaction();
                        messages1.Add(new Message(e));
                        return GetErrorMessage(messages1);
                    }
                    #endregion

                }
                else
                {
                    return GetErrorMessage(messages1);
                }
                if (messages1.IsSuccess())
                {
                    return "OK";
                }
                else
                {
                    return messages1.Objects(messages1.Count() - 1).Body;
                }
            }
            catch (Exception e)
            {
                return MutiLanguages.ParserMessage(e.Message);
            }
            finally
            {
                ((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

        }

        public string GetSourceCard(string rCard, string moCode)
        {
            string sql = string.Empty;
            sql += " select  distinct GetSourceCard('" + rCard.Trim().ToUpper() + "','" + moCode.Trim().ToUpper() + "') scard from tblonwipcardtrans ";
            sql += "  WHERE GetSourceCard('" + rCard.Trim().ToUpper() + "','" + moCode.Trim().ToUpper() + "')=tblonwipcardtrans.scard";
            object[] OnWIPCardTransferList = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(sql));

            if (OnWIPCardTransferList != null)
            {
                return ((OnWIPCardTransfer)OnWIPCardTransferList[0]).SourceCard;
            }

            return rCard;
        }
        #endregion


        #region Simulation
        /// <summary>
        /// 
        /// </summary>
        public Simulation CreateNewSimulation()
        {
            return new Simulation();
        }

        public void AddSimulation(Simulation simulation)
        {
            this.DataProvider.Insert(simulation);
        }

        public void UpdateSimulation(Simulation simulation)
        {
            this.DataProvider.Update(simulation);
        }

        public void DeleteSimulation(Simulation simulation)
        {
            this.DataProvider.Delete(simulation);
        }

        //		public void DeleteSimulation(Simulation[] simulation)
        //		{
        //			//this.DataProvider.Delete ( simulation );
        //		}

        // Added By Hi1/Venus.Feng on 20080718 For Hisense Version
        public void UpdateSimulationForLot(string rCard, string moCode, string lotNo)
        {
            string sql = "UPDATE tblsimulation SET lotno='" + lotNo + "' WHERE rcard='" + rCard + "' AND mocode='" + moCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        // End Added

        /// <summary>
        /// ** �� ��:		Jane Shu	2005-07-21
        ///					�Ĵ���SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object GetSimulation(string runningCard)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(Simulation),
                //new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD order by MDATE desc,MTIME desc",
                new SQLParamCondition(string.Format(
                @"select {0} from tblsimulation where
					(rcard,mocode) in (
					select rcard, mocode
					from (select rcard, mocode
							from TBLSIMULATION
							where RCARD = $RCARD
							order by MDATE desc, MTIME desc)
							where rownum = 1)",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation))),
                new SQLParameter[] { new SQLParameter("RCARD", typeof(string), runningCard.ToUpper()) }));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations[0];
            else
                return null;

        }

        public object[] QuerySimulation(string runningCard)
        {
            string sql = "SELECT {0} FROM tblsimulation WHERE rcard = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation)), runningCard.ToUpper());

            return this.DataProvider.CustomQuery(typeof(Simulation), new SQLCondition(sql));
        }

        public object[] QuerySimulationReport(string runningCard)
        {
            string sql = "SELECT {0} FROM tblsimulationreport WHERE rcard = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport)), runningCard.ToUpper());

            return this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(sql));
        }

        /// <summary>
        /// ** �� ��:		Jane Shu	2005-07-21
        ///					�Ĵ���SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] GetOnlineSimulationByMoCode(string moCode)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(Simulation),
                //new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD order by MDATE desc,MTIME desc",
                new SQLCondition(string.Format(
                @"select {0} from tblsimulation where
					mocode = '{1}' and iscom = 0",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation)),
                moCode.ToUpper())));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations;
            else
                return null;

        }

        /// <summary>
        /// ** �� ��:		Jane Shu	2005-07-21
        ///					�Ĵ���SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] GetSimulationFromCarton(string cartonno)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(Simulation),
                //new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD order by MDATE desc,MTIME desc",
                new SQLCondition(string.Format(
                @"select {0} from tblsimulation where
					cartoncode = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation)),
                cartonno.ToUpper())));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations;
            else
                return null;

        }

        // Added by HI1/Venus.Feng on 20081118 for Hisense Version
        public object GetRCardByCartonCode(string cartonCode)
        {
            string strSql = "";
            strSql += "SELECT {0}";
            strSql += "  FROM (SELECT   {0}";
            strSql += "            FROM tblsimulationreport";
            strSql += "           WHERE cartoncode = '" + cartonCode + "'";
            strSql += "        ORDER BY mdate DESC, mtime DESC)";
            strSql += " WHERE ROWNUM = 1";

            object[] list = this.DataProvider.CustomQuery(typeof(SimulationReport),
                new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport)))));

            if (list == null || list.Length == 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }
        // End Added

        /// <summary>
        /// ** �� ��:		Laws Lu	2005-08-19
        ///					�Ĵ���SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object GetSimulation(string moCode, string runningCard)
        {
            //return this.DataProvider.CustomSearch(typeof(Simulation), new object[]{ runningCard });
            object[] simulations = this.DataProvider.CustomQuery(typeof(Simulation),
                new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD and MOCODE = $MOCODE order by MDATE desc,MTIME desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation))),
                new SQLParameter[] { new SQLParameter("RCARD", typeof(string), runningCard.ToUpper()), new SQLParameter("MOCODE", typeof(string), moCode.ToUpper()) }));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations[0];
            else
                return null;

        }

        // Added by Icyer 2006/11/08
        /// <summary>
        /// ����translateCard��ѯת�������к�
        /// </summary>
        public object[] GetSimulationFromTCard(string translateCard)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(Simulation),
                new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where TCARD = $TCARD ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation))),
                new SQLParameter[] { new SQLParameter("TCARD", typeof(string), translateCard.ToUpper()) }));
            return simulations;
        }
        // Added end

        //marked not using

        public Object GetLastSimulation(string rcard)
        {
            string strSql = "SELECT * FROM tblSimulation WHERE RCard='" + rcard + "' and mocode in(select mocode from tblmo where mostatus='mostatus_open')";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(Simulation), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return objs[0];
        }
        //end 


        //add by hiro 2008/11/08 
        public Object GetLastSimulationOrderByDateAndTime(string rcard)
        {
            string strSql = "SELECT * FROM tblSimulation WHERE RCard='" + rcard + "' and mocode in(select mocode from tblmo where mostatus='mostatus_open')";
            strSql = strSql + " and productstatus in ('GOOD','OFFLINE','OFFMO','OUTLINE') order by Mdate desc, mtime desc";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(Simulation), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return objs[0];
        }
        //add end 
        #endregion

        #region VersionCollect
        /// <summary>
        /// 
        /// </summary>
        public VersionCollect CreateNewVersionCollect()
        {
            return new VersionCollect();
        }

        public void AddVersionCollect(VersionCollect versionCollect)
        {
            this.DataProvider.Insert(versionCollect);
        }

        public void UpdateVersionCollect(VersionCollect versionCollect)
        {
            this.DataProvider.Update(versionCollect);
        }

        public void DeleteVersionCollect(VersionCollect versionCollect)
        {
            this.DataProvider.Delete(versionCollect);
        }

        /// <summary>
        ///	��ȡ�汾��Ϣ���ɿͻ��׵�ϵͳ��
        /// </summary>
        /// <param name="runningCard">���̿���</param>
        /// <returns></returns>
        public object GetVersionCollect(string runningCard)
        {
            object obj = this.DataProvider.CustomSearch(typeof(VersionCollect), new object[] { runningCard });

            if (obj == null)
                return null;

            return obj;

        }

        #endregion

        #region VersionError
        /// <summary>
        /// 
        /// </summary>
        public VersionError CreateNewVersionError()
        {
            return new VersionError();
        }

        public void AddVersionError(VersionError versionError)
        {
            this.DataProvider.Insert(versionError);
        }

        public void UpdateVersionError(VersionError versionError)
        {
            this.DataProvider.Update(versionError);
        }

        public void DeleteVersionError(VersionError versionError)
        {
            this.DataProvider.Delete(versionError);
        }

        /// <summary>
        /// ��ȡ�汾������Ϣ
        /// </summary>
        /// <param name="runningCard">���̿���</param>
        /// <param name="moCode">������</param>
        /// <returns></returns>
        public object GetVersionError(string runningCard, string moCode)
        {
            object obj = this.DataProvider.CustomSearch(typeof(VersionError), new object[] { runningCard, moCode });

            if (obj == null)
                return null;

            return obj;

        }

        #endregion

        #region ConfigInfo
        /// <summary>
        /// 
        /// </summary>
        public ConfigInfo CreateNewConfigInfo()
        {
            return new ConfigInfo();
        }

        public void AddConfigInfo(ConfigInfo ConfigInfo)
        {
            this.DataProvider.Insert(ConfigInfo);
        }

        public void UpdateConfigInfo(ConfigInfo ConfigInfo)
        {
            this.DataProvider.Update(ConfigInfo);
        }

        public void DeleteConfigInfo(ConfigInfo ConfigInfo)
        {
            this.DataProvider.Delete(ConfigInfo);
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="runningCard">���̿���</param>
        /// <returns></returns>
        public object[] GetConfigInfo(string runningCard)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(ConfigInfo),
                new SQLCondition(String.Format("select {0} from tblconfiginfo where rcard='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ConfigInfo)), runningCard)));

            if (objs == null)
                return null;

            return objs;

        }

        #endregion

        #region OnWipConfigCollect
        /// <summary>
        /// 
        /// </summary>
        public OnWipConfigCollect CreateNewOnWipConfigCollect()
        {
            return new OnWipConfigCollect();
        }

        public void AddOnWipConfigCollect(OnWipConfigCollect OnWipConfigCollect)
        {
            this.DataProvider.Insert(OnWipConfigCollect);
        }

        public void UpdateOnWipConfigCollect(OnWipConfigCollect OnWipConfigCollect)
        {
            this.DataProvider.Update(OnWipConfigCollect);
        }

        public void DeleteOnWipConfigCollect(OnWipConfigCollect OnWipConfigCollect)
        {
            this.DataProvider.Delete(OnWipConfigCollect);
        }

        //		/// <summary>
        //		/// ��ȡ�汾������Ϣ
        //		/// </summary>
        //		/// <param name="runningCard">���̿���</param>
        //		/// <param name="moCode">������</param>
        //		/// <returns></returns>
        //		public object GetOnWipConfigCollect( string runningCard , string RCARDSEQ)
        //		{
        //			object obj = this.DataProvider.CustomSearch(typeof(OnWipConfigCollect),new object[]{RCARD,RCARDSEQ,CheckItemCode,CatergoryCode,ITEMCODE,MoCode});
        //
        //			if (obj == null)
        //				return null;
        //			
        //			return obj;
        //
        //		}

        #endregion

        #region Down

        public Down CreateNewDown()
        {
            return new Down();
        }

        public void AddDown(Down down)
        {
            this._domainDataProvider.Insert(down);
        }

        public void UpdateDown(Down down)
        {
            this._domainDataProvider.Update(down);
        }

        public void DeleteDown(Down down)
        {
            this._domainDataProvider.Delete(down);
        }

        public void DeleteDown(Down[] down)
        {
            this._domainDataProvider.Delete(down);
        }

        public object GetDown(string downcode, string rcard)
        {
            return this.DataProvider.CustomSearch(typeof(Down), new object[] { downcode, rcard });
        }

        public object[] QueryDown(string downcode, string downreason)
        {
            string sql = "";
            sql += " select * from tbldown where downcode='" + downcode + "' and downreason='" + downreason + "' and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            return this.DataProvider.CustomQuery(typeof(Down), new SQLCondition(sql));
        }

        public object[] QueryDownByRcard(string rcard)
        {
            string sql = "";
            sql += " select * from tbldown where rcard='" + rcard + "' and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            return this.DataProvider.CustomQuery(typeof(Down), new SQLCondition(sql));
        }

        public object GetMaxDownCode(string sscode, string dbdate, int orgid)
        {
            string StartDownCode = sscode + dbdate + "_" + "000";
            string EndDownCode = sscode + dbdate + "_" + "999";
            string sql = "";
            sql = "select max(downcode) as downcode from tbldown where 1=1 and orgid in (" + orgid + ") ";
            sql += " and downcode between '" + StartDownCode + "' ";
            sql += " and  '" + EndDownCode + "' ";
            sql += " and length(downcode)=length('" + StartDownCode + "')";
            sql += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;

            object objectMaxDownCode = this.DataProvider.CustomQuery(typeof(Down), new SQLCondition(sql));
            if (objectMaxDownCode == null)
            {
                return this.DataProvider.CustomQuery(typeof(Down), new SQLCondition(sql));
            }
            else
            {
                return this.DataProvider.CustomQuery(typeof(Down), new SQLCondition(sql))[0];
            }
        }

        public object[] GetDownEventList(string downCode)
        {
            string sql = "";
            sql += "SELECT   downcode, downreason, COUNT (*) as downqty";
            sql += "    FROM tbldown";
            sql += "   WHERE downcode LIKE '%" + downCode + "%' AND downstatus = '" + DownStatus.DownStatus_Down + "'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += "GROUP BY downcode, downreason ";
            sql += "ORDER BY downcode";

            return this.DataProvider.CustomQuery(typeof(DownWithRCardInfo), new SQLCondition(sql));
        }

        public object GetRcardFromSimulationReport(string runningcard)
        {
            string sql = "";
            sql += " Select rcard from (";
            sql += " Select rcard from (select rcard from tblsimulationreport where rcard='" + runningcard + "' order by mdate desc,mtime desc ) where rownum=1 ";
            sql += " union ";
            sql += " Select rcard from (select rcard from tblsimulationreport where cartoncode='" + runningcard + "' order by mdate desc,mtime desc  ) where rownum=1";
            sql += ") where rownum=1";
            object objectRcard = this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(sql));
            if (objectRcard == null)
            {
                return this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(sql));
            }
            else
            {
                return this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(sql))[0];
            }
        }

        public object[] GetRcardListFromDown(string downcode)
        {
            string sql = "";
            sql += " select * from tbldown where downcode='" + downcode + "' order by rcard";
            return this.DataProvider.CustomQuery(typeof(Down), new SQLCondition(sql));
        }


        public object[] QueryDownANDItemdesc(string downcode)
        {
            string sql = "";
            sql += "select *  from tbldown a, tblitem b";
            sql += " where a.itemcode = b.itemcode";
            sql += " and a.downcode = '" + downcode + "'";
            sql += " order by a.mocode";
            return this.DataProvider.CustomQuery(typeof(DownWithRCardInfo), new SQLCondition(sql));
        }

        public object[] GetDownRCardListByDownCode(string downCode)
        {
            string sql = "";
            sql += "SELECT   downcode, rcard, mocode, itemcode, mdesc AS itemdesc";
            sql += "    FROM tbldown, tblmaterial";
            sql += "   WHERE tbldown.itemcode = tblmaterial.mcode(+)";
            sql += "     AND downcode = '" + downCode + "'";
            sql += "     AND downstatus = '" + DownStatus.DownStatus_Down + "'";
            sql += "     AND tbldown.orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            sql += "     AND tblmaterial.orgid(+)=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            sql += "ORDER BY rcard";

            return this.DataProvider.CustomQuery(typeof(DownWithRCardInfo), new SQLCondition(sql));
        }

        public object[] GetDownRCardListByCode(string rcardOrCarton)
        {
            string sql = "";
            sql += "SELECT   downcode, rcard, mocode, itemcode, mdesc AS itemdesc";
            sql += "    FROM tbldown, tblmaterial";
            sql += "   WHERE tbldown.itemcode = tblmaterial.mcode(+)";
            sql += "     AND (rcard LIKE '%" + rcardOrCarton + "%' OR rcard IN (SELECT rcard";
            sql += "                                         FROM tblsimulationreport";
            sql += "                                        WHERE cartoncode LIKE '%" + rcardOrCarton + "%'))";
            sql += "     AND downstatus = '" + DownStatus.DownStatus_Down + "'";
            sql += "     AND tbldown.orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            sql += "     AND tblmaterial.orgid(+)=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            sql += "ORDER BY rcard";

            return this.DataProvider.CustomQuery(typeof(DownWithRCardInfo), new SQLCondition(sql));
        }


        public Messages CheckISDown(string rcard)
        {
            Messages msg = new Messages();
            object[] objectDowns = this.QueryDownByRcard(FormatHelper.CleanString(rcard.Trim().ToUpper()));
            if (objectDowns != null)
            {
                for (int i = 0; i < objectDowns.Length; i++)
                {
                    if (!string.IsNullOrEmpty(((Down)objectDowns[i]).DownReason) && ((Down)objectDowns[i]).DownStatus == DownStatus.DownStatus_Down)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_Rcard_IS_Down_And_Reason:" + ((Down)objectDowns[0]).DownReason));
                        return msg;
                    }
                }

            }
            return msg;
        }

        public Messages CheckReworkRcardIsScarp(string rcard, string resCode)
        {
            Messages msg = new Messages();
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);

            Resource resource = (Resource)baseModelFacade.GetResource(resCode);
            SimulationReport simulationReport = (SimulationReport)this.GetLastSimulationReport(rcard);

            if (resource != null && simulationReport != null
                && resource.ReworkRouteCode.Trim() != string.Empty
                && simulationReport.Status == ProductStatus.Scrap)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_Rcard_IS_Scrap_Can_Not_Rework"));
            }

            return msg;
        }

        #endregion

        #region OnWIP
        /// <summary>
        /// 
        /// </summary>
        public OnWIP CreateNewOnWIP()
        {
            return new OnWIP();
        }

        public void AddOnWIP(OnWIP onWIP)
        {
            PerformanceFacade performanceFacade = new PerformanceFacade(this.DataProvider);
            if (performanceFacade.IsPerformanceCollectLine(onWIP.ResourceCode))
            {
                PerformanceCollectFacade performanceCollectFacade = new PerformanceCollectFacade(this.DataProvider);
                Messages msg = performanceCollectFacade.PerformanceCollect(onWIP);
                if (!msg.IsSuccess())
                {
                    throw new Exception(msg.OutPut());
                }
            }

            this.DataProvider.Insert(onWIP);
        }

        public void UpdateOnWIP(OnWIP onWIP)
        {
            this.DataProvider.Update(onWIP);
        }

        public void DeleteOnWIP(OnWIP onWIP)
        {
            this.DataProvider.Delete(onWIP);
        }

        public int GetRCardInfoCount(string rCard, string moCode, string ssCode, string opCode)
        {
            string sql = "SELECT COUNT(*) FROM tblonwip WHERE 1=1 ";
            if (rCard.Trim() != string.Empty)
            {
                sql += " AND rcard='" + rCard.Trim().ToUpper() + "'";
            }
            if (moCode.Trim() != string.Empty)
            {
                sql += " AND mocode='" + moCode.Trim().ToUpper() + "'";
            }
            if (ssCode.Trim() != string.Empty)
            {
                sql += " AND SSCode='" + ssCode.Trim().ToUpper() + "'";
            }
            if (opCode.Trim() != string.Empty)
            {
                sql += " AND opCode='" + opCode.Trim().ToUpper() + "'";
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //add By Jarvis For DeductQty 20120315
        public object[] QueryOnWIP(string mCard, string moCode, string opCode, string action)
        {
            string sql = "";
            sql += " SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIP)) + " ";
            sql += " FROM tblonwip ";
            sql += " WHERE 1 = 1 ";

            sql += " AND rcard = '" + mCard.Trim().ToUpper() + "' ";

            sql += " AND action = '" + action.Trim().ToUpper() + "' ";

            sql += " AND moCode ='" + moCode + "' ";

            sql += " AND opCode ='" + opCode + "' ";

            sql += " ORDER BY mdate desc, mtime desc";

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(sql));
        }

        #endregion

        #region SimulationReport
        /// <summary>
        /// 
        /// </summary>
        public SimulationReport CreateNewSimulationReport()
        {
            return new SimulationReport();
        }

        public void AddSimulationReport(SimulationReport simulationReport)
        {
            this.DataProvider.Insert(simulationReport);
        }

        public void UpdateSimulationReport(SimulationReport simulationReport)
        {
            this.DataProvider.Update(simulationReport);
        }

        // Added By Hi1/Venus.Feng on 20080718 For Hisense Version
        public void UpdateSimulationReportForLot(string rCard, string moCode, string lotNo)
        {
            string sql = "UPDATE tblsimulationReport SET lotno='" + lotNo + "' WHERE rcard='" + rCard + "' AND mocode='" + moCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        // End Added

        //Laws Lu,2006/06/01 ����Carton��
        public void UpdateSimulationReportCartonNo(string rcard, string mocode, string ctnno)
        {
            this.DataProvider.CustomExecute(
                new SQLCondition("update tblsimulationreport set cartoncode='" + ctnno + "'"
                + " where rcard='" + rcard + "' and mocode='" + mocode + "'"));
        }

        public void DeleteSimulationReport(Simulation simulation)
        {
            this.DataProvider.CustomExecute(new SQLParamCondition("delete from  TBLSIMULATIONREPORT where RCARD=$RCARD and MOCODE=$MOCODE",
                new SQLParameter[] {
									   new SQLParameter("RCARD", typeof(string), simulation.RunningCard),
									   new SQLParameter("MOCODE", typeof(string), simulation.MOCode)
								   }));
        }

        public void DeleteSimulationReport(SimulationReport simulationReport)
        {
            this.DataProvider.Delete(simulationReport);
        }

        /// <summary>
        /// ��ȡ���һ��SimulationReport
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public SimulationReport GetLastSimulationReport(string rcard)
        {
            string strSql = "SELECT * FROM tblSimulationReport WHERE RCard='" + rcard + "' ";
            strSql += "ORDER BY MDate DESC,MTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (SimulationReport)objs[0];
        }

        public SimulationReport GetLastSimulationReportByRMA(string rcard, string rmaBillCode)
        {
            string strSql = "SELECT * FROM tblSimulationReport WHERE RCard='" + rcard + "' and RMABILLCODE='" + rmaBillCode + "'";
            strSql += "ORDER BY MDate DESC,MTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (SimulationReport)objs[0];
        }

        public SimulationReport GetLastSimulationReport(string rcard, bool isComp)
        {
            string strSql = "SELECT * FROM tblSimulationReport WHERE RCard='" + rcard + "' and ISCOM='" + isComp + "'";
            strSql += "ORDER BY MDate DESC,MTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (SimulationReport)objs[0];
        }

        public SimulationReport GetLastSimulationReportByCarton(string cartonCode)
        {
            string strSql = "SELECT * FROM tblSimulationReport WHERE cartoncode='" + cartonCode + "' ";
            strSql += "ORDER BY MDate DESC,MTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (SimulationReport)objs[0];
        }

        public object[] QuerySimulationReportByCarton(string cartonno)
        {
            object[] simulationReports = this.DataProvider.CustomQuery(typeof(SimulationReport),
                new SQLCondition(string.Format(
                @"select {0} from tblSimulationReport where
					cartoncode = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport)),
                cartonno.ToUpper())));

            if (simulationReports == null)
                return null;
            if (simulationReports.Length > 0)
                return simulationReports;
            else
                return null;
        }

        public object GetSimulationReport(string moCode, string runningCard)
        {
            object[] simulationReports = this.DataProvider.CustomQuery(typeof(SimulationReport),
                new SQLParamCondition(string.Format("select {0} from tblSimulationReport where RCARD = $RCARD and MOCODE = $MOCODE order by MDATE desc,MTIME desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport))),
                new SQLParameter[] { new SQLParameter("RCARD", typeof(string), runningCard.ToUpper()), new SQLParameter("MOCODE", typeof(string), moCode.ToUpper()) }));

            if (simulationReports == null)
                return null;
            if (simulationReports.Length > 0)
                return simulationReports[0];
            else
                return null;

        }
        #endregion

        #region OnWIPCardTransfer
        /// <summary>
        /// 
        /// </summary>
        public OnWIPCardTransfer CreateNewOnWIPCardTransfer()
        {
            return new OnWIPCardTransfer();
        }

        public void AddOnWIPCardTransfer(OnWIPCardTransfer onWIPCardTransfer)
        {
            if (this.DataProvider.CustomSearch(typeof(OnWIPCardTransfer), DomainObjectUtility.GetDomainObjectKeyScheme(typeof(OnWIPCardTransfer)), DomainObjectUtility.GetDomainObjectKeyValues(onWIPCardTransfer)) != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
                return;
            }
            //Laws Lu,2006/11/13 uniform system collect date
            DBDateTime dbDateTime;

            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);


            //DateTime dtWorkDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

            onWIPCardTransfer.MaintainDate = dbDateTime.DBDate;
            onWIPCardTransfer.MaintainTime = dbDateTime.DBTime;

            this.DataProvider.Insert(onWIPCardTransfer);
        }

        public void UpdateOnWIPCardTransfer(OnWIPCardTransfer onWIPCardTransfer)
        {
            //Laws Lu,2006/11/13 uniform system collect date
            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            //DateTime dtWorkDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);


            onWIPCardTransfer.MaintainDate = dbDateTime.DBDate;
            onWIPCardTransfer.MaintainTime = dbDateTime.DBTime;

            this.DataProvider.Update(onWIPCardTransfer);
        }

        public void DeleteOnWIPCardTransfer(OnWIPCardTransfer onWIPCardTransfer)
        {
            this.DataProvider.Delete(onWIPCardTransfer);
        }

        public object GetOnWIPCardTransfer(string runningCard, string mOCode)
        {
            return this.DataProvider.CustomSearch(typeof(OnWIPCardTransfer), new object[] { runningCard, mOCode });
        }

        //add by klaus 
        public string QueryOnWIPCardTransferByRcard(string rcard)
        {
            string sql = " SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer)) + "  FROM TBLONWIPCARDTRANS A WHERE A.RCARD = '" + rcard + "'    ";
            //sql += " Order by a.serial desc ";
            object[] objs = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(sql));
            if (objs != null && objs.Length != 0)
            {
                return ((OnWIPCardTransfer)objs[0]).SourceCard;
            }
            return rcard;
        }
        public ArrayList QueryOnWIPCardTransferByScard(string scard)
        {
            string sql = " SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer)) + "  FROM TBLONWIPCARDTRANS A WHERE A.SCARD = '" + scard + "'    ";
            //sql += " Order by a.serial desc ";
            object[] objs = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(sql));
            if (objs != null && objs.Length != 0)
            {
                ArrayList arrRcard = new ArrayList();
                for (int i = 0; i < objs.Length; i++)
                {
                    string rcard = (objs[i] as OnWIPCardTransfer).RunningCard;
                    string soucceCard = (objs[i] as OnWIPCardTransfer).SourceCard;
                    if (!arrRcard.Contains(rcard))
                    {
                        arrRcard.Add(rcard);
                    }
                    if (!arrRcard.Contains(soucceCard))
                    {
                        arrRcard.Add(soucceCard);
                    }
                }
                return arrRcard;
            }
            return null;

        }
        #endregion

        #region OnWIPECN
        /// <summary>
        /// 
        /// </summary>
        public OnWIPECN CreateNewOnWIPECN()
        {
            return new OnWIPECN();
        }

        public void AddOnWIPECN(OnWIPECN onWIPECN)
        {
            if (this.DataProvider.CustomSearch(typeof(OnWIPECN), DomainObjectUtility.GetDomainObjectKeyScheme(typeof(OnWIPECN)), DomainObjectUtility.GetDomainObjectKeyValues(onWIPECN)) != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
                return;
            }
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            onWIPECN.MaintainDate = FormatHelper.TODateInt(dtNow);
            onWIPECN.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            this.DataProvider.Insert(onWIPECN);
        }

        public void UpdateOnWIPECN(OnWIPECN onWIPECN)
        {
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            onWIPECN.MaintainDate = FormatHelper.TODateInt(dtNow);
            onWIPECN.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            this.DataProvider.Update(onWIPECN);
        }

        public void DeleteOnWIPECN(OnWIPECN onWIPECN)
        {
            this.DataProvider.Delete(onWIPECN);
        }

        public object GetOnWIPECN(string runningCard, decimal runningCardSequence, string mOCode, string eCNNO)
        {
            return this.DataProvider.CustomSearch(typeof(OnWIPECN), new object[] { runningCard, runningCardSequence, mOCode, eCNNO });
        }
        #endregion

        #region OnWIPSoftVersion
        /// <summary>
        /// 
        /// </summary>
        public OnWIPSoftVersion CreateNewOnWIPSoftVersion()
        {
            return new OnWIPSoftVersion();
        }

        public void AddOnWIPSoftVersion(OnWIPSoftVersion onWIPSoftVersion)
        {
            object[] exists = this.DataProvider.CustomSearch(typeof(OnWIPSoftVersion),
                DomainObjectUtility.GetDomainObjectKeyScheme(typeof(OnWIPSoftVersion)),
                DomainObjectUtility.GetDomainObjectKeyValues(onWIPSoftVersion));
            if (exists != null)
            {
                this.DataProvider.Delete(exists[0]);
            }
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            onWIPSoftVersion.MaintainDate = FormatHelper.TODateInt(dtNow);
            onWIPSoftVersion.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            this.DataProvider.Insert(onWIPSoftVersion);
        }

        public void UpdateOnWIPSoftVersion(OnWIPSoftVersion onWIPSoftVersion)
        {
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            onWIPSoftVersion.MaintainDate = FormatHelper.TODateInt(dtNow);
            onWIPSoftVersion.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            this.DataProvider.Update(onWIPSoftVersion);
        }

        public void DeleteOnWIPSoftVersion(OnWIPSoftVersion onWIPSoftVersion)
        {
            this.DataProvider.Delete(onWIPSoftVersion);
        }

        public object GetOnWIPSoftVersion(string runningCard, decimal runningCardSequence, string mOCode, string softwareVersion)
        {
            return this.DataProvider.CustomSearch(typeof(OnWIPSoftVersion), new object[] { runningCard, runningCardSequence, mOCode, softwareVersion });
        }

        // Added By Hi1/Roger.xue on 20081106 for Hisense Version : add software upgrade for semi
        public OnWipSoftVer4Upgrade GetOldSoftVersion(string rcard)
        {
            string sql = "";
            sql += "SELECT *";
            sql += "  FROM (SELECT DISTINCT t1.rcard AS rcard, t1.itemcode AS itemcode,";
            sql += "                        t1.softver AS softver, t1.mocode AS mocode,";
            sql += "                        t1.modelcode AS modelcode, t1.softname AS softname,";
            sql += "                        t1.routecode AS routecode,";
            sql += "                        t1.eattribute1 AS eattribute1, t1.moseq AS moseq,";
            sql += "                        t1.mdate AS mdate, t1.mtime AS mtime,";
            sql += "                        t2.mdesc AS mdesc";
            sql += "                   FROM tblonwipsoftver t1, tblmaterial t2";
            sql += "                  WHERE t1.rcard = '" + rcard + "'";
            sql += "                    AND t2.mcode = t1.itemcode";
            sql += "               ORDER BY t1.mdate DESC, t1.mtime DESC)";
            sql += " WHERE ROWNUM = 1";

            object[] list = this.DataProvider.CustomQuery(typeof(OnWipSoftVer4Upgrade), new SQLCondition(sql));
            if (list == null || list.Length == 0)
            {
                return null;
            }
            else
            {
                return list[0] as OnWipSoftVer4Upgrade;
            }
        }
        //end add
        // Added By Hi1/Venus.Feng on 20081106 for Hisense Version : add software upgrade

        public void AddNewOnWIPSoftVer(OnWIPSoftVersion softVer)
        {
            this.DataProvider.Insert(softVer);
        }

        public decimal GetMinRcardSequenceFromOnWipSoftVer(string rCard, string moCode)
        {
            string sql = "";
            sql += "SELECT NVL (MIN (rcardseq), 0) - 1 AS rcardseq";
            sql += "  FROM tblonwipsoftver";
            sql += " WHERE rcard = '" + rCard + "' AND mocode = '" + moCode + "'";

            object[] list = this.DataProvider.CustomQuery(typeof(OnWIPSoftVersion), new SQLCondition(sql));
            return (list[0] as OnWIPSoftVersion).RunningCardSequence;
        }

        public decimal GetMinRCardSequenceFromOnWipItem(string rCardList)
        {
            string sql = "";
            sql += "SELECT NVL (MIN (rcardseq), 0) - 1 AS rcardseq";
            sql += " FROM tblonwipitem";
            sql += " WHERE rcard IN " + rCardList + " ";

            object[] list = this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(sql));
            return (list[0] as OnWIPItem).RunningCardSequence;
        }

        public decimal GetMaxRCardSequenceFromTS(string runningCard)
        {
            string sql = "";
            sql += "SELECT NVL (MAX (rcardseq), 0) AS rcardseq ";
            sql += "FROM tblts ";
            sql += "WHERE rcard = '" + runningCard + "' ";

            object[] list = this.DataProvider.CustomQuery(typeof(Domain.TS.TS), new SQLCondition(sql));
            if (list == null)
            {
                return 0;
            }
            else
            {
                return (list[0] as Domain.TS.TS).RunningCardSequence;
            }
        }

        public object[] GetOnWipSoftVersionList(string rCard, string moCode, string itemCode)
        {
            string sql = "";
            sql += "SELECT DISTINCT itemcode AS ITEMCODE, rcard AS RCARD";
            sql += "           FROM tblonwipsoftver";
            sql += "          WHERE (itemcode, rcard) IN (";
            sql += "                   SELECT DISTINCT mitemcode AS itemcode, mcard as rcard";
            sql += "                              FROM tblonwipitem";
            sql += "                             WHERE rcard = '" + rCard + "')";
            //sql += "                               AND mocode = '" + moCode + "'";
            //sql += "                               AND itemcode = '" + itemCode + "')";

            return this.DataProvider.CustomQuery(typeof(OnWIPSoftVersion), new SQLCondition(sql));
        }

        public OnWipSoftVer4Upgrade GetOnWipSoftVersion(string rCard, string itemcode)
        {
            string sql = "";
            sql += "SELECT *";
            sql += "  FROM (SELECT   a.itemcode AS itemcode, b.mdesc AS mdesc, a.rcard AS rcard,";
            sql += "                 a.softver AS softver";
            sql += "            FROM tblonwipsoftver a, tblmaterial b";
            sql += "           WHERE a.itemcode = b.mcode AND a.rcard = '" + rCard + "' AND a.itemcode = '" + itemcode + "'";
            sql += "        ORDER BY a.mdate DESC, a.mtime DESC)";
            sql += " WHERE ROWNUM = 1";

            object[] verList = this.DataProvider.CustomQuery(typeof(OnWipSoftVer4Upgrade), new SQLCondition(sql));
            if (verList != null && verList.Length > 0)
            {
                return verList[0] as OnWipSoftVer4Upgrade;
            }
            else
            {
                return null;
            }
        }

        public OnWipSoftVer4Upgrade GetOnWipSoftVersion2Upgrade(string rCard, string mItemCode)
        {
            string sql = "";
            sql += "SELECT   {0}";
            sql += "    FROM tblonwipsoftver";
            sql += "   WHERE (rcard, itemcode) IN (SELECT DISTINCT mcard AS rcard, mitemcode AS itemcode";
            sql += "                                          FROM tblonwipitem";
            sql += "                                         WHERE rcard = '" + rCard + "' AND mitemcode = '" + mItemCode + "') ";
            sql += "ORDER BY mdate DESC, mtime DESC";

            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPSoftVersion)));

            sql = "SELECT * FROM (" + sql + ") WHERE ROWNUM=1";
            sql = "SELECT a.*,b.mdesc FROM (" + sql + ") a, tblmaterial b WHERE a.itemcode=b.mcode(+)";

            object[] list = this.DataProvider.CustomQuery(typeof(OnWipSoftVer4Upgrade), new SQLCondition(sql));
            if (list != null && list.Length > 0)
            {
                return list[0] as OnWipSoftVer4Upgrade;
            }
            else
            {
                return null;
            }
        }

        public Messages UpgradeNewSoftVer(OnWIPSoftVersion oldVer, Resource res, string userCode, string newVersionCode)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "UpgradeNewSoftVer");
            dataCollectDebug.WhenFunctionIn(messages);

            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            OnWIPSoftVersion newVersion = new OnWIPSoftVersion();
            try
            {
                BaseModelFacade bmf = new BaseModelFacade(this.DataProvider);
                Operation2Resource op2res = bmf.GetOperationByResource(res.ResourceCode);
                if (op2res == null)
                {
                    throw new Exception("$Error_Res_not_belong_To_Op");
                }

                ShiftModelFacade shiftModel = new ShiftModelFacade(this.DataProvider);
                TimePeriod period = (TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode, currentDateTime.DBTime);
                if (period == null)
                {
                    throw new Exception("$OutOfPerid");
                }

                newVersion.EAttribute1 = oldVer.EAttribute1;
                newVersion.ItemCode = oldVer.ItemCode;
                newVersion.MaintainDate = currentDateTime.DBDate;
                newVersion.MaintainTime = currentDateTime.DBTime;
                newVersion.MaintainUser = userCode;
                newVersion.MOCode = oldVer.MOCode;
                newVersion.ModelCode = oldVer.ModelCode;
                newVersion.MOSeq = oldVer.MOSeq;
                newVersion.OPCode = op2res.OPCode;
                newVersion.ResourceCode = res.ResourceCode;
                newVersion.RouteCode = oldVer.RouteCode;
                newVersion.RunningCard = oldVer.RunningCard;
                newVersion.RunningCardSequence = this.GetMinRcardSequenceFromOnWipSoftVer(oldVer.RunningCard, oldVer.MOCode);
                newVersion.SegmnetCode = res.SegmentCode;
                newVersion.ShiftCode = period.ShiftCode;
                newVersion.ShiftTypeCode = res.ShiftTypeCode;
                newVersion.SoftwareName = oldVer.SoftwareName;
                newVersion.SoftwareVersion = newVersionCode;
                newVersion.StepSequenceCode = res.StepSequenceCode;
                newVersion.TimePeriodCode = period.TimePeriodCode;

                this.AddNewOnWIPSoftVer(newVersion);
            }
            catch (Exception ex)
            {
                messages.Add(new Message(ex));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }
        // End Added

        #endregion

        #region OnWIPTRY
        /// <summary>
        /// 
        /// </summary>
        public OnWIPTRY CreateNewOnWIPTRY()
        {
            return new OnWIPTRY();
        }

        public void AddOnWIPTRY(OnWIPTRY onWIPTRY)
        {
            if (this.DataProvider.CustomSearch(typeof(OnWIPTRY), DomainObjectUtility.GetDomainObjectKeyScheme(typeof(OnWIPTRY)), DomainObjectUtility.GetDomainObjectKeyValues(onWIPTRY)) != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
                return;
            }

            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            onWIPTRY.MaintainDate = FormatHelper.TODateInt(dtNow);
            onWIPTRY.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            this.DataProvider.Insert(onWIPTRY);
        }

        public void UpdateOnWIPTRY(OnWIPTRY onWIPTRY)
        {
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            onWIPTRY.MaintainDate = FormatHelper.TODateInt(dtNow);
            onWIPTRY.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            this.DataProvider.Update(onWIPTRY);
        }

        public void DeleteOnWIPTRY(OnWIPTRY onWIPTRY)
        {
            this.DataProvider.Delete(onWIPTRY);
        }

        public object GetOnWIPTRY(string runningCard, decimal runningCardSequence, string mOCode, string tRYNO)
        {
            return this.DataProvider.CustomSearch(typeof(OnWIPTRY), new object[] { runningCard, runningCardSequence, mOCode, tRYNO });
        }
        #endregion

        #region OnWIPItem
        /// <summary>
        /// 
        /// </summary>
        public OnWIPItem CreateNewOnWIPItem()
        {
            return new OnWIPItem();
        }

        public void AddOnWIPItem(OnWIPItem onWIPItem)
        {
            this.DataProvider.Insert(onWIPItem);
        }

        public void UpdateOnWIPItem(OnWIPItem onWIPItem)
        {
            this.DataProvider.Update(onWIPItem);
        }

        public void DeleteOnWIPItem(OnWIPItem onWIPItem)
        {
            this.DataProvider.Delete(onWIPItem);
        }

        public object GetOnWIPItem(string runningCard, decimal runningCardSequence, string mOCode, decimal mSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OnWIPItem), new object[] { runningCard, runningCardSequence, mOCode, mSequence });
        }

        public object[] GetLastOnWIPItem(string runningCard, string moCode)
        {
            string selectSQL = " select {0} from (select * from tblonwipitem where rcard in ({1}) and mocode = '{2}' order by mdate desc,mseq desc) where rownum = 1";

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem)), runningCard, moCode })));
        }

        public object[] ExtraQuery(string runningCard, decimal runningCardSequence, string moCode)
        {

            string selectSQL = " select distinct mitemcode from tblonwipitem where mcardtype='" + MCardType.MCardType_Keyparts + "' and rcard = '{0}' and rcardseq = {1} and mocode = '{2}'"
                + " union (select mitemcode from tblonwipitem where mcardtype=" + MCardType.MCardType_INNO + " and rcard = '{0}' and rcardseq = {1} and mocode = '{2}')";
            return this.DataProvider.CustomQuery(typeof(ONWIPItemQueryObject), new SQLCondition(String.Format(selectSQL, new object[] { runningCard, runningCardSequence, moCode })));
        }

        public object[] ExtraQuery(string runningCard)
        {
            ArrayList arRcard = new ArrayList();
            BenQGuru.eMES.Material.CastDownHelper castHelper = new BenQGuru.eMES.Material.CastDownHelper(DataProvider);

            castHelper.GetAllRCard(ref arRcard, runningCard);
            arRcard.Add(runningCard);
            string runningCards = "('" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "')";

            string selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem))
                + " from tblonwipitem where rcard in {0} and actiontype="
                + (int)MaterialType.CollectMaterial;

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(String.Format(selectSQL, new object[] { runningCards })));
        }

        public object[] ExtraQuery(string runningCard, string opCODE, string moCode, string actionType, ProductInfo product)
        {
            string selectSQL = String.Empty;
            //Laws Lu,2005/12/17 ��ѯ���ϼ�¼�����ϼ�¼
            if (actionType == ActionType.DataCollectAction_DropMaterial)
            {
                string runningCards = runningCard;
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    ArrayList arRcard = new ArrayList();
                    BenQGuru.eMES.Material.CastDownHelper castHelper = new BenQGuru.eMES.Material.CastDownHelper(DataProvider);

                    castHelper.GetAllRCard(ref arRcard, runningCard);
                    arRcard.Add(runningCard);

                    runningCards = "('" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "')";

                    selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ONWIPItemObject))
                        + " from tblonwipitem where rcard in {0} and dropOP in ('{1}') and actiontype="
                        + (int)MaterialType.DropMaterial;

                }
                else
                {
                    selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ONWIPItemObject))
                        + " from tblonwipitem where rcard = '{0}' and dropOP in ('{1}') and actiontype="
                        + (int)MaterialType.DropMaterial;


                }
                return this.DataProvider.CustomQuery(typeof(ONWIPItemObject)
                    , new SQLCondition(String.Format(selectSQL, new object[] { runningCards, String.Join("','", new string[] { opCODE, "TS" }) })));

            }
            else
            {
                selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ONWIPItemObject))
                    + " from tblonwipitem where  rcard = '{0}' and opcode = '{1}' and mocode = '{2}' and actiontype="
                    + (int)MaterialType.CollectMaterial;

                return this.DataProvider.CustomQuery(typeof(ONWIPItemObject)
                    , new SQLCondition(String.Format(selectSQL, new object[] { runningCard, opCODE, moCode })));
            }


        }

        public object[] QueryOnWIPItem(string mCard)
        {
            string selectSQL = " select {0} from tblonwipitem where 1=1 and mcard = '{1}'";

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem)), mCard })));
        }

        public object[] QueryOnWIPItem(string mCard, string MItemCode)
        {
            string selectSQL = " select {0} from tblonwipitem where 1=1 and mcard = '{1}' and mitemcode='{2}'";

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem)), mCard, MItemCode })));
        }

        public object[] QueryOnWIPItemWithmoCode(string mCard, string MItemCode, string moCode)
        {
            string selectSQL = " select {0} from tblonwipitem where 1=1 and mcard = '{1}' and mitemcode='{2}' and mocode='{3}'";

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem)), mCard, MItemCode, moCode })));
        }

        public object[] QueryOnWIPItem(string mCard, string rCardList, string actionType)
        {
            string sql = "";
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem)) + " ";
            sql += "FROM tblonwipitem ";
            sql += "WHERE 1 = 1 ";
            if (mCard.Trim().Length > 0)
            {
                sql += "AND mcard = '" + mCard.Trim().ToUpper() + "' ";
            }
            if (actionType.Trim().Length > 0)
            {
                sql += "AND actiontype = '" + actionType.Trim().ToUpper() + "' ";
            }
            if (rCardList.Trim().Length > 0)
            {
                sql += "AND rcard IN " + rCardList.Trim().ToUpper() + " ";
            }
            sql += "ORDER BY mdate desc, mtime desc";

            return this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(sql));
        }

        #endregion

        #region FT��Ʒ�ɼ�
        public string CheckRoute(string ID, string Resrouce, string UserCode, int Language)
        {
            ID = ID.Trim().ToUpper();
            Resrouce = Resrouce.Trim().ToUpper();
            UserCode = UserCode.Trim().ToUpper();

            if (ID == string.Empty)
                return MutiLanguages.ParserMessage("$CS_IDisNull");
            if (Resrouce == string.Empty)
                return MutiLanguages.ParserMessage("$CS_ResrouceisNull");
            if (UserCode == string.Empty)
                return MutiLanguages.ParserMessage("$CS_UserCodeisNull");
            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            Messages messages1 = onLine.GetIDInfo(ID);
            if (messages1.IsSuccess())
            {
                ((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                try
                {
                    ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                    messages1.AddMessages(onLine.CheckID(new ActionEventArgs(ActionType.DataCollectAction_GOOD, ID,
                        UserCode, Resrouce,
                        product
                        )));
                    if (!messages1.IsSuccess())
                        return GetErrorMessage(messages1);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    ((Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }
                //return "";
            }
            else
            {
                return GetErrorMessage(messages1);
            }
        }

        public string FTActionCollectGood(string rCard, string userCode, string resCode)
        {
            UserControl.Messages messages = new UserControl.Messages();

            this.DataProvider.BeginTransaction();

            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            Messages messages1 = onLine.GetIDInfo(rCard);
            if (messages1.IsSuccess())
            {
                try
                {
                    IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GOOD);
                    ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                    messages1.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                        new ActionEventArgs(
                        ActionType.DataCollectAction_GOOD,
                        rCard,
                        userCode,
                        resCode,
                        product)));
                    if (messages1.IsSuccess())
                    {
                        this.DataProvider.CommitTransaction();
                        return "OK";
                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();
                        return GetErrorMessage(messages1);
                    }

                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    return ex.Message;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }
            }
            else
            {
                return GetErrorMessage(messages1);
            }

        }

        public string ActionCollectGood(string rCard, string userCode, string resCode)
        {
            UserControl.Messages messages = new UserControl.Messages();

            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            Messages messages1 = onLine.GetIDInfo(rCard);
            if (messages1.IsSuccess())
            {
                try
                {
                    IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GOOD);
                    ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                    messages1.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                        new ActionEventArgs(
                        ActionType.DataCollectAction_GOOD,
                        rCard,
                        userCode,
                        resCode,
                        product)));
                    if (messages1.IsSuccess())
                    {
                        return "OK";
                    }
                    else
                    {
                        return GetErrorMessage(messages1);
                    }

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return GetErrorMessage(messages1);
            }

        }
        #endregion

        #region FT����Ʒ�ɼ�

        private void ParseErrorInfo(string userCode, string errorCode, out ArrayList errList, out ArrayList errLocList, string factory)
        {
            errList = new ArrayList();
            errLocList = new ArrayList();
            //Laws Lu,2006/06/26 Get error code from Power FT 
            if (factory == POWER)
            {
                string[] errorParmInfo = errorCode.Split(',');
                //ArrayList errList = new ArrayList();
                if (errorCode != string.Empty)
                {
                    foreach (string epinfo in errorParmInfo)
                    {
                        ErrorCodeGroup2ErrorCode errinfo = new ErrorCodeGroup2ErrorCode();
                        errinfo.ErrorCodeGroup = epinfo.Split(':')[0];
                        errinfo.ErrorCode = epinfo.Split(':')[1];
                        errinfo.MaintainUser = userCode.ToUpper();

                        errList.Add(errinfo);
                    }
                }
            }
            //Laws Lu,2006/06/26 Get error code from Power FT 
            if (factory == PID)
            {
                string[] errorParmInfo = errorCode.Split('*');
                //ArrayList errList = new ArrayList();
                if (errorCode != string.Empty)
                {
                    foreach (string epinfo in errorParmInfo)//top loop
                    {
                        string[] errorDetail = epinfo.Split(':');
                        for (int i = 0; i < errorDetail.Length; i++)//detail loop
                        {
                            //							if( i == 0)
                            //							{
                            //								//set error group
                            //								errinfo.ErrorCodeGroup = errorDetail[0].ToUpper().Trim();
                            //							}
                            //							else
                            //							{
                            if (i > 0)
                            {
                                ErrorCodeGroup2ErrorCode errinfo = new ErrorCodeGroup2ErrorCode();
                                errinfo.ErrorCodeGroup = errorDetail[0].ToUpper().Trim();

                                string[] errorLoc = errorDetail[i].Split('^');
                                for (int j = 0; j < errorLoc.Length; j++)//error code 2 location loop
                                {
                                    #region set error code 2 error location

                                    //									if(j == 0)
                                    //									{
                                    //										//set error code
                                    //										
                                    //									}
                                    if (j > 0)
                                    {
                                        errinfo.ErrorCode = errorLoc[0].ToUpper().Trim();

                                        string[] errorLocDetails = errorLoc[j].Split('#');
                                        for (int k = 0; k < errorLocDetails.Length; k++)//error location loop
                                        {
                                            TSErrorCode2Location errLoc = new TSErrorCode2Location();
                                            //set error location
                                            errLoc.ErrorCodeGroup = errinfo.ErrorCodeGroup;
                                            errLoc.ErrorCode = errinfo.ErrorCode;
                                            errLoc.ErrorLocation = errorLocDetails[k].ToUpper().Trim();
                                            errLoc.AB = "A";

                                            errLocList.Add(errLoc);
                                        }

                                    }

                                    #endregion
                                }
                                errList.Add(errinfo);
                            }

                        }
                    }
                }
            }
        }

        public string FTActionCollectNG(string rCard, string userCode, string resCode, string errorCode, string factory)
        {
            UserControl.Messages messages = new UserControl.Messages();

            #region  ���첻����Ϣ

            ArrayList errList = new ArrayList();
            ArrayList errLocList = new ArrayList();

            ParseErrorInfo(userCode, errorCode, out errList, out errLocList, factory);

            object[] errorInfor = (ErrorCodeGroup2ErrorCode[])errList.ToArray(typeof(ErrorCodeGroup2ErrorCode));
            object[] errorLoc = (TSErrorCode2Location[])errLocList.ToArray(typeof(TSErrorCode2Location));
            #endregion

            this.DataProvider.BeginTransaction();

            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            Messages messages1 = onLine.GetIDInfo(rCard);
            if (messages1.IsSuccess())
            {
                try
                {
                    if (factory == POWER)
                    {
                        IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_NG);
                        ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                        messages1.AddMessages(dataCollectModule.Execute(
                            new TSActionEventArgs(ActionType.DataCollectAction_NG,
                            rCard,
                            userCode,
                            resCode,
                            product,
                            errorInfor,
                            errorLoc,
                            null)));
                    }
                    if (factory == PID)
                    {
                        IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_SMTNG);
                        ProductInfo product = (ProductInfo)messages1.GetData().Values[0];
                        messages1.AddMessages(dataCollectModule.Execute(
                            new TSActionEventArgs(ActionType.DataCollectAction_SMTNG,
                            rCard,
                            userCode,
                            resCode,
                            product,
                            //							errorInfor,
                            errorLoc,
                            null)));
                    }

                    if (messages1.IsSuccess())
                    {
                        //Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().CommitTransaction();
                        this.DataProvider.CommitTransaction();
                        messages1.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_NGSUCCESS")));
                        return "OK";
                    }
                    else
                    {
                        //Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().RollbackTransaction();
                        this.DataProvider.RollbackTransaction();
                        return GetErrorMessage(messages1);
                    }


                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    return ex.Message;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }
            }
            else
            {
                return GetErrorMessage(messages1);
            }

        }
        #endregion

        #region ���䡢��ջ��

        public Messages RemoveFromCarton(string rcard, string userCode)
        {
            //�κ�����δʹ��Trans

            Messages returnValue = new Messages();

            BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(DataProvider);
            Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

            SimulationReport simulationReport = (SimulationReport)GetLastSimulationReport(rcard.Trim().ToUpper());
            if (simulationReport == null)
            {
                return returnValue;
            }
            else if (simulationReport.CartonCode.Trim().Length <= 0)
            {
                return returnValue;
            }
            else
            {
                string oldCartonCode = simulationReport.CartonCode;
                string newCartonCode = String.Empty;

                simulationReport.CartonCode = newCartonCode;
                Simulation simulation = (Simulation)GetSimulation(rcard.Trim().ToUpper());
                if (simulation != null)
                {
                    simulation.CartonCode = newCartonCode;
                    UpdateSimulation(simulation);
                }

                UpdateSimulationReport(simulationReport);
                //ɾ��Carton2RCARD
                Carton2RCARD oldCarton2RCARD = (Carton2RCARD)packageFacade.GetCarton2RCARD(oldCartonCode.Trim().ToUpper(), rcard.Trim().ToUpper());
                if (oldCarton2RCARD != null)
                {
                    packageFacade.DeleteCarton2RCARD(oldCarton2RCARD);
                }
                //end

                packageFacade.SubtractCollected(oldCartonCode);

                CARTONINFO oldCarton = packageFacade.GetCARTONINFO(oldCartonCode) as CARTONINFO;
                if (oldCarton.COLLECTED == 0)
                {
                    packageFacade.DeleteCARTONINFO(oldCarton);
                    
                    #region ɾ��Lot�����йصĹ�ϵ
                    //Added Lisa@20120829
                    //ɾ��С����OQC���Ĺ�ϵ
                    OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                    Lot2Carton lot2Carton = oqcFacade.GetLot2CartonByCartonNo(oldCartonCode) as Lot2Carton;
                    if (lot2Carton != null)
                    {
                        oqcFacade.DeleteLot2Carton(lot2Carton);
                        oqcFacade.UpdateLot2CartonLogWhenRemove(lot2Carton.OQCLot, lot2Carton.CartonNo, userCode);
                    }
                    #endregion
                    
                }

                returnValue.Add(new UserControl.Message(MessageType.Success, "$CS_Delete_Success"));

                //��log
                packageFacade.SaveRemoveCarton2RCARDLog(oldCartonCode, rcard, userCode);
            }

            return returnValue;
        }

        public Messages RemoveFromPallet(string rcard, string userCode, bool checkIsInStack)
        {
            //�κ�����δʹ��Trans

            Messages returnValue = new Messages();

            Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

            Pallet2RCard pallet2RCard = (Pallet2RCard)packageFacade.GetPallet2RCardByRCard(rcard.Trim().ToUpper());
            if (pallet2RCard == null)
            {
                return returnValue;
                //returnValue.Add(new Message(MessageType.Error, "$CS_Pallet2RCardNotFound"));
            }
            else
            {
                Pallet pallet = (Pallet)packageFacade.GetPallet(pallet2RCard.PalletCode);
                if (pallet == null)
                {
                    return returnValue;
                    //returnValue.Add(new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IS_NOT_EXIT"));
                }
                else
                {
                    if (checkIsInStack)
                    {
                        SimulationReport simulationReport = (SimulationReport)this.GetLastSimulationReport(rcard);
                        string cartonCode = string.Empty;
                        if (simulationReport != null)
                        {
                            cartonCode = simulationReport.CartonCode;
                        }

                        InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);

                        object[] StacktoRcardByRcardList = inventoryFacade.QueryStacktoRcardByRcardAndCarton(rcard, cartonCode);

                        if (StacktoRcardByRcardList != null)
                        {
                            returnValue.Add(new UserControl.Message(MessageType.Error, rcard + " $CS_SERIAL_EXIST_Storge_Not_Remove"));
                            return returnValue;
                        }
                    }

                    packageFacade.DeletePallet2RCard(pallet2RCard);

                    pallet.RCardCount--;

                    if (pallet.RCardCount == 0)
                    {
                        packageFacade.DeletePallet(pallet);
                    }
                    else
                    {
                        packageFacade.UpdatePallet(pallet);
                    }

                    returnValue.Add(new UserControl.Message(MessageType.Success, "$CS_Delete_Success"));

                    //��log
                    packageFacade.SaveRemovePallet2RcardLog(pallet2RCard.PalletCode, rcard, userCode);


                }
            }

            return returnValue;
        }



        public void TryToDeleteRCardFromLot(string rCard)
        {
            this.TryToDeleteRCardFromLot(rCard, true);
        }

        public void TryToDeleteRCardFromLot(string rCard, bool needCheckLotStatusAndType)
        {
            rCard = rCard.Trim().ToUpper();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            TryFacade tryFacade = new TryFacade(this.DataProvider);
            ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);

            object[] lot2CardList = oqcFacade.ExactQueryOQCLot2Card(rCard);
            if (lot2CardList != null)
            {
                foreach (OQCLot2Card lot2Card in lot2CardList)
                {
                    bool needToDelete = true;

                    //�ж�����һ���ɼ�����ʱ����FQC�ɼ�����������
                    //              ����ò�Ʒ�Ѿ����������У�
                    //              ����������tbllot.oqclottype=oqclottype_normal
                    //              ����δ�ж�Reject��Pass
                    OQCLot lot = (OQCLot)oqcFacade.GetOQCLot(lot2Card.LOTNO, lot2Card.LotSequence);
                    if (lot == null)
                    {
                        needToDelete = false;
                    }
                    else
                    {
                        if (needCheckLotStatusAndType)
                        {
                            if (lot.OQCLotType != OQCLotType.OQCLotType_Normal)
                            {
                                needToDelete = false;
                            }
                            else if (lot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || lot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
                            {
                                needToDelete = false;
                            }
                        }
                    }

                    //�ж�����������Ʒ�����������ļ�������
                    if (needToDelete)
                    {
                        object[] lot2CardCheckList = oqcFacade.ExtraQueryOQCLot2CardCheck(rCard, string.Empty, string.Empty, lot2Card.LOTNO, lot2Card.LotSequence.ToString());

                        if (lot2CardCheckList != null && lot2CardCheckList.Length > 0)
                        {
                            needToDelete = false;
                        }
                    }

                    if (needToDelete)
                    {
                        //ɾ������һ��
                        //              ����Ʒ��tbllot2card��ɾ����
                        //              ��tbllot��lotsize��һ��
                        //              ���lotsizeΪ����ɾ��tbllot��tbltry2lot������
                        oqcFacade.DeleteOQCLot2Card(lot2Card);

                        lot.LotSize--;
                        oqcFacade.UpdateOQCLot(lot);

                        if (lot.LotSize <= 0)
                        {
                            OQCLOTCheckList lotCheckList = (OQCLOTCheckList)oqcFacade.GetOQCLOTCheckList(lot.LOTNO, lot.LotSequence);
                            if (lotCheckList != null)
                            {
                                oqcFacade.DeleteOQCLOTCheckList(lotCheckList);
                            }

                            oqcFacade.DeleteOQCLot(lot);

                            object[] try2LotList = tryFacade.GetTry2LotList(lot.LOTNO);
                            if (try2LotList != null)
                            {
                                foreach (Try2Lot try2Lot in try2LotList)
                                {
                                    tryFacade.DeleteTry2Lot(try2Lot);
                                }
                            }
                        }

                        //ɾ����������
                        //              ����lotno+rcardɾ��tblfrozen�е�ֵ
                        object[] frozenList = oqcFacade.QueryFrozen(rCard, rCard, lot.LOTNO, string.Empty, string.Empty, string.Empty, -1, -1, -1, -1, int.MinValue, int.MaxValue);
                        if (frozenList != null)
                        {
                            foreach (Frozen frozen in frozenList)
                            {
                                oqcFacade.DeleteFrozen(frozen);
                            }
                        }

                        //ɾ����������
                        //              ɾ��tbltempreworkrcard�е�ֵ
                        //              �����lot���鷵�������к��Ѿ�ȫ��ɾ������ɾ��tbltempreworklotno��ֵ�� 
                        ReworkRcard reworkRcard = (ReworkRcard)reworkFacade.GetReworkRcard(lot.LOTNO, rCard);
                        if (reworkRcard != null)
                        {
                            reworkFacade.DeleteReworkRcard(reworkRcard);

                            object[] reworkRcardList = reworkFacade.QueryReworkRcard(lot.LOTNO);
                            if (reworkRcardList == null || reworkRcardList.Length <= 0)
                            {
                                ReworkLotNo reworkLotNo = (ReworkLotNo)reworkFacade.GetReworkLotNo(lot.LOTNO);
                                if (reworkLotNo != null)
                                {
                                    reworkFacade.DeleteReworkLotNo(reworkLotNo);
                                }
                            }
                        }

                        //ɾ�������ģ�
                        //              ����ֶ�tblsimulation.lotno��tblsimulationreport.lotno
                        object[] simulationList = QuerySimulation(rCard);
                        if (simulationList != null)
                        {
                            foreach (Simulation simulation in simulationList)
                            {
                                simulation.LOTNO = string.Empty;
                                UpdateSimulation(simulation);
                            }
                        }

                        object[] simulationReportList = QuerySimulationReport(rCard);
                        if (simulationReportList != null)
                        {
                            foreach (SimulationReport simulationReport in simulationReportList)
                            {
                                simulationReport.LOTNO = string.Empty;
                                UpdateSimulationReport(simulationReport);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ������ʽ����������ط���

        public Messages ParseFromBarcode(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength)
        {
            return ParseFromBarcode(ref newMINNO, barcode, opBOMDetailForItemCode, opBOMDetailSNLength, true);
        }
        public Messages ParseFromBarcode(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength, bool dealSecondSource)
        {
            Messages returnValue = new Messages();

            string splitter = "-";

            string itemCode = string.Empty;
            string vendorCode = string.Empty;
            string lotNo = string.Empty;
            string productDate = string.Empty;

            string pattern = @"^.+-.+-.+-.+[Rr]?$";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = reg.Match(barcode);
            if (!match.Success)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_barcode: $CS_Error_Format" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            lotNo = barcode;

            int pos = 0;

            //Vendor Code
            pos = barcode.IndexOf(splitter);
            vendorCode = barcode.Substring(0, pos);
            barcode = barcode.Substring(pos + 1);

            //Item code
            pos = barcode.IndexOf(splitter);
            itemCode = barcode.Substring(0, pos);
            barcode = barcode.Substring(pos + 1);

            //Product date
            pos = barcode.IndexOf(splitter);
            productDate = barcode.Substring(0, pos);
            barcode = barcode.Substring(pos + 1);

            //Return
            if (itemCode == string.Empty
                || vendorCode == string.Empty
                || lotNo == string.Empty
                || productDate == string.Empty)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_barcode: $CS_Error_LackOfInfo" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            //�ȶ��Ϻź����кų���
            returnValue.AddMessages(this.CheckAfterParse(itemCode, opBOMDetailForItemCode, lotNo, opBOMDetailSNLength, "$parse_barcode:", dealSecondSource));

            if (!returnValue.IsSuccess())
            {
                return returnValue;
            }

            if (returnValue.IsSuccess())
            {
                newMINNO.MItemCode = itemCode;
                newMINNO.VendorCode = vendorCode;
                newMINNO.LotNO = lotNo;
                newMINNO.DateCode = productDate;

                returnValue.ClearMessages();
                returnValue.Add(new UserControl.Message(MessageType.Success, "$parse_barcode$CS_Error_ParseSuccess"));
            }

            return returnValue;
        }

        public Messages ParseFromPrepare(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength)
        {
            return ParseFromPrepare(ref newMINNO, barcode, opBOMDetailForItemCode, opBOMDetailSNLength, true);
        }

        public Messages ParseFromPrepare(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength, bool dealSecondSource)
        {
            Messages returnValue = new Messages();

            string sql = "select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MKeyPart)) + " ";
            sql += "from tblmkeypart, tblmkeypartdetail ";
            sql += "where tblmkeypart.mitemcode = tblmkeypartdetail.mitemcode ";
            sql += "and tblmkeypart.seq = tblmkeypartdetail.seq ";
            sql += "and serialno = '" + barcode.Trim().ToUpper() + "' ";

            //֧�������ϴ�tblmkeypart��ȡ��Ϣ
            if (newMINNO.EAttribute1 == BOMItemControlType.ITEM_CONTROL_LOT)
            {
                sql = "select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MKeyPart)) + " ";
                sql += "from tblmkeypart where LOTNO = '" + barcode.Trim().ToUpper() + "' ";
            }
            //End

            object[] mKeyParts = this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLCondition(sql));

            if (mKeyParts == null)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_prepare: $CS_Error_NoPrepareInfo" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            for (int i = 0; i < mKeyParts.Length; i++)
            {
                returnValue = new Messages();
                string prepareMOCode = ((MKeyPart)mKeyParts[i]).MoCode == null ? "" : ((MKeyPart)mKeyParts[i]).MoCode.Trim().ToUpper();
                string keyPartMOCode = newMINNO.MOCode == null ? "" : newMINNO.MOCode.Trim().ToUpper();

                if (prepareMOCode.Length > 0 && prepareMOCode != keyPartMOCode)
                {
                    if (i == mKeyParts.Length - 1)
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_prepare: $CS_Error_MOLimited " + ((MKeyPart)mKeyParts[i]).MoCode.Trim().ToUpper() + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                        return returnValue;
                    }
                    else
                    {
                        continue;
                    }
                }

                //�ȶ��Ϻź����кų���
                returnValue.AddMessages(this.CheckAfterParse(((MKeyPart)mKeyParts[i]).MItemCode, opBOMDetailForItemCode, barcode, opBOMDetailSNLength, "$parse_prepare:", dealSecondSource));
                if (!returnValue.IsSuccess())
                {
                    if (i == mKeyParts.Length - 1)
                    {
                        return returnValue;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (returnValue.IsSuccess())
                {
                    newMINNO.MItemCode = ((MKeyPart)mKeyParts[i]).MItemCode;
                    newMINNO.VendorCode = ((MKeyPart)mKeyParts[i]).VendorCode;
                    newMINNO.LotNO = ((MKeyPart)mKeyParts[i]).LotNO;
                    newMINNO.DateCode = ((MKeyPart)mKeyParts[i]).DateCode;
                    newMINNO.VendorItemCode = ((MKeyPart)mKeyParts[i]).VendorItemCode;
                    newMINNO.Version = ((MKeyPart)mKeyParts[i]).Version;
                    newMINNO.PCBA = ((MKeyPart)mKeyParts[i]).PCBA;
                    newMINNO.BIOS = ((MKeyPart)mKeyParts[i]).BIOS;

                    returnValue.ClearMessages();
                    returnValue.Add(new UserControl.Message(MessageType.Success, "$parse_prepare$CS_Error_ParseSuccess"));
                    break;
                }
            }
            return returnValue;
        }

        public Messages ParseFromProduct(ref MINNO newMINNO, bool checkComplete, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength)
        {
            return ParseFromProduct(ref newMINNO, checkComplete, barcode, opBOMDetailForItemCode, opBOMDetailSNLength, true, false);
        }

        public Messages ParseFromProduct(ref MINNO newMINNO, bool checkComplete, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength, bool dealSecondSource, bool isSKDCartonCheck)
        {
            Messages returnValue = new Messages();

            SimulationReport simRpt = GetLastSimulationReport(barcode);

            if (simRpt == null)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_Error_NoSimulationReport" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            if (!isSKDCartonCheck && simRpt.IsLoadedPart.Trim().Length > 0 && simRpt.IsLoadedPart.Trim() == FormatHelper.TRUE_STRING)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_Error_AlreadyLoaded" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            if (checkComplete && simRpt.IsComplete == FormatHelper.FALSE_STRING)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_Error_NotComplete" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            if (simRpt.Status != ProductStatus.GOOD
                && simRpt.Status != ProductStatus.OffLine
                && simRpt.Status != ProductStatus.OffMo
                && simRpt.Status != ProductStatus.OutLine)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_ProductStatusError" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            //�ȶ��Ϻź����кų���
            returnValue.AddMessages(this.CheckAfterParse(simRpt.ItemCode, opBOMDetailForItemCode, barcode, opBOMDetailSNLength, "$parse_product:", dealSecondSource));
            if (!returnValue.IsSuccess())
            {
                return returnValue;
            }

            if (returnValue.IsSuccess())
            {
                newMINNO.MItemCode = simRpt.ItemCode;
                newMINNO.LotNO = simRpt.LOTNO;

                returnValue.ClearMessages();
                returnValue.Add(new UserControl.Message(MessageType.Success, "$parse_product$CS_Error_ParseSuccess"));
            }

            return returnValue;
        }

        //add by hiro 08/10/13
        private Messages CheckAfterParse(string inputItemCode, OPBOMDetail opBOMDetail, string input, int opBOMDetailSNLength, string msgPrefix, bool dealSecondSource)
        {
            Messages returnValue = new Messages();

            //�Ϻűȶ�
            if (opBOMDetail != null)
            {
                if (dealSecondSource)
                {
                    string itemCode = opBOMDetail.ItemCode;
                    string opID = opBOMDetail.OPID;
                    string opBOMCode = opBOMDetail.OPBOMCode;
                    string opBOMVersion = opBOMDetail.OPBOMVersion;
                    string opBOMSourceItemcode = opBOMDetail.OPBOMSourceItemCode;
                    int orgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

                    OPBOMFacade opbomfacade = new OPBOMFacade(this.DataProvider);
                    object[] objs = opbomfacade.QueryOPBOMDetail(opID, itemCode, opBOMCode, opBOMVersion, orgID, inputItemCode);
                    if (objs != null)
                    {
                        if (((OPBOMDetail)objs[0]).OPBOMSourceItemCode.Trim().ToUpper() != opBOMSourceItemcode.Trim().ToUpper())
                        {
                            returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + " $CS_LotControlMaterial_CompareItemFailed"));
                            return returnValue;
                        }
                    }
                    else
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + " $CS_LotControlMaterial_CompareItemFailed"));
                        return returnValue;
                    }
                }
                else
                {
                    if (string.Compare(inputItemCode, opBOMDetail.OPBOMItemCode, true) != 0)
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + "$CS_LotControlMaterial_CompareItemFailed"));
                        return returnValue;
                    }
                }
            }

            //������кų���
            if (opBOMDetailSNLength > 0)
            {
                if (input.Length != opBOMDetailSNLength)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + " $Error_SNLength_Wrong"));
                    return returnValue;
                }
            }

            return returnValue;
        }
        //end by hiro 08/10/13

        //�����鹩Ӧ��
        public Messages CheckNeedVebdor(MINNO newMINNO)
        {
            Messages returnValue = new Messages();

            if (string.IsNullOrEmpty(newMINNO.VendorCode))
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$CS_Material_Have_No_Vendor" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            return returnValue;
        }

        public bool KeyPartUsed(string keyPartRCard, string mItemCode, bool checkCache, ArrayList inputPartList)
        {
            if (checkCache)
            {
                //����Ƿ����ҳ���ϵģ����ǻ�û�б��浽���ݿ��
                if (inputPartList != null)
                {
                    foreach (MINNO minno in inputPartList)
                    {
                        if (minno.EAttribute1 == MCardType.MCardType_Keyparts
                            && minno.MItemPackedNo.Trim().ToUpper() == keyPartRCard.Trim().ToUpper())
                        {
                            return true;
                        }
                    }
                }
            }

            //Check DB
            object[] onWIPItemList = QueryOnWIPItem(keyPartRCard, mItemCode);
            if (onWIPItemList != null)
            {
                foreach (OnWIPItem onWIPItem in onWIPItemList)
                {
                    if (onWIPItem.MCardType == MCardType.MCardType_Keyparts
                        && onWIPItem.ActionType == (int)MaterialType.CollectMaterial)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Messages GetMINNOByBarcode(OPBOMDetail opBOMDetail, string barcode, string moCode, ArrayList inputPartList, bool dealSecondSource, bool isSKDCartonCheck, out MINNO minno)
        {
            Messages returnValue = new Messages();
            minno = null;


            //������ʽ
            string parseTypeSetting = "," + opBOMDetail.OPBOMParseType + ",";
            bool checkStatus = opBOMDetail.CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;

            //�������
            string checkTypeSetting = "," + opBOMDetail.OPBOMCheckType + ",";
            int inputLength = opBOMDetail.SerialNoLength;
            string checkNeedVendor = !string.IsNullOrEmpty(opBOMDetail.NeedVendor) ? checkNeedVendor = opBOMDetail.NeedVendor : string.Empty;

            string mItemCode = opBOMDetail.OPBOMItemCode;

            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            MINNO newMINNO = materialFacade.CreateNewMINNO();
            newMINNO.MItemCode = mItemCode;

            Messages parseSuccess = new Messages();
            Messages oldParseSuccess = new Messages();
            parseSuccess.Add(new UserControl.Message(MessageType.Error, "$CS_Error_ParseFailed"));

            //add by Jarvis 20120316 for ����
            //begin
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            object objItemLot = inventoryFacade.QueryLastItemLotByLotNo(barcode);

            if (objItemLot == null)
            {
                objItemLot = inventoryFacade.QueryItemLotByBarCode(barcode);

                if (objItemLot != null)
                {
                    //newMINNO.MItemCode = ((ItemLot)objItemLot).Mcode.ToString();
                    newMINNO.LotNO = ((ItemLot)objItemLot).Lotno.ToString();
                    //newMINNO.DateCode = ((ItemLot)objItemLot).Datecode.ToString();
                    //newMINNO.VendorCode = ((ItemLot)objItemLot).Vendorcode;
                    //newMINNO.VendorItemCode = ((ItemLot)objItemLot).Vendoritemcode;
                }
            }
            else
            {
                //newMINNO.MItemCode = ((ItemLot)objItemLot).Mcode.ToString();
                newMINNO.LotNO = ((ItemLot)objItemLot).Lotno.ToString();
                //newMINNO.DateCode = ((ItemLot)objItemLot).Datecode.ToString();
                //newMINNO.VendorCode = ((ItemLot)objItemLot).Vendorcode;
                //newMINNO.VendorItemCode = ((ItemLot)objItemLot).Vendoritemcode;
            }
            newMINNO.MOCode = moCode;
            //end

            //��ѡ���Ϻűȶ�,����ѡ�������ʽ
            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
            {
                if (parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") < 0
                  && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") < 0
                  && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") < 0)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, ">>$CS_Error_ParseFailed:$CheckCompareItem_Must_CheckOneParse:" + mItemCode + ""));
                    return returnValue;
                }
            }

            if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") >= 0)
            {
                //Parse from barcode
                OPBOMDetail opBOMDetailForItemCode = null;
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                {
                    opBOMDetailForItemCode = opBOMDetail;
                }
                oldParseSuccess.AddMessages(parseSuccess);
                parseSuccess = ParseFromBarcode(ref newMINNO, barcode, opBOMDetailForItemCode, inputLength, dealSecondSource);
            }

            if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") >= 0)
            {
                //Parse from prepare
                newMINNO.MOCode = moCode;
                newMINNO.EAttribute1 = opBOMDetail.OPBOMItemControlType;

                OPBOMDetail opBOMDetailForItemCode = null;
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                {
                    opBOMDetailForItemCode = opBOMDetail;
                }
                oldParseSuccess.AddMessages(parseSuccess);
                parseSuccess = ParseFromPrepare(ref newMINNO, barcode, opBOMDetailForItemCode, inputLength, dealSecondSource);
            }

            if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") >= 0)
            {
                //Parse from product
                OPBOMDetail opBOMDetailForItemCode = null;
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                {
                    opBOMDetailForItemCode = opBOMDetail;
                }
                oldParseSuccess.AddMessages(parseSuccess);
                parseSuccess = ParseFromProduct(ref newMINNO, checkStatus, barcode, opBOMDetailForItemCode, inputLength, dealSecondSource, isSKDCartonCheck);
            }

            //���VendorCodeΪ�գ���tblmaterial�л�ȡ
            if (newMINNO.VendorCode == null || newMINNO.VendorCode.Trim().Length <= 0)
            {
                ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                object objMaterial = itemfacade.GetMaterial(newMINNO.MItemCode.Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (objMaterial != null)
                {
                    //ע�� by sam 2016��2��25��13:14:36
                    //newMINNO.VendorCode = ((Domain.MOModel.Material)objMaterial).VendorCode;
                }
            }

            //oldParseSuccess�м�¼�������н������������δ�����κν�����oldParseSuccess�ǿյģ�Ҳ��ʾ�ɹ�
            if (!parseSuccess.IsSuccess() && opBOMDetail.OPBOMParseType.Trim().Length > 0)
            {
                oldParseSuccess.AddMessages(parseSuccess);
                returnValue.AddMessages(oldParseSuccess);
                return returnValue;
            }

            //Check if key part was used
            if (!isSKDCartonCheck && opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS
                && KeyPartUsed(barcode, newMINNO.MItemCode, true, inputPartList))
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$CS_Error_KeyPartUsed"));
                return returnValue;
            }

            //���кų��ȱȶ�
            if (opBOMDetail.OPBOMParseType.Trim().Length <= 0)
            {
                if (inputLength > 0 && barcode.Trim().Length != inputLength)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_SNLength_Wrong"));
                    return returnValue;
                }
            }

            //check NeedVendor
            if (!isSKDCartonCheck && checkNeedVendor == NeedVendor.NeedVendor_Y)
            {
                Messages checkNeedVendorMsg = new Messages();
                checkNeedVendorMsg = CheckNeedVebdor(newMINNO);

                if (!checkNeedVendorMsg.IsSuccess())
                {
                    returnValue.AddMessages(checkNeedVendorMsg);
                    return returnValue;
                }
            }

            if (!isSKDCartonCheck)
            {
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_LINKBARCODE.ToLower() + ",") >= 0)
                {
                    //Link barcode
                    if (string.IsNullOrEmpty(newMINNO.MItemCode))
                    {
                        newMINNO.MItemCode = mItemCode;
                    }

                    newMINNO.MItemPackedNo = barcode;
                    newMINNO.MSourceItemCode = opBOMDetail.OPBOMSourceItemCode;

                    if (opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                    {
                        newMINNO.EAttribute1 = MCardType.MCardType_Keyparts;
                    }
                    else if (opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                    {
                        newMINNO.EAttribute1 = MCardType.MCardType_INNO;
                        newMINNO.Qty = opBOMDetail.OPBOMItemQty;
                    }

                    minno = newMINNO;
                }
                else
                {
                    minno = null;
                }
            }

            return returnValue;
        }

        #endregion

        #region WokingError

        public WorkingError CreateNewWorkingError()
        {
            return new WorkingError();
        }

        public void AddWorkingError(WorkingError workingError)
        {
            this.DataProvider.Insert(workingError);
        }

        public void UpdateWorkingError(WorkingError workingError)
        {
            this.DataProvider.Update(workingError);
        }

        public void DeleteWorkingError(WorkingError workingError)
        {
            this.DataProvider.Delete(workingError);
        }

        public object GetWorkingError(string resCode, string createUser, int createDate, int createTime)
        {
            return this.DataProvider.CustomSearch(typeof(WorkingError), new object[] { resCode, createUser, createDate, createTime });
        }

        public void LogWorkingError(string userCode, string resCode, string segCode, string ssCode, string shiftTypeCode,
            string functionType, string fucntion, string inputContent, string errorMessageCode, string errorMessage)
        {
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DateTime currentDateTime = DateTime.Parse(FormatHelper.ToDateString(dbDateTime.DBDate, "-")
                + " " + FormatHelper.ToTimeString(dbDateTime.DBTime, ":"));

            WorkingError workingError = CreateNewWorkingError();

            if (resCode != null && resCode.Trim().Length > 0)
            {
                workingError.ResourceCode = resCode;
                workingError.SegmentCode = segCode;
                workingError.StepSequenceCode = ssCode;
                workingError.ShiftTypeCode = shiftTypeCode;

                TimePeriod tp = (TimePeriod)shiftModelFacade.GetTimePeriod(shiftTypeCode, dbDateTime.DBTime);
                if (tp != null)
                {
                    workingError.TimePeriodCode = tp.TimePeriodCode;
                    workingError.ShiftCode = tp.ShiftCode;
                    workingError.ShiftDay = shiftModelFacade.GetShiftDay(tp, currentDateTime);
                }
            }
            else
            {
                workingError.ResourceCode = " ";
            }

            workingError.CreateDate = dbDateTime.DBDate;
            workingError.CreateTime = dbDateTime.DBTime;
            workingError.MaintainDate = dbDateTime.DBDate;
            workingError.MaintainTime = dbDateTime.DBTime;

            if (userCode != null && userCode.Trim().Length > 0)
            {
                workingError.CreateUser = userCode;
                workingError.MaintainUser = userCode;
            }
            else
            {
                workingError.CreateUser = " ";
                workingError.MaintainUser = " ";
            }

            workingError.Status = WorkingErrorStatus.NEW;
            workingError.FunctionType = functionType;
            workingError.Function = fucntion;
            workingError.InputContent = inputContent;
            workingError.ErrorMessageCode = errorMessageCode;
            workingError.ErrorMessage = errorMessage;

            AddWorkingError(workingError);
        }


        #endregion

        #region MACInfo

        public MACInfo CreateNewMACInfo()
        {
            return new MACInfo();
        }

        public void AddMACInfo(MACInfo macInfo)
        {
            this.DataProvider.Insert(macInfo);
        }

        public void DeleteMACInfo(MACInfo macInfo)
        {
            this.DataProvider.Delete(macInfo);
        }

        public void UpdateMACInfo(MACInfo macInfo)
        {
            this.DataProvider.Update(macInfo);
        }

        public object GetMACInfo(string moCode, string rCard)
        {
            return this.DataProvider.CustomSearch(typeof(MACInfo), new object[] { moCode, rCard });
        }

        #endregion

        #region IDInfo

        public IDInfo CreateNewIDInfo()
        {
            return new IDInfo();
        }

        public void AddIDInfo(IDInfo idInfo)
        {
            this.DataProvider.Insert(idInfo);
        }

        public void DeleteIDInfo(IDInfo idInfo)
        {
            this.DataProvider.Delete(idInfo);
        }

        public void UpdateIDInfo(IDInfo idInfo)
        {
            this.DataProvider.Update(idInfo);
        }

        public object GetIDInfo(string moCode, string rCard)
        {
            return this.DataProvider.CustomSearch(typeof(IDInfo), new object[] { moCode, rCard });
        }


        public object GetIDInfo(string moCode, string rcard, string id, int number)
        {
            string sql = " SELECT * FROM tblidinfo where ";

            if (number == 1)
            {
                sql += " ID1='" + id.Trim() + "'";
            }

            if (number == 2)
            {
                sql += " ID2='" + id.Trim() + "'";
            }

            if (number == 3)
            {
                sql += " ID3='" + id.Trim() + "'";
            }

            if (rcard.Trim() != string.Empty)
            {
                sql += " and rcard='" + rcard.Trim().ToUpper() + "'";
            }

            if (moCode.Trim() != string.Empty)
            {
                sql += " and mocode='" + moCode.Trim().ToUpper() + "'";
            }

            object[] IDInfoList = this.DataProvider.CustomQuery(typeof(IDInfo), new SQLCondition(sql));

            if (IDInfoList != null)
            {
                return IDInfoList[0];
            }
            return null;
        }

        #endregion

        #region OQC��ҵʱ�Զ���ⷽ��
        /// <summary>
        /// �Զ����
        /// </summary>
        /// <param name="type">DomainObject������</param>
        /// <returns></returns>
        public void AutoInventory(object obj, string userCode)
        {

            BaseSetting.SystemSettingFacade systemSettingFacade = new SystemSettingFacade(DataProvider);

            string outin = systemSettingFacade.GetGetParameterFileName("OUTIN", "AUTOOUTIN");
            if (outin != "Y")
            {
                return;
            }

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(DataProvider);
            string storageName = systemSettingFacade.GetGetParameterFileName("STORAGE", "AUTOSTORAGE");
            BenQGuru.eMES.Domain.Warehouse.InvIntransSum invIntransSum = new BenQGuru.eMES.Domain.Warehouse.InvIntransSum();
            BenQGuru.eMES.Domain.Warehouse.InvInTransaction invInTransaction = new BenQGuru.eMES.Domain.Warehouse.InvInTransaction();
            BenQGuru.eMES.Domain.Warehouse.StackToRcard stackToRcard = new BenQGuru.eMES.Domain.Warehouse.StackToRcard();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            OQCLot oqcLot = obj as OQCLot;
            object[] objLot2Card = oqcFacade.GetOQCLot2CardByLotNo(oqcLot.LOTNO, OQCFacade.Lot_Sequence_Default, ProductStatus.GOOD);
            if (objLot2Card != null && objLot2Card.Length > 0)
            {
                int goodQty = objLot2Card.Length;

                //Insert tblInvIntransSum                        
                invIntransSum.ItemCode = oqcLot.ItemCode;
                //invIntransSum.ItemGrade = " ";
                invIntransSum.MaintainDate = dbDateTime.DBDate;
                invIntransSum.StackCode = " ";
                invIntransSum.StorageCode = storageName;
                invIntransSum.InQty = goodQty;
                inventoryFacade.AddInvIntransSum(invIntransSum);
                foreach (OQCLot2Card lot2Card in objLot2Card)
                {
                    Simulation simulation = GetSimulation(lot2Card.RunningCard) as Simulation;
                    //Insert tblInvInTransaction                        
                    invInTransaction.TransCode = " ";
                    invInTransaction.StorageCode = storageName;
                    invInTransaction.StackCode = " ";
                    //invInTransaction.ItemGrade = " ";
                    invInTransaction.ItemCode = lot2Card.ItemCode;
                    invInTransaction.Rcard = lot2Card.RunningCard;
                    invInTransaction.CartonCode = simulation.CartonCode;
                    invInTransaction.MOCode = lot2Card.MOCode;
                    invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    invInTransaction.Serial = 0;
                    invInTransaction.MaintainUser = userCode;
                    invInTransaction.MaintainDate = dbDateTime.DBDate;
                    invInTransaction.MaintainTime = dbDateTime.DBTime;
                    invInTransaction.BusinessReason = BussinessReason.type_produce;
                    invInTransaction.Company = " ";
                    invInTransaction.BusinessCode = " ";
                    inventoryFacade.AddInvInTransaction(invInTransaction);
                    //Insert tblStackToRcard
                    stackToRcard.StorageCode = storageName;
                    stackToRcard.StackCode = " ";
                    stackToRcard.Oqclot = lot2Card.LOTNO;
                    stackToRcard.SerialNo = lot2Card.RunningCard;
                    stackToRcard.ItemCode = lot2Card.ItemCode;
                    stackToRcard.BusinessReason = BussinessReason.type_produce;
                    stackToRcard.Company = " ";
                    //stackToRcard.ItemGrade = " ";
                    stackToRcard.InUser = userCode;
                    stackToRcard.InDate = dbDateTime.DBDate;
                    stackToRcard.InTime = dbDateTime.DBTime;
                    stackToRcard.MaintainUser = userCode;
                    stackToRcard.MaintainDate = dbDateTime.DBDate;
                    stackToRcard.MaintainTime = dbDateTime.DBTime;
                    stackToRcard.TransInSerial = 0;
                    stackToRcard.Cartoncode = simulation.CartonCode;
                    inventoryFacade.AddStackToRcard(stackToRcard);
                }
            }
        }
        #endregion

        #region rcard���Զ���ⷽ�������깤ʱ��
        /// <summary>
        /// ����rcard�Զ����
        /// </summary>
        /// <param name="rcard"></param>
        /// <param name="userCode"></param>
        public void AutoInventory(Simulation simulation, string userCode)
        {
            if (simulation == null)
            {
                return;
            }

            BaseSetting.SystemSettingFacade systemSettingFacade = new SystemSettingFacade(DataProvider);

            string outin = systemSettingFacade.GetGetParameterFileName("OUTIN", "AUTOOUTIN");
            if (outin != "Y")
            {
                return;
            }
            

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(DataProvider);
            string storageName = systemSettingFacade.GetGetParameterFileName("STORAGE", "AUTOSTORAGE");
            BenQGuru.eMES.Domain.Warehouse.InvIntransSum invIntransSum = new BenQGuru.eMES.Domain.Warehouse.InvIntransSum();
            BenQGuru.eMES.Domain.Warehouse.InvInTransaction invInTransaction = new BenQGuru.eMES.Domain.Warehouse.InvInTransaction();
            BenQGuru.eMES.Domain.Warehouse.StackToRcard stackToRcard = new BenQGuru.eMES.Domain.Warehouse.StackToRcard();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            //�ж��Ƿ��Ѿ����
            StackToRcard stack2Rcard = (StackToRcard)inventoryFacade.GetStackToRcard(simulation.RunningCard);
            if (stack2Rcard != null)
            {
                return;
            }
            //Insert tblInvIntransSum                        
            invIntransSum.ItemCode = simulation.ItemCode;
            //invIntransSum.ItemGrade = " ";
            invIntransSum.MaintainDate = dbDateTime.DBDate;
            invIntransSum.StackCode = " ";
            invIntransSum.StorageCode = storageName;
            invIntransSum.InQty = 1;
            inventoryFacade.AddInvIntransSum(invIntransSum);

            //Insert tblInvInTransaction                        
            invInTransaction.TransCode = " ";
            invInTransaction.StorageCode = storageName;
            invInTransaction.StackCode = " ";
            //invInTransaction.ItemGrade = " ";
            invInTransaction.ItemCode = simulation.ItemCode;
            invInTransaction.Rcard = simulation.RunningCard;
            invInTransaction.CartonCode = simulation.CartonCode;
            invInTransaction.MOCode = simulation.MOCode;
            invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            invInTransaction.Serial = 0;
            invInTransaction.MaintainUser = userCode;
            invInTransaction.MaintainDate = dbDateTime.DBDate;
            invInTransaction.MaintainTime = dbDateTime.DBTime;
            invInTransaction.BusinessReason = BussinessReason.type_produce;
            invInTransaction.Company = " ";
            invInTransaction.BusinessCode = " ";
            inventoryFacade.AddInvInTransaction(invInTransaction);
            //Insert tblStackToRcard
            stackToRcard.StorageCode = storageName;
            stackToRcard.StackCode = " ";
            stackToRcard.Oqclot = simulation.LOTNO;
            stackToRcard.SerialNo = simulation.RunningCard;
            stackToRcard.ItemCode = simulation.ItemCode;
            stackToRcard.BusinessReason = BussinessReason.type_produce;
            stackToRcard.Company = " ";
            //stackToRcard.ItemGrade = " ";
            stackToRcard.InUser = userCode;
            stackToRcard.InDate = dbDateTime.DBDate;
            stackToRcard.InTime = dbDateTime.DBTime;
            stackToRcard.MaintainUser = userCode;
            stackToRcard.MaintainDate = dbDateTime.DBDate;
            stackToRcard.MaintainTime = dbDateTime.DBTime;
            stackToRcard.TransInSerial = 0;
            if (string.IsNullOrEmpty(simulation.CartonCode))
            {
                stackToRcard.Cartoncode = " ";
            }
            else
            {
                stackToRcard.Cartoncode = simulation.CartonCode;
            }
            
            inventoryFacade.AddStackToRcard(stackToRcard);

        }
        #endregion

        #region ����ͼ�鿴

        public object[] QueryItemOpFlow(string itemCode, string routeCode)
        {
            string sql = " SELECT *  FROM TBLITEMROUTE2OP A WHERE A.ITEMCODE = '" + itemCode + "'   AND A.ROUTECODE = '" + routeCode + "' ORDER BY OPSEQ ";
            return this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLCondition(sql));
        }

        public object[] QueryItemTracingFlow(string rcard, string moCode)
        {
            string sql = " SELECT *  FROM tblonwip A WHERE A.RCARD = '" + rcard + "'   AND A.mocode = '" + moCode + "'  ORDER BY a.rcardseq DESC";
            return this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sql));
        }

        public object[] QueryItemTracingFlow(string rcard, string routeCode, string moCode)
        {
            string sql = " SELECT *  FROM tblonwip A WHERE A.RCARD = '" + rcard + "' and a.routecode='" + routeCode + "'  AND A.mocode = '" + moCode + "' ";
            sql += " ORDER BY a.rcardseq  ASC";
            return this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sql));
        }

        public object[] QueryOnwipRoutesByRcard(string rcard)
        {
            string sql = " SELECT *  FROM tblonwip A WHERE A.RCARD = '" + rcard + "'    ";
            sql += " Order by a.serial desc ";
            return this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sql));
        }

        public object[] QueryOnwipRoutesByRcardAndMoCode(string rcard, string mocode)
        {
            string sql = " SELECT *  FROM tblonwip A WHERE A.RCARD = '" + rcard + "' AND A.MOCODE='" + mocode + "'   ";
            sql += "Order by a.serial desc ";
            return this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sql));
        }

        public string CheckOpIsExist(string rcard, string mocode, string routecode, string opcode)
        {
            string sql = " SELECT *  FROM tblonwip A WHERE A.RCARD = '" + rcard + "'  AND A.MOCODE='" + mocode + "'   ";
            sql += " AND A.ROUTECODE='" + routecode + "' AND A.OPCODE='" + opcode + "'  ";
            sql += " ORDER BY a.rcardseq DESC";
            object[] objs = this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sql));
            if (objs != null && objs.Length >= 1)
            {
                return ((OnWIP)(objs[0])).ActionResult;
            }
            else
            {
                return "";
            }
        }

        //add by klaus 
        public object[] QueryOnwipRoutesInRcard(string rcard)
        {
            string sql = " SELECT *  FROM tblonwip A WHERE A.RCARD in( " + rcard + " )   ";
            sql += " Order by a.serial desc ";
            return this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sql));
        }
        #endregion ����ͼ�鿴


        #region SPLITBOARD
        /// <summary>
        /// TBLSPLITBOARD
        /// </summary>
        public SplitBoard CreateNewSplitBoard()
        {
            return new SplitBoard();
        }

        public void AddSplitBoard(SplitBoard splitboard)
        {
            this.DataProvider.Insert(splitboard);
        }

        public void DeleteSplitBoard(SplitBoard splitboard)
        {
            this.DataProvider.Delete(splitboard);
        }

        public void UpdateSplitBoard(SplitBoard splitboard)
        {
            this.DataProvider.Update(splitboard);
        }

        public object GetSplitBoard(string MOCODE, string RCARD)
        {
            return this.DataProvider.CustomSearch(typeof(SplitBoard), new object[] { MOCODE, RCARD });
        }

        public int GetSplitBoardCount(string RCARD)
        {
            string sql = "SELECT COUNT(*) FROM TBLSPLITBOARD A  WHERE a.RCARD ='" + RCARD + "' OR SCARD = '" + RCARD + "' ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        #endregion

        #region DisToLineList
        /// <summary>
        /// TBLDISTOLINELIST
        /// </summary>
        public DisToLineList CreateNewDistolinelist()
        {
            return new DisToLineList();
        }

        public void AddDistolinelist(DisToLineList disToLineList)
        {
            this.DataProvider.Insert(disToLineList);
        }

        public void DeleteDistolinelist(DisToLineList disToLineList)
        {
            this.DataProvider.Delete(disToLineList);
        }

        public void UpdateDistolinelist(DisToLineList disToLineList)
        {
            this.DataProvider.Update(disToLineList);
        }

        public object GetDistolinelist(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(DisToLineList), new object[] { Serial });
        }

        #endregion


        #region DisToLineHead
        /// <summary>
        /// TBLDISTOLINELIST
        /// </summary>
        public DisToLineHead CreateNewDisToLineHead()
        {
            return new DisToLineHead();
        }

        public void AddDisToLineHead(DisToLineHead disToLineHead)
        {
            this.DataProvider.Insert(disToLineHead);
        }

        public void DeleteDisToLineHead(DisToLineHead disToLineHead)
        {
            this.DataProvider.Delete(disToLineHead);
        }

        public void UpdateDisToLineHead(DisToLineHead disToLineHead)
        {
            this.DataProvider.Update(disToLineHead);
        }

        public object GetDisToLineHead(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(DisToLineHead), new object[] { Serial });
        }

        public object GetDisToLineHead(string moCode,string mCode)
        {
            return this.DataProvider.CustomSearch(typeof(DisToLineHead), new object[] { moCode, mCode });
        }

        #endregion

        #region Distolinedetail
        /// <summary>
        /// TBLDISTOLINEDETAIL
        /// </summary>
        public DisToLineDetail CreateNewDistolinedetail()
        {
            return new DisToLineDetail();
        }

        public void AddDistolinedetail(DisToLineDetail disToLineDetail)
        {
            this.DataProvider.Insert(disToLineDetail);
        }

        public void DeleteDistolinedetail(DisToLineDetail disToLineDetail)
        {
            this.DataProvider.Delete(disToLineDetail);
        }

        public void UpdateDistolinedetail(DisToLineDetail disToLineDetail)
        {
            this.DataProvider.Update(disToLineDetail);
        }

        public object GetDistolinedetail(string OpCode, string MoCode, string SsCode, string MCode)
        {
            return this.DataProvider.CustomSearch(typeof(DisToLineDetail), new object[] { OpCode, MoCode, SsCode, MCode });
        }

        public object[] QueryDistolinedetail(string opCode, string moCode, string ssCode, string mCode)
        {
            string sql = "select * from tbldistolinedetail where 1=1 ";
            if (!string.IsNullOrEmpty(opCode))
            {
                sql += " and opcode = '" + opCode + "' ";
            }
            if (!string.IsNullOrEmpty(moCode))
            {
                sql += " and mocode = '" + moCode + "' ";
            }
            if (!string.IsNullOrEmpty(ssCode))
            {
                sql += " and sscode = '" + ssCode + "' ";
            }
            if (!string.IsNullOrEmpty(mCode))
            {
                sql += " and mcode = '" + mCode + "' ";
            }

            return this.DataProvider.CustomQuery(typeof(DisToLineDetail), new SQLCondition(sql));
        }

        public object[] QueryDisToLineDetailForMType(string opCode, string moCode, string ssCode, string mControlType)
        {
            string sql = "select a.* from tbldistolinedetail a inner join tblmaterial b on a.mcode = b.mcode where 1=1 ";
            if (!string.IsNullOrEmpty(opCode))
            {
                sql += " and a.opcode = '" + opCode + "' ";
            }
            if (!string.IsNullOrEmpty(moCode))
            {
                sql += " and a.mocode = '" + moCode + "' ";
            }
            if (!string.IsNullOrEmpty(ssCode))
            {
                sql += " and a.sscode = '" + ssCode + "' ";
            }
            if (!string.IsNullOrEmpty(mControlType))
            {
                sql += " and b.mcontroltype in ( " + mControlType + " ) ";
            }

            return this.DataProvider.CustomQuery(typeof(DisToLineDetail), new SQLCondition(sql));

        }

        /// <summary>
        /// ��ѯ��ť�������
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="mCode"></param>
        /// <param name="segCode"></param>
        /// <param name="ssCode"></param>
        /// <param name="statusList"></param>
        /// <returns></returns>
        public object[] QueryDistolinedetailForDis(string moCode, string mCode, string segCode, string ssCode, string statusList)
        {
            string sql = string.Format(@"Select d.segcode,d.sscode,d.mssdisqty,d.mssleftqty,d.mqty,d.status as status,
                        Case d.status when '" + DisToLineStatus.ShortDis + @"' then 0
                                    when '" + DisToLineStatus.ERDis + @"' then 1
                                    when '" + DisToLineStatus.WaitDis + @"' then 2
                                    when '" + DisToLineStatus.NormalDis + @"' then 3
                                else 4 end as orderseq,
                        h.mocode,h.mcode,h.mplanqty,h.mdisqty,h.status as hstatus,m.mname,
                        nvl(wt.cycletime,0) as cycletime,nvl(mb.mobitemqty,0) as mobitemqty
                          from tbldistolinedetail d 
                        inner join tbldistolinehead h on h.mocode=d.mocode and h.mcode=d.mcode
                        left join tblmaterial m on m.mcode=d.mcode
                        inner join tblmo mo on mo.mocode=d.mocode
                        left join tblmobom mb on mb.mocode=d.mocode and mb.mobitemcode=d.mcode
                        Left join tblplanworktime wt on wt.itemcode=mo.itemcode and wt.sscode=d.sscode
                        where 1=1 and h.mocode like '{0}%' and h.mcode like '{1}%' 
                            and d.status in ({2})", moCode, mCode, statusList);

            if (!string.IsNullOrEmpty(segCode))
            {
                sql += " and d.segcode = '" + segCode + "' ";
            }
            if(!string.IsNullOrEmpty(ssCode))
            {
                sql += " and d.sscode = '" + ssCode + "' ";
            }

            sql += " order by orderseq,d.mdate,d.mtime";

            return this.DataProvider.CustomQuery(typeof(DisToLineForDistribute), new SQLCondition(sql));

        }

        /// <summary>
        /// ��ȡ������ϸ��Ϣ����ӦBOM������CycleTime
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="mCode"></param>
        /// <param name="segCode"></param>
        /// <param name="ssCode"></param>
        /// <returns></returns>
        public object[] QueryDistolinedetailForEdit(string moCode, string mCode, string segCode, string ssCode)
        {
            string sql = string.Format(@"Select d.segcode,d.sscode,d.mssdisqty,d.mssleftqty,d.mqty,d.status as status,
                        d.mocode,d.mcode,m.mname, nvl(wt.cycletime,0) as cycletime,nvl(mb.mobitemqty,0) as mobitemqty
                          from tbldistolinedetail d 
                        left join tblmaterial m on m.mcode=d.mcode
                        inner join tblmo mo on mo.mocode=d.mocode
                        left join tblmobom mb on mb.mocode=d.mocode and mb.mobitemcode=d.mcode
                        Left join tblplanworktime wt on wt.itemcode=mo.itemcode and wt.sscode=d.sscode
                        where 1=1 and d.mocode = '{0}' and d.mcode = '{1}' and d.segcode = '{2}'  
                            and d.sscode = '{3}' order by d.mdate desc,d.mtime desc", moCode, mCode, segCode,ssCode);

            return this.DataProvider.CustomQuery(typeof(DisToLineForDistribute), new SQLCondition(sql));

        }

        /// <summary>
        /// ��ȡ�������ϵ�BOM������CycleTime
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="mCode"></param>
        /// <param name="ssCode"></param>
        /// <returns></returns>
        public object[] GetDisMobItemQtyAndCycleTime(string moCode, string mCode,string ssCode)
        {
            string sql = string.Format(@"Select h.mocode,h.mcode, nvl(wt.cycletime,0) as cycletime,nvl(mb.mobitemqty,0) as mobitemqty
                          from tbldistolinehead h
                        inner join tblmo mo on mo.mocode=h.mocode
                        left join tblmobom mb on mb.mocode=h.mocode and mb.mobitemcode=h.mcode
                        Left join tblplanworktime wt on wt.itemcode=mo.itemcode
                        where 1=1 and h.mocode = '{0}' and h.mcode = '{1}' and wt.sscode = '{2}' ", moCode, mCode, ssCode);

            return this.DataProvider.CustomQuery(typeof(DisToLineForDistribute), new SQLCondition(sql));

        }
        #endregion

        #region Distolinelist
        /// <summary>
        /// TBLDISTOLINEDETAIL
        /// </summary>
        public DisToLineList CreateNewDisToLineList()
        {
            return new DisToLineList();
        }

        public void AddDisToLineList(DisToLineList disToLineList)
        {
            this.DataProvider.Insert(disToLineList);
        }

        public void DeleteDisToLineList(DisToLineList disToLineList)
        {
            this.DataProvider.Delete(disToLineList);
        }

        public void UpdateDisToLineList(DisToLineList disToLineList)
        {
            this.DataProvider.Update(disToLineList);
        }

        public object GetDisToLineList(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(DisToLineList), new object[] { serial });
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns></returns>
        public object[] GetDisToLineList(string moCode,string mCode,string segCode,string ssCode,string opCode)
        {
            string sql = string.Format(@"select {0} from tbldistolinelist 
                        where mocode='{1}' and segcode='{2}' and sscode='{3}' and opcode='{4}' and mcode='{5}' 
                        order by serial desc", new object[]{DomainObjectUtility.GetDomainObjectFieldsString(typeof(DisToLineList)),
                                             moCode,segCode,ssCode,opCode,mCode});
            object[] objs = this.DataProvider.CustomQuery(typeof(DisToLineList), new SQLCondition(sql));

            return objs;
        }
        #endregion 

    }


    [Serializable]
    //����״̬ Note By Simone Xu
    public class MOManufactureStatus
    {
        private ArrayList _list = new ArrayList();

        public MOManufactureStatus()
        {
            this._list.Add(MOManufactureStatus.MOSTATUS_INITIAL);
            this._list.Add(MOManufactureStatus.MOSTATUS_RELEASE);
            this._list.Add(MOManufactureStatus.MOSTATUS_OPEN);
            this._list.Add(MOManufactureStatus.MOSTATUS_CLOSE);
            this._list.Add(MOManufactureStatus.MOSTATUS_PENDING);
        }

        public const string MOSTATUS_INITIAL = "mostatus_initial";		//��ʼ
        public const string MOSTATUS_RELEASE = "mostatus_release";		//�·�
        public const string MOSTATUS_OPEN = "mostatus_open";			//������
        public const string MOSTATUS_CLOSE = "mostatus_close";			//�ص�
        public const string MOSTATUS_PENDING = "mostatus_pending";		//��ͣ

        #region IInternalSystemVariable ��Ա

        public string Group
        {
            get
            {
                return "MOManufactureStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }


    [Serializable]
    public class ONWIPItemQueryObject : DomainObject
    {
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;
    }
    [Serializable]
    public class ONWIPItemObject : DomainObject
    {
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public string RunningCardSequence;
        [FieldMapAttribute("MCARD", typeof(string), 40, false)]
        public string MCARD;
        [FieldMapAttribute("MCardType", typeof(string), 40, false)]
        public string MCardType;
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCODE;
    }

    // Added By Hi1/Venus.Feng on 20081106 for Hisense Version : add upgrade sofeware
    [Serializable]
    public class OnWipSoftVer4Upgrade : OnWIPSoftVersion
    {
        [FieldMapAttribute("MDESC", typeof(string), 100, true)]
        public string ItemDescription;
    }

    [Serializable]
    public class DisToLineForDistribute : DisToLineDetail
    {
        [FieldMapAttribute("OrderSeq", typeof(int), 2, true)]
        public int OrderSeq;

        [FieldMapAttribute("MName", typeof(string), 200, true)]
        public string MName;

        [FieldMapAttribute("HStatus", typeof(string), 40, true)]
        public string HStatus;

        [FieldMapAttribute("MPlanQty", typeof(decimal), 22, true)]
        public decimal MPlanQty;

        [FieldMapAttribute("MDisQty", typeof(decimal), 22, true)]
        public decimal MDisQty;

        [FieldMapAttribute("CycleTime", typeof(decimal), 22, true)]
        public decimal CycleTime;

        [FieldMapAttribute("MOBItemQty", typeof(decimal), 22, true)]
        public decimal MOBItemQty;
    }
}
