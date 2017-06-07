using System;
using System.Drawing;
using System.Windows.Forms;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using UserControl;
using BenQGuru.eMES.Domain.SopPicture;
using System.IO;
using System.Net;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectionPICView : BaseForm
    {
        #region 构造方法
        public FCollectionPICView()
        {
            InitializeComponent();
            this.checkBoxMo.Checked = true;
            this.m_MOFacade = new MOFacade(this.DataProvider);
            this.m_ItemFacade = new ItemFacade(this.DataProvider);
            this.m_EsopPicsFacade = new EsopPicsFacade(this.DataProvider);
            this.m_DataCollectFacade = new DataCollectFacade(this.DataProvider);
            this.m_BaseModelFacade = new BaseModelFacade(this.DataProvider);
            this._facade = new SystemSettingFacade(this.DataProvider);
        }
        #endregion 构造方法

        private void FCollectionPICView_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
        }

        #region  变量
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> ItemCodeSelectedEvent;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private ItemFacade m_ItemFacade;
        private MOFacade m_MOFacade;
        private DataCollectFacade m_DataCollectFacade;
        private EsopPicsFacade m_EsopPicsFacade;
        private BaseModelFacade m_BaseModelFacade;
        protected SystemSettingFacade _facade = null;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        private ItemFacade baseModelFacade
        {
            get
            {
                if (this.m_ItemFacade == null)
                {
                    this.m_ItemFacade = new ItemFacade(this.DataProvider);
                }
                return m_ItemFacade;
            }
        }

        #endregion

        #region 自定义事件

        private void DoQuery(string ItemCode, string OpCode)
        {
            Messages msg = new Messages();
            try
            {
                if (m_EsopPicsFacade == null) { m_EsopPicsFacade = new EsopPicsFacade(this.DataProvider); }
                object[] EsopPics = this.m_EsopPicsFacade.QueryEsopPics(ItemCode, OpCode);
                int imgnum = 0;
                if (EsopPics != null && EsopPics.Length > 0)
                {
                    this.imageList1.Images.Clear();
                    string filaPath = "";
                    if (_facade == null)
                    {
                        _facade = new SystemSettingFacade(this.DataProvider);
                    }
                    object parameter = _facade.GetParameter("PUBLISHESOPPICSDIRPATH", "PUBLISHEOPSPICSDIRPATHGROUP");

                    if (parameter != null)
                    {
                        //服务器目录路径
                        filaPath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                    }

                    foreach (Esoppics pic in EsopPics)
                    {                       
                        string fileName = filaPath + pic.Picfullname + ".jpg";

                        try
                        {
                            WebClient wc = new WebClient();
                            wc.Credentials = CredentialCache.DefaultCredentials;
                            // wc.DownloadData(fileName);
                            Stream fs = wc.OpenRead(fileName);
                            this.imageList1.Images.Add(Image.FromStream(fs));
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = pic.Picfullname;         //图片名称
                            lvi.ImageIndex = imgnum;   //这里是Listview每项显示的图片
                            lvi.Tag = pic;  //这里绑定不显示的数据
                            this.listEsopPicsView.Items.Add(lvi);
                            fs.Dispose();
                            fs.Flush();
                            fs.Close();
                            imgnum++;
                        }
                        catch (Exception ex)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_FileReadError"));
                            ApplicationRun.GetInfoForm().Add(msg);
                            return;
                        }
                    }
                    if (this.imageList1.Images.Count > 0)
                    {
                        this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
                        Size size = new Size();
                        size.Width = 100;
                        size.Height = 82;                       
                        this.imageList1.ImageSize = size;
                        this.listEsopPicsView.LargeImageList = this.imageList1;
                        this.listEsopPicsView.MultiSelect = false;
                    }
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Success, "$File_Not_Exist"));
                    ApplicationRun.GetInfoForm().Add(msg);
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_FileReadError"));
                ApplicationRun.GetInfoForm().Add(msg);
                clearAll();
                return;
            }

        }

        private void checkBoxRcard_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRcard.Checked)
            {
                this.txtMoCode.Text = string.Empty;
                this.txtMoCode.Enabled = false;
                this.checkBoxMo.Checked = false;
                this.txtRcard.Enabled = true;
                this.txtRcard.Focus();

            }
            clearAll();
        }

        private void checkBoxMo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMo.Checked)
            {
                this.txtRcard.Text = string.Empty;
                this.checkBoxRcard.Checked = false;
                this.txtMoCode.Enabled = true;
                this.txtRcard.Enabled = false;
                this.txtMoCode.Focus();
            }
            clearAll();
        }

        private void clearAll()
        {
            this.txtItemCode.Text = string.Empty;
            this.txtItemDesc.Text = string.Empty;

            this.txtPicTitle.Text = string.Empty;
            this.txtPicDesc.Text = string.Empty;
            this.pbEsopIMG.Image = null;
            this.imageList1.Images.Clear();
            this.listEsopPicsView.Clear();
        }

        private void txtMoCode_KeyDown(object sender, KeyEventArgs e)
        {
            Messages msg = new Messages();
            clearAll();
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(this.txtMoCode.Text))
                    {
                        object objMo = m_MOFacade.GetMO(txtMoCode.Text.ToUpper());

                        if (objMo != null)
                        {
                            MO mo = (MO)objMo;
                            this.txtItemCode.Text = mo.ItemCode;
                            int orgID = Convert.ToInt16(mo.OrganizationID);
                            Item item = (Item)m_ItemFacade.GetItem(mo.ItemCode, orgID);
                            this.txtItemDesc.Text = item.ItemDescription;
                            Operation2Resource opRes = (Operation2Resource)
                                m_BaseModelFacade.GetOperationByResource(
                                ApplicationService.Current().LoginInfo.Resource.ResourceCode);
                            DoQuery(mo.ItemCode, opRes.OPCode);
                        }
                        else
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_MOCode_NotCompare"));
                            ApplicationRun.GetInfoForm().Add(msg);
                            clearAll();
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.Add(new UserControl.Message(ex));
                    ApplicationRun.GetInfoForm().Add(msg);
                    txtMoCode.Focus();
                    clearAll();
                    return;
                }

                try
                {
                    if (!string.IsNullOrEmpty(this.txtRcard.Text))
                    {
                        object objSim = m_DataCollectFacade.GetSimulation(txtRcard.Text);

                        if (objSim != null)
                        {
                            Simulation sim = (Simulation)objSim;
                            this.txtItemCode.Text = sim.ItemCode;
                            object objMo = m_MOFacade.GetMO(sim.MOCode.ToUpper());
                            int orgID = Convert.ToInt16(((MO)objMo).OrganizationID);
                            Item item = (Item)m_ItemFacade.GetItem(((MO)objMo).ItemCode, orgID);
                            this.txtItemDesc.Text = item.ItemDescription;
                            DoQuery(sim.ItemCode, sim.OPCode);
                        }
                        else
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));
                            ApplicationRun.GetInfoForm().Add(msg);
                            clearAll();
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.Add(new UserControl.Message(ex));
                    ApplicationRun.GetInfoForm().Add(msg);
                    txtRcard.Focus();
                    clearAll();
                    return;
                }
            }
        }      

        private void listEsopPicsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Messages msg = new Messages();
            try
            {
                if (listEsopPicsView.SelectedItems.Count > 0)
                {
                    int num = listEsopPicsView.Items.IndexOf(listEsopPicsView.SelectedItems[0]);
                    Esoppics selectedPic = (Esoppics)this.listEsopPicsView.SelectedItems[0].Tag;
                    this.txtPicTitle.Text = selectedPic.Pictitle;
                    this.txtPicDesc.Text = selectedPic.Picmemo;

                    string filaPath = "";

                    if (_facade == null)
                    {
                        _facade = new SystemSettingFacade(this.DataProvider);
                    }
                    object parameter = _facade.GetParameter("PUBLISHESOPPICSDIRPATH", "PUBLISHEOPSPICSDIRPATHGROUP");

                    if (parameter != null)
                    {
                        //服务器目录路径
                        filaPath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                    }
                    string fileName = filaPath + selectedPic.Picfullname + ".jpg";
                    WebClient wc = new WebClient();
                    wc.Credentials = CredentialCache.DefaultCredentials;                   
                    Stream fs = wc.OpenRead(fileName);
                    Image img = Image.FromStream(fs);
                    this.pbEsopIMG.Size = img.Size;
                    this.pbEsopIMG.Image = img;
                    fs.Flush();
                    fs.Dispose();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_FileReadError"));
                ApplicationRun.GetInfoForm().Add(msg);
                clearAll();
                return;
            }
        }
        #endregion    

 
   
    }
}
