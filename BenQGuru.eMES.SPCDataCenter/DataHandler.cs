using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Data;
using System.Text;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.SPC;

namespace BenQGuru.eMES.SPCDataCenter
{
    /// <summary>
    /// DataHandler 的摘要说明。
    /// </summary>
    public class DataHandler
    {
        public DataHandler(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        private IDomainDataProvider _domainDataProvider = null;
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

        private string BuildFilterInQueryNew(string[] objectCode, string itemCode, string moCode, string lotNo, string rcardStart, string rcardEnd, int dateStart, int dateEnd, string lineCode, string resourceCode, int groupSeq, int timeFrom, string testResult)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("WHERE 1=1 ");
            sb.Append("  AND TBLTESTDATA.ckitemcode = NVL(TBLSPCOBJECTSTORE.ckitemcode, TBLTESTDATA.ckitemcode)");
            if (itemCode != string.Empty)
            {
                if (itemCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLTESTDATA.ItemCode='" + itemCode.ToUpper() + "' ");
                else
                    sb.Append(" AND TBLTESTDATA.ItemCode IN ('" + itemCode.ToUpper().Replace(",", "','") + "') ");
            }
            if (objectCode != null && objectCode.Length > 0)
            {
                sb.Append(" AND TBLSPCOBJECTSTORE.objectCode in ( ");
                foreach (string str in objectCode)
                {
                    sb.Append(" '" + str + "' ");
                }
                sb.Append(" ) ");
            }
            if (groupSeq.ToString() != string.Empty)
            {
                sb.Append(" AND TBLSPCOBJECTSTORE.GroupSEQ = " + groupSeq.ToString());
            }
            if (moCode != string.Empty)
            {
                if (moCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLMESENTITYLIST.MOCode='" + moCode.ToUpper() + "' ");
                else
                    sb.Append(" AND TBLMESENTITYLIST.MOCode IN ('" + moCode.ToUpper().Replace(",", "','") + "') ");
            }

            if (rcardStart != string.Empty && rcardStart == rcardEnd)
            {
                if (rcardStart.IndexOf(",") < 0)
                    sb.Append(" AND TBLTESTDATA.RCard='" + rcardStart + "' ");
                else
                    sb.Append(" AND TBLTESTDATA.RCard IN ('" + rcardStart.Replace(",", "','") + "') ");
            }
            else
            {
                if (rcardStart != string.Empty)
                    sb.Append(" AND TBLTESTDATA.RCard>='" + rcardStart + "' ");
                if (rcardEnd != string.Empty)
                    sb.Append(" AND TBLTESTDATA.RCard<='" + rcardEnd + "' ");
            }
            if (dateStart > 0)
                sb.Append(" AND TBLTESTDATA.TestingDate>=" + dateStart.ToString() + " ");

            if (dateEnd > 0)
                sb.Append(" AND TBLTESTDATA.TestingDate<=" + dateEnd.ToString() + " ");
            if (lineCode != string.Empty)
            {
                if (lineCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLMESENTITYLIST.SSCode='" + lineCode + "' ");
                else
                    sb.Append(" AND TBLMESENTITYLIST.SSCode IN ('" + lineCode.Replace(",", "','") + "') ");
            }
            if (resourceCode != string.Empty)
            {
                if (resourceCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLMESENTITYLIST.ResCode='" + resourceCode + "' ");
                else
                    sb.Append(" AND TBLMESENTITYLIST.ResCode IN ('" + resourceCode.Replace(",", "','") + "') ");
            }
            if (timeFrom > 0)
                sb.Append(" AND TBLTESTDATA.TestingTime>" + timeFrom.ToString() + " ");
            if (testResult != string.Empty)
                sb.Append(" AND TBLTESTDATA.TestingResult='" + testResult + "' ");

            string strDataFilter = string.Empty;
            strDataFilter += " NOT TBLTESTDATA.TESTINGVALUE IS NULL  ";
            sb.Append(" AND (" + strDataFilter + ") ");
            return sb.ToString();
        }

        private string BuildFilterInQueryRes(string[] objectCode, string itemCode, string moCode, string lotNo, string rcardStart, string rcardEnd, int dateStart, int dateEnd, string lineCode, string resourceCode, int groupSeq, int timeFrom, string testResult)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("WHERE 1=1 ");
            if (itemCode != string.Empty)
            {
                if (itemCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLTESTDATA.ItemCode='" + itemCode.ToUpper() + "' ");
                else
                    sb.Append(" AND TBLTESTDATA.ItemCode IN ('" + itemCode.ToUpper().Replace(",", "','") + "') ");
            }
            if (moCode != string.Empty)
            {
                if (moCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLMESENTITYLIST.MOCode='" + moCode.ToUpper() + "' ");
                else
                    sb.Append(" AND TBLMESENTITYLIST.MOCode IN ('" + moCode.ToUpper().Replace(",", "','") + "') ");
            }

            if (rcardStart != string.Empty && rcardStart == rcardEnd)
            {
                if (rcardStart.IndexOf(",") < 0)
                    sb.Append(" AND TBLTESTDATA.RCard='" + rcardStart + "' ");
                else
                    sb.Append(" AND TBLTESTDATA.RCard IN ('" + rcardStart.Replace(",", "','") + "') ");
            }
            else
            {
                if (rcardStart != string.Empty)
                    sb.Append(" AND TBLTESTDATA.RCard>='" + rcardStart + "' ");
                if (rcardEnd != string.Empty)
                    sb.Append(" AND TBLTESTDATA.RCard<='" + rcardEnd + "' ");
            }
            if (dateStart > 0)
                sb.Append(" AND TBLTESTDATA.TestingDate>=" + dateStart.ToString() + " ");

            if (dateEnd > 0)
                sb.Append(" AND TBLTESTDATA.TestingDate<=" + dateEnd.ToString() + " ");
            if (lineCode != string.Empty)
            {
                if (lineCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLMESENTITYLIST.SSCode='" + lineCode + "' ");
                else
                    sb.Append(" AND TBLMESENTITYLIST.SSCode IN ('" + lineCode.Replace(",", "','") + "') ");
            }
            if (resourceCode != string.Empty)
            {
                if (resourceCode.IndexOf(",") < 0)
                    sb.Append(" AND TBLMESENTITYLIST.ResCode='" + resourceCode + "' ");
                else
                    sb.Append(" AND TBLMESENTITYLIST.ResCode IN ('" + resourceCode.Replace(",", "','") + "') ");
            }
            if (timeFrom > 0)
                sb.Append(" AND TBLTESTDATA.TestingTime>" + timeFrom.ToString() + " ");
            if (testResult != string.Empty)
                sb.Append(" AND TBLTESTDATA.TestingResult='" + testResult + "' ");

            string strDataFilter = string.Empty;
            strDataFilter += " NOT TBLTESTDATA.TESTINGVALUE IS NULL  ";
            sb.Append(" AND (" + strDataFilter + ") ");
            return sb.ToString();
        }

        /// <summary>
        /// 查询SPC测试数据，返回每行数据格式：资源、日期、时间、值
        /// </summary>
        public string[][] QuerySPCData(string itemCode, string objectCode, int groupSeq, string resourceCode, int dateStart, int dateEnd, string testResult, int timeFrom)
        {
            string strWhere = BuildFilterInQueryNew(new string[] { objectCode }, itemCode, string.Empty, string.Empty, string.Empty, string.Empty, dateStart, dateEnd, string.Empty, resourceCode, groupSeq, timeFrom, testResult);
            if (strWhere.IndexOf("NULL") < 0)
                return null;

            DatabaseHelper helper = new DatabaseHelper();

            return helper.QuerySPCDataNew(groupSeq, strWhere);
        }

        /// <summary>
        /// 查询SPC测试数据的资源列表
        /// </summary>
        public string[] QuerySPCDataResource(string itemCode, string objectCode, int groupSeq, string resourceCode, int dateStart, int dateEnd, string testResult, int timeFrom)
        {
            string strWhere = BuildFilterInQueryRes(new string[] { objectCode }, itemCode, string.Empty, string.Empty, string.Empty, string.Empty, dateStart, dateEnd, string.Empty, string.Empty, groupSeq, timeFrom, testResult);

            if (strWhere.IndexOf("NULL") < 0)
                return new string[0];

            DatabaseHelper helper = new DatabaseHelper();

            return helper.QuerySPCDataResource(groupSeq, strWhere);
        }
 
    }
}
