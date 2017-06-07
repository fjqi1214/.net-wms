using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.InterfaceDomain;

namespace BenQGuru.eMES.InterfaceFacade
{
    public class MainDataFacade : MarshalByRefObject
    {
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public MainDataFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        #region DataProvider

        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public MainDataFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
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

        #endregion

        #region Common
        
        #endregion

        #region I_Sapmaterial
        /// <summary>
        /// I_SAPMATERIAL
        /// </summary>
        public I_Sapmaterial CreateNewSapmaterial()
        {
            return new I_Sapmaterial();
        }

        public void AddSapmaterial(I_Sapmaterial sapmaterial)
        {
            this.DataProvider.Insert(sapmaterial);
        }

        public void DeleteSapmaterial(I_Sapmaterial sapmaterial)
        {
            this.DataProvider.Delete(sapmaterial);
        }

        public void UpdateSapmaterial(I_Sapmaterial sapmaterial)
        {
            this.DataProvider.Update(sapmaterial);
        }

        public object GetSapmaterial(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapmaterial), new object[] { Id });
        }

        #region custom
        public void MergeMaterial()
        {
            string sql = @"MERGE INTO TBLMATERIAL c
                        USING (SELECT MCODE,DQMCODE,MUOM,ROHS,MCHSHORTDESC,MENSHORTDESC,MENLONGDESC,MCHLONGDESC,MODELCODE,MATERIALTYPE,
                        MSTATE1,MSTATE2,MSTATE3,VALIDFROM,EATTRIBUTE8,CDQTY,CDFOR,PURCHASINGGROUP,ABCINDICATOR,MRPTYPE,REORDERPOINT,
                        MRPCONTORLLER,MINIMUMLOTSIZE,ROUNDINGVALUE,SPECIALPROCYREMENT,SAFETYSTOCK,BULKMATERIAL,eattribute1,eattribute2,eattribute3 
                        FROM I_SAPMATERIAL where mesflag='W') ic
                        ON (c.MCODE=ic.MCODE)
                        WHEN MATCHED THEN
                           UPDATE SET c.DQMCODE = ic.DQMCODE,c.MUOM=ic.MUOM,c.ROHS=ic.ROHS,c.MCHSHORTDESC=ic.MCHSHORTDESC,
                           c.MENSHORTDESC=ic.MENSHORTDESC,c.MENLONGDESC=ic.MENLONGDESC,c.MCHLONGDESC=ic.MCHLONGDESC,c.MODELCODE=ic.MODELCODE,
                           c.MATERIALTYPE=ic.MATERIALTYPE,c.MSTATE1=ic.MSTATE1,c.MSTATE2=ic.MSTATE2,c.MSTATE3=ic.MSTATE3,c.VALIDFROM=ic.VALIDFROM,
                           c.EATTRIBUTE8=ic.EATTRIBUTE8,c.CDQTY=ic.CDQTY,c.CDFOR=ic.CDFOR,c.PURCHASINGGROUP=ic.PURCHASINGGROUP,
                           c.ABCINDICATOR=ic.ABCINDICATOR,c.MRPTYPE=ic.MRPTYPE,c.REORDERPOINT=ic.REORDERPOINT,c.MRPCONTORLLER=ic.MRPCONTORLLER,
                           c.MINIMUMLOTSIZE=ic.MINIMUMLOTSIZE,c.ROUNDINGVALUE=ic.ROUNDINGVALUE,c.SPECIALPROCYREMENT=ic.SPECIALPROCYREMENT,
                           c.SAFETYSTOCK=ic.SAFETYSTOCK,c.BULKMATERIAL=ic.BULKMATERIAL,
                           c.eattribute1=ic.eattribute1,c.eattribute2=ic.eattribute2,c.eattribute3=ic.eattribute3,muser='JOB',mdate=sys_date,mtime=sys_time
                        WHEN NOT MATCHED THEN 
                           INSERT (MCODE,DQMCODE,MUOM,ROHS,MCHSHORTDESC,MENSHORTDESC,MENLONGDESC,MCHLONGDESC,MODELCODE,MATERIALTYPE,
                        MSTATE1,MSTATE2,MSTATE3,VALIDFROM,EATTRIBUTE8,CDQTY,CDFOR,PURCHASINGGROUP,ABCINDICATOR,MRPTYPE,REORDERPOINT,
                        MRPCONTORLLER,MINIMUMLOTSIZE,ROUNDINGVALUE,SPECIALPROCYREMENT,SAFETYSTOCK,BULKMATERIAL,MTYPE,MSTATE,
                        eattribute1,eattribute2,eattribute3,Sourceflag,cuser,cdate,ctime,muser,mdate,mtime) 
                           VALUES(ic.MCODE,ic.DQMCODE,ic.MUOM,ic.ROHS,ic.MCHSHORTDESC,ic.MENSHORTDESC,ic.MENLONGDESC,ic.MCHLONGDESC,
                           ic.MODELCODE,ic.MATERIALTYPE,ic.MSTATE1,ic.MSTATE2,ic.MSTATE3,ic.VALIDFROM,ic.EATTRIBUTE8,ic.CDQTY,ic.CDFOR,
                           ic.PURCHASINGGROUP,ic.ABCINDICATOR,ic.MRPTYPE,ic.REORDERPOINT,ic.MRPCONTORLLER,ic.MINIMUMLOTSIZE,ic.ROUNDINGVALUE,
                           ic.SPECIALPROCYREMENT,ic.SAFETYSTOCK,ic.BULKMATERIAL,'itemtype_finishedproduct',0,
                           ic.eattribute1,ic.eattribute2,ic.eattribute3,'SAP','JOB',sys_date,sys_time,
                           'JOB',sys_date,sys_time)";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSapmaterialSuccess()
        {
            string sql = "update I_SAPMATERIAL set mesflag='S',pdate=sys_date,ptime=sys_time where mesflag='W'";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
         public void  DeleteSapmaterial(int date)
        {
            string sql = string.Format("Delete I_SAPMATERIAL  where mesflag='S' and SDATE <{0} ",date);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
         public void DeleteSapVendor(int date)
         {
             string sql = string.Format("Delete I_SAPVENDOR  where mesflag='S' and SDATE <{0} ", date);
             SQLCondition SC = new SQLCondition(sql);
             this.DataProvider.CustomExecute(SC);
         }
         public void DeleteSapCustomer(int date)
         {
             string sql = string.Format("Delete I_SAPCUSTOMER  where mesflag='S' and SDATE <{0} ", date);
             SQLCondition SC = new SQLCondition(sql);
             this.DataProvider.CustomExecute(SC);
         }
        #endregion

        #endregion

        #region Material
        /// <summary>
        /// TBLMATERIAL
        /// </summary>
        //public Material CreateNewMaterial()
        //{
        //    return new Material();
        //}

        //public void AddMaterial(Material material)
        //{
        //    this.DataProvider.Insert(material);
        //}

        //public void DeleteMaterial(Material material)
        //{
        //    this.DataProvider.Delete(material);
        //}

        //public void UpdateMaterial(Material material)
        //{
        //    this.DataProvider.Update(material);
        //}

        //public object GetMaterial(string Mcode)
        //{
        //    return this.DataProvider.CustomSearch(typeof(Material), new object[] { Mcode });
        //}

        #region custom
        public object[] GetAllMaterial()
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Material),
                new SQLCondition("select * from tblmaterial"));
        }

        public int QueryMaterialCount(string mcode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMATERIAL where mcode = '{0}' ",
                mcode)));
        }
        #endregion
        #endregion

        #region I_Sapcustomer
        /// <summary>
        /// I_SAPCUSTOMER
        /// </summary>
        public I_Sapcustomer CreateNewSapcustomer()
        {
            return new I_Sapcustomer();
        }

        public void AddSapcustomer(I_Sapcustomer sapcustomer)
        {
            this.DataProvider.Insert(sapcustomer);
        }

        public void DeleteSapcustomer(I_Sapcustomer sapcustomer)
        {
            this.DataProvider.Delete(sapcustomer);
        }

        public void UpdateSapcustomer(I_Sapcustomer sapcustomer)
        {
            this.DataProvider.Update(sapcustomer);
        }

        public object GetSapcustomer(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapcustomer), new object[] { Id });
        }

        #region custom
        public void MergeCustomer()
        {
            string sql = @"MERGE INTO tblcustomer c
                USING (SELECT customercode,customername,address,tel,flag,eattribute1,eattribute2,eattribute3 
                FROM I_SAPCUSTOMER where mesflag='W') ic
                ON ( c.customercode=ic.customercode)
                WHEN MATCHED THEN
                    UPDATE SET c.customername = ic.customername,c.address=ic.address,c.tel=ic.tel,c.flag=ic.flag,
                    c.eattribute1=ic.eattribute1,c.eattribute2=ic.eattribute2,c.eattribute3=ic.eattribute3,mdate=sys_date,mtime=sys_time
                WHEN NOT MATCHED THEN 
                    INSERT (customercode,customername,address,tel,flag,eattribute1,eattribute2,eattribute3,muser,mdate,mtime) 
                    VALUES(ic.customercode,ic.customername,ic.address,ic.tel,ic.flag,ic.eattribute1,ic.eattribute2,ic.eattribute3,
                    'JOB',sys_date,sys_time)";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSapcustomerSuccess()
        {
            string sql = "update I_SAPCUSTOMER set mesflag='S',pdate=sys_date,ptime=sys_time where mesflag='W'";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        #endregion
        #endregion

        #region Customer
        /// <summary>
        /// TBLCUSTOMER
        /// </summary>
        public Customer CreateNewCustomer()
        {
            return new Customer();
        }

        public void AddCustomer(Customer customer)
        {
            this.DataProvider.Insert(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            this.DataProvider.Delete(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            this.DataProvider.Update(customer);
        }

        public object GetCustomer(string Customercode)
        {
            return this.DataProvider.CustomSearch(typeof(Customer), new object[] { Customercode });
        }

        #endregion

        #region I_Sapvendor
        /// <summary>
        /// I_SAPVENDOR
        /// </summary>
        public I_Sapvendor CreateNewSapvendor()
        {
            return new I_Sapvendor();
        }

        public void AddSapvendor(I_Sapvendor sapvendor)
        {
            this.DataProvider.Insert(sapvendor);
        }

        public void DeleteSapvendor(I_Sapvendor sapvendor)
        {
            this.DataProvider.Delete(sapvendor);
        }

        public void UpdateSapvendor(I_Sapvendor sapvendor)
        {
            this.DataProvider.Update(sapvendor);
        }

        public object GetSapvendor(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapvendor), new object[] { Id });
        }

        #region custom
        public void MergeVendor()
        {
            string sql = @"MERGE INTO TBLVENDOR c
                        USING (SELECT VENDORCODE,VENDORNAME,ALIAS,VENDORUSER,VENDORADDR,FAXNO,MOBILENO,eattribute1,eattribute2,eattribute3 
                        FROM I_SAPVENDOR where mesflag='W') ic
                        ON ( c.VENDORCODE=ic.VENDORCODE)
                        WHEN MATCHED THEN
                            UPDATE SET c.VENDORNAME = ic.VENDORNAME,c.ALIAS=ic.ALIAS,c.VENDORUSER=ic.VENDORUSER,c.VENDORADDR=ic.VENDORADDR,
                            c.FAXNO=ic.FAXNO,c.MOBILENO=ic.MOBILENO,
                            c.eattribute1=ic.eattribute1,c.eattribute2=ic.eattribute2,c.eattribute3=ic.eattribute3,mdate=sys_date,mtime=sys_time
                        WHEN NOT MATCHED THEN 
                            INSERT (VENDORCODE,VENDORNAME,ALIAS,VENDORUSER,VENDORADDR,FAXNO,MOBILENO,eattribute1,eattribute2,eattribute3,muser,mdate,mtime) 
                            VALUES(ic.VENDORCODE,ic.VENDORNAME,ic.ALIAS,ic.VENDORUSER,ic.VENDORADDR,ic.FAXNO,ic.MOBILENO,ic.eattribute1,ic.eattribute2,ic.eattribute3,
                            'JOB',sys_date,sys_time)";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSapvendorSuccess()
        {
            string sql = "update I_SAPVENDOR set mesflag='S',pdate=sys_date,ptime=sys_time where mesflag='W'";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #endregion
        #endregion

        #region Vendor
        /// <summary>
        /// TBLVENDOR
        /// </summary>
        public Vendor CreateNewVendor()
        {
            return new Vendor();
        }

        public void AddVendor(Vendor vendor)
        {
            this.DataProvider.Insert(vendor);
        }

        public void DeleteVendor(Vendor vendor)
        {
            this.DataProvider.Delete(vendor);
        }

        public void UpdateVendor(Vendor vendor)
        {
            this.DataProvider.Update(vendor);
        }

        public object GetVendor(string Vendorcode)
        {
            return this.DataProvider.CustomSearch(typeof(Vendor), new object[] { Vendorcode });
        }

        #endregion

        #region I_Sapstorage
        /// <summary>
        /// I_SAPSTORAGE
        /// </summary>
        public I_Sapstorage CreateNewSapstorage()
        {
            return new I_Sapstorage();
        }

        public void AddSapstorage(I_Sapstorage sapstorage)
        {
            this.DataProvider.Insert(sapstorage);
        }

        public void DeleteSapstorage(I_Sapstorage sapstorage)
        {
            this.DataProvider.Delete(sapstorage);
        }

        public void UpdateSapstorage(I_Sapstorage sapstorage)
        {
            this.DataProvider.Update(sapstorage);
        }

        public object GetSapstorage(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapstorage), new object[] { Id });
        }

        #region custom
        public void MergeStorage()
        {
            string sql = @"MERGE INTO TBLSTORAGE c
                        USING (SELECT STORAGECODE,STORAGENAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,CONTACTUSER1,CONTACTUSER2,CONTACTUSER3,CONTACTUSER4,
                        eattribute1,eattribute2,eattribute3 
                        FROM I_SAPSTORAGE where mesflag='W') ic
                        ON ( c.ORGID=1 and c.STORAGECODE=ic.STORAGECODE)
                        WHEN MATCHED THEN
                            UPDATE SET c.STORAGENAME = ic.STORAGENAME,c.ADDRESS1=ic.ADDRESS1,c.ADDRESS2=ic.ADDRESS2,c.ADDRESS3=ic.ADDRESS3,
                            c.ADDRESS4=ic.ADDRESS4,c.CONTACTUSER1=ic.CONTACTUSER1,c.CONTACTUSER2=ic.CONTACTUSER2,c.CONTACTUSER3=ic.CONTACTUSER3,
                            c.CONTACTUSER4=ic.CONTACTUSER4,
                            c.eattribute1=ic.eattribute1,c.eattribute2=ic.eattribute2,c.eattribute3=ic.eattribute3,mdate=sys_date,mtime=sys_time
                        WHEN NOT MATCHED THEN 
                            INSERT (ORGID,STORAGECODE,STORAGENAME,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,CONTACTUSER1,CONTACTUSER2,
                            CONTACTUSER3,CONTACTUSER4,eattribute1,eattribute2,eattribute3,Virtualflag,Sourceflag,
                            muser,mdate,mtime) 
                            VALUES(1,ic.STORAGECODE,ic.STORAGENAME,ic.ADDRESS1,ic.ADDRESS2,ic.ADDRESS3,ic.ADDRESS4,ic.CONTACTUSER1,ic.CONTACTUSER2,
                            ic.CONTACTUSER3,ic.CONTACTUSER4,ic.eattribute1,ic.eattribute2,ic.eattribute3,'N','SAP',
                            'JOB',sys_date,sys_time)";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSapstorageSuccess()
        {
            string sql = "update I_SAPSTORAGE set mesflag='S',pdate=sys_date,ptime=sys_time where mesflag='W'";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #endregion

        #endregion

        #region Storage
        /// <summary>
        /// TBLSTORAGE
        /// </summary>
        public Storage CreateNewStorage()
        {
            return new Storage();
        }

        public void AddStorage(Storage storage)
        {
            this.DataProvider.Insert(storage);
        }

        public void DeleteStorage(Storage storage)
        {
            this.DataProvider.Delete(storage);
        }

        public void UpdateStorage(Storage storage)
        {
            this.DataProvider.Update(storage);
        }

        public object GetStorage(string Storagecode, int Orgid)
        {
            return this.DataProvider.CustomSearch(typeof(Storage), new object[] { Storagecode, Orgid });
        }

        #endregion

    }
}
