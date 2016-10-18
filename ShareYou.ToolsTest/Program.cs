using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShareYou.DBAccess;
using ShareYou.DBAccess.Forum;
using ShareYou.Model.UserInfo;
using ShareYou.Utility;
using ShareYou.Utility.Mail;

namespace ShareYou.ToolsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 示例代码
            //            //SendMail smail = new SendMail();
            //            //SendMail.BeignWork();
            //            //int i = 1;
            //            //while (true)
            //            //{
            //            //    i++;
            //            //    smail.Send("374187303@qq.com", "666");
            //            //    Thread.Sleep(4000);

            //            //    if (i > 400)
            //            //        break;
            //            //}
            //            //try
            //            //POST http://iphone.lxtx2015.net/api/CustomerOfFound/CustomerOfFound241%20HTTP/1.1 HTTP/1.1
            ////Host: iphone.lxtx2015.net
            ///*
            //CustomerId: 68872
            //Ticket: 6b9f06f4-1f7a-4a3f-805d-3ddc5095278f
            //Version: 2.4.1
            //User-Agent: lxtxAppCustomer/2.4.1 (iPhone; iOS 9.3.1; Scale/2.00)
            //Accept-Language: zh-Hans-CN;q=1, en-CN;q=0.9
            //Accept-Encoding: utf-8, deflate
            //Content-Length: 47
            //Expect: 100-continue
            //Connection: Keep-Alive

            //CustomerId=68872&FoundInfoId=1861&FoundTypeId=1
            //            //{
            //            //    int x = 1;
            //            //}
            //            //catch(Exception e)
            //            //{
            //            //    e.Message
            //            //}
            //            //缓存测试

            //            //CacheResolver.mc.Add("1", "1");

            //            //CacheResolver.mc.Add("1", "22222");


            //            //Console.WriteLine(CacheResolver.mc.Get("1").ToString());
            //            //Console.ReadKey();
            //            //****** 重新来写

            //            //构建信息*/

            //            HttpWebRequest http = HttpWebRequest.CreateHttp("http://iphone.lxtx2015.net/api/CustomerOfFound/CustomerOfFound241");
            //            SetHeaderValue(http.Headers, "Host", " iphone.lxtx2015.net");
            //            http.Accept = "*/*";
            //            http.CookieContainer = new CookieContainer();
            //            http.Headers.Add("Cookie", " Nop.customer=0a2db372-daa8-46c3-814f-7984fe7d1203");
            //            //http.CookieContainer.Add(new Cookie("Nop.customer", "0a2db372-daa8-46c3-814f-7984fe7d1203","",""));
            //            http.Method = "POST";
            //            http.Headers.Add("CustomerId: 68872");  //需要变更
            //            //第一次进入版块要变，所以
            //            http.Headers.Add("Ticket: 6b9f06f4-1f7a-4a3f-805d-3ddc5095278f");

            //            http.Headers.Add("Version: 2.4.1");
            //            http.ContentType = "application/x-www-form-urlencoded";
            //            http.ContentLength = 47;
            //            http.UserAgent = " lxtxAppCustomer/2.4.1 (iPhone; iOS 9.3.1; Scale/2.00)";
            //            http.Headers.Add("Accept-Language: zh-Hans-CN;q=1, en-CN;q=0.9");
            //            http.Headers.Add("Accept-Encoding: utf-8, deflate");
            //            SetHeaderValue(http.Headers, "Connection", "Keep-Alive");
            //            var ss = http.GetRequestStream();
            //             var buffer = Encoding.UTF8.GetBytes("CustomerId=68872&FoundInfoId=1861&FoundTypeId=1");

            //            ss.Write(buffer,0,buffer.Length);
            //            ss.Flush();
            //            var response = http.GetResponse();

            //            using (var streams = response.GetResponseStream())
            //            {
            //                using (StreamReader reader = new StreamReader(streams))
            //                {
            //                    string s = reader.ReadToEnd();
            //                }
            //            }

            //            //string body =
            //            //    "id=3D470B64A117E085B9B2BEB2C3DD6E93D12D777B7AAFBC3B34BA9A33FC2209B70ACCA07DA2CDCDA826A21D53FB4E87FE&params=84FA6611CEBA1141BB27A811A1E750DA57C06EE8D838892DB189FE663D55C09E8743F0CA02A6F4CCA3C4C2C1D6675F151700A7E9E304199174274F3E0DFB5F98B9E20B3C4C2D8EF8FA9A6B6B6163";
            //            //byte[] buffer = Encoding.UTF8.GetBytes(body);
            //            //http.ContentLength = buffer.Length;
            //            //http.ContentType = "application/x-www-form-urlencoded";
            //            //http.Method = "POST";
            //            //http.CookieContainer=new CookieContainer();
            //            //http.Host = "reg.163.com";
            //            //SetHeaderValue(http.Headers, "Connection", "Keep-Alive");
            //            //Stream st = http.GetRequestStream();
            //            //st.Write(buffer,0,buffer.Length);
            //            //st.Flush();
            //            //var response =(HttpWebResponse)http.GetResponse();
            //            //string sid = response.Cookies["SID"].Value;
            //            //string jsessionid = response.Cookies["JSESSIONID"].Value;

            //            //HttpWebRequest http2 =
            //            //    HttpWebRequest.CreateHttp(
            //            //        "http://api.lofter.com/v1.1/usercounts.api?market=LOFTER&deviceid=00000000-2544-4752-524e-4ba562cce3ff&product=lofter-android-5.1.1");
            //            //http2.Headers.Add("URSMobID: " + "3D470B64A117E085B9B2BEB2C3DD6E93D12D777B7AAFBC3B34BA9A33FC2209B70ACCA07DA2CDCDA826A21D53FB4E87FE");//和第一个id保持一致
            //            //http2.UserAgent = "LOFTER-Android 5.1.1 (Nexus 4; Android 5.0.1; CMCC) WIFI";
            //            //http2.Accept = "utf-8";
            //            //http2.CookieContainer=new CookieContainer();
            //            //http2.CookieContainer.Add(new Cookie("NTESLOFTSI", "1460627706691CA295F68B35312D26CD4A172977ECB9A.hzabj-lofter4-8010","/",".api.lofter.com"));//这个cookie未知
            //            //http2.Host = "api.lofter.com";
            //            //SetHeaderValue(http.Headers, "Connection", "Keep-Alive");
            //            ////开始发送请求
            //            //var responese2 = http.GetResponse();
            //            //using (Stream s = responese2.GetResponseStream())
            //            //{
            //            //    using (StreamReader reader = new StreamReader(s))
            //            //    {
            //            //        string ss = reader.ReadToEnd();
            //            //    }
            //            //}

            //            //第二次发起请求
            //            //NTESLOFTSI=1460627706691CA295F68B35312D26CD4A172977ECB9A.hzabj-lofter4-8010

            //            //HttpWebRequest http = HttpWebRequest.CreateHttp("http://reg.www.lofter.com/next.jsp");
            //            //var col = new WebHeaderCollection();
            //            //string param =
            //                //"username=kangze25@126.com&loginCookie=WwxikEIvAxjIqw3xbrZetuSKNmP7YLhFdsTfOsnjIOm1d3GbdnHABw2x.8gUyR7E.kXgrXQZAOuUMOc3vEo9f1HJj23G_D_wbQ.iH2HC3Q0R9fR_3TKaOCc391xYJHp.6Nx7htwObEHEl.W1jjvbfX5AH.1KpfP.0_FxbFrONnpmPKzVi7ylBpbCA&persistCookie=GporCG8mpHAaOGn..sOQIOieA5YoJ8aZfnHM4WWqQPI2QkujQqfOaE5CLcDXF.B2Ld0D3091OiUX5kZWdyeJdYaQoAnzgRG5o0dstRMJNlaknzBkbvdBcSCnO&sInfoCookie=1460579016%7C0%7C%233%2640%23%7Ckangze25%40126.com&pInfoCookie=kangze25%40126.com%7C1460579016%7C1%7Clofter%7C11%2611%7Cchq%261460578891%26lofter%23chq%26null%2310%230%230%7C%260%7Cblog%26lofter%7Ckangze25%40126.com&antiCSRFCookie=72d74244a72d51af9cdd3e6358a190b9&checkCookieTime=1&next=www.lofter.com&url=http%3A%2F%2Fwww.lofter.com%2Flogingate.do%3Fusername%3Dkangze25%40126.com";
            //            //byte[] buffer = Encoding.UTF8.GetBytes(param);
            //            //http.ContentLength = buffer.Length;
            //            ////col.Add("Accept", "text/html, application/xhtml+xml, image/jx
            //            //http.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
            //            ////http.Headers.Add("Accept: text/html, application/xhtml+xml, image/jxr, */*");
            //            //http.Headers.Add("Accept-Encoding", "utf-8, deflate");
            //            //http.Headers.Add("Accept-Language", " zh-Hans-CN,zh-Hans; q=0.8, en-US; q=0.5, en; q=0.3");
            //            //http.Headers.Add("Cache-Control: no-cache");
            //            ////http.Connection = "Keep-Alive";
            //            //SetHeaderValue(http.Headers, "Connection", "Keep-Alive");
            //            //SetHeaderValue(http.Headers, "Host", "reg.163.com");
            //            //http.CookieContainer = new CookieContainer();
            //            ////http.CookieContainer.
            //            ////http.CookieContainer.Add(new Cookie(){Domain = ".163.com",Name = "P_INFO"});
            //            ////http.Headers.Add("Connection: Keep-Alive");
            //            ////http.Headers.Add("Content-Length: 155");
            //            //http.Host = "reg.www.lofter.com";
            //            //http.ContentType = "Content-Type: application/x-www-form-urlencoded";
            //            ////http.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            //            ////http.Headers.Add("Host: reg.163.com");
            //            //http.Referer = "http://www.lofter.com/login?urschecked=true";
            //            ////http.Headers.Add("Referer: ");
            //            //http.UserAgent =
            //            //    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.103 Safari/537.36";
            //            ////http.Headers.Add("");
            //            //http.Method = "POST";
            //            //http.AllowAutoRedirect = true;
            //            //var stream = http.GetRequestStream();
            //            //string message = string.Empty;
            //            //stream.Write(buffer, 0, buffer.Length);
            //            //stream.Flush();
            //            ////stream.Dispose();
            //            //using (HttpWebResponse response = (HttpWebResponse)http.GetResponse())
            //            //{
            //            //    //这个时候获取到了跳转页面，能够获取到cookies了
            //            //    //Console.WriteLine(response.Cookies["JSESSIONID"]);
            //            //    string s = response.Headers["Set-Cookie"];
            //            //    //var text = http.CookieContainer.GetCookies(new Uri(".163.com"));

            //            //}
            //            //Console.WriteLine(message);
            //            //Console.ReadKey();

            //            //var response = http.GetRequestStream();
            //            //Console.WriteLine(response.ContentLength);
            //            //Console.WriteLine(response.Length);
            //            //List<int> list=new List<int>();
            //            //while (true)
            //            //{
            //            //    list.Add(response.ReadByte());
            //            //    if (list.Count == 500)
            //            //        break;
            //            //}
            //            Console.ReadKey(); 
            #endregion

            //string sql = "select * from user_usersimp";
            //DataSet set=new DataSet();
            //SqlHelper.GetDataTable(sql, CommandType.Text,set);
            LikePersonDal p=new LikePersonDal();
            List<UserSimp> list = p.GetLikedPersons(new List<int>() {4, 5}).ToList();
            Console.ReadKey();


        }
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }


    }

    public class person
    {
        public string Name
        {
            get
            {
                int i;
                int y;
                return "kangze";
            }
        }
    }
}
