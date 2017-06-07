#region System
using System;
using System.Text;
using System.Runtime.Remoting;  
using System.Collections;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Rework;
#endregion
 

namespace BenQGuru.eMES.SAPData
{
	/// <summary>
	/// SAPImportJob 执行计划
	/// </summary>
	public class SAPImportJob
	{
		private object[] sapItems;

		private int page_exclusive = 500;

		private SAPImportLoger importLogger; // 写文本日志,每次执行Job都会创建一个文件

		public SAPImportJob()
		{
			importLogger = new SAPImportLoger();
		}

		public void ImportData()
		{
			this.Begin();		//开始
			this.CheckData();	//数据检查
			this.Running();		//运行
			this.Logging();		//写日志
			this.End();			//结束
		}
		
		//开始
		private void Begin()
		{
			System.Console.WriteLine("准备从SAP取数据");
			importLogger.Write("准备从SAP取数据");
			
		}

		//数据检查
		private bool CheckData()
		{
			System.Console.WriteLine("正在执行数据检查");
			return false;
		}

		//运行
		private void Running()
		{
			
			// 先导入工单,再导入工单BOM,最后导BOM
			this.Import(DataName.SAPMO);		//导入工单							
			this.Import(DataName.SAPMOBom);		//导入工单bom
			this.Import(DataName.SAPBom);		//导入BOM	

		}

		#region 导入数据 按分页导入

		
		private void Import(DataName _dataName)
		{
			DateTime jobLogStartTime  = DateTime.Now;		//JobLog日志开始时间
			#region	从sap数据库获取数据
			//从sap数据库获取数据
			IDomainDataProvider _sapDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.SAP);
			IDomainDataProvider _mesDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.MES);

			if(_sapDataProvider!=null && ((SQLDomainDataProvider)_sapDataProvider).PersistBroker != null)
			{
				importLogger.Write(string.Format("已经连接到SAP的数据库 {0}",_dataName));
				System.Console.WriteLine(string.Format("已经连接到SAP的数据库 {0}",_dataName));
			}
			else
			{
				importLogger.Write(string.Format("连接到SAP的数据库失败 {0}",_dataName));
				System.Console.WriteLine(string.Format("连接到SAP的数据库失败 {0}",_dataName));
			}

			if(_mesDataProvider!=null && ((SQLDomainDataProvider)_mesDataProvider).PersistBroker != null)
			{
				importLogger.Write(string.Format("已经连接到MES的数据库 {0}",_dataName));
				System.Console.WriteLine(string.Format("已经连接到MES的数据库 {0}",_dataName));
			}
			else
			{
				importLogger.Write(string.Format("连接到MES的数据库失败 {0}",_dataName));
				System.Console.WriteLine(string.Format("连接到MES的数据库失败 {0}",_dataName));
			}

			importLogger.Write("");
			System.Console.WriteLine("");

			bool isImportSuccess = true; //导入是否成功
			try
			{
				SAPDataGeter dateGetter = new SAPDataGeter(_sapDataProvider);
				int count = dateGetter.GetImportCount(_dataName);
				importLogger.Write(string.Format("获取到{0}数据 {1}条",_dataName,count.ToString()));
				
				if(count > 0)
				{
					int pageCount = (int)Math.Floor(Convert.ToDecimal(count/this.page_exclusive)) + 1; //获取总页数
					importLogger.Write(string.Format("将要分成{0}页进行导入,每页数据{1}条",pageCount.ToString(),page_exclusive.ToString()));
					importLogger.Write(string.Format("正在导入{0}数据 ",_dataName));
					System.Console.WriteLine(string.Format("正在导入{0}数据 ",_dataName));
					int SucceedNum = 0;		//成功导入的数据
					for( int i = 1;i< pageCount+1;i++ )
					{
						if(i == 1)System.Console.WriteLine(string.Format("将要分成{0}页进行导入,每页数据{1}条",pageCount.ToString(),page_exclusive.ToString()));
						SucceedNum += this.RunItemImportByPage(_dataName,_sapDataProvider,_mesDataProvider,i);
						System.Console.WriteLine(string.Format("正在导入{0}第{1}页数据",_dataName,i.ToString()));
					}
					importLogger.Write(string.Format("成功导入{0}  {1}条数据 ",_dataName,SucceedNum));
					System.Console.WriteLine(string.Format("成功导入{0}  {1}条数据 ",_dataName,SucceedNum));
					System.Console.WriteLine("");
				}

			}
			catch(Exception ex)
			{
				importLogger.Write(string.Format("导入{0}数据 出错,详细信息为{1}",_dataName,ex.Message));
				isImportSuccess = false;
			}
			finally
			{
				if(_sapDataProvider!=null)((SQLDomainDataProvider)_sapDataProvider).PersistBroker.CloseConnection();
				if(_mesDataProvider!=null)((SQLDomainDataProvider)_mesDataProvider).PersistBroker.CloseConnection();
			}

