using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;

using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Material;
namespace BenQGuru.eMES.Web.WarehouseWeb
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FMaterialQueryCheck : IHttpHandler, IRequiresSessionState
    {
        private MaterialFacade _facade = null;//
        private IDomainDataProvider _domainDataProvider = null;
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

        public void ProcessRequest(HttpContext context)
        {
            string itemCodeValue = context.Request.QueryString["itemCodeValue"].Trim().ToUpper();
            string snPrefix = context.Request.QueryString["snPrefix"].Trim().ToUpper();
            string StartSnCodeValue = context.Request.QueryString["StartSnCodeValue"].Trim().ToUpper();
            string actionType = context.Request.QueryString["Action"].Trim().ToUpper();
            string EndSnValue = context.Request.QueryString["EndSnValue"].Trim().ToUpper();
            string SnSeqValue = context.Request.QueryString["SnSeqValue"].Trim().ToUpper();
            string CheckedValue = context.Request.QueryString["CheckedValue"].Trim().ToUpper();           

            try
            {
                _facade = new MaterialFacade(this.DataProvider);
                string returnValue = "false";
                string _scale = string.Empty;

                NumberScale scale = NumberScale.Scale34;
                _scale = "34";

                if (CheckedValue == "10")
                {
                    scale = NumberScale.Scale10;
                    _scale = "10";

                }
                else if (CheckedValue == "16")
                {
                    scale = NumberScale.Scale16;
                    _scale = "16";

                }
                else if (CheckedValue == "34")
                {
                    scale = NumberScale.Scale34;
                    _scale = "34";
                }
                else
                {
                    scale = NumberScale.Scale34;
                    _scale = "34";
                }


                int length = StartSnCodeValue.Trim().Length;

                if (StartSnCodeValue.Trim() != "" && EndSnValue.Trim() != "")
                {

                    long startSN = 0;
                    try
                    {
                        startSN = long.Parse(NumberScaleHelper.ChangeNumber(StartSnCodeValue.Trim(), scale, NumberScale.Scale10));
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        //txtMORCardStartEdit.Focus();
                        returnValue = "false";
                    }

                    long endSN = 0;
                    try
                    {
                        endSN = long.Parse(NumberScaleHelper.ChangeNumber(EndSnValue.Trim(), scale, NumberScale.Scale10));
                    }
                    catch (Exception ex)
                    {
                        returnValue = "false";
                    }                 

                    if ((startSN.ToString().Length + snPrefix.Trim().Length) > 40)
                    {
                        returnValue = "false";
                    }

                    if (startSN > endSN)
                    {

                        returnValue = "false";
                    }

                    bool needUpdateDetail = true;

                    if (actionType == "UPDATE")
                    {
                        MKeyPart oldMKeyPart = (MKeyPart)_facade.GetMKeyPart(Convert.ToDecimal(SnSeqValue), itemCodeValue);

                        if (oldMKeyPart != null
                           && oldMKeyPart.RCardPrefix.Trim().ToUpper() == snPrefix.Trim().ToUpper()
                           && oldMKeyPart.RunningCardStart.Trim().ToUpper() == StartSnCodeValue.Trim().ToUpper()
                           && oldMKeyPart.RunningCardEnd.Trim().ToUpper() == EndSnValue.Trim().ToUpper()
                           && oldMKeyPart.SNScale.Trim().ToUpper() == _scale)
                        {
                            needUpdateDetail = false;
                        }
                    }

                    //if (actionType == "ADD")
                    //{
                        //检查需要插入的detail数据量是否太多

                    if (needUpdateDetail)
                    {
                        if (endSN - startSN > 4999)
                        {
                            returnValue = "true";
                        }
                        else
                        {
                            returnValue = "false";
                        }
                    }
                    else
                    {
                        returnValue = "false";
 
                    }                    


                    //}

                    //if (actionType == "UPDATE")
                    //{
                    //    long count = endSN - startSN + 1;  //当前界面上算出来的数量

                    //    try
                    //    {

                    //        long checkCount = _facade.CheckMKeyPartDetail(SnSeqValue, itemCodeValue, StartSnCodeValue, EndSnValue, snPrefix.Trim().ToUpper());


                    //        if (checkCount == 0)
                    //        {
                    //            if (endSN - startSN > 4999)
                    //            {
                    //                returnValue = "true";
                    //            }
                    //            else
                    //            {
                    //                returnValue = "false";
                    //            }


                    //        }
                    //        else
                    //        {

                    //            if (count - checkCount < 0 || count - checkCount == 0) //会删掉数据库的序列号，所以速度比较快。
                    //            {
                    //                returnValue = "false";

                    //            }
                    //            else
                    //            {
                    //                if (count - checkCount > 4999)  //为4999 //会insert into 序列号到数据库，所以要判断inset into的数量是否大于4999
                    //                {
                    //                    returnValue = "true";

                    //                }
                    //                else
                    //                {
                    //                    returnValue = "false";

                    //                }
                    //            }
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        returnValue = "false";

                    //    }


                    //}
                }
                else
                {
                    returnValue = "false";
                }

                context.Response.Write(returnValue);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
