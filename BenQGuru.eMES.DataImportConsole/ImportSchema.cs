using System;
using System.Data;
using System.Collections;
using System.Xml;
using System.IO;
using System.Reflection;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.DataImportConsole
{
	/// <summary>
	/// 解析XML
	/// </summary>
	public class ImportSchema
	{
		private string FilePath
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"DataImportSchema.xml");
			}
		}

		private XmlDocument xmlDoc = new XmlDocument();
		private XmlDocument ImportXmlDocument
		{
			get
			{
				return xmlDoc;
			}
		}

		public string ImportUser
		{
			get
			{
				return System.Configuration.ConfigurationSettings.AppSettings["User"].ToString();
			}
		}

		public string Factory
		{
			get
			{
				return System.Configuration.ConfigurationSettings.AppSettings["FormFactory"].ToString();
			}
		}

		public string[] ImportSequence
		{
			get
			{
				return GetImportSequence();
			}
		}

		private Hashtable moHt = new Hashtable();

		/// <summary>
		/// 导入顺序
		/// </summary>
		/// <returns></returns>
		private string[] GetImportSequence()
		{
			ImportXmlDocument.Load( this.FilePath );
			XmlNodeList nodeList = ImportXmlDocument.GetElementsByTagName("object");

			string[] importSequence = null;
			if(nodeList.Count>0)
			{
				importSequence = new string[nodeList.Count];
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					int sequence = Convert.ToInt32( node.Attributes["Sequene"].InnerText.ToString() );
					string importType = node.Attributes["Name"].InnerText.ToString();
					importSequence.SetValue( importType, sequence-1 );
				}
			}
			return importSequence;
		}


		/// <summary>
		/// 对应的字段
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private string MatchField( string type )
		{
			ImportXmlDocument.Load( this.FilePath );
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("FieldMapAttribute");

			string matchfield = string.Empty;
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( string.Compare( node.ParentNode.Attributes["Name"].InnerText.ToString(), type, true)==0 )
					{
						string fromfield = node.Attributes["Fromfield"].InnerText.ToString();
						//string tofield = node.Attributes["ToField"].InnerText.ToString();
						matchfield += string.Format(" {0} ,", fromfield) ;								
					}
				}
			}
			return matchfield.TrimEnd(',');
		}


		/// <summary>
		/// 来源表
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private string FromTable( string type )
		{
			ImportXmlDocument.Load( this.FilePath );
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("object");
			string fromtable = string.Empty;
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( string.Compare( node.Attributes["Name"].InnerText.ToString(), type, true)==0 )
					{
						fromtable = node.Attributes["FromTableName"].InnerText.ToString();
						break;	
					}
				}
			}
			return fromtable;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private string FactoryField( string type )
		{
			ImportXmlDocument.Load( this.FilePath );
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("object");
			string factoryfield = string.Empty;
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( string.Compare( node.Attributes["Name"].InnerText.ToString(), type, true)==0 )
					{
						factoryfield = node.Attributes["FactoryField"].InnerText.ToString();
						break;	
					}
				}
			}
			return factoryfield;
		}
		public string BuildQuerySQL( string type )
		{
			string filed = MatchField( type );
			string table = FromTable( type );
			string factoryfield = FactoryField( type );
			long seq = 0;
			if( string.Compare(type, "Model2Item", true)==0 )
			{
				return string.Format("SELECT {0} FROM {1} where {2}='{3}' and imodl <> ' '", filed, table, factoryfield, this.Factory);
			}
			if( string.Compare(type, "ERPBOM", true)==0 )
			{
				BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider mesDomainDataProvider = 
					BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
					as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
				
				try
				{
					seq = mesDomainDataProvider.GetCount(new BenQGuru.eMES.Common.Domain.SQLCondition("select max(SEQUENCE)  from tblerpbom"));
				}
				catch{}

				return string.Format("SELECT {0} FROM {1} where {2}='{3}' and serialno > {4}", filed, table, factoryfield, this.Factory,seq);
				//return string.Format("SELECT {0} FROM {1} where {2}='{3}' and serialno > 1846699", filed, table, factoryfield, this.Factory,seq);
			}

			return string.Format("SELECT {0} FROM {1} where {2}='{3}'", filed, table, factoryfield, this.Factory);
		}

		public string BuildDeleteSQL( string type )
		{
			string table = FromTable( type );
			string factoryfield = FactoryField( type );
			return string.Format("delete from {0} where {1}='{2}'", table, factoryfield, this.Factory);
		}

		public System.Type ImportType( string type )
		{
			switch( type )
			{
				case "Item":
					return typeof(BenQGuru.eMES.DataImportConsole.ImpItem);
				case "Model2Item":
					return typeof(BenQGuru.eMES.DataImportConsole.ImpModel2Item);
				case "SBOM":
					return typeof(BenQGuru.eMES.DataImportConsole.ImpSBOM);
				case "MO":
					return typeof(BenQGuru.eMES.DataImportConsole.ImpMO);
				case "MOBOM":
					return typeof(BenQGuru.eMES.DataImportConsole.ImpMOBOM);
				case "ERPBOM":
					return typeof(BenQGuru.eMES.DataImportConsole.ImpERPBOM);
				default:
					return typeof(System.Object);
			}
		}

		/// <summary>
		/// 补齐没有来源又必填的栏位
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public object FillImportObject(object obj,string type)
		{
			if( obj==null )
			{
				return null;
			}
			
			switch(type)
			{
				case "Item":
					#region Item
					ImpItem impitem = obj as ImpItem;
					BenQGuru.eMES.Domain.MOModel.Item item = new BenQGuru.eMES.Domain.MOModel.Item();

					item.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impitem.ItemCode) );
					item.ItemName = FormatHelper.CleanString(impitem.ItemName);
					//item.ItemType = FormatHelper.CleanString(impitem.ItemType);
					item.ItemDescription = FormatHelper.CleanString(impitem.ItemDescription);
					item.ItemUOM = FormatHelper.CleanString(impitem.ItemUOM);
					if( string.Compare(FormatHelper.CleanString(impitem.ItemType),"1",true)==0 )
					{
						item.ItemType = ItemType.ITEMTYPE_FINISHEDPRODUCT ;
					}
					else
					{
						item.ItemType = ItemType.ITEMTYPE_SEMIMANUFACTURE ;
					}

					item.MaintainUser = ImportUser;
					item.MaintainDate = FormatHelper.TODateInt( DateTime.Now );
					item.MaintainTime = FormatHelper.TOTimeInt( DateTime.Now );
					item.ItemUser = ImportUser;
					item.ItemDate = item.MaintainDate;
                    item.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
					return item;
					#endregion
				case "Model2Item":
					#region Model2Item
					ImpModel2Item impmodel2item = obj as ImpModel2Item;
					BenQGuru.eMES.Domain.MOModel.Model2Item model2Item = new BenQGuru.eMES.Domain.MOModel.Model2Item();

					model2Item.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impmodel2item.ItemCode) );
					model2Item.ModelCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impmodel2item.ModelCode) );

					model2Item.MaintainUser = ImportUser;
					model2Item.MaintainDate = FormatHelper.TODateInt( DateTime.Now );
					model2Item.MaintainTime = FormatHelper.TOTimeInt( DateTime.Now );
                    model2Item.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
					return model2Item;
					#endregion
				case "SBOM":
					#region SBOM
					ImpSBOM impsbom = obj as ImpSBOM;
					BenQGuru.eMES.Domain.MOModel.SBOM sbom= new BenQGuru.eMES.Domain.MOModel.SBOM ();

					sbom.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impsbom.ItemCode) ) ; 
					sbom.SBOMItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impsbom.SBOMItemCode) ) ;
					sbom.SBOMItemECN = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impsbom.SBOMItemECN) ) ;
					sbom.SBOMItemEffectiveDate = impsbom.SBOMItemEffectiveDate ;
					sbom.SBOMItemInvalidDate = impsbom.SBOMItemInvalidDate ;
					sbom.SBOMItemLocation = FormatHelper.CleanString(impsbom.SBOMItemLocation) ;
					sbom.SBOMItemName = FormatHelper.CleanString(impsbom.SBOMItemName) ;
					sbom.SBOMItemQty = impsbom.SBOMItemQty ;
					sbom.SBOMItemUOM = FormatHelper.CleanString(impsbom.SBOMItemUOM) ;
					sbom.SBOMSourceItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impsbom.SBOMSourceItemCode) ) ; 
					sbom.SBOMWH = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impsbom.SBOMWH) ) ; 
					sbom.SBOMParentItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impsbom.SBOMParentItemCode) ) ; 
					
					sbom.Sequence = 0;
					sbom.SBOMItemControlType = ItemControlType.ITEMCONTROLTYPE_LOT;
					sbom.SBOMItemStatus = "0";
					sbom.MaintainUser = ImportUser;
					sbom.MaintainDate = FormatHelper.TODateInt( DateTime.Now );
					sbom.MaintainTime = FormatHelper.TOTimeInt( DateTime.Now );
                    sbom.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
					return sbom;
					#endregion
				case "MO":
					#region MO
					ImpMO impmo = obj as ImpMO;
					BenQGuru.eMES.Domain.MOModel.MO mo= new BenQGuru.eMES.Domain.MOModel.MO();

					mo.MOCode =  FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmo.MOCode ) ) ;
				