			#endregion

			//#region 写JobLog

			IDomainDataProvider _JobLogDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.SAP);

			if(_JobLogDataProvider!=null && ((SQLDomainDataProvider)_JobLogDataProvider).PersistBroker != null)
			{
				importLogger.Write("已经连接到JobLog的数据库");
				System.Console.WriteLine("已经连接到JobLog的数据库");
			}
			else
			{
				importLogger.Write("连接到JobLog的数据库失败");
				System.Console.WriteLine("连接到JobLog的数据库失败");
			}

			try
			{
				DateTime jobLogEndTime  = DateTime.Now;			//JobLog日志结束时间
				importLogger.Write(string.Format("正在写{0} JobLog",_dataName));
				SAPDataGeter JobLogWriter = new SAPDataGeter(_JobLogDataProvider);

				if(isImportSuccess)
				{
					JobLogWriter.AddSuccessJobLog(this.getJobName(_dataName),jobLogStartTime,jobLogEndTime);
				}
				else
				{
					JobLogWriter.AddFailedJobLog(this.getJobName(_dataName),jobLogStartTime,jobLogEndTime);
				}

				importLogger.Write(string.Format("写入{0} JobLog 完成",_dataName));
				importLogger.Write("");
			}
			catch(Exception ex)
			{
				importLogger.Write(string.Format("写入{0} JobLog 失败,,详细信息为{1}",_dataName,ex.Message));
			}
			finally
			{
				if(_JobLogDataProvider!=null)((SQLDomainDataProvider)_JobLogDataProvider).PersistBroker.CloseConnection();
			}

			//#endregion
		}

		private int RunItemImportByPage(DataName _dataName,IDomainDataProvider _sapDataProvider,IDomainDataProvider _mesDataProvider,int currentPage)
		{
			#region	从sap数据库获取数据
			//从sap数据库获取数据
			SAPMapper mapper = new SAPMapper();
			ArrayList mesItems  = new ArrayList();
			
			SAPDataGeter dateGetter = new SAPDataGeter(_sapDataProvider);
			int inclusive = this.page_exclusive * (currentPage-1) +1;
			int exclusive = this.page_exclusive * currentPage;
			sapItems = dateGetter.GetSAPData(_dataName,inclusive,exclusive);
			#endregion

			#region 映射数据
			//映射产品
			if(sapItems!=null && sapItems.Length>0)
			{
				mesItems = mapper.MapSAPData(this.sapItems);
			}
			#endregion

			#region 导入数据

			//导入到MES数据库
			SAPImporter sapImpoter = new SAPImporter(_mesDataProvider);
			sapImpoter.importLogger = this.importLogger;	//日志写入器
			sapImpoter.pageCountNum = currentPage;			//导入的当前页
			sapImpoter.Import(mesItems);

			if(sapItems!=null && sapItems.Length>0 && sapItems[0].GetType() == typeof(SAPBOM))
			{
				//如果是SAPBOM，特殊处理首选料问题
				sapImpoter.UpdateSBom(sapItems);
			}

			return sapImpoter.SucceedImportNum;
			#endregion
		}

		#endregion


		//写日志
		private void Logging()
		{
			System.Console.WriteLine("正在写导入日志");
		}


		//结束
		private void End()
		{
			System.Console.WriteLine("数据从SAP成功导入到MES数据库!");
			System.Console.WriteLine("导入结束");
			//System.Console.ReadLine();
		}

		public JobName getJobName(DataName _dataName)
		{
			if(_dataName == DataName.SAPMO)
			{
				return JobName.MO;
			}
			else if(_dataName == DataName.SAPMOBom)
			{
				return JobName.MOBom;
			}
			else if(_dataName == DataName.SAPBom)
			{
				return JobName.BOM;
			}

			return JobName.MO;
		}
	}

	public class LogMessage
	{
		public static string LogHead = string.Format("SAP数据导入日志 {0}",DateTime.Now.ToString());

		public static string BeginGetDataMsg = "准备从SAP取数据";
		public static string SucessGetDataMsg = "成功获取到数据";
		public static string FailGetDataMsg = "获取数据失败";

		public static string BeginImportDataMsg = "正在导入数据";
		public static string SucessImportDataMsg = "导入数据完成";
		public static string FailImportDataMsg= "导入数据失败";

		public static string SucessMsg = "数据从SAP成功导入到MES数据库";
		public static string FailMsg = "数据从SAP导入到MES数据库失败";
		public static string EndMsg = "导入结束";
	}
}
