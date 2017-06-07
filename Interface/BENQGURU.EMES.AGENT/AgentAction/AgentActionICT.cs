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
	public class AgentActionICT : IAgentAction	
	{
		private IDomainDataProvider _domainDataProvider = null;
		public AgentActionICT(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		private object[] parserObjs = null;

		#region IAgentAction 成员

		public UserControl.Messages CollectExcute(string filePath, string encoding)
		{
			object[] objs = null;
			ICTFileParser parser = new ICTFileParser();
			try
			{
				objs = parser.Parse(filePath);
			}
			catch{}

			#region ForTest
			//			if(objs != null && objs.Length > 0)
			//			{
			//				foreach(object obj in objs)
			//				{
			//					ICTData ictdata = obj as ICTData;
			//					string error = ictdata.ERRORCODES;
			//
			//					object[] tst =  GetErrorInfor( ictdata);
			//					string ss ="";
			//				}
			//			}
			#endregion

			parserObjs = objs;

			UserControl.Messages returnMsg = new Messages();

			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.OpenConnection();

			UserControl.Messages checkMsg= this.CheckData(parserObjs);
			UserControl.Messages goodMsg = this.GoodCollect(parserObjs);
			UserControl.Messages ngMsg	 = this.NGCollect(parserObjs);

			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.AutoCloseConnection = true;
			if(checkMsg != null)
			{
				returnMsg.AddMessages(checkMsg);
			}
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
					ICTData ictData = obj as ICTData;
					
					if(ictData.RESULT.Trim().ToUpper() == "PASS")
					{
						ActionOnLineHelper onLine=new ActionOnLineHelper(this._domainDataProvider);

						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.AutoCloseConnection = false;
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.OpenConnection();
						Messages messages=  onLine.GetIDInfo(ictData.RCARD.Trim().ToUpper());
						try
						{
							ProductInfo product=(ProductInfo)messages.GetData().Values[0];
							this._domainDataProvider.BeginTransaction();

							if(AgentInfo.AllowGoToMO == true)
							{
								string moCode = this.getMOCode(ictData.RCARD.Trim().ToUpper());

								if(System.Configuration.ConfigurationSettings.AppSettings["MOPREFIXSTRING"] != null)
								{
									string moPrefix = System.Configuration.ConfigurationSettings.AppSettings["MOPREFIXSTRING"].Trim();
									//首字符串检查
									if(moCode.Length < moPrefix.Length || moCode.Substring(0,moPrefix.Length) != moPrefix)
									{
										returnMsg.Add(new UserControl.Message(MessageType.Error
											,"$CS_Before_Card_FLetter_NotCompare $CS_Param_ID: " + ictData.RCARD.Trim().ToUpper()));
									}

									
								}
								if(System.Configuration.ConfigurationSettings.AppSettings["SNLENGTH"] != null)
								{
									try
									{
										int snLength = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["SNLENGTH"].Trim());
										//长度检查
										if(ictData.RCARD.Trim().ToUpper().Length != snLength)
										{
											returnMsg.Add(new UserControl.Message(MessageType.Error,
												"$CS_Before_Card_Length_FLetter_NotCompare $CS_Param_ID: " + ictData.RCARD.Trim().ToUpper()));
										}
									}
									catch(Exception ex)
									{
										returnMsg.Add(new UserControl.Message(ex));
									}
								}

								if(returnMsg.IsSuccess())
								{
									IAction dataCollectMO = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
									messages.AddMessages(((IActionWithStatus)dataCollectMO).Execute(
										new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, 
										ictData.RCARD.Trim().ToUpper(),
										ictData.USER.Trim().ToUpper(),
										AgentHelp.getResCode(ictData.RESOURCE.Trim().ToUpper()),
										product,moCode)));
							
									
									if(messages.IsSuccess())
									{
										returnMsg.Add( new UserControl.Message(UserControl.MessageType.Success,string.Format("{0} $CS_GOMO_CollectSuccess",ictData.RCARD.ToUpper())) );
									}
									else
									{
										returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,AgentHelp.GetErrorMessage(messages)));
									}
								}
							
								messages.ClearMessages();
							}
							
							string goodResult = string.Empty;
							//GOOD采集
							messages=  onLine.GetIDInfo(ictData.RCARD.Trim().ToUpper());
							product=(ProductInfo)messages.GetData().Values[0];
							BenQGuru.eMES.DataCollect.DataCollectFacade dcFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this._domainDataProvider);
							goodResult = dcFacade.ActionCollectGood( ictData.RCARD.ToUpper(),ictData.USER.ToUpper() ,AgentHelp.getResCode(ictData.RESOURCE.ToUpper()) ); 
							if (goodResult == "OK")
							{
								this._domainDataProvider.CommitTransaction();

								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Success,string.Format("{0} $CS_GOOD_CollectSuccess",ictData.RCARD.ToUpper())) );
								returnMsg.Add( new UserControl.Message(" "));
								BenQGuru.eMES.Common.Log.Info(string.Format("{0} $CS_GOOD_CollectSuccess： {1}",ictData.RCARD.Trim().ToUpper(),"OK"));
								messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_GOODSUCCESS,$CS_Param_ID:{0}",ictData.RCARD.ToUpper())));
							}
							else
							{
								this._domainDataProvider.RollbackTransaction();

								string errorMsg = string.Format("{0} $CS_GOOD_CollectFail ： {1}",ictData.RCARD.Trim().ToUpper(),goodResult);
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
//							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
//							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.AutoCloseConnection = true;
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
			try
			{
				foreach(object obj in parserObjs)
				{
					ICTData ictData = obj as ICTData;
					if(ictData.RESULT.Trim().ToUpper() == "FAIL")
					{
						ActionOnLineHelper onLine=new ActionOnLineHelper(this._domainDataProvider);

						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.AutoCloseConnection = false;
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.OpenConnection();
						Messages messages=  onLine.GetIDInfo(ictData.RCARD.Trim().ToUpper());
						try
						{
							ProductInfo product=(ProductInfo)messages.GetData().Values[0];

							this._domainDataProvider.BeginTransaction();

							if(AgentInfo.AllowGoToMO == true)
							{
								string moCode = this.getMOCode(ictData.RCARD.Trim().ToUpper());

								if(System.Configuration.ConfigurationSettings.AppSettings["MOPREFIXSTRING"] != null)
								{
									string moPrefix = System.Configuration.ConfigurationSettings.AppSettings["MOPREFIXSTRING"].Trim();
									//首字符串检查
									if(moCode.Length < moPrefix.Length || moCode.Substring(0,moPrefix.Length) != moPrefix)
									{
										returnMsg.Add(new UserControl.Message(MessageType.Error
											,"$CS_Before_Card_FLetter_NotCompare $CS_Param_ID: " + ictData.RCARD.Trim().ToUpper()));
									}

									
								}
								if(System.Configuration.ConfigurationSettings.AppSettings["SNLENGTH"] != null)
								{
									try
									{
										int snLength = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["SNLENGTH"].Trim());
										//长度检查
										if(ictData.RCARD.Trim().ToUpper().Length != snLength)
										{
											returnMsg.Add(new UserControl.Message(MessageType.Error,
												"$CS_Before_Card_Length_FLetter_NotCompare $CS_Param_ID: " + ictData.RCARD.Trim().ToUpper()));
										}
									}
									catch(Exception ex)
									{
										returnMsg.Add(new UserControl.Message(ex));
									}
								}

								if(returnMsg.IsSuccess())
								{
									IAction dataCollectMO = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
									messages.AddMessages(((IActionWithStatus)dataCollectMO).Execute(
										new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO, 
										ictData.RCARD.Trim().ToUpper(),
										ictData.USER.Trim().ToUpper(),
										AgentHelp.getResCode(ictData.RESOURCE.Trim().ToUpper()),
										product,moCode)));
							
									
									if(messages.IsSuccess())
									{
										returnMsg.Add( new UserControl.Message(UserControl.MessageType.Success,string.Format("{0} $CS_GOMO_CollectSuccess",ictData.RCARD.ToUpper())) );
									}
									else
									{
										returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,AgentHelp.GetErrorMessage(messages)));
									}
								}
							
								messages.ClearMessages();
							}
							
							//取不良信息
							object[] errorinfor =  GetErrorInfor(ictData);
							messages=  onLine.GetIDInfo(ictData.RCARD.Trim().ToUpper());
							product=(ProductInfo)messages.GetData().Values[0];
							//NG采集
							IAction dataCollectNG = new ActionFactory(this._domainDataProvider).CreateAction(ActionType.DataCollectAction_SMTNG);
							messages.AddMessages(((IActionWithStatus)dataCollectNG).Execute(
								new TSActionEventArgs(ActionType.DataCollectAction_SMTNG,
								ictData.RCARD.Trim().ToUpper(),
								ictData.USER.Trim().ToUpper(),
								AgentHelp.getResCode(ictData.RESOURCE.Trim().ToUpper()),
								product,
								errorinfor, 
								"")));
				
							if (messages.IsSuccess())
							{
								this._domainDataProvider.CommitTransaction();

								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Success,string.Format("{0} $CS_NGSUCCESS",ictData.RCARD.ToUpper())) );
								returnMsg.Add( new UserControl.Message(" "));
								BenQGuru.eMES.Common.Log.Info(string.Format("{0} $CS_NGSUCCESS： {1}",ictData.RCARD.Trim().ToUpper(),"OK"));
								messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_NGSUCCESS,$CS_Param_ID:{0}",ictData.RCARD.ToUpper())));
							}
							else
							{
								this._domainDataProvider.RollbackTransaction();

								string errorMsg = string.Format("{0} $CS_NGFail ： {1}",ictData.RCARD.Trim().ToUpper(),AgentHelp.GetErrorMessage(messages));
								returnMsg.Add( new UserControl.Message(UserControl.MessageType.Error,errorMsg) );
								returnMsg.Add( new UserControl.Message(" "));
								BenQGuru.eMES.Common.Log.Info(errorMsg);
							}
						}
						catch ( Exception ex )
						{
							this._domainDataProvider.RollbackTransaction();
							BenQGuru.eMES.Common.Log.Info(AgentHelp.GetErrorMessage(messages),ex);
						}
						finally
						{
//							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
//							((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.AutoCloseConnection = true;
						}
					}
				}
			}
			catch
			{}

			return returnMsg;
		}

		private object[] GetErrorInfor(ICTData ictData)
		{
			string[] errorList = ictData.ERRORCODES.Split('|');
			ArrayList ecg2ecList = new ArrayList();
			for (int i=0 ; i<errorList.Length; i++)
			{
				if(errorList[i].Trim() == string.Empty)continue;

				bool hasShort = false;	//是否有短路不良
				bool hasOpen = false;   //是否有开路不良
				string[] deError = errorList[i].Split(';');
				if(deError != null && deError.Length>0)
				{
					for(int j=0;j<deError.Length;j++)
					{
						if(deError[j].Trim()==string.Empty)continue;

						string[] ldeErrorInfo=deError[j].Split(',');
						TSErrorCode2Location tsinfo = new TSErrorCode2Location();
						object[] ecg2ec = this.QueryECG2ECByECode(ldeErrorInfo[0]);
						tsinfo.ErrorCode = ldeErrorInfo[0];

						if(tsinfo.ErrorCode.IndexOf("PVIB") > -1)
						{
							//OPEN Error
							if(hasShort)continue;
						}
						else if(tsinfo.ErrorCode.IndexOf("PVIA") > -1)
						{
							//Short Error
							if(hasOpen)continue;
						}


						tsinfo.ErrorLocation = getErrorLocation(ldeErrorInfo[1]);
						tsinfo.AB = "A";
						if(ecg2ec!=null && ecg2ec.Length>0)
						{
							tsinfo.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)ecg2ec[0]).ErrorCodeGroup;
						}
						else
						{
							tsinfo.ErrorCodeGroup = ldeErrorInfo[0] + "GROUP";	//如果没有不良代码组，填入默认值
						}
						
						ecg2ecList.Add(tsinfo);
						if(tsinfo.ErrorCode.IndexOf("PVIB") > -1)
						{
							//OPEN Error
							hasShort = true;
						}
						else if(tsinfo.ErrorCode.IndexOf("PVIA") > -1)
						{
							//Short Error
							hasOpen = true;
						}
					}
				}
			}

			return (TSErrorCode2Location[])ecg2ecList.ToArray(typeof(TSErrorCode2Location));
		}

		private string getErrorLocation(string eclocation)
		{
			if(eclocation.Length > 40)
			{
				return eclocation.Substring(0,40).Trim();
			}
			return eclocation;
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

		//根据不良代码查询不良代码组和不良代码
		private object[] QueryECG2ECByECode(string ecode)
		{
			try
			{
				string sql = string.Format("select tblecg2ec.* from tblecg2ec where ecode = '{0}'",ecode);
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
//			string mocode = DateTime.Now.Year.ToString() + "-" + sn.Substring(1,5);
//			return mocode;

			//Laws Lu,2006/12/26 自动归属工单可配置 
			string index = System.Configuration.ConfigurationSettings.AppSettings["AutoGoMOIndex"];
			string len = System.Configuration.ConfigurationSettings.AppSettings["AutoGoMOLen"];

			// Added by Icyer 2006/07/06
			if (sn.Length < Convert.ToInt32(index) + Convert.ToInt32(len) - 1)
			{
				throw new  Exception("$Format_Error");
			}
			// Added end

			string mocode = DateTime.Now.Year.ToString()+"-"+ sn.Substring( Convert.ToInt32(index)-1, Convert.ToInt32(len) );
			//产品序列号的第二位到第六位是工单号码”，也就是说它的产品序列号中包含了5位工单编码
			//MES系统中的工单编码是“年（4位）＋分隔符（－）＋ERP中的工单号码（5位）”因此在解析产品序列号得到5位工单编码后在前面增加当前日期对应的年（4位），再加上分隔符（－）构成新的工单编码（就是MES系统中的工单号码）
			//string mocode = DateTime.Now.Year.ToString() + "-" + sn.Substring(1,5);
			return mocode;
		}

		#endregion

		#region 数据检查

		private UserControl.Messages CheckData(object[] parserObjs)
		{
			UserControl.Messages returnMsg = new Messages();
			
			if(parserObjs!=null && parserObjs.Length>0)
			{
				foreach(object obj in parserObjs)
				{
					ICTData ictData = obj as ICTData;

					//抽检结果
					if(ictData.RESULT == string.Empty)
					{
						returnMsg.Add(new UserControl.Message(UserControl.MessageType.Error,"ICT HAS NO RESULT!"));
					}

					//用户检查
					if(ictData.USER == string.Empty)
					{
						returnMsg.Add(new UserControl.Message(UserControl.MessageType.Error,"USERNAME IS EMPTY!"));
					}

					//资源检查
					if(ictData.RESOURCE == string.Empty)
					{
						returnMsg.Add(new UserControl.Message(UserControl.MessageType.Error,"RESOURECODE IS EMPTY!"));
					}
				
				}
			}

			return returnMsg;
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