//					switch( FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impmo.MOType) ) )
//					{
//						case "N":
//						case "F":
//							mo.MOType = MOType.MOTYPE_NORMALMOTYPE;
//							break;
//						case "R":
//							mo.MOType = MOType.MOTYPE_REWORKMOTYPE;
//							break;
//						default:
//							mo.MOType = MOType.MOTYPE_NORMALMOTYPE;
//							break;
//					}

					mo.MOType = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(impmo.MOType) );
				
					mo.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmo.ItemCode ) ) ;
					mo.MOMemo = FormatHelper.CleanString( impmo.MOMemo ) ;
					mo.MOPlanEndDate =  impmo.MOPlanEndDate ;
					mo.MOPlanQty = impmo.MOPlanQty ;
					mo.MOPlanStartDate = impmo.MOPlanStartDate ;
					mo.Factory = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmo.Factory ) ) ;
					mo.CustomerCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmo.CustomerCode ) ) ;
					mo.CustomerOrderNO = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmo.CustomerOrderNO ) ) ;

					mo.MOStatus = MOManufactureStatus.MOSTATUS_INITIAL;
					mo.MOVersion = "1.0";
					mo.IDMergeRule = 1;
					mo.IsControlInput = FormatHelper.TRUE_STRING;
					mo.IsCompareSoft = 0;
					mo.IsBOMPass = FormatHelper.FALSE_STRING;
					mo.MOImportDate = FormatHelper.TODateInt( DateTime.Now );
					mo.MOImportTime = FormatHelper.TOTimeInt( DateTime.Now );
					mo.MODownloadDate = FormatHelper.TODateInt( DateTime.Now );
					mo.MaintainUser = ImportUser;
					mo.MaintainDate = FormatHelper.TODateInt( DateTime.Now );
					mo.MaintainTime = FormatHelper.TOTimeInt( DateTime.Now );

                    mo.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

					return mo;
					#endregion 
				case "MOBOM":
					#region MOBOM
					ImpMOBOM impmobom = obj as ImpMOBOM;
					BenQGuru.eMES.Domain.MOModel.MOBOM mobom= new BenQGuru.eMES.Domain.MOModel.MOBOM();

					mobom.MOCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmobom.MOCode ) ) ;
					mobom.MOBOMItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmobom.MOBOMItemCode ));
					mobom.MOBOMItemName = impmobom.MOBOMItemName;
					mobom.MOBOMItemQty = impmobom.MOBOMItemQty;
					mobom.MOBOMItemUOM = FormatHelper.CleanString( impmobom.MOBOMItemUOM );
					mobom.MOBOMSourceItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( impmobom.MOBOMSourceItemCode ));

					mobom.MOBOMItemEffectiveDate = FormatHelper.TODateInt( DateTime.Now );
					mobom.MOBOMItemEffectiveTime = FormatHelper.TOTimeInt( DateTime.Now );
					mobom.MOBOMItemInvalidDate = FormatHelper.TODateInt( DateTime.MaxValue );
					mobom.MOBOMItemInvalidTime = FormatHelper.TOTimeInt( DateTime.MaxValue );
					mobom.MOBOMItemStatus = "0";
					mobom.Sequence = 1;

					mobom.MaintainUser = ImportUser;
					mobom.MaintainDate = FormatHelper.TODateInt( DateTime.Now );
					mobom.MaintainTime = FormatHelper.TOTimeInt( DateTime.Now );
                    
					return mobom;
					#endregion
				case "ERPBOM":
					#region ERPBOM
					ImpERPBOM imperpbom = obj as ImpERPBOM;
					BenQGuru.eMES.Domain.MOModel.ERPBOM erpbom = new BenQGuru.eMES.Domain.MOModel.ERPBOM();

					erpbom.MOCODE = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( imperpbom.MOCODE ) ) ;
					erpbom.BITEMCODE = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( imperpbom.BITEMCODE ));
					erpbom.BQTY = imperpbom.BQTY;
					erpbom.LOTNO = imperpbom.LOTNO;
				
					erpbom.SEQUENCE = imperpbom.SEQUENCE;

					erpbom.MaintainUser = ImportUser;
					erpbom.MaintainDate = FormatHelper.TODateInt( DateTime.Now );
					erpbom.MaintainTime = FormatHelper.TOTimeInt( DateTime.Now );
					return erpbom;
					#endregion
				default:
					return null;
			}
		}
	}
}
