using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
    public class ReportSQLHelper
    {
        private IDomainDataProvider _DomainDataProvider = null;

        public ReportSQLHelper(IDomainDataProvider domainDataProvider)
        {
            this._DomainDataProvider = domainDataProvider;
            if (_DomainDataProvider == null)
            {
                _DomainDataProvider = DomainDataProviderManager.DomainDataProvider();
            }
        }

        private string GetPerformanceReportDetailedCoreTable(
            bool useFacCode, 
            bool useBigSS,
            bool excludeReworkOutput, 
            bool excludeLostManHour, 
            bool includeIndirectManHour)
        {
            string returnValue = string.Empty;

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(_DomainDataProvider);

            //获取标准机型工时
            double standardWorkingTime = 0;            
            double.TryParse(systemSettingFacade.GetParameterAlias("PERFORMANCEREPORT", "STANDARDWORKINGTIME"), out standardWorkingTime);
            if (standardWorkingTime == 0)
            {
                standardWorkingTime = 1;
            }

            //去除返工产出准备
            string excludeMOType = string.Empty;
            if (excludeReworkOutput)
            {
                object[] parameterArray = systemSettingFacade.GetParametersByParameterValue("MOTYPE", BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE);
                if (parameterArray != null)
                {
                    foreach (Parameter moType in parameterArray)
                    {
                        if (excludeMOType.Trim().Length > 0)
                        {
                            excludeMOType += ",";
                        }

                        excludeMOType += moType.ParameterCode;
                    }
                }
            }

            #region 第一部分：核心部分

            string corePart = string.Empty;

            string qtyField = "tblrptsoqty.molineoutputcount";
            string workingTimeField = "tblplanworktime.bigssworkingtime";
            if (useBigSS)
            {
                qtyField = "tblrptsoqty.molineoutputcount";
                workingTimeField = "tblplanworktime.bigssworkingtime";
            }
            else
            {
                qtyField = "tblrptsoqty.lineoutputcount";
                workingTimeField = "tblplanworktime.workingtime";
            }

            string groupFieldForCorePart = "tblproddetail.shiftdate, tblproddetail.shiftcode ";
            if (useFacCode)
            {
                groupFieldForCorePart += ", tblproddetail.faccode, tblitemclass.firstclass, tblline2crew.crewcode ";
            }
            else
            {
                groupFieldForCorePart += ", tblproddetail.sscode, tblproddetail.itemcode, tblproddetail.segcode ";
                if (!excludeLostManHour) //不需要减去LostManHour
                {
                    groupFieldForCorePart += ", tblproddetail.mocode ";
                }
            }

            DBDateTime now = FormatHelper.GetNowDBDateTime(this._DomainDataProvider);
            long dateTime = now.DBDate * 1000000L + now.DBTime;

            corePart += "SELECT SUM(NVL(tblproddetail.manhour, 0)) / 3600.0 AS manhour, ";
            corePart += "SUM(NVL(tblproddetail.duration, 0)) / 3600.0 AS duration, ";
            corePart += "SUM(DECODE(NVL(tblplanworktime.cycletime, 0), 0, 0, NVL(tblproddetail.duration, 0) / NVL(tblplanworktime.cycletime, 0))) AS planqty, ";
            corePart += "SUM(NVL(tblrptsoqty.qty, 0)) AS realqty, ";
            corePart += "SUM(NVL(tblrptsoqty.qty, 0) * NVL(" + workingTimeField + ", 0)) / 3600.0 AS acquiredmanhour, ";
            corePart += "SUM(DECODE(" + standardWorkingTime.ToString() + ", 0, 0, NVL(tblrptsoqty.qty, 0) * NVL(" + workingTimeField + ", 0) / " + standardWorkingTime.ToString() + ")) AS standardqty, ";
            corePart += "SUM((DECODE(NVL(tblplanworktime.cycletime, 0), 0, 0, NVL(tblproddetail.duration, 0) / NVL(tblplanworktime.cycletime, 0)) - NVL(tblrptsoqty.qty, 0)) * NVL(" + workingTimeField + ", 0)) / 3600.0 AS lostmanhour1, ";
            corePart += "SUM(NVL(tblproddetail.manhour, 0) - NVL(tblrptsoqty.qty, 0) * NVL(" + workingTimeField + ", 0)) / 3600.0 AS lostmanhour2, ";
            corePart += "MAX(NVL(tblplanworktime.workingtime, 0)) AS workingtime, ";
            corePart += "MAX(NVL(tblplanworktime.cycletime, 0)) AS cycletime, ";
            corePart += groupFieldForCorePart;
            corePart += "FROM ";
            corePart += "( ";
            corePart += "    SELECT tblproddetail.shiftdate, tblproddetail.shiftcode, tblproddetail.sscode, tblproddetail.mocode, "; 
            corePart += "    tblmo.itemcode, tblss.bigsscode, tblss.segcode, tblseg.faccode, "; 
            corePart += "    SUM(NVL(mancount, 0)) AS mancount, "; 
            corePart += "    SUM(NVL(duration, 0)) AS duration, ";
            corePart += "    SUM(NVL(mancount, 0) * ";

            corePart += "        (CASE WHEN " + dateTime.ToString() + " <= enddate * 1000000 + endtime ";
            corePart += "                  AND " + dateTime.ToString() + " >= begindate * 1000000 + begintime ";
            corePart += "              THEN (TO_DATE('" + dateTime.ToString() + "', 'yyyymmddhh24miss') - TO_DATE(TO_CHAR(begindate * 1000000 + begintime), 'yyyymmddhh24miss')) * 24 * 60 * 60 ";
            corePart += "              ELSE NVL(duration, 0) END) ";
            
            corePart += "    ) AS manhour ";
            corePart += "    FROM tblproddetail, tblmo, tblss, tblseg ";
            corePart += "    WHERE tblproddetail.mocode = tblmo.mocode ";
            corePart += "    AND tblproddetail.sscode = tblss.sscode ";
            corePart += "    AND tblss.segcode = tblseg.segcode ";
            corePart += "    GROUP BY tblproddetail.shiftdate, tblproddetail.shiftcode, tblproddetail.sscode, tblproddetail.mocode, "; 
            corePart += "    tblmo.itemcode, tblss.bigsscode, tblss.segcode, tblseg.faccode ";
            corePart += ") tblproddetail ";
            corePart += "LEFT OUTER JOIN( ";
            corePart += "    SELECT shiftday, mocode, itemcode, bigsscode, segcode, sscode, shiftcode, faccode, SUM(" + qtyField + ") AS qty ";
            corePart += "    FROM tblrptsoqty ";
            corePart += "    INNER JOIN tblmesentitylist ";
            corePart += "    ON tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial ";

            if (excludeMOType.Trim().Length > 0)
            {
                corePart += "    WHERE tblrptsoqty.mocode NOT IN (";
                corePart += "        SELECT mocode FROM tblmo WHERE motype IN (" + FormatHelper.ProcessQueryValues(excludeMOType) + ")) ";
            }

            corePart += "    GROUP BY shiftday, mocode, itemcode, bigsscode, segcode, sscode, shiftcode, faccode ";
            corePart += "    ) tblrptsoqty ";
            corePart += "ON tblrptsoqty.sscode = tblproddetail.sscode ";
            corePart += "AND tblrptsoqty.shiftday = tblproddetail.shiftdate ";
            corePart += "AND tblrptsoqty.shiftcode = tblproddetail.shiftcode ";
            corePart += "AND tblrptsoqty.mocode = tblproddetail.mocode ";

            if (useBigSS)
            {
                corePart += "LEFT OUTER JOIN (SELECT tblplanworktime.itemcode, tblplanworktime.sscode, tblplanworktime.cycletime, tblplanworktime.workingtime, ";
                corePart += "SUM(tblplanworktime.workingtime) OVER (PARTITION BY tblplanworktime.itemcode, tblss.bigsscode) AS bigssworkingtime ";
                corePart += "FROM tblplanworktime, tblss ";
                corePart += "WHERE tblplanworktime.sscode = tblss.sscode) tblplanworktime ";
            }
            else
            {
                corePart += "LEFT OUTER JOIN tblplanworktime ";
            }

            corePart += "ON tblplanworktime.itemcode = tblproddetail.itemcode ";
            corePart += "AND tblplanworktime.sscode = tblproddetail.sscode ";
            if (useFacCode)
            {
                corePart += "LEFT OUTER JOIN tblmaterial ";
                corePart += "ON tblmaterial.mcode = tblproddetail.itemcode ";
                corePart += "LEFT OUTER JOIN tblitemclass ";
                corePart += "ON tblitemclass.itemgroup = tblmaterial.mgroup ";
                corePart += "LEFT OUTER JOIN tblline2crew ";
                corePart += "ON tblline2crew.shiftdate = tblproddetail.shiftdate ";
                corePart += "AND tblline2crew.sscode = tblproddetail.sscode ";
                corePart += "AND tblline2crew.shiftcode = tblproddetail.shiftcode ";

            }
            corePart += "GROUP BY " + groupFieldForCorePart;

            #endregion

            #region 第二部分：损失工时

            string lostManHourPart = string.Empty;

            string groupFieldForLostManHourPart = "tbllostmanhourdetail.shiftdate, tbllostmanhourdetail.shiftcode ";
            if (useFacCode)
            {
                groupFieldForLostManHourPart += ", tblseg.faccode, tblitemclass.firstclass, tblline2crew.crewcode ";
            }
            else
            {
                groupFieldForLostManHourPart += ", tbllostmanhourdetail.sscode, tbllostmanhourdetail.itemcode ";
            }

            lostManHourPart += "SELECT SUM(NVL(tbllostmanhourdetail.lostmanhour, 0)) / 3600.0 AS lostmanhour, ";
            lostManHourPart += groupFieldForLostManHourPart;
            lostManHourPart += "FROM tbllostmanhourdetail ";
            lostManHourPart += "INNER JOIN tblexceptioncode ";
            lostManHourPart += "ON tbllostmanhourdetail.exceptioncode = tblexceptioncode.exceptioncode ";
            lostManHourPart += "AND tblexceptioncode.exceptionflag = 'Y' ";
            if (useFacCode)
            {
                lostManHourPart += "LEFT OUTER JOIN tblss ";
                lostManHourPart += "ON tbllostmanhourdetail.sscode = tblss.sscode ";
                lostManHourPart += "LEFT OUTER JOIN tblseg ";
                lostManHourPart += "ON tblseg.segcode = tblss.segcode ";
                lostManHourPart += "LEFT OUTER JOIN tblmaterial ";
                lostManHourPart += "ON tblmaterial.mcode = tbllostmanhourdetail.itemcode ";
                lostManHourPart += "LEFT OUTER JOIN tblitemclass ";
                lostManHourPart += "ON tblitemclass.itemgroup = tblmaterial.mgroup ";
                lostManHourPart += "LEFT OUTER JOIN tblline2crew ";
                lostManHourPart += "ON tblline2crew.shiftdate = tbllostmanhourdetail.shiftdate ";
                lostManHourPart += "AND tblline2crew.sscode = tbllostmanhourdetail.sscode ";
                lostManHourPart += "AND tblline2crew.shiftcode = tbllostmanhourdetail.shiftcode ";
            }
            lostManHourPart += "GROUP BY " + groupFieldForLostManHourPart;

            #endregion

            #region 第三部分：间接人力

            string indirectManHourPart = string.Empty;

            indirectManHourPart += "SELECT SUM (NVL(mancount, 0) * NVL(duration, 0)) / 3600.0 indirectmanhour, ";
            indirectManHourPart += "shiftdate, shiftcode, faccode, firstclass, crewcode ";
            indirectManHourPart += "FROM tblindirectmancount ";
            indirectManHourPart += "GROUP BY shiftdate, shiftcode, faccode, firstclass, crewcode ";

            #endregion

            #region 合并所有部分

            if (useFacCode)
            {
                returnValue += "SELECT a.shiftdate AS shiftday, a.shiftcode, a.faccode, a.firstclass, a.crewcode, ";
                returnValue += "a.manhour, a.duration, a.planqty, a.realqty, a.acquiredmanhour, a.standardqty, a.lostmanhour1, a.lostmanhour2 ";

                if (excludeLostManHour)
                {
                    returnValue += ", NVL(b.lostmanhour, 0) AS lostmanhour ";
                }

                if (includeIndirectManHour)
                {
                    returnValue += ", NVL(c.indirectmanhour, 0) AS indirectmanhour ";
                }

                returnValue += "FROM (" + corePart + ") a ";

                if (excludeLostManHour)
                {
                    returnValue += "LEFT OUTER JOIN (" + lostManHourPart + ") b ";
                    returnValue += "ON a.shiftdate = b.shiftdate ";
                    returnValue += "AND a.shiftcode = b.shiftcode ";
                    returnValue += "AND a.faccode = b.faccode ";
                    returnValue += "AND a.firstclass = b.firstclass ";
                    returnValue += "AND a.crewcode = b.crewcode ";
                }

                if (includeIndirectManHour)
                {
                    returnValue += "LEFT OUTER JOIN  (" + indirectManHourPart + ") c ";
                    returnValue += "ON a.shiftdate = c.shiftdate ";
                    returnValue += "AND a.shiftcode = c.shiftcode ";
                    returnValue += "AND a.faccode = c.faccode ";
                    returnValue += "AND a.firstclass = c.firstclass ";
                    returnValue += "AND a.crewcode = c.crewcode ";
                }
            }
            else
            {
                returnValue += "SELECT a.shiftdate AS shiftday, a.shiftcode, a.sscode, a.segcode, tblss.bigsscode, a.itemcode, a.workingtime, a.cycletime, ";
                returnValue += "a.manhour, a.duration, a.planqty, a.realqty, a.acquiredmanhour, a.standardqty, a.lostmanhour1, a.lostmanhour2 ";

                if (excludeLostManHour)
                {
                    returnValue += ", NVL(b.lostmanhour, 0) AS lostmanhour ";
                }
                else
                {
                    returnValue += ", a.mocode ";
                }

                returnValue += "FROM (" + corePart + ") a ";

                if (excludeLostManHour)
                {
                    returnValue += "LEFT OUTER JOIN (" + lostManHourPart + ") b ";
                    returnValue += "ON a.shiftdate = b.shiftdate ";
                    returnValue += "AND a.shiftcode = b.shiftcode ";
                    returnValue += "AND a.sscode = b.sscode ";
                    returnValue += "AND a.itemcode = b.itemcode ";
                }

                returnValue += "LEFT OUTER JOIN tblss ";
                returnValue += "ON tblss.sscode = a.sscode ";
            }

            #endregion

            return returnValue;
        }

        public string GetFormularForUPPH(bool excludeLostManHour, bool includeIndirectManHour)
        {
            string returnValue = string.Empty;

            returnValue = "SUM(manhour)";

            if (excludeLostManHour)
            {
                returnValue += " - SUM(lostmanhour)";
            }

            if (includeIndirectManHour)
            {
                returnValue += " + SUM(indirectmanhour)";
            }

            returnValue = "DECODE(" + returnValue + ", 0, 0, SUM(realqty) / (" + returnValue + ")) AS upph";

            returnValue += ", SUM(realqty) AS realqty, SUM(manhour) AS manhoursum";
            if (excludeLostManHour)
            {
                returnValue += ", SUM(lostmanhour) AS invalidmanhour";
            }
            if (includeIndirectManHour)
            {
                returnValue += ", SUM(indirectmanhour) AS indirectmanhour ";
            }

            return returnValue;
        }

        public string GetFormularForManHourPerProduct(bool excludeLostManHour, bool includeIndirectManHour)
        {
            string returnValue = string.Empty;

            returnValue = "SUM(manhour)";

            if (excludeLostManHour)
            {
                returnValue += " - SUM(lostmanhour)";
            }

            if (includeIndirectManHour)
            {
                returnValue += " + SUM(indirectmanhour)";
            }

            returnValue = "DECODE(SUM(standardqty), 0, 0, (" + returnValue + ") / SUM(standardqty)) AS manhourperproduct";

            returnValue += ", SUM(standardqty) AS standardqty, SUM(manhour) AS manhoursum";
            if (excludeLostManHour)
            {
                returnValue += ", SUM(lostmanhour) AS invalidmanhour";
            }
            if (includeIndirectManHour)
            {
                returnValue += ", SUM(indirectmanhour) AS indirectmanhour ";
            }

            return returnValue;
        }

        public string GetFormularForProduceEfficiency(bool excludeLostManHour, bool includeIndirectManHour)
        {
            string returnValue = string.Empty;

            returnValue = "SUM(manhour)";

            if (excludeLostManHour)
            {
                returnValue += " - SUM(lostmanhour)";
            }

            if (includeIndirectManHour)
            {
                returnValue += " + SUM(indirectmanhour)";
            }

            returnValue = "DECODE(" + returnValue + ", 0, 0, SUM(acquiredmanhour) / (" + returnValue + ")) AS produceefficiency";

            returnValue += ", SUM(acquiredmanhour) AS acquiredmanhour, SUM(manhour) AS manhoursum";
            if (excludeLostManHour)
            {
                returnValue += ", SUM(lostmanhour) AS invalidmanhour";
            }
            if (includeIndirectManHour)
            {
                returnValue += ", SUM(indirectmanhour) AS indirectmanhour ";
            }

            return returnValue;
        }

        public string GetFormularForDetail(bool excludeLostManHour, bool includeIndirectManHour, bool useItemCode)
        {
            string returnValue = string.Empty;

            string manHour = string.Empty;
            if (excludeLostManHour)
            {
                manHour += " - lostmanhour ";
            }
            if (includeIndirectManHour)
            {
                manHour += " + indirectmanhour ";
            }

            if (useItemCode)
            {
                returnValue += "SUM(workingtime) AS workingtime, ";
                returnValue += "SUM(cycletime) AS cycletime, ";
            }

            returnValue += "SUM(manhour) AS manhoursum, ";
            returnValue += "SUM(duration) AS inputduration, ";


            returnValue += "SUM(planqty) AS planqty, ";
            returnValue += "SUM(realqty) AS realqty, ";
            returnValue += "SUM(planqty - realqty) AS lostqty, ";

            returnValue += "SUM(lostmanhour1) AS lostmanhour1, ";
            returnValue += "SUM(lostmanhour2 " + manHour + ") AS lostmanhour2, ";
            returnValue += "DECODE(SUM(manhour), 0, 0, SUM(manhour - lostmanhour) / SUM(manhour)) AS utilization ";
            returnValue += ", SUM(lostmanhour) AS invalidmanhour ";
        
            if (includeIndirectManHour)
            {
                returnValue += ", SUM(indirectmanhour) AS indirectmanhour ";
            }

            return returnValue;
        }

        public string GetPerformanceReportSQL(
            bool useFacCode, 
            bool useBigSS,
            bool excludeReworkOutput, 
            bool excludeLostManHour, 
            bool includeIndirectManHour, 
            string whereFields, 
            string groupFieldsX, 
            string groupFieldsY, 
            string formularFields)
        {
            string returnValue = string.Empty;

            ReportSQLEngine engine = new ReportSQLEngine(null, null);
            engine.DetailedCoreTable = GetPerformanceReportDetailedCoreTable(useFacCode, useBigSS, excludeReworkOutput, excludeLostManHour, includeIndirectManHour);
            engine.Formular = formularFields;
            engine.WhereCondition = whereFields;
            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            returnValue = engine.GetReportSQL();

            return returnValue;
        }
    }
}
