using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BenQGuru.eMES.BaseSetting;
using System.IO;
using BenQGuru.eMES.Domain.SopPicture;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.BaseSetting
{
	public partial class ShowImg  : BasePage
	{
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected SystemSettingFacade _facade = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string fullname = Request.QueryString["PICFULLNAME"];

            if (!string.IsNullOrEmpty(fullname))
            {
                try
                {
                    if (_facade == null)
                    {
                        _facade = new SystemSettingFacade(this.DataProvider);
                    }
                    //object parameter = _facade.GetParameter("PICUPLOADPATH", "ESOPPICDIRPATHGROUP");
                    //if (parameter != null)
                    //{
                        //服务器目录路径
                    string filePath = System.AppDomain.CurrentDomain.BaseDirectory
+ "ESFileUpload"; //((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        DirectoryInfo dir = new DirectoryInfo(filePath);
                        foreach (FileInfo file in dir.GetFiles())
                        {
                            if (file.Name == (fullname))
                            {
                                string currentPath = filePath + "\\" + fullname;
                                using (FileStream fs = new FileStream(currentPath, FileMode.Open))
                                {
                                    byte[] bytes = new byte[fs.Length];
                                    fs.Seek(0, SeekOrigin.Begin);
                                    fs.Read(bytes, 0, (int)fs.Length);
                                    Response.ContentType = "image/jpg";
                                    using (System.Drawing.Image theImage = System.Drawing.Image.FromStream(fs))
                                        theImage.Save(Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    Response.BinaryWrite(bytes);                             
                                    fs.Flush();
                                    fs.Close();
                                }
                            }
                        }
                    //}
                }
                catch (Exception ex)
                {
                    WebInfoPublish.PublishInfo(this, "$Error_UpLoadFile_Exception", this.languageComponent1);
                    return;
                }
            }
        }
		
	}
}
