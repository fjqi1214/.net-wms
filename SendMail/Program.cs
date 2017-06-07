using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }


        private void SendEmail(string title, string bodyhtml, string toemail)
        {
            try
            {
                MailAddress from = new MailAddress("fjqi1214@sina.com");
                MailMessage mail = new MailMessage();
                //设置邮件的标题
                mail.Subject = title;
                mail.From = from;//设置邮件的发件人
                mail.To.Clear();
                mail.To.Add(new MailAddress(toemail));
                mail.Body = bodyhtml;// "邮件内容";
                //设置邮件的格式
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;
                //设置图片
                //mail.Attachments.Add(new Attachment("bg_login_top.jpg"));
                //mail.Attachments[0].ContentType.Name = "image/jpg";
                //mail.Attachments[0].ContentId = "NHLHIMG";
                //mail.Attachments[0].ContentDisposition.Inline = true;
                //mail.Attachments[0].TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                //设置图片

                //发送附件
                //mail.Attachments.Add(new Attachment(@"D:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf));
                //设置邮件的发送级别
                mail.Priority = MailPriority.Normal;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                SmtpClient client = new SmtpClient();
                //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
                client.Host = "smtp.sina.com";
                //设置用于 SMTP 事务的端口，默认的是 25
                int port = 25;
                client.Port = port;
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                //这里才是真正的邮箱登陆名和密码
                client.Credentials = new System.Net.NetworkCredential("fjqi1214@sina.com", "1i21i4051i8");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);


            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message + ee.InnerException.Message);
                Console.ReadKey();
            }


        }
    }
}
