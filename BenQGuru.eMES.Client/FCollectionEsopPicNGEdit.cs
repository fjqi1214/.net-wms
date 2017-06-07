using System;
using System.Collections.Generic;
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
using BenQGuru.eMES.Domain.SopPicture;
using System.IO;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain;
using System.Net;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectionEsopPicNGEdit : BaseForm
    {
        #region 变量
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private DataCollectFacade m_DataCollectFacade;
        private ItemFacade m_ItemFacade;
        private MOFacade m_MOFacade;
        private EsopPicsFacade m_EsopPicsFacade;
        protected SystemSettingFacade _facade = null;
        private EsopPicsNGFacade m_EsopPicsNGFacade = null;
        private TSFacade _tsFacade = null;

        private System.Windows.Forms.ColorDialog colorDialog;
        IList<Image> imageListCache = null;
        private Color newColor = Color.Red;
        private float lineWidth = 2;
        Point PointBegin, PointEnd;
        bool isMouseDown = false;

        #endregion

        #region 自定义事件
        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FCollectionEsopPicNGEdit()
        {
            InitializeComponent();
            this.m_MOFacade = new MOFacade(this.DataProvider);
            this.m_ItemFacade = new ItemFacade(this.DataProvider);
            this.m_EsopPicsFacade = new EsopPicsFacade(this.DataProvider);
            this.m_DataCollectFacade = new DataCollectFacade(this.DataProvider);
            this._facade = new SystemSettingFacade(this.DataProvider);
            this.m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider);
            this._tsFacade = new TSFacade(this.DataProvider);
            imageListCache = new List<Image>();
            ((Control)this).MouseWheel += new MouseEventHandler(FormDemo_MouseWheel);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnColor.BackColor = Color.Red;

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
                        DoQuery(sim.ItemCode, sim.OPCode);
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

        private void DoQuery(string ItemCode, string OpCode)
        {
            try
            {
                this.imageListCache.Clear();
                this.listPics.Items.Clear();
                if (m_EsopPicsFacade == null) { m_EsopPicsFacade = new EsopPicsFacade(this.DataProvider); }
                object[] EsopPics = this.m_EsopPicsFacade.QueryEsopPicsByTS(ItemCode, OpCode);
                int imgnum = 0;
                if (EsopPics != null && EsopPics.Length > 0)
                {
                    this.imageListEsopPics.Images.Clear();
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
                            Image img = Image.FromStream(fs);
                            this.imageListEsopPics.Images.Add(img);
                            ListViewItem lvi = new ListViewItem();
                            //lvi.Text = pic.Picfullname;         //图片名称
                            lvi.ImageIndex = imgnum;   //这里是Listview每项显示的图片
                            lvi.Tag = pic;  //这里绑定不显示的数据
                            this.listPics.Items.Add(lvi);
                            fs.Flush();
                            fs.Close();
                            imgnum++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (this.imageListEsopPics.Images.Count > 0)
                    {
                        this.imageListEsopPics.ColorDepth = ColorDepth.Depth32Bit;
                        Size size = new Size();
                        size.Width = 100;
                        size.Height = 82;
                        this.imageListEsopPics.ImageSize = size;
                        this.listPics.LargeImageList = this.imageListEsopPics;
                        this.listPics.MultiSelect = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new Messages().Add(new UserControl.Message(ex));
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
                }
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_TS_Not_Exist"));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private void listEsopPicsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listPics.SelectedItems.Count > 0)
                {
                    this.imageListCache.Clear();
                    this.pbEsopIMG.Image = null;
                    this.txtNGPicMemo.Text = string.Empty;
                    this.listPicsNG.SelectedItems.Clear();
                 
                    int num = listPics.Items.IndexOf(listPics.SelectedItems[0]);
                    Esoppics selectedPic = (Esoppics)this.listPics.SelectedItems[0].Tag;

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

                    try
                    {
                        WebClient wc = new WebClient();
                        wc.Credentials = CredentialCache.DefaultCredentials;                   
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
                return;
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
                    this.listPics.SelectedItems.Clear();


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

        #endregion 自定义事件

        #region 页面按钮事件

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            imageListCache.Clear();
            listEsopPicsView_SelectedIndexChanged(sender, e);
            listPicsNG_SelectedIndexChanged(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pbEsopIMG.Image == null) { return; }

            Messages msg = new Messages();
            if (_tsFacade == null) { _tsFacade = new TSFacade(this.DataProvider); }
            if (string.IsNullOrEmpty(this.txtRcard.Text)) { return; }

            object obj = _tsFacade.QueryLastTSByRunningCard(this.txtRcard.Text);
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
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    EsopPicsNGFacade m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider);

                    if (this.listPicsNG.SelectedItems.Count == 0)
                    {
                        Esoppicsng picNG = m_EsopPicsNGFacade.CreateNewEsoppicsng();
                        picNG.Serial = ((Esoppicsng)m_EsopPicsNGFacade.GetEsoppicNGNextSerial()[0]).Serial;
                        picNG.Rcard = FormatHelper.CleanString(this.txtRcard.Text, 40);
                        picNG.Ngpicmemo = FormatHelper.CleanString(this.txtNGPicMemo.Text, 500);
                        picNG.Tsid = ts.TSId;
                        picNG.Picsname = "NG" + picNG.Rcard + picNG.Tsid + dbDateTime.DateTime.ToString("yyyyMMddHHmmss");
                        picNG.Muser = ApplicationService.Current().LoginInfo.UserCode;
                        picNG.Mdate = dbDateTime.DBDate;
                        picNG.Mtime = dbDateTime.DBTime;
                        AddDomainObject(picNG);
                    }
                    else
                    {
                        Esoppicsng picNG = (Esoppicsng)this.listPicsNG.SelectedItems[0].Tag;
                        picNG.Muser = ApplicationService.Current().LoginInfo.UserCode;
                        picNG.Ngpicmemo = FormatHelper.CleanString(this.txtNGPicMemo.Text, 500);
                        picNG.Mdate = dbDateTime.DBDate;
                        picNG.Mtime = dbDateTime.DBTime;
                        UpdateDomainObject(picNG);
                    }
                    Init(this.txtRcard.Text); 
                    this.pbEsopIMG.Image = null;
                    this.txtNGPicMemo.Text = string.Empty;
                    imageListCache.Clear();
                }
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_TS_Not_Exist"));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private void AddDomainObject(Esoppicsng picNG)
        {
            if (m_EsopPicsNGFacade == null)
            {
                m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider);
            }
            Messages msg = new Messages();
            this.DataProvider.BeginTransaction();
            try
            {
                if (UpLoadFile(picNG, "ADD"))
                {
                    m_EsopPicsNGFacade.AddEsoppicsng(picNG);
                    this.DataProvider.CommitTransaction();
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
                    ApplicationRun.GetInfoForm().Add(msg);
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_Save_Lot_Error"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.DataProvider.RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_Save_Lot_Error"));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private void UpdateDomainObject(Esoppicsng picNG)
        {
            if (m_EsopPicsNGFacade == null) { m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider); }
            Messages msg = new Messages();
            this.DataProvider.BeginTransaction();
            try
            {
                if (UpLoadFile(picNG, "UPDATE"))
                {
                    m_EsopPicsNGFacade.UpdateEsoppicsng(picNG);
                    this.DataProvider.CommitTransaction();
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
                    ApplicationRun.GetInfoForm().Add(msg);
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_Save_Lot_Error"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.DataProvider.RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_Save_Lot_Error"));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listPicsNG.SelectedItems.Count > 0 && this.pbEsopIMG.Image != null)
            {
                try
                {
                    Esoppicsng selectedPic = (Esoppicsng)this.listPicsNG.SelectedItems[0].Tag;
                    if (m_EsopPicsNGFacade == null) { m_EsopPicsNGFacade = new EsopPicsNGFacade(this.DataProvider); }
                    this.DataProvider.BeginTransaction();
                    m_EsopPicsNGFacade.DeleteEsoppicsng(selectedPic);
                    string filaPath = "";

                    if (_facade == null)
                    {
                        _facade = new SystemSettingFacade(this.DataProvider);
                    }
                    object parameter = _facade.GetParameter("PUBLISHESOPPICSDIRPATH", "PUBLISHEOPSPICSDIRPATHGROUP");

                    if (parameter != null)
                    {
                        //服务器目录路径
                        filaPath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias + "TS/";
                    }

                    string fileName = filaPath + selectedPic.Picsname + ".jpg";
                    /// 创建WebClient实例
                    WebClient myWebClient = new WebClient();
                    myWebClient.Credentials = CredentialCache.DefaultCredentials;

                    byte[] postArray = new byte[0];
                    Stream postStream = myWebClient.OpenWrite(fileName, "PUT");
                    try
                    {
                        if (postStream.CanWrite)
                        {
                            postStream.Write(postArray, 0, 0);
                            postStream.Close();

                        }
                        else
                        {
                            postStream.Close();

                        }

                    }
                    catch (Exception err)
                    {
                        postStream.Close();
                        throw err;
                    }
                    finally
                    {
                        postStream.Close();
                    }

                    this.DataProvider.CommitTransaction();
                    imageListCache.Clear();
                    this.pbEsopIMG.Image = null;
                    QueryPicsNG();
                    Messages msg = new Messages();
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_Delete_Success"));
                    ApplicationRun.GetInfoForm().Add(msg);

                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                }
            }
            else
            {
                Messages msg = new Messages();
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_NoDataSelected"));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private bool UpLoadFile(Esoppicsng picNG, string action)
        {
            try
            {
                if (_facade == null)
                {
                    _facade = new SystemSettingFacade(this.DataProvider);
                }
                object parameter = _facade.GetParameter("PUBLISHESOPPICSDIRPATH", "PUBLISHEOPSPICSDIRPATHGROUP");
                if (parameter != null)
                {
                    //服务器目录路径
                    string filePath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias + "TS/";
                    string fileName = filePath + picNG.Picsname + ".jpg";
                    if (picNG != null)
                    {
                        /**/
                        /// 创建WebClient实例
                        WebClient myWebClient = new WebClient();
                        myWebClient.Credentials = CredentialCache.DefaultCredentials;

                        string localPath = "c:\\esop\\";
                        if (!Directory.Exists(localPath))
                        {
                            Directory.CreateDirectory(localPath);
                        }

                        Image image = this.pbEsopIMG.Image;
                        image.Save("c:\\esop\\1.jpg", pbEsopIMG.Image.RawFormat);

                        FileStream fs = new FileStream("C:\\esop\\1.jpg", FileMode.Open, FileAccess.ReadWrite);
                        BinaryReader br = new BinaryReader(fs);
                        byte[] postArray = br.ReadBytes((int)fs.Length);
                        Stream postStream = myWebClient.OpenWrite(fileName, "PUT");
                        try
                        {
                            if (postStream.CanWrite)
                            {
                                postStream.Write(postArray, 0, postArray.Length);
                                postStream.Close();
                                fs.Dispose();
                            }
                            else
                            {
                                postStream.Close();
                                fs.Dispose();
                            }

                        }
                        catch (Exception err)
                        {
                            postStream.Close();
                            fs.Close();
                            throw err;
                        }
                        finally
                        {
                            postStream.Close();
                            fs.Dispose();
                        }
                    }
                    return true;
                }
                else
                {
                    Messages msg = new Messages();
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_PicDirPath_NotExist"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_UpLoadFile_Exception"));
                ApplicationRun.GetInfoForm().Add(msg);
                return false;
            }
        }

        #endregion 页面按钮事件

        #region PictureBox事件

        private void pictureBoxDemo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //using (Graphics g = Graphics.FromImage(this.pictureBoxDemo.Image))
                //{
                //    imageList.Add(pictureBoxDemo.Image.Clone() as Image);

                //    Pen pen = new Pen(Color.Red, 2);
                //    g.DrawEllipse(pen, e.Location.X - 25, e.Location.Y - 25, 50, 50);//在画板上画圆
                //    g.Save();
                //    pictureBoxDemo.Refresh();

                //}
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (imageListCache.Count == 0)
                    return;

                pbEsopIMG.Image = imageListCache[imageListCache.Count - 1];
                imageListCache.RemoveAt(imageListCache.Count - 1);
                pbEsopIMG.Refresh();
            }

            //string fileName = string.Empty;
            //if (System.IO.File.Exists(this.textBoxFileName.Text))
            //{
            //    fileName = Directory.GetCurrentDirectory() + "\\Test\\" + DateTime.Now.Date.ToShortDateString()
            //        + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
            //        DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".jpg";
            //}
            //pictureBoxDemo.Image.Save(fileName);
            //this.pictureBoxDemo.Image = Image.FromFile(fileName, true);
        }

        private void pictureBoxDemo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (pbEsopIMG.Image == null)
                    return;

                PointBegin = e.Location;
                isMouseDown = true;
            }
        }

        private void pictureBoxDemo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!isMouseDown)
                    return;
                PointEnd = e.Location;
                Image img = this.pbEsopIMG.Image;
                imageListCache.Add(img.Clone() as Image);

                try
                {
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        Pen pen = new Pen(newColor, lineWidth);
                        g.DrawEllipse(pen, PointBegin.X, PointBegin.Y,
                            PointEnd.X - PointBegin.X, PointEnd.Y - PointBegin.Y);//在画板上画圆
                        g.Save();
                        g.Dispose();
                        pbEsopIMG.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    Graphics graphics;
                    //对于indexed pixel format，抛出异常的特殊处理 20120323 Ian Add
                    Bitmap newBmp = new Bitmap(img.Width, img.Height);
                    graphics = System.Drawing.Graphics.FromImage(newBmp);
                    graphics.DrawImage(img,
                                       new Rectangle(0, 0, newBmp.Width, newBmp.Height),
                                       new Rectangle(0, 0, img.Width, img.Height),
                                       GraphicsUnit.Pixel);
              
                    this.pbEsopIMG.Image = newBmp; //捕获到异常，重绘原图

                    Pen pen = new Pen(newColor, lineWidth);
                    graphics.DrawEllipse(pen, PointBegin.X, PointBegin.Y,
                        PointEnd.X - PointBegin.X, PointEnd.Y - PointBegin.Y);//在画板上画圆
                    pbEsopIMG.Refresh();
                    graphics.Save();
                    graphics.Dispose();

                }
                isMouseDown = false;
            }
        }

        private void pictureBoxDemo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!isMouseDown)
                    return;
                PointEnd = e.Location;
                pbEsopIMG.Invalidate();
            }
        }

        private void pictureBoxDemo_Paint(object sender, PaintEventArgs e)
        {
            if (!isMouseDown)
                return;

            Graphics displayGraphics = e.Graphics;
            displayGraphics.DrawEllipse(new Pen(newColor, lineWidth), new Rectangle(PointBegin.X, PointBegin.Y,
                        PointEnd.X - PointBegin.X, PointEnd.Y - PointBegin.Y));

        }

        private void FormDemo_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (lineWidth < 101)
                    lineWidth++;
            }
            else
            {
                if (lineWidth > -1)
                    lineWidth--;
            }
        }

        #endregion  PictureBox事件

        #region 颜色
        /// <summary>
        /// 颜色。
        /// </summary>		
        private void Black_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.Black.BackColor;
            newColor = this.Black.BackColor;
        }

        private void White_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.White.BackColor;
            newColor = this.White.BackColor;
        }

        private void Red_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.Red.BackColor;
            newColor = this.Red.BackColor;
        }

        private void blue_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.blue.BackColor;
            newColor = this.blue.BackColor;
        }

        private void Yellow_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.Yellow.BackColor;
            newColor = this.Yellow.BackColor;
        }

        private void LawnGreen_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.LawnGreen.BackColor;
            newColor = this.LawnGreen.BackColor;
        }

        private void Cyan_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.Cyan.BackColor;
            newColor = this.Cyan.BackColor;
        }

        private void Magenta_Click(object sender, System.EventArgs e)
        {
            this.btnColor.BackColor = this.Magenta.BackColor;
            newColor = this.Magenta.BackColor;
        }

        private void MoreColor_Click(object sender, System.EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                newColor = colorDialog.Color;
                this.btnColor.BackColor = newColor;
            }
        }
        #endregion     
        
    }
}
