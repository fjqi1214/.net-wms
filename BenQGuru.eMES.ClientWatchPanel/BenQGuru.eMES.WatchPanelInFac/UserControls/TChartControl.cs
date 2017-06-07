using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.WebQuery;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class TChartControl : System.Windows.Forms.UserControl
    {
        private Hashtable objectList = new Hashtable();
        public TChartControl()
        {
            InitializeComponent();
        }

        public void SetHeader(string hearName)
        {
            this.tChartData.Header.Lines = new string[] { hearName };
        }
        //Modified By Nettie Chen 2009/09/23
        //public void SetDataChartValue(object[] FinishedRateLineDataSource, object[] SemimanuRateLineDataSource,
        //                                      object[] FinishedProductDateLineDataSource, object[] SemimanuProductDateLineDataSource,
        //                                      object[] FinishedBarDataSource, object[] SemimanuBarDataSource, bool isFristLoad,
        //                                      object[] TPCodeList)
        public void SetDataChartValue(object[] FinishedRateLineDataSource, object[] SemimanuRateLineDataSource,
                                      object[] FinishedProductDateLineDataSource, object[] SemimanuProductDateLineDataSource,
                                      object[] FinishedBarDataSource, object[] SemimanuBarDataSource, bool isFristLoad,
                                      object[] TPCodeList, bool NeedFinishedProduct, bool NeedSemimanuProduct)
        //End Modified
        {
            GetAllTPCode(TPCodeList);
            //Added By Nettie Chen 2009/09/23
            if (NeedFinishedProduct == false)
            {
                SemimanuProductDateLine.Visible = false;
                FinishedRateLine.Visible = false;
            }
            if (NeedSemimanuProduct == false)
            {
                FinishedProductDateLine.Visible = false;
                SemimanuRateLine.Visible = false;
            }
            //End Added
            SetBarChartValue(FinishedBarDataSource, SemimanuBarDataSource, isFristLoad);
            SetRateLineChartValue(FinishedRateLineDataSource, SemimanuRateLineDataSource);
            SetProductDateLineChartValue(FinishedProductDateLineDataSource, SemimanuProductDateLineDataSource, isFristLoad);

            FinishedDataBar.ShowInLegend = false;
            SemimanuDataBar.ShowInLegend = false;
            FinishedRateLine.ShowInLegend = false;
            SemimanuRateLine.ShowInLegend = false;
           
            this.tChartData.Legend.Left = 30;
            this.tChartData.Refresh();
        }

        private void GetAllTPCode(object[] TPCodeList)
        {
            objectList.Clear();
            //获取所有的时段
            if (TPCodeList!=null)
            {
                for (int i = 0; i < TPCodeList.Length; i++)
                {
                    objectList.Add(i, ((NewReportDomainObject)TPCodeList[i]).PeriodCode);
                }
            }   
            //end
        }

        private void SetBarChartValue(object[] FinishedBarDataSource, object[] SemimanuBarDataSource, bool isFristLoad)
        {
            #region 成品

            Hashtable bigLineHashtable = new Hashtable();

            //如果是刷新数据，删除先前刷新的Series
            if (!isFristLoad && FinishedBarDataSource != null)
            {
                int addCount = 0;

                //得到产线个数
                foreach (NewReportDomainObject obj in FinishedBarDataSource)
                {
                    if (!bigLineHashtable.ContainsValue(obj.BigSSCode))
                    {
                        bigLineHashtable.Add(addCount, obj.BigSSCode);
                        addCount += 1;
                    }
                }

                for (int i = 0; i < bigLineHashtable.Count; i++)
                {
                    this.tChartData.Series.RemoveAt(6);
                }
            }

            if (!isFristLoad && SemimanuBarDataSource != null)
            {
                bigLineHashtable.Clear();
                int addCount = 0;

                foreach (NewReportDomainObject obj in SemimanuBarDataSource)
                {
                    if (!bigLineHashtable.ContainsValue(obj.BigSSCode))
                    {
                        bigLineHashtable.Add(addCount, obj.BigSSCode);
                        addCount += 1;
                    }
                }

                for (int i = 0; i < bigLineHashtable.Count; i++)
                {
                    this.tChartData.Series.RemoveAt(6);
                }
            }
            //end 


            //添加Series
            if (FinishedBarDataSource != null)
            {
                bigLineHashtable.Clear();
                int addCount = 0;
                //得到产线个数
                foreach (NewReportDomainObject obj in FinishedBarDataSource)
                {
                    if (!bigLineHashtable.ContainsValue(obj.BigSSCode))
                    {
                        bigLineHashtable.Add(addCount, obj.BigSSCode);
                        addCount += 1;
                    }
                }

                for (int i = 0; i < bigLineHashtable.Count; i++)
                {
                    object[] BarDataSource = new object[objectList.Count];
                    for (int j = 0; j < objectList.Count; j++)
                    {
                        bool isHave = false;
                        foreach (NewReportDomainObject obj in FinishedBarDataSource)
                        {
                            if (bigLineHashtable[i].ToString() == obj.BigSSCode)
                            {
                                if (objectList[j].ToString() == obj.PeriodCode)
                                {
                                    isHave = true;
                                    BarDataSource[j] = obj;
                                    break;
                                }
                            }

                            if (isHave)
                            {
                                break;
                            }
                        }

                        if (!isHave)
                        {
                            NewReportDomainObject NewReportDomainObject1 = new NewReportDomainObject();
                            NewReportDomainObject1.PeriodCode = objectList[j].ToString();
                            NewReportDomainObject1.Output = 0;
                            BarDataSource[j] = NewReportDomainObject1;
                        }
                    }

                    DataTable finishedBar_DataTable = new DataTable();

                    finishedBar_DataTable.Columns.Add("TimePeriodCode", typeof(string));
                    finishedBar_DataTable.Columns.Add("OutPutQty", typeof(int));
                    finishedBar_DataTable.AcceptChanges();

                    if (FinishedBarDataSource != null)
                    {
                        foreach (NewReportDomainObject obj in BarDataSource)
                        {
                            DataRow newRow = finishedBar_DataTable.NewRow();
                            newRow["TimePeriodCode"] = obj.PeriodCode;
                            newRow["OutPutQty"] = obj.Output;
                            finishedBar_DataTable.Rows.Add(newRow);
                        }

                        finishedBar_DataTable.AcceptChanges();
                    }

                    Steema.TeeChart.Styles.Bar FinishedDataBar1 = new Steema.TeeChart.Styles.Bar();

                    FinishedDataBar1.YValues.DataMember = finishedBar_DataTable.Columns["OutPutQty"].ToString();
                    FinishedDataBar1.LabelMember = finishedBar_DataTable.Columns["TimePeriodCode"].ToString();

                    FinishedDataBar1.Title = bigLineHashtable[i].ToString();
                    FinishedDataBar1.Marks.Visible = false;
                    FinishedDataBar1.DataSource = finishedBar_DataTable;

                    FinishedDataBar1.StackGroup = 1;
                    this.tChartData.Series.Add(FinishedDataBar1);
                }
            }

            #endregion

            #region 半成品

            bigLineHashtable.Clear();

            if (SemimanuBarDataSource != null)
            {
                int addCount = 0;
                //得到产线个数
                foreach (NewReportDomainObject obj in SemimanuBarDataSource)
                {
                    if (!bigLineHashtable.ContainsValue(obj.BigSSCode))
                    {
                        bigLineHashtable.Add(addCount, obj.BigSSCode);
                        addCount += 1;
                    }
                }

                //每条产线绑定Tchart
                for (int i = 0; i < bigLineHashtable.Count; i++)
                {
                    object[] BarDataSource = new object[objectList.Count];

                    for (int j = 0; j < objectList.Count; j++)
                    {
                        bool isHave = false;
                        foreach (NewReportDomainObject obj in SemimanuBarDataSource)
                        {
                            if (bigLineHashtable[i].ToString() == obj.BigSSCode)
                            {
                                if (objectList[j].ToString() == obj.PeriodCode)
                                {
                                    isHave = true;
                                    BarDataSource[j] = obj;
                                    break;
                                }
                            }

                            if (isHave)
                            {
                                break;
                            }
                        }

                        if (!isHave)
                        {
                            NewReportDomainObject NewReportDomainObject1 = new NewReportDomainObject();
                            NewReportDomainObject1.PeriodCode = objectList[j].ToString();
                            NewReportDomainObject1.Output = 0;
                            BarDataSource[j] = NewReportDomainObject1;
                        }
                    }

                    DataTable semimanuBar_DataTable = new DataTable();

                    semimanuBar_DataTable.Columns.Add("TimePeriodCode", typeof(string));
                    semimanuBar_DataTable.Columns.Add("OutPutQty", typeof(int));
                    semimanuBar_DataTable.AcceptChanges();

                    if (SemimanuBarDataSource != null)
                    {
                        DataRow newRow;
                        foreach (NewReportDomainObject obj in BarDataSource)
                        {
                            newRow = semimanuBar_DataTable.NewRow();
                            newRow["TimePeriodCode"] = obj.PeriodCode;
                            newRow["OutPutQty"] = obj.Output;
                            semimanuBar_DataTable.Rows.Add(newRow);
                        }
                        semimanuBar_DataTable.AcceptChanges();
                    }

                    Steema.TeeChart.Styles.Bar FinishedDataBar2 = new Steema.TeeChart.Styles.Bar();

                    FinishedDataBar2.YValues.DataMember = semimanuBar_DataTable.Columns["OutPutQty"].ToString();
                    FinishedDataBar2.LabelMember = semimanuBar_DataTable.Columns["TimePeriodCode"].ToString();

                    FinishedDataBar2.Title = bigLineHashtable[i].ToString();
                    FinishedDataBar2.Marks.Visible = false;
                    FinishedDataBar2.StackGroup = 2;

                    FinishedDataBar2.DataSource = semimanuBar_DataTable;
                    this.tChartData.Series.Add(FinishedDataBar2);
                }
            }

            #endregion
        }

        private void SetRateLineChartValue(object[] FinishedRateLineDataSource, object[] SemimanuRateLineDataSource)
        {
            #region 成品

            DataTable finishedRateLine_RateTable = new DataTable();

            finishedRateLine_RateTable.Columns.Add("TimePeriodCode", typeof(string));
            finishedRateLine_RateTable.Columns.Add("PassRate", typeof(double));
            finishedRateLine_RateTable.AcceptChanges();

            if (objectList.Count > 0)
            {
                NewReportDomainObject[] FinishedRateLineObject = new NewReportDomainObject[objectList.Count];

                for (int i = 0; i < objectList.Count; i++)
                {
                    bool isFind = false;
                    if (FinishedRateLineDataSource != null)
                    {
                        foreach (NewReportDomainObject obj in FinishedRateLineDataSource)
                        {
                            if (obj.PeriodCode.Trim().ToUpper() == objectList[i].ToString().Trim().ToUpper())
                            {
                                FinishedRateLineObject[i] = obj;
                                isFind = true;
                                break;
                            }
                        }
                    }

                    if (!isFind)
                    {
                        NewReportDomainObject NewReportDomainObject1 = new NewReportDomainObject();
                        NewReportDomainObject1.PeriodCode = objectList[i].ToString();
                        NewReportDomainObject1.PassRcardRate = 0;
                        FinishedRateLineObject[i] = NewReportDomainObject1;
                    }
                }


                if (FinishedRateLineObject != null)
                {
                    foreach (NewReportDomainObject obj in FinishedRateLineObject)
                    {
                        DataRow newRow = finishedRateLine_RateTable.NewRow();
                        newRow["TimePeriodCode"] = obj.PeriodCode;
                        newRow["PassRate"] = Math.Round(obj.PassRcardRate * 100, 2);
                        finishedRateLine_RateTable.Rows.Add(newRow);
                    }

                    finishedRateLine_RateTable.AcceptChanges();
                }
            }
            FinishedRateLine.Color = Color.LightCoral;
            FinishedRateLine.LinePen.Width = 2;
            FinishedRateLine.YValues.DataMember = finishedRateLine_RateTable.Columns["PassRate"].ToString();
            FinishedRateLine.LabelMember = finishedRateLine_RateTable.Columns["TimePeriodCode"].ToString();

            FinishedRateLine.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            FinishedRateLine.DataSource = finishedRateLine_RateTable;



            #endregion

            #region 半成品

            DataTable semimanuRateLine_RateTable = new DataTable();

            semimanuRateLine_RateTable.Columns.Add("TimePeriodCode", typeof(string));
            semimanuRateLine_RateTable.Columns.Add("PassRate", typeof(double));
            semimanuRateLine_RateTable.AcceptChanges();

            if (objectList.Count > 0)
            {
                NewReportDomainObject[] FinishedRateLineObject = new NewReportDomainObject[objectList.Count];

                for (int i = 0; i < objectList.Count; i++)
                {
                    bool isFind = false;
                    if (SemimanuRateLineDataSource != null)
                    {
                        foreach (NewReportDomainObject obj in SemimanuRateLineDataSource)
                        {
                            if (obj.PeriodCode.Trim().ToUpper() == objectList[i].ToString().Trim().ToUpper())
                            {
                                FinishedRateLineObject[i] = obj;
                                isFind = true;
                                break;
                            }
                        }
                    }

                    if (!isFind)
                    {
                        NewReportDomainObject NewReportDomainObject1 = new NewReportDomainObject();
                        NewReportDomainObject1.PeriodCode = objectList[i].ToString();
                        NewReportDomainObject1.PassRcardRate = 0;
                        FinishedRateLineObject[i] = NewReportDomainObject1;
                    }
                }

                if (FinishedRateLineObject != null)
                {
                    DataRow newRow;
                    foreach (NewReportDomainObject obj in FinishedRateLineObject)
                    {
                        newRow = semimanuRateLine_RateTable.NewRow();
                        newRow["TimePeriodCode"] = obj.PeriodCode;
                        newRow["PassRate"] = Math.Round(obj.PassRcardRate * 100, 2);
                        semimanuRateLine_RateTable.Rows.Add(newRow);
                    }

                    semimanuRateLine_RateTable.AcceptChanges();
                }

            }
            SemimanuRateLine.Color = Color.CornflowerBlue;
            SemimanuRateLine.LinePen.Width = 2;
            SemimanuRateLine.YValues.DataMember = semimanuRateLine_RateTable.Columns["PassRate"].ToString();
            SemimanuRateLine.LabelMember = semimanuRateLine_RateTable.Columns["TimePeriodCode"].ToString();

            SemimanuRateLine.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            SemimanuRateLine.DataSource = semimanuRateLine_RateTable;

            #endregion
        }

        private void SetProductDateLineChartValue(object[] FinishedProductDateLineDataSource, object[] SemimanuProductDateLineDataSource, bool isFristLoad)
        {
            #region 成品

            if (!isFristLoad)
            {
                for (int i = 0; i < 2; i++)
                {
                    this.tChartData.Series.RemoveAt(6);
                }
            }

            DataTable finishedProductDateLine_Table = new DataTable();

            finishedProductDateLine_Table.Columns.Add("TimePeriodCode", typeof(string));
            finishedProductDateLine_Table.Columns.Add("OutPutQty", typeof(int));
            finishedProductDateLine_Table.AcceptChanges();

            if (objectList.Count > 0)
            {
                NewReportDomainObject[] FinishedRateLineObject = new NewReportDomainObject[objectList.Count];

                for (int i = 0; i < objectList.Count; i++)
                {
                    bool isFind = false;
                    if (FinishedProductDateLineDataSource != null)
                    {
                        foreach (NewReportDomainObject obj in FinishedProductDateLineDataSource)
                        {
                            if (obj.PeriodCode.Trim().ToUpper() == objectList[i].ToString().Trim().ToUpper())
                            {
                                FinishedRateLineObject[i] = obj;
                                isFind = true;
                                break;
                            }
                        }
                    }

                    if (!isFind)
                    {
                        NewReportDomainObject NewReportDomainObject1 = new NewReportDomainObject();
                        NewReportDomainObject1.PeriodCode = objectList[i].ToString();
                        NewReportDomainObject1.PassRcardRate = 0;
                        FinishedRateLineObject[i] = NewReportDomainObject1;
                    }
                }

                if (FinishedRateLineObject != null)
                {
                    DataRow newRow;
                    foreach (NewReportDomainObject obj in FinishedRateLineObject)
                    {
                        newRow = finishedProductDateLine_Table.NewRow();
                        newRow["TimePeriodCode"] = obj.PeriodCode;
                        newRow["OutPutQty"] = obj.Output;
                        finishedProductDateLine_Table.Rows.Add(newRow);
                    }

                    finishedProductDateLine_Table.AcceptChanges();
                }
            }
            Steema.TeeChart.Styles.FastLine FinishedProductDateLine1 = new Steema.TeeChart.Styles.FastLine();

            FinishedProductDateLine1.YValues.DataMember = finishedProductDateLine_Table.Columns["OutPutQty"].ToString();
            FinishedProductDateLine1.LabelMember = finishedProductDateLine_Table.Columns["TimePeriodCode"].ToString();

            FinishedProductDateLine1.Color = Color.LightCoral;
            FinishedProductDateLine1.LinePen.Width = 2;
            FinishedProductDateLine1.ShowInLegend = false;
            FinishedProductDateLine1.Marks.Visible = true;
            FinishedProductDateLine1.Marks.Font.Size = 10;
            FinishedProductDateLine1.Marks.ArrowLength = 5;
            FinishedProductDateLine1.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            FinishedProductDateLine1.DataSource = finishedProductDateLine_Table;

            this.tChartData.Series.Add(FinishedProductDateLine1);

            #endregion

            #region 半成品

            DataTable semimanuProductDateLine_Table = new DataTable();

            semimanuProductDateLine_Table.Columns.Add("TimePeriodCode", typeof(string));
            semimanuProductDateLine_Table.Columns.Add("OutPutQty", typeof(int));
            semimanuProductDateLine_Table.AcceptChanges();

            if (objectList.Count > 0)
            {
                NewReportDomainObject[] FinishedRateLineObject = new NewReportDomainObject[objectList.Count];

                for (int i = 0; i < objectList.Count; i++)
                {
                    bool isFind = false;
                    if (SemimanuProductDateLineDataSource != null)
                    {
                        foreach (NewReportDomainObject obj in SemimanuProductDateLineDataSource)
                        {
                            if (obj.PeriodCode.Trim().ToUpper() == objectList[i].ToString().Trim().ToUpper())
                            {
                                FinishedRateLineObject[i] = obj;
                                isFind = true;
                                break;
                            }
                        }
                    }

                    if (!isFind)
                    {
                        NewReportDomainObject NewReportDomainObject1 = new NewReportDomainObject();
                        NewReportDomainObject1.PeriodCode = objectList[i].ToString();
                        NewReportDomainObject1.PassRcardRate = 0;
                        FinishedRateLineObject[i] = NewReportDomainObject1;
                    }
                }
                if (FinishedRateLineObject != null)
                {
                    foreach (NewReportDomainObject obj in FinishedRateLineObject)
                    {
                        DataRow newRow = semimanuProductDateLine_Table.NewRow();
                        newRow["TimePeriodCode"] = obj.PeriodCode;
                        newRow["OutPutQty"] = obj.Output;
                        semimanuProductDateLine_Table.Rows.Add(newRow);
                    }
                    semimanuProductDateLine_Table.AcceptChanges();
                }
            }
            Steema.TeeChart.Styles.FastLine FinishedProductDateLine2 = new Steema.TeeChart.Styles.FastLine();

            FinishedProductDateLine2.YValues.DataMember = semimanuProductDateLine_Table.Columns["OutPutQty"].ToString();
            FinishedProductDateLine2.LabelMember = semimanuProductDateLine_Table.Columns["TimePeriodCode"].ToString();

            FinishedProductDateLine2.Color = Color.CornflowerBlue;
            FinishedProductDateLine2.LinePen.Width = 2;
            FinishedProductDateLine2.Marks.Visible = true;
            FinishedProductDateLine2.Marks.Font.Color = Color.White;
            FinishedProductDateLine2.Marks.Transparent = true;
            FinishedProductDateLine2.Marks.Font.Size = 10;
            FinishedProductDateLine2.Marks.ArrowLength = 8;
            FinishedProductDateLine2.ShowInLegend = false;
            FinishedProductDateLine2.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            FinishedProductDateLine2.DataSource = semimanuProductDateLine_Table;

            this.tChartData.Series.Add(FinishedProductDateLine2);
            #endregion

        }

        public bool IsShowTChart
        {
            set { this.tChartData.Visible = value; }
        }
    }
}
