using System;
using System.Drawing;
using System.Windows.Forms;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using UserControl;
using BenQGuru.eMES.BaseSetting;
using System.IO;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain;
using System.Net;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectionEsopPicNGView : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private DataCollectFacade m_DataCollectFacade;
        private ItemFacade m_ItemFacade;
        private MOFacade m_MOFacade;
        private EsopPicsFacade m_EsopPicsFacade;
        protected SystemSettingFacade _facade = null;
        private EsopPicsNGFacade m_EsopPicsNGFacade = null;
        private TSFacade _tsFacade = null;

        private string picNGSerial = "";

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FCollectionEsopPicNGView()
        {
            InitializeComponent();
            this.m_MOFacade = new MOFacade(this.DataProvider);
            this.m_ItemFacade = new ItemFacade(this.DataProvider);
            this.m_EsopPicsFacade = new EsopPicsFacade(this.DataProvider);
            this.m_DataCollectFacade = new DataCollectFacade(this.DataProvider);
            this._facade = new SystemSettingFacade(this.DataProvider);
            this.m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider);
            this._tsFacade = new TSFacade(this.DataProvider);
            //this.InitPageLanguage();
        }

        public void Init(string rcard)
        {
            Messages msg = new Messages();
            try
            {
                if (!string.IsNullOrEmpty(rcard))
                {
                    this.txtRcard.Text = rcard;

                    if (m_DataCollectFacade == null) { m_DataCollectFacade = new DataCollectFacade(this.DataProvider); }
                    object objSim = m_DataCollectFacade.GetSimulation(rcard);

                    if (objSim != null)
                    {
                        Simulation sim = (Simulation)objSim;
                        this.txtItemCode.Text = sim.ItemCode;
                        object objMo = m_MOFacade.GetMO(sim.MOCode.ToUpper());
                        int orgID = Convert.ToInt16(((MO)objMo).OrganizationID);
                        Item item = (Item)m_ItemFacade.GetItem(((MO)objMo).ItemCode, orgID);
                        this.txtItemDesc.Text = item.ItemDescription;
                        QueryPicsNG();
                    }
                    else
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));
                        ApplicationRun.GetInfoForm().Add(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
                return;
            }
        }

        private void QueryPicsNG()
        {
            Messages msg = new Messages();
            this.imageListPicsNG.Images.Clear();
            this.listPicsNG.Items.Clear();
            if (_tsFacade == null) { _tsFacade = new TSFacade(this.DataProvider); }
            if (string.IsNullOrEmpty(this.txtRcard.Text)) { return; }

            object obj = _tsFacade.QueryLastTSByRunningCard(this.txtRcard.Text);
            int imgnum = 0;
            if (obj != null)
            {
                Domain.TS.TS ts = (Domain.TS.TS)obj;
                if (ts.TSStatus == TSStatus.TSStatus_Complete)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_TS_Completed"));
                    ApplicationRun.GetInfoForm().Add(msg);
                }
                else
                {
                    EsopPicsNGFacade m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider);
                    object[] objPicsNG = m_EsopPicsNGFacade.QueryEsopPicsNG(this.txtRcard.Text, ts.TSId);
                    if (objPicsNG != null && objPicsNG.Length > 0)
                    {
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

                        foreach (Esoppicsng picNG in objPicsNG)
                        {                           
                            string fileName = filaPath + "TS/" + picNG.Picsname + ".jpg";

                            try
                            {
                                WebClient wc = new WebClient();
                                wc.Credentials = CredentialCache.DefaultCredentials;
                                // wc.DownloadData(fileName);
                                Stream fs = wc.OpenRead(fileName);
                                this.imageListPicsNG.Images.Add(Image.FromStream(fs));
                                ListViewItem lvi = new ListViewItem();
                                // lvi.Text = pic.Picfullname;         //图片名称
                                lvi.ImageIndex = imgnum;   //这里是Listview每项显示的图片
                                lvi.Tag = picNG;  //这里绑定不显示的数据
                                this.listPicsNG.Items.Add(lvi);
                                fs.Flush();
                                fs.Close();
                                imgnum++;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        if (this.imageListPicsNG.Images.Count > 0)
                        {
                            this.imageListPicsNG.ColorDepth = ColorDepth.Depth32Bit;
                            Size size = new Size();
                            size.Width = 100;
                            size.Height = 82;
                            this.imageListPicsNG.ImageSize = size;
                            this.listPicsNG.LargeImageList = this.imageListPicsNG;
                            this.listPicsNG.MultiSelect = false;
                        }
                    }
                    else
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$File_Not_Exist"));
                        ApplicationRun.GetInfoForm().Add(msg);
                    }
                }
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_TS_Not_Exist"));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private void listPicsNG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listPicsNG.SelectedItems.Count > 0)
                {
                    this.pbEsopIMG.Image = null;
                    this.txtNGPicMemo.Text = string.Empty;


                    int num = listPicsNG.Items.IndexOf(listPicsNG.SelectedItems[0]);
                    Esoppicsng selectedPic = (Esoppicsng)this.listPicsNG.SelectedItems[0].Tag;
                    this.txtNGPicMemo.Text = selectedPic.Ngpicmemo;
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

                    string fileName = filaPath + "TS/" + selectedPic.Picsname + ".jpg";

                    try
                    {
                        WebClient wc = new WebClient();
                        wc.Credentials = CredentialCache.DefaultCredentials;
                        // wc.DownloadData(fileName);
                        Stream fs = wc.OpenRead(fileName);
                        Image img = Image.FromStream(fs);
                        this.pbEsopIMG.Size = img.Size;
                        this.pbEsopIMG.Image = img;
                        fs.Flush();
                        fs.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_NoDataSelected"));
                ApplicationRun.GetInfoForm().Add(msg);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
