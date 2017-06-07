using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.SAPRFCService;
using SAP.Middleware.Connector;

namespace TestRFCToSAP
{
    public partial class MESToSAPFrm : Form
    {
        public MESToSAPFrm()
        {
            InitializeComponent();

            cbAllPGI.SelectedIndex = 0;
            cbPGIorRe.SelectedIndex = 0;
            cbUBInOutFlag1.SelectedIndex = 0;
            cbUBInOutFlag2.SelectedIndex = 0;
            //dgvWWPOList.AutoGenerateColumns = false;
            cbWWPOInOutFlag1.SelectedIndex = 0;
            cbWWPOInOutFlag2.SelectedIndex = 0;
            cbRSInOutFlag1.SelectedIndex = 0;
            cbRSInOutFlag2.SelectedIndex = 0;
        }

        private void btnSendPO_Click(object sender, EventArgs e)
        {
            try
            {
                List<PO> list = new List<PO>();
                PO po = new PO();
                po.PONO = txtPONO.Text;
                po.POLine = Convert.ToInt32(txtPOLine.Text);
                po.FacCode = txtFacCode.Text;
                po.SerialNO = txtSerialNO.Text;
                po.MCode = txtMCode.Text;
                po.Qty = Convert.ToDecimal(txtQty.Text);
                po.Unit = txtUnit.Text;
                if (rbPOReceive.Checked == true)
                {
                    po.Status = "103";
                }
                else if (rbPOReverse.Checked == true)
                {
                    po.Status = "104";
                }
                else if (rbPOAccept.Checked == true)
                {
                    po.Status = "105";
                }
                else
                {
                    po.Status = "101";
                }
                po.SAPMaterialInvoice = txtSAPMaterialInvoice.Text;
                po.Operator = txtOperator.Text;
                po.VendorInvoice = txtVendorInvoice.Text;
                po.StorageCode = txtStorageCode.Text;
                po.Remark = txtRemark.Text;
                po.InvoiceDate = Convert.ToInt32(txtInvoiceDate.Text);

                list.Add(po);

                if (!string.IsNullOrEmpty(txtPONO2.Text))
                {
                    PO po2 = new PO();
                    po2.PONO = txtPONO2.Text;
                    po2.POLine = Convert.ToInt32(txtPOLine2.Text);
                    po2.FacCode = txtFacCode2.Text;
                    po2.SerialNO = txtSerialNO2.Text;
                    po2.MCode = txtMCode2.Text;
                    po2.Qty = Convert.ToDecimal(txtQty2.Text);
                    po2.Unit = txtUnit2.Text;
                    if (rbPOReceive2.Checked == true)
                    {
                        po2.Status = "103";
                    }
                    else if (rbPOReverse2.Checked == true)
                    {
                        po2.Status = "104";
                    }
                    else if (rbPOAccept2.Checked == true)
                    {
                        po2.Status = "105";
                    }
                    else
                    {
                        po2.Status = "101";
                    }
                    po2.SAPMaterialInvoice = txtSAPMaterialInvoice2.Text;
                    po2.Operator = txtOperator2.Text;
                    po2.VendorInvoice = txtVendorInvoice2.Text;
                    po2.StorageCode = txtStorageCode2.Text;
                    po2.Remark = txtRemark2.Text;
                    po2.InvoiceDate = Convert.ToInt32(txtInvoiceDate2.Text);

                    list.Add(po2);
                }


                POToSAP poToSAP = new POToSAP();
                SAPRfcReturn msg = poToSAP.POReceiveToSAP(list);
                ShowMessage("====================" + btnSendPO.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + msg.Result);
                ShowMessage("SAP物料凭证编码：" + msg.MaterialDocument);
                ShowMessage("执行结果信息：" + msg.Message + "\r\n");
                //object parameter = null;
                //if (exportParameters != null)
                //{
                //    if (exportParameters.TryGetValue("ES_RESULT", out parameter))
                //    {
                //        IRfcStructure ROFStrcture = parameter as IRfcStructure;
                //        ShowMessage("执行结果标识(S表示成功，E表示失败)：" + ROFStrcture["RETUN"].GetValue().ToString());
                //        ShowMessage("SAP物料凭证编码：" + ROFStrcture["MBLNR"].GetValue().ToString());
                //        ShowMessage("执行结果信息：" + ROFStrcture["MESSG"].GetValue().ToString());
                //    }
                //}
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void btnClearPO_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox1.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            foreach (var control in groupBox2.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }

        private void btnSendDN_Click(object sender, EventArgs e)
        {
            try
            {
                List<DN> dnList = new List<DN>();
                SAPRfcReturn msg = new SAPRfcReturn();
                DNToSAP dnToSAP = new DNToSAP();

                DN dn = new DN();
                dn.DNNO = txtDNNO.Text;
                //整单过账
                if (cbPGIorRe.SelectedIndex == 0 && cbAllPGI.SelectedIndex == 0)
                {
                    dnList.Add(dn);
                    msg = dnToSAP.DNPGIToSAP(dnList, true);
                }
                else if(cbPGIorRe.SelectedIndex == 1)//PGI冲销
                {
                    dnList.Add(dn);
                    msg = dnToSAP.DNRePGIToSAP(dn);
                }
                else
                {
                    dn.DNLine = Convert.ToInt32(txtDNLine1.Text);
                    dn.Qty = Convert.ToDecimal(txtDNActQty1.Text);

                    if (!string.IsNullOrEmpty(txtDNLine2.Text))
                    {
                        DN dn2 = new DN();
                        dn2.DNNO = txtDNNO.Text;
                        dn2.DNLine = Convert.ToInt32(txtDNLine2.Text);
                        dn2.Qty = Convert.ToDecimal(txtDNActQty2.Text);
                        dnList.Add(dn2);
                    }
                    if (!string.IsNullOrEmpty(txtDNLine3.Text))
                    {
                        DN dn3 = new DN();
                        dn3.DNNO = txtDNNO.Text;
                        dn3.DNLine = Convert.ToInt32(txtDNLine3.Text);
                        dn3.Qty = Convert.ToDecimal(txtDNActQty3.Text);
                        dnList.Add(dn3);
                    }
                    if (!string.IsNullOrEmpty(txtDNLine4.Text))
                    {
                        DN dn4 = new DN();
                        dn4.DNNO = txtDNNO.Text;
                        dn4.DNLine = Convert.ToInt32(txtDNLine4.Text);
                        dn4.Qty = Convert.ToDecimal(txtDNActQty4.Text);
                        dnList.Add(dn4);
                    }
                    if (!string.IsNullOrEmpty(txtDNLine5.Text))
                    {
                        DN dn5 = new DN();
                        dn5.DNNO = txtDNNO.Text;
                        dn5.DNLine = Convert.ToInt32(txtDNLine5.Text);
                        dn5.Qty = Convert.ToDecimal(txtDNActQty5.Text);
                        dnList.Add(dn5);
                    }

                    msg = dnToSAP.DNPGIToSAP(dnList, false);
                }

                ShowMessage("====================" + btnSendDN.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + msg.Result);
                ShowMessage("执行结果信息：" + msg.Message + "\r\n");
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void btnClearDN_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox11.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }

        private void ShowMessage(string msg)
        {
            rMsg.AppendText(msg + "\r\n");
            rMsg.Focus();
        }

        private void btnSendUB_Click(object sender, EventArgs e)
        {
            try
            {
                List<UB> list = new List<UB>();
                UB ub = new UB();
                ub.UBNO = txtUBNO1.Text;
                ub.UBLine = Convert.ToInt32(txtUBLine1.Text);
                ub.FacCode = txtUBFacCode1.Text;
                ub.StorageCode = txtUBStorageCode1.Text;
                ub.MCode = txtUBMCode1.Text;
                ub.Unit = txtUBUnit1.Text;
                ub.Qty = Convert.ToDecimal(txtUBQty1.Text);
                ub.DocumentDate = Convert.ToInt32(txtUBDocumentDate1.Text);
                ub.MesTransNO = txtUBMesTransNum1.Text;
                ub.ContactUser = txtUBContactUser1.Text;
                ub.InOutFlag = cbUBInOutFlag1.SelectedIndex == 0 ? "351" : "101";
                list.Add(ub);

                if (!string.IsNullOrEmpty(txtUBNO2.Text))
                {
                    UB ub2 = new UB();
                    ub2.UBNO = txtUBNO2.Text;
                    ub2.UBLine = Convert.ToInt32(txtUBLine2.Text);
                    ub2.FacCode = txtUBFacCode2.Text;
                    ub2.StorageCode = txtUBStorageCode2.Text;
                    ub2.MCode = txtUBMCode2.Text;
                    ub2.Unit = txtUBUnit2.Text;
                    ub2.Qty = Convert.ToDecimal(txtUBQty2.Text);
                    ub2.DocumentDate = Convert.ToInt32(txtUBDocumentDate2.Text);
                    ub2.MesTransNO = txtUBMesTransNum2.Text;
                    ub2.ContactUser = txtUBContactUser2.Text;
                    ub2.InOutFlag = cbUBInOutFlag2.SelectedIndex == 0 ? "351" : "101";
                    list.Add(ub2);
                }

                UBToSAP ubToSAP = new UBToSAP();
                SAPRfcReturn msg = ubToSAP.PostUBToSAP(list);
                ShowMessage("====================" + btnSendUB.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + msg.Result);
                ShowMessage("SAP物料凭证编码：" + msg.MaterialDocument);
                ShowMessage("执行结果信息：" + msg.Message + "\r\n");
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void btnClearUB_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox3.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            foreach (var control in groupBox4.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }

        private void btnGetWWPOComponent_Click(object sender, EventArgs e)
        {
            try
            {
                WWPO2SAP wwpo2SAP = new WWPO2SAP();
                List<WWPOComponentPara> paraList = new List<WWPOComponentPara>();
                if (!string.IsNullOrEmpty(txtWWPONO1.Text))
                {
                    WWPOComponentPara para = new WWPOComponentPara();
                    para.PONO = txtWWPONO1.Text;
                    if (!string.IsNullOrEmpty(txtWWPOLine1.Text))
                    {
                        para.POLine = Convert.ToInt32(txtWWPOLine1.Text);
                    }
                    paraList.Add(para);
                }
                if (!string.IsNullOrEmpty(txtWWPONO2.Text))
                {
                    WWPOComponentPara para = new WWPOComponentPara();
                    para.PONO = txtWWPONO2.Text;
                    if (!string.IsNullOrEmpty(txtWWPOLine2.Text))
                    {
                        para.POLine = Convert.ToInt32(txtWWPOLine2.Text);
                    }
                    paraList.Add(para);
                }
                WWPOComponentResult rs = wwpo2SAP.GetWWPOList(paraList);
                dgvWWPOList.DataSource = rs.WWPOComponentList;
                ShowMessage("====================" + btnGetWWPOComponent.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + rs.RfcResult.Result);
                //ShowMessage("SAP物料凭证编码：" + rs.RfcResult.MaterialDocument);
                ShowMessage("执行结果信息：" + rs.RfcResult.Message + "\r\n");
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void btnSendWWPO_Click(object sender, EventArgs e)
        {
            try
            {
                List<WWPO> list = new List<WWPO>();
                WWPO wwpo = new WWPO();
                if (cbWWPOInOutFlag1.SelectedIndex == 0)
                {
                    wwpo.PONO = txtWWPONO1.Text;
                    wwpo.POLine = Convert.ToInt32(txtWWPOLine1.Text);
                    wwpo.VendorCode = txtWWPOVendorCode1.Text;
                }
                wwpo.FacCode = txtWWPOFacCode1.Text;
                wwpo.StorageCode = txtWWPOStorageCode1.Text;
                wwpo.MCode = txtWWPOMCode1.Text;
                wwpo.Unit = txtWWPOUnit1.Text;
                wwpo.Qty = Convert.ToDecimal(txtWWPOQty1.Text);
                wwpo.MesTransDate = Convert.ToInt32(txtWWPOMesTransDate1.Text);
                wwpo.MesTransNO = txtWWPOMesTransNO1.Text;
                wwpo.Remark = txtWWPORemark1.Text;
                wwpo.InOutFlag = cbWWPOInOutFlag1.SelectedIndex == 0 ? "541" : "542";
                list.Add(wwpo);

                if (!string.IsNullOrEmpty(txtWWPOMCode2.Text))
                {
                    WWPO wwpo2 = new WWPO();
                    if (cbWWPOInOutFlag2.SelectedIndex == 0)
                    {
                        wwpo2.PONO = txtWWPONO2.Text;
                        wwpo2.POLine = Convert.ToInt32(txtWWPOLine2.Text);
                        wwpo2.VendorCode = txtWWPOVendorCode2.Text;
                    }
                    wwpo2.FacCode = txtWWPOFacCode2.Text;
                    wwpo2.StorageCode = txtWWPOStorageCode2.Text;
                    wwpo2.MCode = txtWWPOMCode2.Text;
                    wwpo2.Unit = txtWWPOUnit2.Text;
                    wwpo2.Qty = Convert.ToDecimal(txtWWPOQty2.Text);
                    wwpo2.MesTransDate = Convert.ToInt32(txtWWPOMesTransDate2.Text);
                    wwpo2.MesTransNO = txtWWPOMesTransNO2.Text;
                    wwpo2.Remark = txtWWPORemark2.Text;
                    wwpo2.InOutFlag = cbWWPOInOutFlag2.SelectedIndex == 0 ? "541" : "542";
                    list.Add(wwpo2);
                }

                WWPO2SAP wwpo2SAP = new WWPO2SAP();
                SAPRfcReturn msg = wwpo2SAP.PostWWPOToSAP(list);
                ShowMessage("====================" + btnSendWWPO.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + msg.Result);
                ShowMessage("SAP物料凭证编码：" + msg.MaterialDocument);
                ShowMessage("执行结果信息：" + msg.Message + "\r\n");
                //ShowMessage("========================================================");
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void btnClearWWPO_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox5.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            foreach (var control in groupBox6.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }

        private void btnSendScrap_Click(object sender, EventArgs e)
        {
            try
            {
                List<StockScrap> list = new List<StockScrap>();
                StockScrap item = new StockScrap();
                item.MESScrapNO = txtScrapNO1.Text;
                item.LineNO = Convert.ToInt32(txtScrapLineNO1.Text);
                item.FacCode = txtScrapFacCode1.Text;
                item.StorageCode = txtScrapStorageCode1.Text;
                item.MCode = txtScrapMCode1.Text;
                item.Unit = txtScrapUnit1.Text;
                item.Qty = Convert.ToDecimal(txtScrapQty1.Text);
                item.CC = txtScrapCC1.Text;
                item.DocumentDate = Convert.ToInt32(txtScrapDocumentDate1.Text);
                item.Remark = txtScrapRemark1.Text;
                item.Operator = txtScrapOperator1.Text;
                item.ScrapCode = "551";
                list.Add(item);

                if (!string.IsNullOrEmpty(txtScrapMCode2.Text))
                {
                    StockScrap item2 = new StockScrap();
                    item2.MESScrapNO = txtScrapNO2.Text;
                    item2.LineNO = Convert.ToInt32(txtScrapLineNO2.Text);
                    item2.FacCode = txtScrapFacCode2.Text;
                    item2.StorageCode = txtScrapStorageCode2.Text;
                    item2.MCode = txtScrapMCode2.Text;
                    item2.Unit = txtScrapUnit2.Text;
                    item2.Qty = Convert.ToDecimal(txtScrapQty2.Text);
                    item2.CC = txtScrapCC2.Text;
                    item2.DocumentDate = Convert.ToInt32(txtScrapDocumentDate2.Text);
                    item2.Remark = txtScrapRemark2.Text;
                    item2.Operator = txtScrapOperator2.Text;
                    item2.ScrapCode = "551";
                    list.Add(item2);
                }

                StockScrapToSAP service = new StockScrapToSAP();
                SAPRfcReturn msg = service.PostStockScrapToSAP(list);
                ShowMessage("====================" + btnSendScrap.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + msg.Result);
                ShowMessage("SAP物料凭证编码：" + msg.MaterialDocument);
                ShowMessage("执行结果信息：" + msg.Message + "\r\n");
                //ShowMessage("========================================================");
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void btnClearStockScrap_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox7.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            foreach (var control in groupBox8.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }

        private void btnSendRS_Click(object sender, EventArgs e)
        {
            try
            {
                List<RS> list = new List<RS>();
                RS rs = new RS();
                rs.RSNO = txtRSNO1.Text;
                rs.RSLine = Convert.ToInt32(txtRSLine1.Text);
                rs.FacCode = txtRSFacCode1.Text;
                rs.StorageCode = txtRSStorageCode1.Text;
                rs.MCode = txtRSMCode1.Text;
                rs.Unit = txtRSUnit1.Text;
                rs.Qty = Convert.ToDecimal(txtRSQty1.Text);
                rs.MesTransNO = txtRSMesTransNO1.Text;
                rs.DocumentDate = Convert.ToInt32(txtRSDocumentDate1.Text);
                rs.InOutFlag = cbUBInOutFlag1.SelectedIndex == 0 ? "201" : (cbUBInOutFlag1.SelectedIndex == 1 ? "202" : "241");
                list.Add(rs);

                if (!string.IsNullOrEmpty(txtRSNO2.Text))
                {
                    RS rs2 = new RS();
                    rs2.RSNO = txtRSNO2.Text;
                    rs2.RSLine = Convert.ToInt32(txtRSLine2.Text);
                    rs2.FacCode = txtRSFacCode2.Text;
                    rs2.StorageCode = txtRSStorageCode2.Text;
                    rs2.MCode = txtRSMCode2.Text;
                    rs2.Unit = txtRSUnit2.Text;
                    rs2.Qty = Convert.ToDecimal(txtRSQty2.Text);
                    rs2.MesTransNO = txtRSMesTransNO2.Text;
                    rs2.DocumentDate = Convert.ToInt32(txtRSDocumentDate2.Text);
                    rs2.InOutFlag = cbUBInOutFlag2.SelectedIndex == 0 ? "201" : (cbUBInOutFlag2.SelectedIndex == 1 ? "202" : "241");
                    list.Add(rs2);
                }

                RSToSAP rsToSAP = new RSToSAP();
                SAPRfcReturn msg = rsToSAP.PostRSToSAP(list);
                ShowMessage("====================" + btnSendRS.Text + " " + DateTime.Now + "====================");
                ShowMessage("执行结果标识(S表示成功，E表示失败)：" + msg.Result);
                ShowMessage("SAP物料凭证编码：" + msg.MaterialDocument);
                ShowMessage("执行结果信息：" + msg.Message + "\r\n");
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void btnClearRS_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox9.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            foreach (var control in groupBox10.Controls)
            {

                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }


    }
}
