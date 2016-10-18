using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShareYou.Utility.Mail
{
    public class SendMail
    {
        public static readonly SmtpClient MailClient;
        public static Queue<MailMessage> messages;
        /// <summary>
        /// 设置或者获取邮件发送服务的状态
        /// </summary>
        public static bool Enable;
        static SendMail()
        {
            MailClient=new SmtpClient();
            MailClient.DeliveryMethod= SmtpDeliveryMethod.Network;
            MailClient.EnableSsl = true;
            MailClient.Host = "smtp.126.com";
            MailClient.Port = 25;
            MailClient.Credentials=new NetworkCredential("kangze25@126.com","126mail4418000");
            messages=new Queue<MailMessage>();
            Enable = true;
        }

        public void Send(string address,string validatecode)
        {
            MailMessage mm=new MailMessage();
            mm.Priority= MailPriority.Normal;
            mm.From=new MailAddress("kangze25@126.com","ShareYou", Encoding.GetEncoding(936));
            mm.ReplyToList.Add(new MailAddress("kangze25@hotmail.com", "站长服务", Encoding.GetEncoding(936)));
            
            //mm.CC.Add(); 不选择抄送
            mm.To.Add(new MailAddress(address));
            mm.Subject = "ShareYou,你的验证码,好好接着";
            mm.SubjectEncoding= Encoding.GetEncoding(936);
            mm.IsBodyHtml = true;
            mm.BodyEncoding= Encoding.GetEncoding(936);
            mm.Body = "<font color=\"red\">"+validatecode+"</font>";

            //mm.Attachments.Add(new Attachment()); 邮件的附件
            //MailClient.Send(mm);
            //不发送，使其添加到队列中来发送邮件
            messages.Enqueue(mm);
        }

        public static void BeignWork()
        {
            //开始创建一个线程来发送邮件
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                //开启一个线程,
                while (true)
                {
                    if (messages.Count == 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    //开始出队队列
                    try
                    {
                        MailClient.Send(messages.Dequeue());

                    }
                    catch (Exception e)
                    {
                        //todo:如何判断发送失败!
                    }
                }
            }, null);
        }

    }
}
