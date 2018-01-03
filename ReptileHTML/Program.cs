using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using EXCHANGE.MAIL.SIGNATURE.Comm;
using HtmlAgilityPack;

namespace ReptileGitBookInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.SetConfig();
            LogHelper.Info("当前I开始发送请求...，请稍候!:" + 1);
            Random r = new Random();
            string strPageSize = SaveGitBook("0", "JS", true);
            if (strPageSize != "")
            {
                int pageSize = Convert.ToInt32(strPageSize);
                int strBooks = Convert.ToInt32(strPageSize);
                int books = 12;
                pageSize = Convert.ToInt32(Math.Ceiling((double)strBooks / books));
                int ran = r.Next(2, 8);
                int sleepTime = 1000 * 60 * ran;
                for (int i = 1; i <= pageSize; i++)
                {
                    LogHelper.Info("当前I睡眠中...，请稍候!:" + i);

                    StopWatch sw = new StopWatch(Console.CursorLeft, Console.CursorTop, sleepTime);
                    sw.Start();
                    System.Threading.Thread.Sleep(sleepTime);
                    sw.finsh();
                    Console.WriteLine();
                    LogHelper.Info("当前I开始发送请求...，请稍候!:" + i);
                    //Save(i + "", "android");
                    SaveGitBook(i.ToString(), "JS");
                    LogHelper.Info("当前I请求完成!:" + i);
                }
                Console.ReadLine();
            }
        }
        public static string Save(string index, string name, bool isOne = false)
        {
            //第一步声明HtmlAgilityPack.HtmlDocument实例
            HtmlDocument doc = new HtmlDocument();
            //获取Html页面代码
            //string html = GetHttp("5","android");
            Dictionary<string, string> result = HttpGET(index, name);
            string html = result["result"];
            LogHelper.Warn("html:" + html);
            Console.WriteLine(html);
            //第二步加载html文档
            doc.LoadHtml(html);
            //获取总页数
            string strPageSize = "";
            if (isOne)
            {
                HtmlNodeCollection htmlnode = doc.DocumentNode.SelectNodes("//div[@class='paginate-container']/div[@class='pagination']/a");
                strPageSize = htmlnode[htmlnode.Count - 2].InnerText;
                LogHelper.Debug("总页数是：" + strPageSize);
                Console.WriteLine();
                Console.WriteLine();
                htmlnode = null;
            }
            //获取所有板块的a标签
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//div[@class='repo-list-item d-flex flex-justify-start py-4 public source']/div");
            if (collection == null)
            {
                LogHelper.Debug("collection=null");
                return "";
            }
            foreach (HtmlNode item in collection)
            {
                string strURL = "";
                HtmlNode mItem;
                mItem = item.SelectSingleNode("//h3/a[@class='v-align-middle']");
                strURL = "https://github.com" + mItem.Attributes["href"].Value;
                LogHelper.Debug("strURL" + strURL);
                if (RedisCacheHelper.Exists(strURL))
                {
                    Dictionary<string, string> mResult = RedisCacheHelper.Get<Dictionary<string, string>>(strURL);
                    Console.WriteLine("重新取出的数据...");
                    Console.WriteLine("名字是：" + mResult["name"]);
                    Console.WriteLine("地址是：" + mResult["url"]);
                    Console.WriteLine("说明是：" + mResult["detailed"]);
                    Console.WriteLine("更新时间是：" + mResult["updateTime"]);
                    Console.WriteLine("Stargazers是：" + mResult["stargazers"]);
                    Console.WriteLine("Forks是：" + mResult["forks"]);
                    Console.WriteLine();
                    item.RemoveAll();
                    strURL = "";
                    //同一路径存在表示当前项目已爬过，跳过即可
                    continue;
                }
                Dictionary<string, string> mDic = new Dictionary<string, string>();
                mDic.Add("name", mItem.InnerText.Replace("/n", "").Trim());
                mDic.Add("url", strURL);
                Console.WriteLine("名字是：" + mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("地址是：" + strURL);
                mItem.RemoveAll();
                mItem = item.SelectSingleNode("//p[@class='col-9 d-inline-block text-gray mb-2 pr-4']");
                mDic.Add("detailed", mItem.InnerText == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("说明是：" + mItem.InnerText == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                mItem.RemoveAll();
                mItem = item.SelectSingleNode("//p[@class='f6 text-gray mr-3 mb-0 mt-2']/relative-time");
                mDic.Add("updateTime", mItem.InnerText == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("更新时间是：" + mItem.InnerText.Replace("/n", "").Trim());
                mItem = item.SelectSingleNode("//div[@class='col-2 text-right pt-1 pr-3 pt-2']/a[@class='muted-link']");
                mDic.Add("stargazers", mItem.InnerText == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("Stargazers是：" + mItem.InnerText.Replace("/n", "").Trim());
                //mItem = item.SelectSingleNode("//div[@class='repo-list-stats']/a[@aria-label='Forks']");
                mDic.Add("forks", mItem.InnerText == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("Forks是：" + mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine();
                LogHelper.Debug("strURL" + strURL + "mDic" + mDic);
                RedisCacheHelper.Add<Dictionary<string, string>>(strURL, mDic);
                item.RemoveAll();
                mDic.Clear();
                strURL = "";
            }
            //Console.ReadLine();
            doc = null;
            result = null;
            collection = null;
            return strPageSize;
        }
        public static string SaveGitBook(string index, string name, bool isOne = false)
        {
            //第一步声明HtmlAgilityPack.HtmlDocument实例
            HtmlDocument doc = new HtmlDocument();
            //获取Html页面代码
            //string html = GetHttp("5","android");
            Dictionary<string, string> result = HttpGET(index, name);
            string html = result["result"];
            LogHelper.Warn("html:" + html);
            Console.WriteLine(html);
            //第二步加载html文档
            doc.LoadHtml(html);
            //获取总页数
            string strPageSize = "";
            if (isOne)
            {
                HtmlNodeCollection htmlnode = doc.DocumentNode.SelectNodes("//div[@class='panel panel-default']/ul[@class='list-group']/a[@class='list-group-item active']/span");
                strPageSize = htmlnode[htmlnode.Count-1].InnerText;
                LogHelper.Debug("书总数：" + strPageSize);
                Console.WriteLine();
                Console.WriteLine();
                htmlnode = null;
            }
            //获取所有板块的a标签
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//div[@class='Books']/div[@class='Book']");
            if (collection == null)
            {
                LogHelper.Debug("collection=null");
                return "";
            }
            foreach (HtmlNode item in collection)
            {
                string strURL = "";
                HtmlNode mItem;
                mItem = item.SelectSingleNode("//div[@class='book-infos']/h2[@class='title']/a");
                strURL = mItem.Attributes["href"].Value;

                LogHelper.Debug("strURL" + strURL);
                if (RedisCacheHelper.Exists(strURL))
                {
                    Dictionary<string, string> mResult = RedisCacheHelper.Get<Dictionary<string, string>>(strURL);
                    Console.WriteLine("重新取出的数据...");
                    Console.WriteLine("名字是：" + mResult["name"]);
                    Console.WriteLine("地址是：" + mResult["url"]);
                    Console.WriteLine("说明是：" + mResult["detailed"]);
                    Console.WriteLine("更新时间是：" + mResult["updateTime"]);
                    Console.WriteLine("Stargazers是：" + mResult["stargazers"]);
                    Console.WriteLine("Forks是：" + mResult["forks"]);
                    Console.WriteLine();
                    item.RemoveAll();
                    strURL = "";
                    //同一路径存在表示当前项目已爬过，跳过即可
                    continue;
                }
                Dictionary<string, string> mDic = new Dictionary<string, string>();
                mDic.Add("name", mItem.InnerText.Replace("/n", "").Trim());
                mDic.Add("url", strURL);
                Console.WriteLine("名字是：" + mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("地址是：" + strURL);
                mItem.RemoveAll();
                mItem = item.SelectSingleNode("//p[@class='description']");
                if (mItem!=null)
                {
                    mDic.Add("detailed", mItem == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                    Console.WriteLine("说明是：" +  mItem.InnerText.Replace("/n", "").Trim());
                    mItem.RemoveAll();
                }
                mItem = item.SelectSingleNode("//p[@class='updated']/span");
                mDic.Add("updateTime", mItem == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("更新时间是：" + mItem.InnerText.Replace("/n", "").Trim());
                mItem = item.SelectSingleNode("//div[@class='btn-group']/a[@class='btn btn-count btn-md']");
                mDic.Add("stargazers", mItem == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("Stargazers是：" + mItem.InnerText.Replace("/n", "").Trim());
                //mItem = item.SelectSingleNode("//div[@class='repo-list-stats']/a[@aria-label='Forks']");
                mDic.Add("forks", mItem == null ? "" : mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine("Forks是：" + mItem.InnerText.Replace("/n", "").Trim());
                Console.WriteLine();
                LogHelper.Debug("strURL" + strURL + "mDic" + mDic);
                RedisCacheHelper.Add<Dictionary<string, string>>(strURL, mDic);
                item.RemoveAll();
                mDic.Clear();
                strURL = "";
            }
            //Console.ReadLine();
            doc = null;
            result = null;
            collection = null;
            return strPageSize;
        }
        public static string GetHttp(string index, string name)
        {
            //string str = "https://github.com/search?o=desc&p=" + index + "&q=" + name + "&ref=searchresults&s=stars&type=Repositories&utf8=%E2%9C%93";
            string str = "https://www.gitbook.com/search?q=js&page=0&type=books&sort=stars";
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

            Byte[] pageData = MyWebClient.DownloadData(str); //从指定网站下载数据
            string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
            return pageHtml;
        }
        #region GET请求
        /// <summary>
        /// 发送请求 GET
        /// </summary>
        /// <param name="Url">请求URL</param>
        /// <param name="ContentType">内容类型  可以为null</param>
        /// <param name="Headers">头文件 可以为null </param>
        /// <param name="PostData">post参数内容 可以为null </param>
        /// <returns></returns>
        public static Dictionary<string, string> HttpGET(string index, string name)
        {
            //string str = "https://github.com/search?o=desc&p=" + index + "&q=" + name + "&ref=searchresults&s=stars&type=Repositories&utf8=%E2%9C%93";
            string str = "https://www.gitbook.com/search?q="+name+"&page="+index+"&type=books&sort=stars";
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add("Cookie", "__cfduid	d5aef08c0a2b71d5daa80bd77439617011512531485_ga	GA1.2.1534405646.1512531488_gid	GA1.2.1852403139.1514532903gitbook:sess	eyJ2YXJpYW50IjoxLCJhbm9ueW1vdXNJZCI6ImY0YmQyMmUzLWJmNDUtNGVkNi1hMGVlLTJlMGQzOTc1YWEwNSIsImNzcmZTZWNyZXQiOiJ3bjRWalBfVkJWRlNLQzM4ZGdyR1phdDgifQ==gitbook:sess.sig	wRbLtl5TEQ2LCwEkA2KjZ7vqbV0mp_aa7ba0807002f1cbd33391bdd14d5ad9_mixpanel	{\"distinct_id\": \"16029e6eb28474 - 08b54259773fa7 - 4c322172 - 100200 - 16029e6eb299e\",\"$search_engine\": \"google\",\"$initial_referrer\": \"https://www.google.com.br/\",\"$initial_referring_domain\": \"www.google.com.br\",\"__mps\": {\"$os\": \"Windows\",\"$browser\": \"Firefox\",\"$browser_version\": 58,\"$initial_referrer\": \"https://www.google.com.br/\",\"$initial_referring_domain\": \"www.google.com.br\"},\"__mpso\": {\"Initial utm_source\": \"\",\"Initial utm_medium\": \"\",\"Initial utm_campaign\": \"\",\"Initial utm_content\": \"\",\"Initial utm_term\": \"\"},\"__mpa\": {},\"__mpu\": {},\"__mpap\": []}mp_mixpanel__c  0");
            return SendRequest(str, "GET", null, Headers, null);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="Url">请求URL</param>
        /// <param name="ContentType">内容类型  可以为null</param>
        /// <param name="Headers">头文件 可以为null </param>
        /// <param name="PostData">post参数内容 可以为null </param>
        /// <returns>code and result</returns>
        public static Dictionary<string, string> SendRequest(string Url, string Method, string ContentType, Dictionary<string, string> Headers, string PostData)
        {
            Dictionary<string, string> responseResult = new Dictionary<string, string>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //设置请求模式
            request.Method = Method;

            //设置内容类型
            if (ContentType != null)
            {
                request.ContentType = ContentType;
            }

            //设置Header
            if (Headers != null)
            {
                foreach (KeyValuePair<string, string> para in Headers)
                {
                    request.Headers.Add(para.Key, para.Value);
                }
            }

            //只有参数不为空的时候才发送
            if (PostData != null)
            {
                //将字符串转换为字节
                byte[] postdata = Encoding.UTF8.GetBytes(PostData);
                request.ContentLength = postdata.Length;

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(postdata, 0, postdata.Length);
                    reqStream.Close();
                }
            }

            string result = "";
            int statusCode = 200;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                statusCode = (int)response.StatusCode;
                result = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (WebException ex)
            {
                HttpWebResponse res = ex.Response as HttpWebResponse;
                Stream myResponseStream = res.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                statusCode = (int)res.StatusCode;
                result = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            responseResult.Add("code", statusCode + "");
            responseResult.Add("result", result);
            return responseResult;
        }
        #endregion

        class StopWatch
        {
            private int Interval = 1000;              //时间间隔，单位毫秒
            private int Time = 0;                        //所显示的时间
            private int left = 0;
            private int top = 0;
            private Thread timer;

            public StopWatch() { }
            public StopWatch(int left, int top, int time)
            {
                this.left = left;
                this.top = top;
                Time = time;
            }

            public void Start()
            {
                timer = new Thread(new ThreadStart(Timer));  //新建一个线程，该线程调用Timer()
                timer.Start();                               //启动线程
                Console.CursorVisible = false;   //隐藏光标

            }
            private void Timer()
            {
                while (true)
                {
                    Display();                               //显示秒表计数
                    Thread.Sleep(Interval);         //等待1秒后再执行Timer()刷新计数
                    Time = Time - 1000;                                 //秒数加1
                }
            }
            private void Display()
            {
                Console.SetCursorPosition(left, top);
                Console.Write("剩余:[" + Time / 1000 + "]秒");
            }
            public void finsh()
            {
                if (timer != null)
                    timer.Abort();                              //终止线程,用于停止秒表
                Console.SetCursorPosition(left, top);
                Console.Write("剩余:[0]秒");
            }
        }
    }
}
