using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;


namespace BenQGuru.eMES.WatchPanelDisToLine
{
    public class WatchPanelDisToLineFacade
    {
        private IDomainDataProvider _domainDataProvider = null;
        public WatchPanelDisToLineFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }
        protected IDomainDataProvider DataProvider
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

        public object[] GetDisToLineQuery()
        {
            string sql = @"Select d.segcode,d.sscode,d.mocode,d.mcode,m.mname,
                    h.moplanqty,d.mssdisqty,d.mssleftqty,
                    nvl(wt.cycletime, 0) as cycletime,nvl(mb.mobitemqty, 0) as mobitemqty,
                    (d.mssleftqty / mobitemqty * cycletime) as lefttime,d.status
                          From tbldistolinedetail d
                         Inner join tbldistolinehead h
                            on h.mocode = d.mocode
                           and h.mcode = d.mcode
                          left join tblmaterial m
                            on m.mcode = d.mcode
                         inner join tblmo mo
                            on mo.mocode = d.mocode
                          left join tblmobom mb
                            on mb.mocode = d.mocode
                           and mb.mobitemcode = d.mcode
                          Left join tblplanworktime wt
                            on wt.itemcode = mo.itemcode
                           and wt.sscode = d.sscode
                         Where mo.mostatus not in ('mostatus_initial', 'mostatus_close')
                           and d.status in ('ShortDis', 'ERDis', 'WaitDis', 'Normal')
                         Order by d.status desc";
            return DataProvider.CustomQuery(typeof(DisToLineQuery), new SQLCondition(sql));
        }

        public object[] GetDisToLineQuery(DisToLineQuery disToLine)
        {
            string sql = @"Select d.segcode,d.sscode,d.mocode,d.mcode,m.mname,
                    h.moplanqty,d.mssdisqty,d.mssleftqty,
                    nvl(wt.cycletime, 0) as cycletime,nvl(mb.mobitemqty, 0) as mobitemqty,
                    (d.mssleftqty / mobitemqty * cycletime) as lefttime,d.status
                          From tbldistolinedetail d
                         Inner join tbldistolinehead h
                            on h.mocode = d.mocode
                           and h.mcode = d.mcode
                          left join tblmaterial m
                            on m.mcode = d.mcode
                         inner join tblmo mo
                            on mo.mocode = d.mocode
                          left join tblmobom mb
                            on mb.mocode = d.mocode
                           and mb.mobitemcode = d.mcode
                          Left join tblplanworktime wt
                            on wt.itemcode = mo.itemcode
                           and wt.sscode = d.sscode
                         Where mo.mostatus not in ('mostatus_initial', 'mostatus_close')
                           and d.status in ('ShortDis', 'ERDis', 'WaitDis', 'Normal')
                    and d.segcode='{0}' and d.sscode='{1}' and d.mocode='{2}' and d.mcode='{3}'";
            sql = string.Format(sql, disToLine.SegCode, disToLine.SSCode, disToLine.MOCode, disToLine.MCode);
            return DataProvider.CustomQuery(typeof(DisToLineQuery), new SQLCondition(sql));
        }
    }
}
