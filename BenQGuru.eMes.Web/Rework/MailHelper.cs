using System;

using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.MailUtility;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// MailHelper 的摘要说明。
	/// </summary>
	public class MailHelper
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;
		private string fromUserMail = "";
		public MailHelper( BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider, string fromuserMail )
		{
			this._domainDataProvider = domainDataProvider;
			fromUserMail = fromuserMail;
		}

		public void SendMailToNext( ReworkPass[] reworkPasses )
		{
			
			ReworkFacade _facade = new ReworkFacadeFactory(_domainDataProvider).Create();
			if( reworkPasses == null || reworkPasses.Length==0 )return;
			for( int i=0; i<reworkPasses.Length; i++ )
			{
				ReworkPass reworkPass = reworkPasses[i];

				object[] users = _facade.GetNextApprover( reworkPass );

				if( users==null || users.Length==0 )continue ;
				
				System.Web.Mail.MailMessage message = BuildMessage( reworkPass.ReworkCode,reworkPass.MaintainUser, reworkPass.MaintainDate,reworkPass.MaintainTime );
				MailFacade.SendMail(fromUserMail,users,message);
			}
		}

		public void SendMailToFirst( ReworkSheet[] reworkSheets )
		{
			
			ReworkFacade _facade = new ReworkFacadeFactory(_domainDataProvider).Create();
			if( reworkSheets == null || reworkSheets.Length==0 )return;
			for( int i=0; i<reworkSheets.Length; i++ )
			{
				ReworkSheet reworkSheet = reworkSheets[i];

				object[] users = _facade.GetFirstApprover( reworkSheet );

				if( users==null || users.Length==0 )continue ;

				System.Web.Mail.MailMessage message = BuildMessage( reworkSheet.ReworkCode, reworkSheet.MaintainUser, reworkSheet.MaintainDate, reworkSheet.MaintainTime);
                MailFacade.SendMail(fromUserMail, users, message);
			}
		}

		private System.Web.Mail.MailMessage BuildMessage( string reworkCode, string mUser, int mdate, int mtime )
		{
			BenQGuru.eMES.BaseSetting.UserFacade userFacade = new BenQGuru.eMES.BaseSetting.UserFacade(_domainDataProvider) ;
			object user = userFacade.GetUser( mUser ); 
			System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
			message.Subject = string.Format("返工需求单({0}),待签核:", reworkCode);
			message.Priority = System.Web.Mail.MailPriority.High ;
			message.Body = string.Format(@"
<html>
<table>
<tr>
<td>您好！
</td>
</tr>
<tr align=right><td>    新增返工需求单 ({0}), 待签核。</td>
</tr>
<tr align=right><td>                               {1}</td>
</tr>
<tr align=right><td>                               {2}</td>
</tr>
<tr align=right><td>                               {3}</td>
</tr>
</table>
</html>", reworkCode, mUser, ( user as BenQGuru.eMES.Domain.BaseSetting.User ).UserTelephone, FormatHelper.TODateTimeString(mdate, mtime, "-", ":" ) ) ;

			return message ;
		}
	}
}
