using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect ;
using BenQGuru.eMES.DataCollect.Action ;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.FacilityFileParser;
using UserControl;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// AgentActionAOI 的摘要说明。
	/// </summary>
	public class AgentActionPIDAOI : IAgentAction	
	{
		private IDomainDataProvider _domainDataProvider = null;
		public AgentActionPIDAOI(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		private object[] parserObjs = null;

		#region IAgentAction 成员

		public UserControl.Messages CollectExcute(string filePath, string encoding)
		{
			object[] objs = null;
			PIDAOIFileParser fileParser = new PIDAOIFileParser(_domainDataProvider);
			try
			{
				fileParser.FormatName = "PIDAOIData" ;
				fileParser.ConfigFile = "PIDAOIDataFileParser.xml" ;

				objs = fileParser.Parse(filePath);
			}
			catch{}
			finally
			{
				fileParser.CloseFile();
			}

			#region ForTest
			//			if(objs != null && objs.Length > 0)
			//			{
			//				foreach(object obj in objs)
			//				{
			//					AOIData aoiData = obj as AOIData;
			//					string error = aoiData.ERRORCODES;
			//
			//				}
			//			}
			#endregion

			parserObjs = objs;

			UserControl.Messages returnMsg = new Messages();
			UserControl.Messages goodMsg = this.GoodCollect(parserObjs);
			UserControl.Messages ngMsg	 = this.NGCollect(parserObjs);
			
			if(goodMsg != null)
			{
				returnMsg.AddMessages(goodMsg);
			}
			if(ngMsg != null)
			{
				returnMsg.AddMessages(ngMsg);
			}
			return returnMsg;
		}

		public UserControl.Messages GoodCollect(object[] parserObjs)
		{
			UserControl.Messages returnMsg = new UserControl.Messages();
			try
			{
				foreach(object obj in parserObjs)
				{
					PIDAOIData aoiData = obj as PIDAOIData;
					
					if(aoiData.RESULT == "PASS")
					{
						ActionOnLineHelper onLine=new ActionOnLineHelper(this._domainDataProvider);
						Messages messages=  onLine.GetIDInfo(aoiData.RCARD.Trim().ToUpper());
						try
						{
							ProductInfo product=(ProductInfo)messages.GetData().Values[0];
							this._domainDataProvider.BeginTransaction();
//							//归属工单采集
//							IAction dataCollectMO = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
//							messages.AddMessages(((IActionWithStatus)dataCollectMO).Execute(
//								new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, 
//								aoiData.RCARD.Trim().ToUpper(),
//								aoiData.USER.Trim().ToUpper(),
//								AgentHelp.getResCode(aoiData.RESOURCE.Trim().ToUpper()),
//								product,this.getMOCode(aoiData.RCARD.Trim().ToUpper()))));
//							
							string goodResult = string.Empty;
//							if(messages.IsSuccess())
//							{
//								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Succes,string.Format("{0} $CS_GOMO_CollectSuccess",aoiData.RCARD.ToUpper())) );
//							}
//							else
//							{
//								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,AgentHelp.GetErrorMessage(messages)));
//							}
//							messages.ClearMessages();

							//GOOD采集
							messages=  onLine.GetIDInfo(aoiData.RCARD.Trim().ToUpper());
							product=(ProductInfo)messages.GetData().Values[0];
							BenQGuru.eMES.DataCollect.DataCollectFacade dcFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this._domainDataProvider);
							goodResult = dcFacade.ActionCollectGood( aoiData.RCARD.ToUpper(),aoiData.USER.ToUpper() ,AgentHelp.getResCode(aoiData.RESOURCE.ToUpper()) ); 
							if (goodResult == "OK")
							{
								this._domainDataProvider.CommitTransaction();
										
								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Success,string.Format("{0} $CS_GOOD_CollectSuccess",aoiData.RCARD.ToUpper())) );
								returnMsg.Add( new UserControl.Message(" "));
								BenQGuru.eMES.Common.Log.Info(string.Format("{0} $CS_GOOD_CollectSuccess： {1}",aoiData.RCARD.Trim().ToUpper(),"OK"));
								messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_GOODSUCCESS,$CS_Param_ID:{0}",aoiData.RCARD.ToUpper())));
							}
							else
							{
								this._domainDataProvider.RollbackTransaction();
								
								string errorMsg = string.Format("{0} $CS_GOOD_CollectFail : {1}",aoiData.RCARD.Trim().ToUpper(),goodResult);
								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,errorMsg) );
								returnMsg.Add( new UserControl.Message(" "));
								BenQGuru.eMES.Common.Log.Info(errorMsg);
							}
							
						}
						catch( Exception ex )
						{
							this._domainDataProvider.RollbackTransaction();
							BenQGuru.eMES.Common.Log.Info(AgentHelp.GetErrorMessage(messages),ex);
						}
						finally
						{
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
						}
					}
				}

				
			}
			catch
			{}
			return returnMsg;
		}

		public UserControl.Messages NGCollect(object[] parserObjs)
		{
			UserControl.Messages returnMsg = new UserControl.Messages();

			UserControl.Messages messages = new UserControl.Messages();
			try
			{
				foreach(object obj in parserObjs)
				{
					PIDAOIData aoiData = obj as PIDAOIData;
					

					if(aoiData.RESULT == "FAIL")
					{
						ActionOnLineHelper onLine=new ActionOnLineHelper(this._domainDataProvider);
						returnMsg.AddMessages(onLine.GetIDInfo(aoiData.RCARD.Trim().ToUpper()));
						try
						{
							if(returnMsg.IsSuccess())
							{
								ProductInfo product= (ProductInfo)returnMsg.GetData().Values[0];

								this._domainDataProvider.BeginTransaction();
//								//归属工单采集
//								IAction dataCollectMO = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
//								messages.AddMessages(((IActionWithStatus)dataCollectMO).Execute(
//									new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, 
//									aoiData.RCARD.Trim().ToUpper(),
//									aoiData.USER.Trim().ToUpper(),
//									AgentHelp.getResCode(aoiData.RESOURCE.Trim().ToUpper()),
//									product,this.getMOCode(aoiData.RCARD.Trim().ToUpper()))));
//							
//								if(messages.IsSuccess())
//								{
//									returnMsg.Add( new UserControl.Message(UserControl.MessageType.Succes,string.Format("{0} $CS_GOMO_CollectSuccess",aoiData.RCARD.ToUpper())) );
//								
//								}
//								else
//								{
//									returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,AgentHelp.GetErrorMessage(messages)));
//								}
//								messages.ClearMessages();

								//取不良信息
								object[] errorinfor =  GetErrorInfor(aoiData);
								messages=  onLine.GetIDInfo(aoiData.RCARD.Trim().ToUpper());
								product=(ProductInfo)messages.GetData().Values[0];
								//NG采集
								IAction dataCollectNG = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_SMTNG);
								messages.AddMessages(((IActionWithStatus)dataCollectNG).Execute(
									new TSActionEventArgs(ActionType.DataCollectAction_SMTNG,
									aoiData.RCARD.Trim().ToUpper(),
									aoiData.USER.Trim().ToUpper(),
									AgentHelp.getResCode(aoiData.RESOURCE.Trim().ToUpper()),
									product,
									errorinfor, 
									"")));
								if (messages.IsSuccess())
								{
									this._domainDataProvider.CommitTransaction();

									returnMsg.Add( new UserControl.Message(UserControl.MessageType.Success,string.Format("{0} $CS_NGSUCCESS",aoiData.RCARD.ToUpper())) );
									returnMsg.Add( new UserControl.Message(" "));
									BenQGuru.eMES.Common.Log.Info(string.Format("{0} $CS_NGSUCCESS： {1}",aoiData.RCARD.Trim().ToUpper(),"OK"));
									messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_NGSUCCESS,$CS_Param_ID:{0}",aoiData.RCARD.ToUpper())));
								}
								else
								{
									this._domainDataProvider.RollbackTransaction();

									string errorMsg = string.Format("{0} $CS_NGFail ： {1}",aoiData.RCARD.Trim().ToUpper(),AgentHelp.GetErrorMessage(messages));
									returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,errorMsg) );
									returnMsg.Add( new UserControl.Message(" "));
									BenQGuru.eMES.Common.Log.Info(errorMsg);
								}
							}
							
						}
						catch ( Exception ex )
						{
							this._domainDataProvider.RollbackTransaction();
							BenQGuru.eMES.Common.Log.Info(AgentHelp.GetErrorMessage(messages),ex);
						}
						finally
						{
							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
						}
					}
				}
			}
			catch
			{}
			return returnMsg;
		}

		private object[] GetErrorInfor(PIDAOIData aoiData)
		{
			ArrayList errList = new ArrayList();
			ArrayList ecg2ecList = new ArrayList();
			(new TS.TSFacade(_domainDataProvider)).ParseErrorInfo(aoiData.USER,aoiData.ERRORCODES,out errList,out ecg2ecList,"PID");
			
//			string defaultECG = "DefaultECGCode";
//			string defaultEC = "DefaultECCode";
//			for (int i=0 ; i<errorList.Length; i++)
//			{
//				TSErrorCode2Location tsinfo = new TSErrorCode2Location();
//				if(errorList[i].Split(',')[0].Trim() == string.Empty)continue;
//				tsinfo.ErrorLocation = errorList[i].Split(',')[0];
//				object[] ecg2ec = this.QueryECG2EC(errorList[i].Split(',')[2]);
//				if(ecg2ec != null && ecg2ec.Length>0)
//				{
//					tsinfo.ErrorCode = ((ErrorCodeGroup2ErrorCode)ecg2ec[0]).ErrorCode;
//					tsinfo.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)ecg2ec[0]).ErrorCodeGroup;
//				}
//				else
//				{
//					tsinfo.ErrorCode = defaultEC;
//					tsinfo.ErrorCodeGroup = defaultECG;
//				}
//				tsinfo.AB = "A";
//
//				ecg2ecList.Add(tsinfo);
//			}

			return (TSErrorCode2Location[])ecg2ecList.ToArray(typeof(TSErrorCode2Location));
		}

		//根据不良代码描述查询不良代码组和不良代码
		private object[] QueryECG2EC(string ecdesc)
		{
			try
			{
				string sql = string.Format("select tblecg2ec.* from tblecg2ec where ecode in (select ecode from tblec where ecdesc='{0}')",ecdesc);
				object[] ecg2ec = this._domainDataProvider.CustomQuery(typeof(ErrorCodeGroup2ErrorCode),new SQLCondition(sql));
				return ecg2ec;
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		//获取工单代码
		private string getMOCode(string sn)
		{
			//产品序列号的第二位到第六位是工单号码”，也就是说它的产品序列号中包含了5位工单编码
			//MES系统中的工单编码是“年（4位）＋分隔符（－）＋ERP中的工单号码（5位）”因此在解析产品序列号得到5位工单编码后在前面增加当前日期对应的年（4位），再加上分隔符（－）构成新的工单编码（就是MES系统中的工单号码）
			string mocode = DateTime.Now.Year.ToString() + "-" + sn.Substring(1,5);
			return mocode;
		}

		#endregion

		#region 权限和途程检查

		
		/// <summary>
		/// 检查权限
		/// </summary>
		/// <param name="resrouce"></param>
		/// <param name="userCode"></param>
		/// <returns></returns>
		private string CheckRight( string resrouce,string userCode )
		{
			try
			{
				BenQGuru.eMES.Security.SecurityFacade sFacade = new BenQGuru.eMES.Security.SecurityFacade(this._domainDataProvider);
				return sFacade.CheckResourceRight(userCode, resrouce)==true? "OK":"没有权限";
				
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
			}
		}

		/// <summary>
		/// 途程检查
		/// </summary>
		/// <param name="id">产品序列号</param>
		/// <param name="resrouce">资源代码</param>
		/// <param name="userCode">用户代码</param>
		/// <returns>“OK” 表示检查通过，其他为错误信息</returns>
		private string CheckRoute(string id,string resrouce,string userCode)
		{ 
			try
			{
				BenQGuru.eMES.DataCollect.DataCollectFacade dcFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this._domainDataProvider);
				return dcFacade.CheckRoute(id, resrouce, userCode, 0); 
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
			}
		}

		#endregion
	}
}
