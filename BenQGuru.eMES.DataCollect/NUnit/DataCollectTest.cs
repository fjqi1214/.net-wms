#region system 
using System;
using System.Data;
using NUnit.Framework;
using System.IO;
using System.Xml;
#endregion

#region project
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;
using UserControl;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
#endregion

namespace BenQGuru.eMES.DataCollect.UnitTest
{
	/// <summary>
	/// DataCollectTest 的摘要说明。
	/// </summary>
	[TestFixture]
	public class DataCollectTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private ActionOnLineHelper dataCollect = null;
		private IDomainDataProvider _domainDataProvider;
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


		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			dataCollect = new ActionOnLineHelper(DataProvider);
		}

		public Messages NormalOneDataCollect(XmlNode node)
		{	
			string action=node.Attributes["action"].Value;
			string card=node["code"].InnerText;			
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			string data=node["data"].InnerText;
			string[] ss=data.Split(';');
			object[] datas=new object[ss.Length];
			for (int i=0;i<ss.Length;i++)
			{
				datas[i]=ss[i];
			}
			Messages productmessages=new Messages ();
			try
			{
				//数据准备
				productmessages.AddMessages( dataCollect.GetIDInfo(card));
				ProductInfo product;				
				if (productmessages.IsSuccess())
				{
					product=(ProductInfo)productmessages.GetData().Values[0];
				}
				else
				{
					return productmessages;
				}
				ActionEventArgs eventArgs;
				switch (action)
				{
					case ActionType.DataCollectAction_OQCLotAddID :
						eventArgs =new OQCLotAddIDEventArgs(action,card,usr,res,data,OQCLotType.OQCLotType_Normal,false,5000,true,product );
						productmessages.AddMessages( dataCollect.Action(eventArgs));
						break;
					default:
						eventArgs =new ActionEventArgs(card,action,	res,usr,product,datas);
						break;

				}
				
				
				//检查数据
				if (productmessages.IsSuccess())
				{
					DataCollectFacade df=new DataCollectFacade();
					Simulation s2=(Simulation)df.GetSimulation(card);
					if (s2==null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"天拉"));
						return productmessages;
					}					
				}
				else
				{
					return productmessages;
				}
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
		}

		public Messages CKeyPartsCollect(XmlNode node)
		{	
			string action=node.Attributes["action"].Value;
			string card=node["code"].InnerText;			
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			string data=node["data"].InnerText;
			string[] ss=data.Split(';');
			string MOSplitRule=node["MOSplitRule"].InnerText;
			
			Messages productmessages=new Messages ();
			try
			{
				//数据准备
				productmessages.AddMessages( dataCollect.GetIDInfo(card));
				ProductInfo product;				
				if (productmessages.IsSuccess())
				{
					product=(ProductInfo)productmessages.GetData().Values[0];
				}
				else
				{
					return productmessages;
				}
				
				#region 先推途程，然后找OPBOM
				
				
				productmessages.AddMessages(  dataCollect.CheckID(new CKeypartsActionEventArgs(
					ActionType.DataCollectAction_CollectKeyParts,card,usr,
							res,product,null)));
				
					if (productmessages.IsSuccess())
					{			
						
					}
					else
					{
						
						return productmessages;					
					}
				OPBomKeyparts opBomKeyparts;
				try
				{
					 BenQGuru.eMES.Common.Domain.IDomainDataProvider _provider= _provider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();      
					OPBOMFacade opBOMFacade=new OPBOMFacade(_provider);
					opBomKeyparts=new OPBomKeyparts(opBOMFacade.GetOPBOMDetails(product.NowSimulation.MOCode,
						product.NowSimulation.RouteCode,product.NowSimulation.OPCode),Convert.ToInt32(MOSplitRule),this.DataProvider);
					if (opBomKeyparts.Count==0)
					{
						productmessages.Add(new Message( "$CS_NOOPBomInfo $CS_Param_MOCode="+product.NowSimulation.MOCode
							+" $CS_Param_RouteCode="+product.NowSimulation.RouteCode
							+" $CS_Param_OPCode="+product.NowSimulation.OPCode));
						
						return productmessages;
					}				
					#endregion
          
					for (int i=0;i<ss.Length;i++)
					{
						opBomKeyparts.AddKeyparts(ss[i].ToUpper());				
					}
				}
				catch (Exception e)
				{
					productmessages.Add(new Message(e));
					return productmessages;
				}
				productmessages.AddMessages( dataCollect.Action(new CKeypartsActionEventArgs(action,card,
					usr,res,product,opBomKeyparts)));
				//检查数据
				if (productmessages.IsSuccess())
				{
					DataCollectFacade df=new DataCollectFacade();
					Simulation s2=(Simulation)df.GetSimulation(card);
					if (s2==null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"天拉"));
						return productmessages;
					}
				}
				else
				{
					return productmessages;
				}
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
		}

		public Messages CINNOCollect(XmlNode node)
		{	
			string action=node.Attributes["action"].Value;
			string card=node["code"].InnerText;			
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			string data=node["data"].InnerText;
			

			Messages productmessages=new Messages ();
			try
			{
				//数据准备
				productmessages.AddMessages( dataCollect.GetIDInfo(card));
				ProductInfo product;				
				if (productmessages.IsSuccess())
				{
					product=(ProductInfo)productmessages.GetData().Values[0];
				}
				else
				{
					return productmessages;
				}
				productmessages.AddMessages( dataCollect.Action(new CINNOActionEventArgs(action,card,
					usr,res,product,data)));
				//检查数据
				if (productmessages.IsSuccess())
				{
					DataCollectFacade df=new DataCollectFacade();
					Simulation s2=(Simulation)df.GetSimulation(card);
					if (s2==null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"天拉"));
						return productmessages;
					}
					//object[] oj=df.QueryOnWIPItem(s2.RunningCard,Convert.ToInt32(s2.SourceCardSequence),"",0,0,1000);
					DataSet ds= this.persistBroker.Query("select RCARD from tblonwipitem where RCARD='"+s2.RunningCard+"'");
					if (ds==null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"OnWIPItem 保存失败"));
						return productmessages;
					}
					if (ds.Tables[0].Rows.Count ==0)
					{
						productmessages.Add(new Message(MessageType.Error,
							"OnWIPItem 保存失败"));
						return productmessages;
					}

				}
				else
				{
					return productmessages;
				}
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
		}

		public Messages Good(XmlNode node)
		{			
			string card=node["code"].InnerText;			
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			Messages productmessages=new Messages ();
			try
			{
				productmessages.AddMessages( dataCollect.GetIDInfo(card));
				ProductInfo product;				
				if (productmessages.IsSuccess())
				{
					product=(ProductInfo)productmessages.GetData().Values[0];
				}
				else
				{
					return productmessages;
				}
				productmessages.AddMessages(  new ActionFactory().CreateAction(ActionType.DataCollectAction_GOOD).Execute(new ActionEventArgs(ActionType.DataCollectAction_GOOD,
					card,usr,res,product)));
				if (productmessages.IsSuccess())
				{
					DataCollectFacade df=new DataCollectFacade();
					Simulation s2=(Simulation)df.GetSimulation(card);
					if (s2==null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"天拉"));
						return productmessages;
					}
				}
				else
				{
					return productmessages;
				}
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}

		}

		public Messages GOMO(XmlNode node)
		{				
			string card=node["code"].InnerText.ToUpper();			
			string res=node["resource"].InnerText.ToUpper();
			string usr=node["user"].InnerText.ToUpper();
			string mocode=node["mo"].InnerText.ToUpper();
			Messages productmessages=new Messages ();
			try
			{
				//数据准备
				productmessages.AddMessages( dataCollect.GetIDInfo(card));
				ProductInfo product;				
				if (productmessages.IsSuccess())
				{
					product=(ProductInfo)productmessages.GetData().Values[0];
				}
				else
				{					
					return productmessages;
				}
				productmessages.AddMessages( dataCollect.Action(new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO,
					card,usr,res,product,mocode)));
				//检查数据
				if (productmessages.IsSuccess())
				{
					DataCollectFacade df=new DataCollectFacade();
					Simulation s2=(Simulation)df.GetSimulation(card);
					if (s2==null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"天拉"));
						return productmessages;
					}					
				}
				else
				{
					return productmessages;
				}
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
		}

		public Messages Split(XmlNode node)
		{	
			string action=node.Attributes["action"].Value;
			string card=node["code"].InnerText;			
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			string data=node["data"].InnerText;
			string[] ss=data.Split(';');
			object[] datas=new object[ss.Length];
			for (int i=0;i<ss.Length;i++)
			{
				datas[i]=ss[i];
			}
			Messages productmessages=new Messages ();
			try
			{
				//数据准备
				productmessages.AddMessages( dataCollect.GetIDInfo(card));
				ProductInfo product;				
				if (productmessages.IsSuccess())
				{
					product=(ProductInfo)productmessages.GetData().Values[0];
				}
				else
				{
					return productmessages;
				}
				productmessages.AddMessages( new ActionFactory().CreateAction(action).Execute(
					new SplitIDActionEventArgs(action,card,
					usr,res,product,datas,IDMergeType.IDMERGETYPE_ROUTER)));
				//检查数据
				if (productmessages.IsSuccess())
				{
					DataCollectFacade df=new DataCollectFacade();
					Simulation s2=(Simulation)df.GetSimulation(card);
					if (s2!=null)
					{
						productmessages.Add(new Message(MessageType.Error,
							"split后SIMULATION数据未删除"+s2.ToString()));
						return productmessages;
					}
					for (int i=0;i<datas.Length;i++)
					{
						Simulation s3=(Simulation)df.GetSimulation(datas[i].ToString());
						if (s3==null)
						{
							productmessages.Add(new Message(MessageType.Error,
								"split后SIMULATION数据未成功添加"+datas[i].ToString()));
							return productmessages;
						}
					}
				}
				else
				{
					return productmessages;
				}
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
		}
		public Messages DataCollect(string testCaseFilePath)
		{
			return DataCollect(testCaseFilePath,false);
				   
		}

		public Messages DataCollect(string testCaseFilePath,bool SaveXMl)
		{
			XmlDocument xml=new XmlDocument();
			xml.Load(testCaseFilePath);
			XmlNode test= xml.SelectSingleNode("/doc/Test"); 
			string testid=test["ID"].InnerText;
			XmlNode param= xml.SelectSingleNode("/doc/Params"); 
			if (param==null)
				throw new Exception("param error");
			for (int k=0;k<param.ChildNodes.Count;k++)
			{
				if (param.ChildNodes[k].Attributes["type"].Value =="Auto")
				param.ChildNodes[k].InnerText =(Convert.ToInt32( param.ChildNodes[k].InnerText)+1).ToString();
			}
			//连接数据库
			dataCollect.GetIDInfo("001");
			
			xml.Save(testCaseFilePath); 

			XmlNodeList testCases=xml.SelectNodes("/doc/testCases/testCase");
			Messages messages=new Messages();
			for (int i=0;i<testCases.Count;i++)
			{
				XmlNode sourceNode=testCases[i];
				XmlNode node=sourceNode.Clone();
				try
				{
					
					for (int j=0;j<node.ChildNodes.Count;j++)
					{
						//node.ChildNodes[j].InnerText =						
						for (int k=0;k<param.ChildNodes.Count;k++)
						{							
							node.ChildNodes[j].InnerText=node.ChildNodes[j].InnerText.Replace("{"+param.ChildNodes[k].Name+"}",param.ChildNodes[k].InnerText); 
						}
					}
					
					#region 执行操作
					switch (node.Attributes["action"].Value.ToString())
					{
						case ActionType.DataCollectAction_GOOD :
							messages=(Good(node));
							break;
						case ActionType.DataCollectAction_NG :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_Reject :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_GoMO:
							messages=(GOMO(node));
							break;
						case ActionType.DataCollectAction_SMTGOOD :
							messages=(Good(node));
							break;				
						case ActionType.DataCollectAction_SMTNG :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_CollectINNO :
							messages=CINNOCollect(node);
							break;
						case ActionType.DataCollectAction_CollectKeyParts :
							messages=CKeyPartsCollect(node);
							break;
						case ActionType.DataCollectAction_ECN :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_TRY :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_SoftINFO :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_Split :
							messages=Split(node);
							break;
						case ActionType.DataCollectAction_OutLineGood :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_OutLineNG :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_OQCLotAddID :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_OQCLotRemoveID :
							messages=NormalOneDataCollect(node);
							break;
						case ActionType.DataCollectAction_IDTran :
							messages=IDTran(node);
							break;
						case ActionType.DataCollectAction_OQCPass :
							messages=OQC(node);
							break;

						default:
							throw new Exception("$CS_SystemError_CheckIDNotSupportAction"+node.Attributes["action"].ToString());
					
					}
					#endregion
					#region 检查流程、性能
					bool g=true;
					string output=messages.OutPut();
					if (output!=node["debug"].InnerText )
					{
						Console.WriteLine(node.Attributes["id"].Value +" Out Error");
						Console.WriteLine(node["debug"].InnerText);
						Console.WriteLine(output);
						g=false;
					}
//					string d=messages.functionList().Replace("。","");
//					if (node["funList"].InnerText !=d)
//					{
//						Console.WriteLine(node.Attributes["id"].Value +" debug Error");
//						Console.WriteLine(node["funList"].InnerText);
//						Console.WriteLine(d);
//						sourceNode["funList"].InnerText=d;
//						g=false;
//					}
					for (int n=messages.Count();n>0;n--)
					{
						if (messages.Objects(n-1).Type ==MessageType.Performance )
						{
							double f=(double)messages.Objects(n-1).Values[0];
							int f1=Convert.ToInt32( node["performance"].InnerText);
						
							if (f>f1)
							{
								Console.WriteLine(node.Attributes["id"].Value +" Performance Error");
								Console.WriteLine(f1.ToString());
								Console.WriteLine(f.ToString() +"  "+messages.Objects(n-1).Body);
							}
							break;
						}
						g=false;
					}
					if (!g)
					{
						StreamWriter fs= File.CreateText (@"TEST\DEBUG\"+testid+node.Attributes["id"].Value+".txt");
						fs.WriteLine(node.OuterXml);
						string[] message=messages.Debug().Split('。');
						for (int m=0;m<message.Length;m++)
							fs.WriteLine((message[m]));
						fs.Flush();
						fs.Close();
						
					}
					#endregion
				}
				catch (Exception e)
				{
					Console.WriteLine(node.OuterXml);
					throw new Exception("error",e);;
				}
			}
			if (SaveXMl)
			  xml.Save(testCaseFilePath);
			return messages;
		}
		
		public Messages IDTran(XmlNode node)
		{	
			string action=node.Attributes["action"].Value;
			string card=node["code"].InnerText;			
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			string newID=node["newID"].InnerText;
			string imei=node["imei"].InnerText;
			Messages productmessages=new Messages ();
			try
			{
				DataCollectFacade dataFacade=new DataCollectFacade();
				string info= dataFacade.SaveTransferInfo(card,res,usr,newID,imei,0);
				if (info!="OK")
					productmessages.Add(new Message(MessageType.Error,info));	
				
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
			
		}
		
		

		public Messages OQC(XmlNode node)
		{
			string action=node.Attributes["action"].Value;
			string card=node["code"].InnerText;
			string res=node["resource"].InnerText;
			string usr=node["user"].InnerText;
			Messages productmessages=new Messages ();
			try
			{
				OQCPASSEventArgs earg=new OQCPASSEventArgs(action,string.Empty,usr,res,card,null);

				productmessages.AddMessages( dataCollect.Action(earg));
				
				return productmessages;
			}
			catch (Exception e)
			{
				productmessages.Add(new Message(e));
				return productmessages;
			}
		}
		[Test]
		public void TestDataCollect()
		{
			Messages messages=DataCollect(@"TEST\Test.xml");
		}
		[Test]
		public void TestCollectItem()
		{
			Messages messages=DataCollect(@"TEST\TestItem.xml");
		}
		[Test]
		public void TestDataCollectSaveDebugChange()
		{
			Messages messages=DataCollect(@"TEST\Test.xml",true);
		}
	}
}
