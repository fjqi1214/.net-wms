using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.InterfaceDomain;


namespace BenQGuru.eMES.InterfaceFacade
{
    public class InvoicesFacade : MarshalByRefObject
    {
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public InvoicesFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        #region DataProvider

        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public InvoicesFacade(IDomainDataProvider domainDataProvider)
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

        #region I_Sappo
        /// <summary>
        /// I_SAPPO
        /// </summary>
        public I_Sappo CreateNewSappo()
        {
            return new I_Sappo();
        }

        public void AddSappo(I_Sappo sappo)
        {
            this.DataProvider.Insert(sappo);
        }

        public void DeleteSappo(I_Sappo sappo)
        {
            this.DataProvider.Delete(sappo);
        }

        public void UpdateSappo(I_Sappo sappo)
        {
            this.DataProvider.Update(sappo);
        }

        public object GetSappo(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sappo), new object[] { Id });
        }

        #region custom
        public object[] GetSappoByFlag(string flag)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.I_Sappo),
                new SQLCondition(string.Format("select * from I_Sappo where mesflag='{0}'", flag)));
        }

        public void UpdateSappoFlag(string flag)
        {
            string sql = string.Format("update I_SAPPO set mesflag='{0}',pdate=sys_date,ptime=sys_time where mesflag='W'", flag);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSappoFlagByPONO(string pono, string flag)
        {
            string sql = string.Format("update I_SAPPO set mesflag='{0}',pdate=sys_date,ptime=sys_time where invno='{1}' and mesflag='W'", flag, pono);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        #endregion

        #endregion

        #region I_Sapdnbatch
        /// <summary>
        /// I_SAPDNBATCH
        /// </summary>
        public I_Sapdnbatch CreateNewSapdnbatch()
        {
            return new I_Sapdnbatch();
        }

        public void AddSapdnbatch(I_Sapdnbatch sapdnbatch)
        {
            this.DataProvider.Insert(sapdnbatch);
        }

        public void DeleteSapdnbatch(I_Sapdnbatch sapdnbatch)
        {
            this.DataProvider.Delete(sapdnbatch);
        }

        public void UpdateSapdnbatch(I_Sapdnbatch sapdnbatch)
        {
            this.DataProvider.Update(sapdnbatch);
        }

        public object GetSapdnbatch(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapdnbatch), new object[] { Id });
        }

        #region custom
        public void UpdateInvoicesFlag(string flag, string invtype)
        {
            string sql = string.Format("update tblinvoices set eattribute1='{0}',mdate=sys_date,mtime=sys_time where invtype in {1} ", flag, invtype);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSapDnFlag(string flag)
        {
            string sql = string.Format("update I_SAPDNBATCH set mesflag='{0}',pdate=sys_date,ptime=sys_time where mesflag='W'", flag);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public object[] GetSapDnByFlag(string flag)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.I_Sapdnbatch),
                new SQLCondition(string.Format("select * from I_Sapdnbatch where mesflag='{0}'", flag)));
        }
        public object[] GetAllSapDnByFlag()
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.I_Sapdnbatch),
                new SQLCondition(string.Format("select * from I_Sapdnbatch ")));
        }

        #endregion
        #endregion

        #region
        #region add by sam
        public void UpdateInvoicesStatusByDnBatchNo(string status, string dnBatchNO)
        {
            string sql = string.Format(@"update tblInvoices  set  Dnbatchstatus='{0}' , INVStatus='{0}',mdate=sys_date,mtime=sys_time,eattribute1='W'
            where DNBATCHNO='{1}' ", status, dnBatchNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateInvoicesdetailStatusByDnBatchNo(string status, string dnBatchNO)
        {
            string sql = string.Format(@"update tblInvoicesdetail  set Status='{0}', INVLINESTATUS='{0}',mdate=sys_date,mtime=sys_time 
            where invno in ( select invno  from  tblInvoices  where DNBATCHNO='{1}')", status, dnBatchNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        #endregion
        public void UpdateInvoicesStatusByDnNo(string status, string dnNO)
        {
            string sql = string.Format(@"update tblInvoices  set Invstatus='{0}',mdate=sys_date,mtime=sys_time 
            where invno='{1}' ", status, dnNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateInvoicesdetailStatusByDnNo(string status, string dnNO)
        {
            string sql = string.Format(@"update tblInvoicesdetail  set Status='{0}',  Invlinestatus='{0}',mdate=sys_date,mtime=sys_time 
            where invno='{1}' ", status, dnNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void UpdateInvoicesdetailStatusByDnNo(string status, string invno, int invline)
        {
            string sql = string.Format(@"update tblInvoicesdetail  set Status='{0}', Invlinestatus='{0}',mdate=sys_date,mtime=sys_time 
            where invno='{1}' and invline='{2}' ", status, invno, invline);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void UpdatePickStatusByDnBatchNo(string status, string dnBatchNO)
        {
            string sql = string.Format("update tblpick  set Status='{0}',mdate=sys_date,mtime=sys_time where invno='{1}'", status, dnBatchNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void UpdatePickStatusByPickNo(string status, string pickNo)
        {
            string sql = string.Format("update tblpick  set Status='{0}',mdate=sys_date,mtime=sys_time where PickNo='{1}'", status, pickNo);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void UpdatePickDetailStatusByDnBatchNo(string status, string dnBatchNO)
        {
            string sql = string.Format(@" update tblpickdetail  set Status='{0}',mdate=sys_date,mtime=sys_time 
                     where pickno in   ( select pickno  from  tblpick where invno='{1}' ) ", status, dnBatchNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void UpdatePickDetailStatusByPickNo(string status, string pickNo)
        {
            string sql = string.Format("update tblpickdetail  set Status='{0}',mdate=sys_date,mtime=sys_time where PickNo='{1}'", status, pickNo);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateCartonInvoicesStatusByDnBatchNo(string status, string dnBatchNO)
        {
            string sql = string.Format(@" update TBLCartonInvoices  set Status='{0}',mdate=sys_date,mtime=sys_time 
                     where pickno in   ( select pickno  from  tblpick where invno='{1}' ) ", status, dnBatchNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateCartonInvoicesDetailStatusByInvno(string status, string invno)
        {
            string sql = string.Format(@" update TBLCartonInvdetail  set Status='{0}',mdate=sys_date,mtime=sys_time 
                     where pickno in   ( select pickno  from  tblpick where invno='{1}' ) ", status, invno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void DeletePickDetailByPickno(string pickno)
        {
            string sql = string.Format("Delete tblpickdetail   where pickno='{0}' ", pickno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateOQCStatusByPickno(string status, string pickno)
        {
            string sql = string.Format(@" update TBLOQC  set Status='{0}',mdate=sys_date,mtime=sys_time 
                     where pickno='{1}'  ", status, pickno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateOQCStatusByBatchno(string status, string batchno)
        {
            string sql = string.Format(@" update TBLOQC  set Status='{0}',mdate=sys_date,mtime=sys_time 
                     where pickno in ( SELECT  pickno FROM tblpick  where  invno='{1}' ) ", status, batchno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        #endregion


        #region I_Saprs
        /// <summary>
        /// I_SAPUB
        /// </summary>
        public I_Saprs CreateNewSaprs()
        {
            return new I_Saprs();
        }

        public void AddSaprs(I_Saprs sapub)
        {
            this.DataProvider.Insert(sapub);
        }

        public void DeleteSaprs(I_Saprs sapub)
        {
            this.DataProvider.Delete(sapub);
        }

        public void UpdateSaprs(I_Saprs sapub)
        {
            this.DataProvider.Update(sapub);
        }

        public object GetSaprs(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Saprs), new object[] { Id });
        }
        #endregion

        #region I_Sapub
        /// <summary>
        /// I_SAPUB
        /// </summary>
        public I_Sapub CreateNewSapub()
        {
            return new I_Sapub();
        }

        public void AddSapub(I_Sapub sapub)
        {
            this.DataProvider.Insert(sapub);
        }

        public void DeleteSapub(I_Sapub sapub)
        {
            this.DataProvider.Delete(sapub);
        }

        public void UpdateSapub(I_Sapub sapub)
        {
            this.DataProvider.Update(sapub);
        }

        public object GetSapub(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapub), new object[] { Id });
        }

        #region custom
        public object[] GetSapubByFlag(string flag)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.I_Sapub),
                new SQLCondition(string.Format("select * from I_Sapub where mesflag='{0}'", flag)));
        }

        public object[] GetSaprsByFlag(string flag)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.I_Saprs),
                new SQLCondition(string.Format("select * from I_Saprs where mesflag='{0}'", flag)));
        }

        public void UpdateSapubFlag(string flag)
        {
            string sql = string.Format("update I_Sapub set mesflag='{0}',pdate=sys_date,ptime=sys_time where mesflag='W'", flag);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSaprsFlag(string flag)
        {
            string sql = string.Format("update I_Saprs set mesflag='{0}',pdate=sys_date,ptime=sys_time where mesflag='W'", flag);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void UpdateSapubFlagByUBNO(string ubno, string flag)
        {
            string sql = string.Format("update I_Sapub set mesflag='{0}',pdate=sys_date,ptime=sys_time where invno='{1}' and mesflag='W'", flag, ubno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #endregion
        #endregion

        #region I_Sapstoragecheck
        /// <summary>
        /// I_SAPSTORAGECHECK
        /// </summary>
        public I_Sapstoragecheck CreateNewSapstoragecheck()
        {
            return new I_Sapstoragecheck();
        }

        public void AddSapstoragecheck(I_Sapstoragecheck sapstoragecheck)
        {
            this.DataProvider.Insert(sapstoragecheck);
        }

        public void DeleteSapstoragecheck(I_Sapstoragecheck sapstoragecheck)
        {
            this.DataProvider.Delete(sapstoragecheck);
        }

        public void UpdateSapstoragecheck(I_Sapstoragecheck sapstoragecheck)
        {
            this.DataProvider.Update(sapstoragecheck);
        }

        public object GetSapstoragecheck(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapstoragecheck), new object[] { Id });
        }

        #region custom
        public object[] GetSapstoragecheckByFlag(string flag)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.I_Sapstoragecheck),
                new SQLCondition(string.Format("select * from I_Sapstoragecheck where mesflag='{0}'", flag)));
        }

        public void UpdateSapstoragecheckFlag(string flag)
        {
            string sql = string.Format("update I_Sapstoragecheck set mesflag='{0}',pdate=sys_date,ptime=sys_time where mesflag='W'", flag);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateSapstoragecheckFlagBySCNO(string scno, string flag)
        {
            string sql = string.Format("update I_Sapstoragecheck set mesflag='{0}',pdate=sys_date,ptime=sys_time where invno='{1}' and mesflag='W'", flag, scno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #endregion
        #endregion


        #region Dnbatchbak
        /// <summary>
        /// TBLDNBATCHBAK
        /// </summary>
        public DNBatchBak CreateNewDNBatchBak()
        {
            return new DNBatchBak();
        }

        public void AddDNBatchBak(DNBatchBak dnbatchbak)
        {
            this.DataProvider.Insert(dnbatchbak);
        }

        public void DeleteDNBatchBak(DNBatchBak dnbatchbak)
        {
            this.DataProvider.Delete(dnbatchbak);
        }

        public void UpdateDNBatchBak(DNBatchBak dnbatchbak)
        {
            this.DataProvider.Update(dnbatchbak);
        }

        public object GetDNBatchBak()
        {
            return this.DataProvider.CustomSearch(typeof(DNBatchBak), new object[] { });
        }

        #endregion

        #region Dnbatchdetailbak
        /// <summary>
        /// TBLDNBATCHDETAILBAK
        /// </summary>
        public DNBatchDetailBak CreateNewDNBatchDetailBak()
        {
            return new DNBatchDetailBak();
        }

        public void AddDNBatchDetailBak(DNBatchDetailBak dnbatchdetailbak)
        {
            this.DataProvider.Insert(dnbatchdetailbak);
        }

        public void DeleteDNBatchDetailBak(DNBatchDetailBak dnbatchdetailbak)
        {
            this.DataProvider.Delete(dnbatchdetailbak);
        }

        public void UpdateDNBatchDetailBak(DNBatchDetailBak dnbatchdetailbak)
        {
            this.DataProvider.Update(dnbatchdetailbak);
        }

        public object GetDNBatchDetailBak()
        {
            return this.DataProvider.CustomSearch(typeof(DNBatchDetailBak), new object[] { });
        }

        #endregion

        #region Invoices
        /// <summary>
        /// TBLINVOICES
        /// </summary>
        public Invoices CreateNewInvoices()
        {
            return new Invoices();
        }

        public void AddInvoices(Invoices invoices)
        {
            this.DataProvider.Insert(invoices);
        }

        public void DeleteInvoices(Invoices invoices)
        {
            this.DataProvider.Delete(invoices);
        }

        public void UpdateInvoices(Invoices invoices)
        {
            this.DataProvider.Update(invoices);
        }

        public object GetInvoices(string Invno)
        {
            return this.DataProvider.CustomSearch(typeof(Invoices), new object[] { Invno });
        }

        public object[] GetInvoicesBybatchno(string batchno)
        {

            string sql = string.Format(@"select * from tblinvoices where Dnbatchno='{0}' ", batchno);
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition(sql));
        }

        public int QueryInvoicesCount(string invNO)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVOICES where 1=1 and invno = '{0}' ",
                invNO)));
        }

        #region custom
        public void UpdateInvoicesStatusByInvNO(string invNO, string status)
        {
            string sql = string.Format("update tblinvoices set orderstatus='{0}',muser='JOB',mdate=sys_date,mtime=sys_time, eattribute1='L'  where invno='{1}'", status, invNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateInvoicesStatusByInvNO(string invNO, string status, string eattribute1)
        {
            string sql = string.Format("update tblinvoices set orderstatus='{0}',muser='JOB',mdate=sys_date,mtime=sys_time, eattribute1='{2}'  where invno='{1}'", status, invNO, eattribute1);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdateInvoicesDetailStatusByInvNO(string invNO, string status, string eattribute1)
        {
            string sql = string.Format("update TBLInvoicesDetail set INVLINESTATUS='{0}',muser='JOB',mdate=sys_date,mtime=sys_time, eattribute1='{2}'  where invno='{1}'", status, invNO, eattribute1);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public object[] GetDNInvoices()
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition("select * from tblinvoices where invtype in ('DNC','DNZC') and   (eattribute1<>'L' or eattribute1 is null ) "));//'DNR',
        }

        public object[] GetPOInvoices()
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition("select * from tblinvoices where invtype in ( 'POC' ) and  (eattribute1<>'L' or eattribute1 is null ) "));
        }

        public object[] GetUBInvoicesNoPick()
        {
            //转储不产生拣货任务令，而是insert TBLStorLocTrans
            //            string sql = @"select * from tblinvoices where invtype in ('UB','ZC','JCC','BLC') and not exists
            //                        (select * from tblpick where tblinvoices.invno=tblpick.invno) ";
            string sql = @"select * from tblinvoices where invtype in ('UB','JCC','BLC') and  (eattribute1<>'L' or eattribute1 is null ) and not exists
                        (select * from tblpick where tblinvoices.invno=tblpick.invno) ";
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition(sql));
        }

        //add by sam
        public object[] GetUBInvoices()
        {
            string sql = @"select * from tblinvoices where invtype in ('UB','JCC','BLC') and   (eattribute1<>'L' or eattribute1 is null ) ";
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition(sql));
        }

        public object[] GetUBInvoicesNoStorLocTrans()
        {
            //转储不产生拣货任务令，而是insert TBLStorLocTrans
            //            string sql = @"select * from tblinvoices where invtype='ZC' and   (eattribute1<>'L' or eattribute1 is null ) and not exists
            //                        (select * from TBLStorLocTrans where tblinvoices.invno=TBLStorLocTrans.invno) ";
            string sql = @"select * from tblinvoices where invtype='ZC' and   (eattribute1<>'L' or eattribute1 is null )  ";
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition(sql));
        }

        public object[] GetStockCheckInvoicesNoPick()
        {
            string sql = @"select * from tblinvoices where invtype='PD' and  (eattribute1<>'L' or eattribute1 is null )  and not exists
                        (select * from tblpick where tblinvoices.invno=tblpick.invno) 
                        and exists (select * from tblinvoicesdetail where tblinvoices.invno=tblinvoicesdetail.invno 
                        and tblinvoicesdetail.type='PKC')";
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoices),
                new SQLCondition(sql));
        }
        #endregion
        #endregion
        #region Invoicesdetail
        /// <summary>
        /// TBLINVOICESDETAIL
        /// </summary>
        public Invoicesdetail CreateNewInvoicesdetail()
        {
            return new Invoicesdetail();
        }

        public void AddInvoicesdetail(Invoicesdetail invoicesdetail)
        {
            this.DataProvider.Insert(invoicesdetail);
        }

        public void DeleteInvoicesdetail(Invoicesdetail invoicesdetail)
        {
            this.DataProvider.Delete(invoicesdetail);
        }

        public void UpdateInvoicesdetail(Invoicesdetail invoicesdetail)
        {
            this.DataProvider.Update(invoicesdetail);
        }

        public object GetInvoicesdetail(string Invno, string Invline)
        {
            return this.DataProvider.CustomSearch(typeof(Invoicesdetail), new object[] { Invno, Invline });
        }

        public int QueryInvoicesdetailCount(string InvNO, int Invline)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVOICESDETAIL where invno = '{0}' and invline = {1} ",
                InvNO, Invline)));
        }

        #region
        public void DeleteInvoicesdetailByDNBatchNo(string dnbatchno)
        {
            string sql = string.Format(@"delete  from  tblInvoicesdetail
             where invno in  ( select invno  from  tblInvoices  where DNBATCHNO='{0}') ", dnbatchno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void DeleteInvoicesByDNBatchNo(string dnbatchno)
        {
            string sql = string.Format("delete TBLINVOICES where DNBATCHNO='{0}' ", dnbatchno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #region 2016年7月1日

        public void DeleteInvoicesByDNBatchNo(string dnbatchno, string invno)
        {
            string sql = string.Format(@"delete TBLINVOICES where DNBATCHNO='{0}' and invno in
            ( select invno from  TBLINVOICES where DNBATCHNO='{0}' and
            invno not in ( {1} ) )", dnbatchno, invno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void DeleteInvoicesdetailByDNBatchNo(string dnbatchno, string invno)
        {
            string sql = string.Format(@"delete  from  tblInvoicesdetail
             where invno in   ( select invno from  TBLINVOICES where DNBATCHNO='{0}' and
            invno not in ( {1} ) ) ", dnbatchno, invno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        #endregion

        public void DeleteInvoicesdetail(string invno)
        {
            string sql = string.Format("delete TBLINVOICESDETAIL   where invno='{0}'", invno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void DeleteInvoices(string invno)
        {
            string sql = string.Format("delete TBLINVOICES   where invno='{0}'", invno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        #endregion

        #region custom
        /// <summary>
        /// 拣货任务令只汇总B层物料
        /// </summary>
        /// <param name="dnBatchNO"></param>
        /// <returns></returns>
        public object[] GetDNInvoicesDetailByDNBatchNO(string dnBatchNO)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoicesdetail),
                new SQLCondition(string.Format("select d.* from tblinvoices h,tblinvoicesdetail d where h.invno=d.invno and h.dnbatchno='{0}' and movementtype is not null AND d.STATUS not in ('Cancel')  ", dnBatchNO)));
        }

        public object[] GetDNInvoicesDetailBybatchno(string batchno)
        {

            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoicesdetail),
                    new SQLCondition(string.Format("select d.* from tblinvoices h,tblinvoicesdetail d where h.invno=d.invno and h.dnbatchno='{0}'", batchno)));
        }


        public object[] GetUBInvoicesDetailByUBNO(string invno)
        {
            string sql = string.Format("select d.* from tblinvoices h,tblinvoicesdetail d where h.invno=d.invno and h.invno='{0}' AND d.invlinestatus not in ('Cancel')   ", invno);
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoicesdetail),
                new SQLCondition(sql));
        }

        public object[] GetStockCheckInvoicesDetailBySCNO(string invno)
        {
            string sql = string.Format("select d.* from tblinvoices h,tblinvoicesdetail d where h.invno=d.invno and h.invno='{0}' and d.type='PKC'  ", invno);
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoicesdetail),
                new SQLCondition(sql));
        }




        public object[] GetASNReject(DateTime now, int minRange)
        {

            string sql = @"select b.stno,b.dqmcode,a.invno,sum(b.qty) qty,count(*) InitRejectQty from tblasndetail b inner join tblasn a  on
              a.stno=b.stno where   
              b.INITRECEIVESTATUS='Reject'" + SendMailTimeFilter("b", now, minRange) + " group by  b.stno,b.dqmcode,a.invno ";


            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.AsndetailEX),
                new SQLCondition(sql));
        }




        #endregion
        #endregion

        #region Pick
        /// <summary>S
        /// TBLPICK
        /// </summary>
        public Pick CreateNewPick()
        {
            return new Pick();
        }

        public void AddPick(Pick pick)
        {
            this.DataProvider.Insert(pick);
        }

        public void DeletePick(Pick pick)
        {
            this.DataProvider.Delete(pick);
        }

        public void UpdatePick(Pick pick)
        {
            this.DataProvider.Update(pick);
        }

        public object GetPick(string Pickno)
        {
            return this.DataProvider.CustomSearch(typeof(Pick), new object[] { Pickno });
        }

        #region SERIALBOOK
        public string GetMaxSerial(string Prefix)
        {
            string sql = string.Format("SELECT  MAXSERIAL FROM TBLSERIALBOOK WHERE SNPREFIX='{0}' ", Prefix);
            object[] result = this.DataProvider.CustomQuery(typeof(SERIALBOOK), new SQLCondition(sql));
            if (result != null && result.Length > 0)
                return ((SERIALBOOK)result[0]).MaxSerial;
            else
                return string.Empty;
        }

        public void AddSerialBook(SERIALBOOK serialBook)
        {
            this.DataProvider.Insert(serialBook);
        }

        public void UpdateSerialBook(SERIALBOOK serialBook)
        {
            this.DataProvider.Update(serialBook);
        }

        public object GetSerialBook(string prefix)
        {
            string sql = string.Format(@"SELECT  * FROM TBLSERIALBOOK WHERE SNPREFIX='{0}' ", prefix);
            object[] result = this.DataProvider.CustomQuery(typeof(SERIALBOOK), new SQLCondition(sql));
            if (result != null && result.Length > 0)
                return result[0];
            else
                return null;
        }
        #endregion

        #region CreateSerialNo

        private string CreateSerialNo(int stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string maxserial = GetMaxSerial("OU" + stno);
            //如果已是最大值就返回为空
            if (maxserial == "9999")
            {
                return "";
            }
            SERIALBOOK serialbook = new SERIALBOOK();
            if (maxserial == "")
            {
                serialbook.SNPrefix = "OU" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = "JOB";
                AddSerialBook(serialbook);
                return string.Format("{0:0000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "OU" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = "JOB";
                UpdateSerialBook(serialbook);
                return string.Format("{0:0000}", int.Parse(serialbook.MaxSerial));
            }
        }

        #endregion

        #region custom
        public string GetPickNO()
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            string serialNo = CreateSerialNo(date);
            return "OU" + date + serialNo;

            //string ymd = DateTime.Now.ToString("yyyyMMdd");
            //string pickNO = string.Format("OU{0}{1}", ymd, "0001");
            //object[] obj = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(
            //    "select nvl(max(pickno),'0') as pickno from tblpick"));

            //if (obj != null && obj.Length > 0)
            //{
            //    Pick pick = obj[0] as Pick;
            //    if (pick.Pickno != "0" && ymd == pick.Pickno.Substring(2, 8))
            //    {
            //        string tmpStr = pick.Pickno.Substring(10);
            //        pickNO = string.Format("OU{0}{1}", DateTime.Now.ToString("yyyyMMdd"), (int.Parse(tmpStr) + 1).ToString().PadLeft(4, '0'));
            //    }
            //}

            //return pickNO;
        }
        public int ParseInt(object obj)
        {
            int reInt = -1;
            if (obj != null)
                int.TryParse(obj.ToString(), out reInt);
            return reInt;
        }

        public string GetMaxPickLine(string pickno)
        {
            string sql = string.Format(@" select max(pickline)  pickline from tblpickdetail
                     where pickno='{0}'  ", pickno);
            object[] obj = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Pickdetail), new SQLCondition(sql));
            int line = 0;
            if (obj != null && obj.Length > 0)
            {
                Pickdetail pickdetail = obj[0] as Pickdetail;
                if (pickdetail != null)
                {
                    line = this.ParseInt(pickdetail.Pickline);
                }
            }
            line++;

            return line.ToString("G0");
        }

        //add by sam
        public int GetMaxInvoicesLine(string Invno)
        {
            string sql = string.Format(@" select max(Invline)  Invline from tblInvoicesdetail
                     where Invno='{0}'  ", Invno);
            object[] obj = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Invoicesdetail), new SQLCondition(sql));
            int line = 0;
            if (obj != null && obj.Length > 0)
            {
                Invoicesdetail invoicesdetail = obj[0] as Invoicesdetail;
                if (invoicesdetail != null)
                {
                    line = invoicesdetail.Invline;
                }
            }
            line++;

            return line;
        }

        public int QueryPickCountByDNBatchNO(string dnBatchNO)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblpick where invno = '{0}' ",
                dnBatchNO)));
        }
        public object GetPickByDNBatchNO(string dnBatchNO)
        {
            object[] list = this.DataProvider.CustomQuery(typeof(Pick),
                new SQLCondition(string.Format("select d.* from tblpick d where d.invno='{0}' ", dnBatchNO)));
            if (list != null)
            {
                return list[0];
            }
            return null;
        }

        public object[] GetPickDetailByDNBatchNO(string dnBatchNO)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Pickdetail),
                new SQLCondition(string.Format("select d.* from tblpick h,tblpickdetail d where h.pickno=d.pickno  and h.invno='{0}' ", dnBatchNO)));
        }

        public object[] GetTransDetailByDNBatchNO(string invno)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Storloctransdetail),
                new SQLCondition(string.Format("select d.* from TBLSTORLOCTRANS h,TBLSTORLOCTRANSDETAIL d where h.TRANSNO=d.TRANSNO  and h.invno='{0}' ", invno)));
        }


        #endregion
        #endregion
        #region Pickdetail
        /// <summary>
        /// TBLPICKDETAIL
        /// </summary>
        public Pickdetail CreateNewPickdetail()
        {
            return new Pickdetail();
        }

        public void AddPickdetail(Pickdetail pickdetail)
        {
            this.DataProvider.Insert(pickdetail);
        }

        public void DeletePickdetail(Pickdetail pickdetail)
        {
            this.DataProvider.Delete(pickdetail);
        }

        public void UpdatePickdetail(Pickdetail pickdetail)
        {
            this.DataProvider.Update(pickdetail);
        }

        public object GetPickdetail()
        {
            return this.DataProvider.CustomSearch(typeof(Pickdetail), new object[] { });
        }

        public object GetPickdetail(string pickno, string mcode)
        {
            string sql = string.Format(@" select * from tblpickdetail
                     where pickno='{0}' and   mcode='{1}' ", pickno, mcode);
            object[] list = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Pickdetail), new SQLCondition(sql));
            if (list != null)
            {
                return list[0];
            }
            return null;
        }

        #endregion

        #region Storloctrans
        /// <summary>
        /// TBLSTORLOCTRANS
        /// </summary>
        public Storloctrans CreateNewStorloctrans()
        {
            return new Storloctrans();
        }

        public void AddStorloctrans(Storloctrans storloctrans)
        {
            this.DataProvider.Insert(storloctrans);
        }

        public void DeleteStorloctrans(Storloctrans storloctrans)
        {
            this.DataProvider.Delete(storloctrans);
        }

        public void UpdateStorloctrans(Storloctrans storloctrans)
        {
            this.DataProvider.Update(storloctrans);
        }

        public object GetStorloctrans(string Transno)
        {
            return this.DataProvider.CustomSearch(typeof(Storloctrans), new object[] { Transno });
        }

        #region custom
        public string GetStorLocTransNO()
        {
            string ymd = DateTime.Now.ToString("yyyyMMdd");
            string transNO = string.Format("ZC{0}{1}", ymd, "0001");
            object[] obj = this.DataProvider.CustomQuery(typeof(Storloctrans), new SQLCondition(
                "select nvl(max(transno),'0') as transno from TBLStorLocTrans"));

            if (obj != null && obj.Length > 0)
            {
                Storloctrans trans = obj[0] as Storloctrans;
                if (trans.Transno != "0" && ymd == trans.Transno.Substring(2, 8))
                {
                    string tmpStr = trans.Transno.Substring(10);
                    transNO = string.Format("ZC{0}{1}", DateTime.Now.ToString("yyyyMMdd"), (int.Parse(tmpStr) + 1).ToString().PadLeft(4, '0'));
                }
            }

            return transNO;
        }

        public object GetStorloctransByInvNO(string invno)
        {
            object[] list = this.DataProvider.CustomQuery(typeof(Storloctrans),
                new SQLCondition(string.Format("select d.* from TBLSTORLOCTRANS d where d.invno='{0}' ", invno)));
            if (list != null)
            {
                return list[0];
            }
            return null;
        }

        public int QueryStorloctransCountByInvNO(string invno)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(1) from TBLSTORLOCTRANS d where d.invno='{0}' ", invno)));
        }

        public int QueryStorloctransdetailCountByTransno(string Transno, string Mcode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(1) from TBLSTORLOCTRANSDETAIL d where d.Transno='{0}' and  Mcode='{1}'  ", Transno, Mcode)));
        }

        #endregion
        #endregion
        #region Storloctransdetail
        /// <summary>
        /// TBLSTORLOCTRANSDETAIL
        /// </summary>
        public Storloctransdetail CreateNewStorloctransdetail()
        {
            return new Storloctransdetail();
        }

        public void AddStorloctransdetail(Storloctransdetail storloctransdetail)
        {
            this.DataProvider.Insert(storloctransdetail);
        }

        public void DeleteStorloctransdetail(Storloctransdetail storloctransdetail)
        {
            this.DataProvider.Delete(storloctransdetail);
        }

        public void UpdateStorloctransdetail(Storloctransdetail storloctransdetail)
        {
            this.DataProvider.Update(storloctransdetail);
        }

        public object GetStorloctransdetail(string Transno, string Mcode)
        {
            return this.DataProvider.CustomSearch(typeof(Storloctransdetail), new object[] { Transno, Mcode });
        }

        #endregion

        #region I_Sapstorageinfo
        /// <summary>
        /// I_SAPSTORAGEINFO
        /// </summary>
        public I_Sapstorageinfo CreateNewISapstorageinfo()
        {
            return new I_Sapstorageinfo();
        }

        public void AddSapstorageinfo(I_Sapstorageinfo sapstorageinfo)
        {
            this.DataProvider.Insert(sapstorageinfo);
        }

        public void DeleteSapstorageinfo(I_Sapstorageinfo sapstorageinfo)
        {
            this.DataProvider.Delete(sapstorageinfo);
        }

        public void UpdateSapstorageinfo(I_Sapstorageinfo sapstorageinfo)
        {
            this.DataProvider.Update(sapstorageinfo);
        }

        public object GetSapstorageinfo(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(I_Sapstorageinfo), new object[] { Id });
        }

        #region custom
        public void UpdateISapstorageinfoFlag(string flag)
        {
            string sql = string.Format("update I_Sapstorageinfo set mesflag='{0}',pdate=sys_date,ptime=sys_time where mesflag='W'", flag);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #endregion

        #endregion

        #region Sapstorageinfo
        /// <summary>
        /// TBLSAPSTORAGEINFO
        /// </summary>
        public Sapstorageinfo CreateNewSapstorageinfo()
        {
            return new Sapstorageinfo();
        }

        public void AddSapstorageinfo(Sapstorageinfo sapstorageinfo)
        {
            this.DataProvider.Insert(sapstorageinfo);
        }

        public void DeleteSapstorageinfo(Sapstorageinfo sapstorageinfo)
        {
            this.DataProvider.Delete(sapstorageinfo);
        }

        public void UpdateSapstorageinfo(Sapstorageinfo sapstorageinfo)
        {
            this.DataProvider.Update(sapstorageinfo);
        }

        public object GetSapstorageinfo()
        {
            return this.DataProvider.CustomSearch(typeof(Sapstorageinfo), new object[] { });
        }

        #region custom
        public void DeleteAllSapstorageinfo()
        {
            string sql = "delete from tblsapstorageinfo";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void InsertSapstorageinfoFromMiddleTable()
        {
            string sql = @"INSERT INTO TBLSAPSTORAGEINFO(MCODE,STORAGECODE,FACCODE,TYPE,TYPENO,AVAILABLEQTY,QUALITYQTY,FREEZEQTY,
                        TRANSITQTY,FREEZERETURNQTY,UNIT,EATTRIBUTE1,EATTRIBUTE2,EATTRIBUTE3,CUSER,CDATE,CTIME,MUSER,MDATE,MTIME)
                        SELECT MCODE,STORAGECODE,FACCODE,TYPE,TYPENO,AVAILABLEQTY,QUALITYQTY,FREEZEQTY,
                        TRANSITQTY,FREEZERETURNQTY,UNIT,EATTRIBUTE1,EATTRIBUTE2,EATTRIBUTE3,'JOB',SYS_DATE,SYS_TIME,'JOB',SYS_DATE,SYS_TIME
                        FROM I_SAPSTORAGEINFO WHERE MESFLAG='W'";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }


        public void DeleteAllStorageSap2Mes()
        {
            string sql = "delete from TBLSTORAGESAP2MES ";
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public object GetMaterial(string materialCode)
        {
            return this.DataProvider.CustomSearch(typeof(BenQGuru.eMES.InterfaceDomain.Material), new object[] { materialCode });
        }

        public object[] GetAllSapstorageinfo()
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Sapstorageinfo),
                new SQLCondition("select * from tblsapstorageinfo where ( type<>'O' or type is null )   "));
        }

        public object[] GetAllStorageDetail()
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.StorageDetail),
                   new SQLCondition("select * from tblStorageDetail "));
        }


        public int GetStorageDetailQty(string storageCode, string mCode)
        {
            string sql = string.Format(@"select sum(storageqty) storageqty from tblstoragedetail  where StorageCode='{0}' and MCode='{1}' ", storageCode, mCode);
            object[] obj = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.StorageDetail), new SQLCondition(sql));
            int qty = 0;
            if (obj != null && obj.Length > 0)
            {
                StorageDetail storageDetail = obj[0] as StorageDetail;
                if (storageDetail != null)
                {
                    qty = storageDetail.StorageQty;
                }
            }
            return qty;
        }

        public int GetSapstorageinfoQty(string storageCode, string mCode)
        {
            string sql = string.Format(@"select sum(a.qualityqty+a.freezeqty+a.availableqty) storageqty from tblsapstorageinfo  where StorageCode='{0}' and MCode='{1}' ", storageCode, mCode);
            object[] obj = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.StorageDetail), new SQLCondition(sql));
            int qty = 0;
            if (obj != null && obj.Length > 0)
            {
                StorageDetail storageDetail = obj[0] as StorageDetail;
                if (storageDetail != null)
                {
                    qty = storageDetail.StorageQty;
                }
            }
            return qty;
        }
        #endregion
        #endregion

        #region Storagesap2mes
        /// <summary>
        /// TBLSTORAGESAP2MES
        /// </summary>
        public Storagesap2mes CreateNewStoragesap2mes()
        {
            return new Storagesap2mes();
        }

        public void AddStoragesap2mes(Storagesap2mes storagesap2mes)
        {
            this.DataProvider.Insert(storagesap2mes);
        }

        public void DeleteStoragesap2mes(Storagesap2mes storagesap2mes)
        {
            this.DataProvider.Delete(storagesap2mes);
        }

        public void UpdateStoragesap2mes(Storagesap2mes storagesap2mes)
        {
            this.DataProvider.Update(storagesap2mes);
        }

        public object GetStoragesap2mes()
        {
            return this.DataProvider.CustomSearch(typeof(Storagesap2mes), new object[] { });
        }

        #endregion


        #region Parameter 参数
        public object GetParameter(string parameterCode, string parameterGroupCode)
        {
            return this.DataProvider.CustomSearch(typeof(Parameter), new object[] { parameterCode, parameterGroupCode });
        }

        public string GetParameterAlias(string parameterGroupCode, string parameterCode)
        {
            string returnValue = string.Empty;

            Parameter setting = (Parameter)GetParameter(parameterCode, parameterGroupCode);
            if (setting != null)
            {
                returnValue = setting.ParameterAlias;
            }

            return returnValue;
        }

        #endregion

        #region Sapjoblog
        /// <summary>
        /// TBLSAPJOBLOG
        /// </summary>
        public Sapjoblog CreateNewSapjoblog()
        {
            return new Sapjoblog();
        }

        public void AddSapjoblog(Sapjoblog sapjoblog)
        {
            this.DataProvider.Insert(sapjoblog);
        }

        public void DeleteSapjoblog(Sapjoblog sapjoblog)
        {
            this.DataProvider.Delete(sapjoblog);
        }

        public void UpdateSapjoblog(Sapjoblog sapjoblog)
        {
            this.DataProvider.Update(sapjoblog);
        }

        public object GetSapjoblog(int Id)
        {
            return this.DataProvider.CustomSearch(typeof(Sapjoblog), new object[] { Id });
        }

        #endregion


        #region SAP
        public object[] GetAllDn2SapIn()
        {

            string sql = string.Format(@"select  * from tbldn2Sap where result is null or  result not in ('S') AND TRANSTYPE='IN'");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.DN2Sap),
                new SQLCondition(sql));
        }

        public object[] GetAllDn2SapOut()
        {

            string sql = string.Format(@"select  * from tbldn2Sap where result is null or  result not in ('S') AND TRANSTYPE='OUT'");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.DN2Sap),
                new SQLCondition(sql));
        }

        public object[] GetAll101Po2Sap()
        {
            string sql = string.Format(@"select  * from tblPo2Sap where (sapreturn is null or  sapreturn not in ('S')) AND STATUS='101'");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Po2Sap),
                new SQLCondition(sql));

        }

        public string GetIdentityFromPo103Log(string stno, string status)
        {
            string sql = string.Format(@"select * from tblPoLog where SapReturn='S' AND status='" + status + "' AND SERIALNO='" + stno + "'");
            object[] objs = this.DataProvider.CustomQuery(typeof(PoLog),
                new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((PoLog)objs[0]).SAPMaterialInvoice;
            else
                return string.Empty;
        }

        public object[] GetAll103Po2Sap()
        {

            string sql = string.Format(@"select  * from tblPo2Sap where (sapreturn is null or  sapreturn not in ('S')) AND STATUS='103'");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Po2Sap),
                new SQLCondition(sql));
        }


        public object[] GetAll104Po2Sap()
        {
            string sql = string.Format(@"select  * from tblPo2Sap where (sapreturn is null or  sapreturn not in ('S')) AND STATUS='104'");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Po2Sap),
                new SQLCondition(sql));

        }

        public object[] GetAll105Po2Sap()
        {
            string sql = string.Format(@"select  * from tblPo2Sap where (sapreturn is null or  sapreturn not in ('S')) AND STATUS='105'");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Po2Sap),
                new SQLCondition(sql));

        }



        public object[] GetAllPo2Sap()
        {

            string sql = string.Format(@"select  * from tblPo2Sap where sapreturn is null or  sapreturn not in ('S') ");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Po2Sap),
                new SQLCondition(sql));
        }

        public object[] GetAllRs2Sap()
        {

            string sql = string.Format(@"select  * from tblrs2Sap where sapreturn is null or  sapreturn not in ('S') ");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Rs2Sap),
                new SQLCondition(sql));
        }
        public object[] GetAllDnIn2Sap()
        {

            string sql = string.Format(@"select  * from TBLDn_in2Sap where sapreturn is null or  sapreturn not in ('S')");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Dn_in2Sap),
                new SQLCondition(sql));
        }
        public object[] GetAllWwpo2Sap()
        {

            string sql = string.Format(@"select  * from TBLWwpo2Sap where sapreturn is null or  sapreturn not in ('S')");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Wwpo2Sap),
                new SQLCondition(sql));
        }
        public object[] GetAllUb2Sap()
        {

            string sql = string.Format(@"select  * from TBLUb2Sap where sapreturn is null or sapreturn not in ('S')");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Ub2Sap),
                new SQLCondition(sql));
        }
        public object[] GetAllStockScrap2Sap()
        {

            string sql = string.Format(@"select  * from TBLStockScrap2Sap  where sapreturn is null or  sapreturn not in ('S') ");
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.StockScrap2Sap),
                new SQLCondition(sql));
        }



        #endregion

        #region UpdateSAP
        #region Po2Sap
        /// <summary>
        /// TBLPo2Sap
        /// </summary>
        public Po2Sap CreateNewPo2Sap()
        {
            return new Po2Sap();
        }

        public void AddPo2Sap(Po2Sap Po2Sap)
        {
            this.DataProvider.Insert(Po2Sap);
        }

        public void DeletePo2Sap(Po2Sap Po2Sap)
        {
            this.DataProvider.Delete(Po2Sap);
        }

        public void UpdatePo2Sap(Po2Sap Po2Sap)
        {
            this.DataProvider.Update(Po2Sap);
        }

        public object GetPo2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Po2Sap), new object[] { Serial });
        }


        public DN2Sap[] QueryDN2Sap(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-1);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            string sql = "SELECT * FROM TBLDN2SAP WHERE RESULT='E' AND MDATE>=" + dateInt + " AND MTIME>=" + timeInt;
            List<DN2Sap> dns = new List<DN2Sap>();
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.DN2Sap),
             new SQLCondition(sql));
            foreach (DN2Sap dn in objs)
                dns.Add(dn);
            return dns.ToArray();

        }

        public Po2Sap[] QueryPO2Saps(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-10);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            List<Po2Sap> pos = new List<Po2Sap>();
            string sql = "SELECT * FROM TBLPO2SAP WHERE RESULT='E' AND SapDateStamp>=" + dateInt + " AND SAPTIMESTAMP>=" + timeInt;
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Po2Sap), new SQLCondition(sql));
            foreach (Po2Sap dn in objs)
                pos.Add(dn);
            return pos.ToArray();
        }

        public Rs2Sap[] QueryRS2Saps(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-1);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            List<Rs2Sap> rss = new List<Rs2Sap>();
            string sql = "SELECT * FROM TBLRS2SAP WHERE SAPRETURN='E' AND MDATE>=" + dateInt + " AND MTIME>=" + timeInt;

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Rs2Sap), new SQLCondition(sql));

            foreach (Rs2Sap dn in objs)
                rss.Add(dn);
            return rss.ToArray();
        }

        public Dn_in2Sap[] QueryDN_IN2Saps(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-1);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            List<Dn_in2Sap> dnins = new List<Dn_in2Sap>();
            string sql = "SELECT * FROM TBLDN_IN2SAP WHERE  SAPRETURN='E' AND MDATE>=" + dateInt + " AND MTIME>=" + timeInt;
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Dn_in2Sap), new SQLCondition(sql));
            foreach (Dn_in2Sap dn in objs)
                dnins.Add(dn);
            return dnins.ToArray();

        }

        public Wwpo2Sap[] QueryWWPO2Sap(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-1);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            List<Wwpo2Sap> wwpos = new List<Wwpo2Sap>();
            string sql = "SELECT * FROM TBLWWPO2SAP WHERE SAPRETURN='E' AND MDATE>=" + dateInt + " AND MTIME>=" + timeInt;
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Dn_in2Sap), new SQLCondition(sql));
            foreach (Wwpo2Sap dn in objs)
                wwpos.Add(dn);
            return wwpos.ToArray();
        }

        public Ub2Sap[] QueryUB2Sap(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-1);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            List<Ub2Sap> ubs = new List<Ub2Sap>();
            string sql = "SELECT * FROM TBLUB2SAP WHERE SAPRETURN='E' AND MDATE>=" + dateInt + " AND MTIME>=" + timeInt;
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Ub2Sap), new SQLCondition(sql));
            foreach (Ub2Sap dn in objs)
                ubs.Add(dn);
            return ubs.ToArray();
        }
        public StockScrap2Sap[] QueryStockScrap2Sap(DateTime now)
        {
            DateTime scanBeginDate = now.AddDays(-1);
            int dateInt = FormatHelper.TODateInt(scanBeginDate);
            int timeInt = FormatHelper.TOTimeInt(scanBeginDate);
            List<StockScrap2Sap> ss = new List<StockScrap2Sap>();
            string sql = "SELECT * FROM TBLSTOCKSCRAP2SAP WHERE SAPRETURN='E' AND MDATE>=" + dateInt + " AND MTIME>=" + timeInt;
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.StockScrap2Sap), new SQLCondition(sql));
            foreach (StockScrap2Sap dn in objs)
                ss.Add(dn);
            return ss.ToArray();
        }

        #endregion

        #region Rs2Sap
        /// <summary>
        /// TBLRs2Sap
        /// </summary>
        public Rs2Sap CreateNewRs2Sap()
        {
            return new Rs2Sap();
        }

        public void AddRs2Sap(Rs2Sap Rs2Sap)
        {
            this.DataProvider.Insert(Rs2Sap);
        }

        public void DeleteRs2Sap(Rs2Sap Rs2Sap)
        {
            this.DataProvider.Delete(Rs2Sap);
        }

        public void UpdateRs2Sap(Rs2Sap Rs2Sap)
        {
            this.DataProvider.Update(Rs2Sap);
        }

        public object GetRs2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Rs2Sap), new object[] { Serial });
        }

        #endregion

        #region Wwpo2Sap
        /// <summary>
        /// TBLWwpo2Sap
        /// </summary>
        public Wwpo2Sap CreateNewWwpo2Sap()
        {
            return new Wwpo2Sap();
        }

        public void AddWwpo2Sap(Wwpo2Sap Wwpo2Sap)
        {
            this.DataProvider.Insert(Wwpo2Sap);
        }

        public void DeleteWwpo2Sap(Wwpo2Sap Wwpo2Sap)
        {
            this.DataProvider.Delete(Wwpo2Sap);
        }

        public void UpdateWwpo2Sap(Wwpo2Sap Wwpo2Sap)
        {
            this.DataProvider.Update(Wwpo2Sap);
        }

        public object GetWwpo2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Wwpo2Sap), new object[] { Serial });
        }

        #endregion

        #region Dn_in2Sap
        /// <summary>
        /// TBLDn_in2Sap
        /// </summary>
        public Dn_in2Sap CreateNewDn_in2Sap()
        {
            return new Dn_in2Sap();
        }

        public void AddDn_in2Sap(Dn_in2Sap Dn_in2Sap)
        {
            this.DataProvider.Insert(Dn_in2Sap);
        }

        public void DeleteDn_in2Sap(Dn_in2Sap Dn_in2Sap)
        {
            this.DataProvider.Delete(Dn_in2Sap);
        }

        public void UpdateDn_in2Sap(Dn_in2Sap Dn_in2Sap)
        {
            this.DataProvider.Update(Dn_in2Sap);
        }

        public object GetDn_in2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Dn_in2Sap), new object[] { Serial });
        }

        #endregion

        #region Ub2Sap
        /// <summary>
        /// TBLUb2Sap
        /// </summary>
        public Ub2Sap CreateNewUb2Sap()
        {
            return new Ub2Sap();
        }

        public void AddUb2Sap(Ub2Sap Ub2Sap)
        {
            this.DataProvider.Insert(Ub2Sap);
        }

        public void DeleteUb2Sap(Ub2Sap Ub2Sap)
        {
            this.DataProvider.Delete(Ub2Sap);
        }

        public void UpdateUb2Sap(Ub2Sap Ub2Sap)
        {
            this.DataProvider.Update(Ub2Sap);
        }

        public object GetUb2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Ub2Sap), new object[] { Serial });
        }

        #endregion

        #region StockScrap2Sap
        /// <summary>
        /// TBLStockScrap2Sap
        /// </summary>
        public StockScrap2Sap CreateNewStockScrap2Sap()
        {
            return new StockScrap2Sap();
        }

        public void AddStockScrap2Sap(StockScrap2Sap StockScrap2Sap)
        {
            this.DataProvider.Insert(StockScrap2Sap);
        }

        public void DeleteStockScrap2Sap(StockScrap2Sap StockScrap2Sap)
        {
            this.DataProvider.Delete(StockScrap2Sap);
        }

        public void UpdateStockScrap2Sap(StockScrap2Sap StockScrap2Sap)
        {
            this.DataProvider.Update(StockScrap2Sap);
        }

        public object GetStockScrap2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(StockScrap2Sap), new object[] { Serial });
        }

        #endregion
        #region DN2Sap
        /// <summary>
        /// TBLDN2Sap
        /// </summary>
        public DN2Sap CreateNewDN2Sap()
        {
            return new DN2Sap();
        }

        public void AddDN2Sap(DN2Sap DN2Sap)
        {
            this.DataProvider.Insert(DN2Sap);
        }

        public void DeleteDN2Sap(DN2Sap DN2Sap)
        {
            this.DataProvider.Delete(DN2Sap);
        }

        public void UpdateDN2Sap(DN2Sap DN2Sap)
        {
            this.DataProvider.Update(DN2Sap);
        }

        public object GetDN2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(DN2Sap), new object[] { Serial });
        }
        public void UpdateDN2Sap(string dnno, string message, string result)
        {
            string sql = string.Format(@"update tblDN2Sap  set Message='{0}', RESULT='{1}' ,mdate=sys_date,mtime=sys_time where dnno='{2}' ", message, result, dnno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion
        #endregion


        public AlertMailSetting[] QueryAlertMailSetting()
        {
            string sql = "SELECT * FROM TBLALERTMAILSETTING";

            object[] objs = this.DataProvider.CustomQuery(typeof(AlertMailSetting), new SQLCondition(sql));
            List<AlertMailSetting> settings = new List<AlertMailSetting>();
            if (objs != null && objs.Length > 0)
            {
                foreach (AlertMailSetting setting in objs)
                    settings.Add(setting);
            }
            return settings.ToArray();
        }


        public object[] GetSQERejects(DateTime now, int minRange)
        {
            string sql = @"SELECT A.QTY,A.NGQTY,A.ReturnQTY,A.ReformQTY,A.GiveQTY,A.AcceptQTY,B.STNO,a.IQCNO,B.CDATE,C.INVNO FROM (SELECT IQCNO,SUM(NGQTY) NGQTY,SUM(QTY) QTY,SUM(ReturnQTY) ReturnQTY,SUM(ReformQTY) ReformQTY,SUM(GiveQTY) GiveQTY,SUM(AcceptQTY) AcceptQTY FROM  TBLASNIQCDETAIL WHERE 1=1 " + SendMailTimeFilter(string.Empty, now, minRange) + " AND (NGQTY>0 OR ReturnQTY>0 OR ReformQTY>0 OR GiveQTY>0 OR AcceptQTY>0)   GROUP BY IQCNO) A ,TBLASNIQC B ,TBLASN C where A.IQCNO=B.IQCNO  AND B.STNO=C.STNO ";


            object[] objs = this.DataProvider.CustomQuery(typeof(SQEReject),
                 new SQLCondition(sql));


            List<SQEReject> settings = new List<SQEReject>();
            if (objs != null && objs.Length > 0)
            {
                foreach (SQEReject setting in objs)
                    settings.Add(setting);
            }
            return settings.ToArray();
        }

        public string SendMailTimeFilter(string t_prefix, DateTime timestamp, int range)
        {
            DateTime now = timestamp;

            DateTime minPrevious = timestamp.AddMinutes(range);
            string filter = string.Empty;

            string table_prefix = string.IsNullOrEmpty(t_prefix) ? t_prefix : t_prefix + ".";
            if (now.Day == minPrevious.Day)
                filter = "AND " + table_prefix + "MDATE=" + FormatHelper.TODateInt(minPrevious) + " AND " + table_prefix + "MTIME>= " + FormatHelper.TOTimeInt(minPrevious);
            else
                filter = "AND ((" + table_prefix + "MDATE=" + FormatHelper.TODateInt(minPrevious) + " AND " + table_prefix + "MTIME>= " + FormatHelper.TOTimeInt(minPrevious) + ") or (" + table_prefix + "MDATE=" + FormatHelper.TODateInt(now) + " AND " + table_prefix + "MTIME<=" + FormatHelper.TOTimeInt(now) + " ))";
            return filter;
        }

        public DateTime timeNow()
        {
            return new DateTime(2016, 8, 31, 10, 54, 32);

        }

        public DateTime timePervious15Min()
        {
            return new DateTime(2016, 8, 31, 10, 54, 32);
        }

        public Asn GetASN(string Stno)
        {
            string sql = "SELECT * FROM TBLASN A LEFT JOIN TBLVendor B ON A.VENDORCODE=B.VENDORCODE WHERE STNO='" + Stno + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return (Asn)objs[0];
            else
                return null;

        }

        public AsnIQC[] QueryIQCReleases()
        {
            string sql = "SELECT * FROM TBLASNIQC WHERE STATUS='Release'";

            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));
            List<AsnIQC> iqcs = new List<AsnIQC>();
            if (objs != null && objs.Length > 0)
            {
                foreach (AsnIQC oqc in objs)
                    iqcs.Add(oqc);
            }
            return iqcs.ToArray();
        }

        public OQC[] QueryOQCReleases()
        {
            string sql = "SELECT * FROM TBLOQC WHERE STATUS='Release'";

            object[] objs = this.DataProvider.CustomQuery(typeof(OQC), new SQLCondition(sql));
            List<OQC> oqcs = new List<OQC>();
            if (objs != null && objs.Length > 0)
            {
                foreach (OQC oqc in objs)
                    oqcs.Add(oqc);
            }
            return oqcs.ToArray();
        }


        public void AddSendMail(SendMail mail)
        {
            this.DataProvider.Insert(mail);
        }

        public OQCDetailEC[] QueryOQCNoHandleEcs()
        {
            string sql = @"SELECT T2.*,T3.PICKNO,T1.TOTAL,T1.NOHANDLE FROM TBLOQCDETAILEC T2,TBLOQC T3,
                          (select a.total,b.nohandle ,a.oqcno from (SELECT count(*) total,oqcno FROM TBLOQCDETAILEC Group by oqcno) A,(SELECT count(*) nohandle,oqcno FROM TBLOQCDETAILEC WHERE SQESTATUS IS NULL group by oqcno) B where a.oqcno=b.oqcno) T1 
                           WHERE T1.OQCNO=T2.OQCNO AND TOTAL=NOHANDLE AND T2.OQCNO=T3.OQCNO";

            object[] objs = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            List<OQCDetailEC> ecs = new List<OQCDetailEC>();
            if (objs != null && objs.Length > 0)
            {
                foreach (OQCDetailEC oqc in objs)
                    ecs.Add(oqc);
            }
            return ecs.ToArray();
        }


        public Asn[] QueryNoReceiveAsns()
        {
            string sql = "SELECT * FROM TBLASN WHERE (STATUS='Release' OR STATUS='WaitReceive') ";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(sql));
            List<Asn> asns = new List<Asn>();
            if (objs != null && objs.Length > 0)
            {
                foreach (Asn obj in objs)
                    asns.Add(obj);
            }
            return asns.ToArray();
        }


        public AsnIQCDetailEc[] QueryIQCNoHandleEcs()
        {
            string sql = @"SELECT T2.*,T1.TOTAL,T1.NOHANDLE FROM TBLASNIQCDETAILEC T2, 
                          (select a.total,b.nohandle ,a.iqcno from (SELECT count(*) total,iqcno FROM TBLASNIQCDETAILEC Group by iqcno) A,(SELECT count(*) nohandle,iqcno FROM TBLASNIQCDETAILEC WHERE SQESTATUS IS NULL group by iqcno) B where a.iqcno=b.iqcno) T1 
                           WHERE T1.IQCNO=T2.IQCNO AND TOTAL=NOHANDLE";

            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            List<AsnIQCDetailEc> ecs = new List<AsnIQCDetailEc>();
            if (objs != null && objs.Length > 0)
            {
                foreach (AsnIQCDetailEc oqc in objs)
                    ecs.Add(oqc);
            }
            return ecs.ToArray();
        }


        public OQCDetail[] GetOQCDetails(string oqcNO)
        {
            string sql = "SELECT DQMCODE,SUM(QTY) QTY  FROM TBLOQCDETAIL WHERE OQCNO='" + oqcNO + "' GROUP BY DQMCODE";
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCDetail), new SQLCondition(sql));
            List<OQCDetail> details = new List<OQCDetail>();
            if (objs != null && objs.Length > 0)
            {
                foreach (OQCDetail d in objs)
                    details.Add(d);
            }

            return details.ToArray();
        }

        public int GetAsnDetailCDate(string stno)
        {
            string sql = "SELECT max(cdate) cdate from tblasndetail where stno='" + stno + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((Asndetail)objs[0]).CDate;
            return 0;
        }

        public OQCDetail GetOQCDetailSummary(string oqcNO, string DQMCode)
        {
            string sql = "SELECT DQMCODE,SUM(QTY) QTY  FROM TBLOQCDETAIL WHERE OQCNO='" + oqcNO + "' AND DQMCODE='" + DQMCode + "' GROUP BY DQMCODE";
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((OQCDetail)objs[0]);
            return null;
        }

        public object GetAsnIQC(string Iqcno)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIQC), new object[] { Iqcno });
        }


        public BenQGuru.eMES.InterfaceDomain.Material GetMaterialFromDQMCode(string DQMCode)
        {

            string sql = "SELECT a.* FROM TBLMATERIAL a WHERE DQMCODE='" + DQMCode + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.InterfaceDomain.Material), new SQLCondition(sql));
            if (o != null && o.Length > 0)
            {
                return (BenQGuru.eMES.InterfaceDomain.Material)(o[0]);
            }
            return new BenQGuru.eMES.InterfaceDomain.Material();
        }


        public string Recipients(string forewarningName)
        {
            string sql = @"SELECT * FROM TBLALERTMAILSETTING WHERE ITEMSEQUENCE='" + forewarningName + "' ";

            object[] objs = this.DataProvider.CustomQuery(typeof(AlertMailSetting), new SQLCondition(sql));

            StringBuilder sb = new StringBuilder(200);
            if (objs != null && objs.Length > 0)
            {
                foreach (AlertMailSetting setting in objs)
                {
                    sb.Append(setting.Recipients);
                    sb.Append(";");
                }
            }
            return sb.ToString().TrimEnd(';');
        }

        public Asn[] QueryNoFinishYFRAsns()
        {
            string sql = "SELECT * FROM TBLASN WHERE STTYPE='YFR' AND STATUS<>'Close'";

            object[] objs = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(sql));
            List<Asn> details = new List<Asn>();
            if (objs != null && objs.Length > 0)
            {
                foreach (Asn d in objs)
                    details.Add(d);
            }

            return details.ToArray();
        }

        public Asndetail[] QueryAsnDetailsSummaryYFR(string stno)
        {
            string sql = "SELECT A.STNO,A.QTY,A.DQMCODE,C.CDATE FROM (SELECT STNO,SUM(QTY) QTY,DQMCODE FROM TBLASNDETAIL GROUP BY STNO,DQMCODE) A,TBLASN B,TBLInvoices C WHERE A.STNO=B.STNO AND B.INVNO=C.INVNO AND A.stno='" + stno + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            List<Asndetail> details = new List<Asndetail>();
            if (objs != null && objs.Length > 0)
            {
                foreach (Asndetail d in objs)
                    details.Add(d);
            }

            return details.ToArray();
        }



        public StorageDetailMe[] QueryStorageDetails()
        {
            string sql = "SELECT A.*,B.VALIDITY FROM TBLStorageDetail A  LEFT JOIN TBLMATERIAL B ON A.MCODE=B.MCODE ";
            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetailMe), new SQLCondition(sql));
            List<StorageDetailMe> storages = new List<StorageDetailMe>();
            if (objs != null && objs.Length > 0)
            {
                foreach (StorageDetailMe s in objs)
                    storages.Add(s);
            }
            return storages.ToArray();
        }


        public SendMail[] GetPendingMails(string mailName)
        {
            string sql = "SELECT A.* FROM TBLSENDMAIL A  WHERE A.MAILTYPE='" + mailName + "' AND  A.SENDFLAG='N' AND A.Recipients IS NOT NULL ORDER BY BLOCKNAME,BLOCKINX "; ;
            object[] objs = this.DataProvider.CustomQuery(typeof(SendMail), new SQLCondition(sql));
            List<SendMail> mails = new List<SendMail>();
            if (objs != null && objs.Length > 0)
            {
                string blockName = string.Empty;
                StringBuilder sb = null;
                SendMail mailObj = null;
                for (int i = 0; i < objs.Length; i++)
                {

                    SendMail s = (SendMail)objs[i];
                    if (string.IsNullOrEmpty(s.BLOCKNAME) && s.BLOCKINX == 0)
                    {
                        s.MAILCONTENT = s.MAILCONTENT.Replace(@"\r\n", "\r\n");
                        mails.Add(s);
                    }
                    else if (!string.IsNullOrEmpty(s.BLOCKNAME))
                    {
                        if (blockName != s.BLOCKNAME)
                        {
                            if (mailObj != null)
                            {
                                mailObj.MAILCONTENT = sb.ToString().Replace(@"\r\n", "\r\n");
                                mails.Add(mailObj);
                            }
                            sb = new StringBuilder(5000);
                            mailObj = s;
                            blockName = s.BLOCKNAME;

                        }
                        if (i == objs.Length - 1)
                        {
                            if (sb != null)
                            {
                                sb.Append(s.MAILCONTENT);
                                s.MAILCONTENT = sb.ToString().Replace(@"\r\n", "\r\n");
                                mails.Add(s);
                            }
                        }
                        if (sb != null && i != objs.Length - 1)
                            sb.Append(s.MAILCONTENT);
                    }

                }
            }
            return mails.ToArray();
        }



        public void UpdateBlocksMailSendStatus(SendMail mail)
        {
            string sql = string.Format("update TBLSENDMAIL set SENDFLAG='Y' where  BLOCKNAME='" + mail.BLOCKNAME + "'");
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);

        }

        public void UpdateSingleMailsSendStatus(SendMail mail)
        {


            string sql = string.Format("update TBLSENDMAIL set SENDFLAG='Y' where  SERIAL=" + mail.SERIAL);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);




        }
        public SendMail GetMailContent(string mailName)
        {
            string sql = "Select A.* FROM TBLSENDMAIL A WHERE MAILTYPE='" + mailName + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(SendMail), new SQLCondition(sql));

            StringBuilder sb = new StringBuilder(5000);
            if (objs != null && objs.Length > 0)
            {
                foreach (SendMail mail in objs)
                    sb.Append(mail.MAILCONTENT);

            }
            if (objs != null && objs.Length > 0)
            {
                ((SendMail)objs[0]).MAILCONTENT = sb.ToString();

            }
            return (SendMail)objs[0];
        }



        public int GetRecordCount(int dateFrom, int timeFrom)
        {
            string datetimeFrom = dateFrom.ToString() + timeFrom.ToString().PadLeft(6, '0');
            string sql = "select COUNT(*) from TBLSAPCLOSEPERIOD WHERE 1=1";
            sql += " AND ( '" + datetimeFrom + "'  BETWEEN STARTDATE*1000000+STARTTIME AND ENDDATE*1000000+ENDTIME)    ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


    }

    public class StorageDetailMe : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageDetailMe()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReworkApplyUser;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///最后一次入库时间
        ///</summary>
        [FieldMapAttribute("LASTSTORAGEAGEDATE", typeof(int), 22, true)]
        public int LastStorageAgeDate;

        ///<summary>
        ///第一次入库时间
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageAgeDate;

        ///<summary>
        ///生产日期
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int ProductionDate;

        ///<summary>
        ///鼎桥批次号
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///供应商批次号
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string SupplierLotNo;

        ///<summary>
        ///冻结数量
        ///</summary>
        [FieldMapAttribute("FREEZEQTY", typeof(int), 22, false)]
        public int FreezeQty;

        ///<summary>
        ///可用数量
        ///</summary>
        [FieldMapAttribute("AVAILABLEQTY", typeof(int), 22, false)]
        public int AvailableQty;

        ///<summary>
        ///库存数量
        ///</summary>
        [FieldMapAttribute("STORAGEQTY", typeof(int), 22, false)]
        public int StorageQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///货位条码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///有效期起算时间
        ///</summary>
        [FieldMapAttribute("ValidStartDate", typeof(int), 22, false)]
        public int ValidStartDate;

        ///<summary>
        ///有效期起算时间
        ///</summary>
        [FieldMapAttribute("VALIDITY", typeof(int), 22, false)]
        public int VALIDITY;
    }

    [Serializable, TableMap("TBLASNIQCDETAIL", "STLINE,IQCNO,STNO")]
    public class AsnIQCDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIQCDetail()
        {
        }

        ///<summary>
        ///送检单号
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///ASN单行项目
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string StLine;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///送检数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///检验通过数量
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
        public int QcPassQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///缺陷品数
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NgQty;

        ///<summary>
        ///退换货数量
        ///</summary>
        [FieldMapAttribute("RETURNQTY", typeof(int), 22, true)]
        public int ReturnQty;

        ///<summary>
        ///现场整改数量
        ///</summary>
        [FieldMapAttribute("REFORMQTY", typeof(int), 22, true)]
        public int ReformQty;

        ///<summary>
        ///让步接收数量
        ///</summary>
        [FieldMapAttribute("GIVEQTY", typeof(int), 22, true)]
        public int GiveQty;

        ///<summary>
        ///特采数量
        ///</summary>
        [FieldMapAttribute("ACCEPTQTY", typeof(int), 22, true)]
        public int AcceptQty;

        ///<summary>
        ///IQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }

    [Serializable, TableMap("TBLASN", "STNO")]
    public class Asn : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Asn()
        {
        }

        ///<summary>
        ///InitrejectQty
        ///</summary>
        [FieldMapAttribute("INITREJECTQTY", typeof(int), 22, true)]
        public int InitrejectQty;

        ///<summary>
        ///InitreceiveDesc
        ///</summary>
        [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
        public string InitreceiveDesc;

        ///<summary>
        ///InitreceiveQty
        ///</summary>
        [FieldMapAttribute("INITRECEIVEQTY", typeof(int), 22, true)]
        public int InitreceiveQty;

        ///<summary>
        ///InitgiveinQty
        ///</summary>
        [FieldMapAttribute("INITGIVEINQTY", typeof(int), 22, true)]
        public int InitgiveinQty;

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string Stno;

        ///<summary>
        ///StType
        ///</summary>
        [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
        public string StType;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string Invno;

        ///<summary>
        ///VEndorCode
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VEndorCode;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///Oano
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 40, true)]
        public string Oano;

        ///<summary>
        ///FacCode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///StorageCode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///FromfacCode
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 40, true)]
        public string FromfacCode;

        ///<summary>
        ///FromstorageCode
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 40, true)]
        public string FromstorageCode;

        ///<summary>
        ///Gross_weight
        ///</summary>
        [FieldMapAttribute("GROSS_WEIGHT", typeof(decimal), 22, true)]
        public decimal Gross_weight;

        ///<summary>
        ///Volume
        ///</summary>
        [FieldMapAttribute("VOLUME", typeof(string), 40, true)]
        public string Volume;

        ///<summary>
        ///Exigency_flag
        ///</summary>
        [FieldMapAttribute("EXIGENCY_FLAG", typeof(string), 1, true)]
        public string Exigency_flag;

        ///<summary>
        ///Direct_flag
        ///</summary>
        [FieldMapAttribute("DIRECT_FLAG", typeof(string), 1, true)]
        public string Direct_flag;

        ///<summary>
        ///Rejects_flag
        ///</summary>
        [FieldMapAttribute("REJECTS_FLAG", typeof(string), 1, true)]
        public string Rejects_flag;

        ///<summary>
        ///Pickno
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, true)]
        public string Pickno;

        ///<summary>
        ///Predict_Date
        ///</summary>
        [FieldMapAttribute("PREDICT_DATE", typeof(int), 22, true)]
        public int Predict_Date;

        ///<summary>
        ///Packinglistno
        ///</summary>
        [FieldMapAttribute("PACKINGLISTNO", typeof(string), 40, true)]
        public string Packinglistno;

        ///<summary>
        ///Provide_Date
        ///</summary>
        //[FieldMapAttribute("PROVIDE_DATE", typeof(int), 22, true)]
        //public int Provide_Date;

        [FieldMapAttribute("PROVIDE_DATE", typeof(string), 40, true)]
        public string Provide_Date;

        ///<summary>
        ///ReworkapplyUser
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReworkapplyUser;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;
        [FieldMapAttribute("VendorName", typeof(string), 200, true)]
        public string VendorName;



    }


    [Serializable, TableMap("TBLOQCDETAIL", "CARINVNO,OQCNO,CARTONNO,MCODE")]
    public class OQCDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public OQCDetail()
        {
        }

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///光伏包装序号 Purchase order item(SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 40, true)]
        public string GfPackingSeq;

        ///<summary>
        ///光伏华为编码 item your reference (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("GFHWITEMCODE", typeof(string), 40, true)]
        public string GfHwItemCode;

        ///<summary>
        ///OQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///让步放行数量
        ///</summary>
        [FieldMapAttribute("GIVEQTY", typeof(int), 22, true)]
        public int GiveQty;

        ///<summary>
        ///退换货数量
        ///</summary>
        [FieldMapAttribute("RETURNQTY", typeof(int), 22, true)]
        public int ReturnQty;

        ///<summary>
        ///缺陷品数
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NgQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///检验通过数量
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
        public int QcPassQty;

        ///<summary>
        ///送检数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///发货箱单号
        ///</summary>
        [FieldMapAttribute("CARINVNO", typeof(string), 40, false)]
        public string CarInvNo;

        ///<summary>
        ///OQC单号
        ///</summary>
        [FieldMapAttribute("OQCNO", typeof(string), 50, false)]
        public string OqcNo;

    }


    [Serializable, TableMap("TBLSENDMAIL", "SERIAL")]
    public class SendMail : DomainObject
    {
        public SendMail()
        {
        }


        [FieldMapAttribute("SERIAL", typeof(int), 40, false)]
        public int SERIAL;

        [FieldMapAttribute("BLOCKNAME", typeof(string), 40, true)]
        public string BLOCKNAME;

        [FieldMapAttribute("BLOCKINX", typeof(int), 40, true)]
        public int BLOCKINX;

        [FieldMapAttribute("MAILTYPE", typeof(string), 40, true)]
        public string MAILTYPE;

        ///<summary>
        ///ItemFirstClass
        ///</summary>	
        [FieldMapAttribute("SENDFLAG", typeof(string), 40, true)]
        public string SENDFLAG;

        ///<summary>
        ///ItemSecondClass
        ///</summary>	
        [FieldMapAttribute("MAILCONTENT", typeof(string), 40, true)]
        public string MAILCONTENT;



        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUSER;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("CDATE", typeof(int), 8, false)]
        public int CDATE;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("CTIME", typeof(int), 6, false)]
        public int CTIME;

        [FieldMapAttribute("Recipients", typeof(string), 6, false)]
        public string Recipients;

    }

    [Serializable, TableMap("TBLASNDETAIL", "STLINE,STNO")]
    public class Asndetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Asndetail()
        {
        }






        ///<summary>
        ///InitreceiveStatus
        ///</summary>
        [FieldMapAttribute("INITRECEIVESTATUS", typeof(string), 40, true)]
        public string InitreceiveStatus;

        ///<summary>
        ///InitreceiveDesc
        ///</summary>
        [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
        public string InitreceiveDesc;

        ///<summary>
        ///VEndormCodeDesc
        ///</summary>
        [FieldMapAttribute("VENDORMCODEDESC", typeof(string), 100, true)]
        public string VEndormCodeDesc;

        ///<summary>
        ///VEndormCode
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 40, true)]
        public string VEndormCode;

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string Stno;

        ///<summary>
        ///Stline
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string Stline;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string Cartonno;

        ///<summary>
        ///Cartonbigseq
        ///</summary>
        [FieldMapAttribute("CARTONBIGSEQ", typeof(string), 40, true)]
        public string Cartonbigseq;

        ///<summary>
        ///Cartonseq
        ///</summary>
        [FieldMapAttribute("CARTONSEQ", typeof(string), 40, true)]
        public string Cartonseq;

        ///<summary>
        ///CustmCode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustmCode;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqmCode;

        ///<summary>
        ///MDesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///ReceivemCode
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 40, true)]
        public string ReceivemCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///ReceiveQty
        ///</summary>
        [FieldMapAttribute("RECEIVEQTY", typeof(int), 22, true)]
        public int ReceiveQty;

        ///<summary>
        ///ActQty
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(int), 22, true)]
        public int ActQty;

        ///<summary>
        ///QcpassQty
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
        public int QcpassQty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Production_Date
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int Production_Date;

        ///<summary>
        ///Supplier_lotno
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string Supplier_lotno;

        ///<summary>
        ///Lotno
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;


        ///<summary>
        ///StorageageDate
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageageDate;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("LocationCode1", typeof(string), 22, false)]
        public string LocationCode1;

        [FieldMapAttribute("Remark2", typeof(string), 200, false)]
        public string Remark2;

        [FieldMapAttribute("INITGIVEINDESC", typeof(string), 40, false)]
        public string InitGIVEINDESC;

    }

    [Serializable, TableMap("TBLASNIQCDETAILEC", "SERIAL")]
    public class AsnIQCDetailEc : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIQCDetailEc()
        {
        }

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("SQECUSER", typeof(string), 40, false)]
        public string SQECUser;

        ///<summary>
        ///自增列
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Iqcno
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///Stline
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string StLine;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///EcgCode
        ///</summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, false)]
        public string EcgCode;

        ///<summary>
        ///ECode
        ///</summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ECode;

        ///<summary>
        ///NgQty
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NgQty;

        ///<summary>
        ///Sn
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, true)]
        public string SN;

        ///<summary>
        ///SqeStatus
        ///</summary>
        [FieldMapAttribute("SQESTATUS", typeof(string), 40, true)]
        public string SqeStatus;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("SQEREMARK1", typeof(string), 200, true)]
        public string SQERemark1;

        ///<summary>
        ///Ngflag
        ///</summary>
        [FieldMapAttribute("NGFLAG", typeof(string), 10, true)]
        public string NgFlag;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }

    [Serializable, TableMap("TBLASNIQC", "IQCNO")]
    public class AsnIQC : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AsnIQC()
        {
        }

        ///<summary>
        ///送检单号
        ///</summary>
        [FieldMapAttribute("IQCNO", typeof(string), 50, false)]
        public string IqcNo;

        ///<summary>
        ///检验方式
        ///</summary>
        [FieldMapAttribute("IQCTYPE", typeof(string), 40, true)]
        public string IqcType;

        ///<summary>
        ///ASN单号
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string StNo;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string InvNo;

        ///<summary>
        ///ASN单号类型
        ///</summary>
        [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
        public string StType;

        ///<summary>
        ///单据状态:Release:初始化；WaitCheck:待检验；SQEJudge:SQE判定；IQCClose:IQC检验完成；Cancel:取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///送检日期
        ///</summary>
        [FieldMapAttribute("APPDATE", typeof(int), 22, false)]
        public int AppDate;

        ///<summary>
        ///送检时间
        ///</summary>
        [FieldMapAttribute("APPTIME", typeof(int), 22, false)]
        public int AppTime;

        ///<summary>
        ///检验完成日期
        ///</summary>
        [FieldMapAttribute("INSPDATE", typeof(int), 22, true)]
        public int InspDate;

        ///<summary>
        ///检验完成时间
        ///</summary>
        [FieldMapAttribute("INSPTIME", typeof(int), 22, true)]
        public int InspTime;

        ///<summary>
        ///华为物料号
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustmCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///送检数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///IQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///供应商代码
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        ///<summary>
        ///供应商物料编码
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 35, true)]
        public string VendorMCode;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("AQLLEVEL", typeof(string), 40, false)]
        public string AQLLevel;

    }

    [Serializable, TableMap("TBLOQC", "OQCNO")]
    public class OQC : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public OQC()
        {
        }

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("AQL", typeof(string), 40, false)]
        public string AQL;




        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///OQC状态(Y:合格；N:不合格)
        ///</summary>
        [FieldMapAttribute("QCSTATUS", typeof(string), 40, true)]
        public string QcStatus;

        ///<summary>
        ///检验完成时间
        ///</summary>
        [FieldMapAttribute("INSPTIME", typeof(int), 22, true)]
        public int InspTime;

        ///<summary>
        ///检验完成日期
        ///</summary>
        [FieldMapAttribute("INSPDATE", typeof(int), 22, true)]
        public int InspDate;

        ///<summary>
        ///送检时间
        ///</summary>
        [FieldMapAttribute("APPTIME", typeof(int), 22, false)]
        public int AppTime;

        ///<summary>
        ///送检日期
        ///</summary>
        [FieldMapAttribute("APPDATE", typeof(int), 22, false)]
        public int AppDate;

        ///<summary>
        ///单据状态:Release:初始化；WaitCheck:待检验；SQEJudge:SQE判定；OQCClose:OQC检验完成；SQEFail:SQE检验FAIL；Cancel:取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///发货箱单号
        ///</summary>
        [FieldMapAttribute("CARINVNO", typeof(string), 40, false)]
        public string CarInvNo;

        ///<summary>
        ///拣货任务令号
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string PickNo;

        ///<summary>
        ///检验方式(SpotCheck:抽检；FullCheck:全检；ExemptCheck:免检)
        ///</summary>
        [FieldMapAttribute("OQCTYPE", typeof(string), 40, true)]
        public string OqcType;

        ///<summary>
        ///OQC单号
        ///</summary>
        [FieldMapAttribute("OQCNO", typeof(string), 50, false)]
        public string OqcNo;

    }


    [Serializable, TableMap("TBLOQCDETAILEC", "SERIAL")]
    public class OQCDetailEC : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public OQCDetailEC()
        {
        }

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("SQECUSER", typeof(string), 40, false)]
        public string SQECUser;

        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string PickNo;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("SQEREMARK1", typeof(string), 200, true)]
        public string SQERemark1;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///缺陷种类(Y:整箱缺陷；N:非整箱缺陷)
        ///</summary>
        [FieldMapAttribute("NGFLAG", typeof(string), 10, true)]
        public string NgFlag;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///SQE判定状态(Return:退换货；Give:让步放行)
        ///</summary>
        [FieldMapAttribute("SQESTATUS", typeof(string), 40, true)]
        public string SqeStatus;

        ///<summary>
        ///SN条码
        ///</summary>
        [FieldMapAttribute("SN", typeof(string), 40, false)]
        public string SN;

        ///<summary>
        ///缺陷品数
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NgQty;

        ///<summary>
        ///缺陷代码
        ///</summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ECode;

        ///<summary>
        ///缺陷类型
        ///</summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, false)]
        public string EcgCode;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///发货箱单号
        ///</summary>
        [FieldMapAttribute("CARINVNO", typeof(string), 40, false)]
        public string CarInvNo;

        ///<summary>
        ///OQC单号
        ///</summary>
        [FieldMapAttribute("OQCNO", typeof(string), 50, false)]
        public string OqcNo;

        ///<summary>
        ///自增列
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;
    }

    public class SQEReject : DomainObject
    {
        [FieldMapAttribute("NGQTY", typeof(int), 40, false)]
        public int NGQTY;
        [FieldMapAttribute("QTY", typeof(int), 40, false)]
        public int QTY;
        [FieldMapAttribute("ReturnQTY", typeof(int), 40, false)]
        public int ReturnQTY;
        [FieldMapAttribute("ReformQTY", typeof(int), 40, false)]
        public int ReformQTY;
        [FieldMapAttribute("GiveQTY", typeof(int), 40, false)]
        public int GiveQTY;
        [FieldMapAttribute("AcceptQTY", typeof(int), 40, false)]
        public int AcceptQTY;
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string STNO;
        [FieldMapAttribute("IQCNO", typeof(string), 40, false)]
        public string IQCNO;
        [FieldMapAttribute("CDATE", typeof(int), 40, false)]
        public int CDATE;
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string INVNO;
    }

    [Serializable, TableMap("TBLALERTMAILSETTING", "SERIAL")]
    public class AlertMailSetting : DomainObject
    {
        public AlertMailSetting()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summ
        [FieldMapAttribute("BIGSSCode", typeof(string), 40, true)]
        public string BIGSSCode;

        ///<summary>
        ///ItemFirstClass
        ///</summary>	
        [FieldMapAttribute("ITEMFIRSTCLASS", typeof(string), 40, true)]
        public string ItemFirstClass;

        ///<summary>
        ///ItemSecondClass
        ///</summary>	
        [FieldMapAttribute("ITEMSECONDCLASS", typeof(string), 40, true)]
        public string ItemSecondClass;

        ///<summary>
        ///Recipients
        ///</summary>	
        [FieldMapAttribute("RECIPIENTS", typeof(string), 2000, false)]
        public string Recipients;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }


    [Serializable, TableMap("TBLALERTITEM", "ITEMSEQUENCE")]
    public class AlertItem : DomainObject
    {
        public AlertItem()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///Description
        ///</summary>	
        [FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
        public string Description;

        ///<summary>
        ///AlertType
        ///</summary>	
        [FieldMapAttribute("ALERTTYPE", typeof(string), 40, false)]
        public string AlertType;

        ///<summary>
        ///MailSubject
        ///</summary>	
        [FieldMapAttribute("MAILSUBJECT", typeof(string), 150, false)]
        public string MailSubject;

        ///<summary>
        ///MailContent
        ///</summary>	
        [FieldMapAttribute("MAILCONTENT", typeof(string), 2000, false)]
        public string MailContent;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }
}
