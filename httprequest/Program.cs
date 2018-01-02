using AddSerect;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace httprequest
{
    class Program
    {
        private static CookieContainer cookie = new CookieContainer();
        private static string TTSServerHostName = ConfigurationManager.AppSettings["ttsServerHostName"];
        private static string TTSServerPort = ConfigurationManager.AppSettings["ttsServerPort"];

        private static XslCompiledTransform xslCompiledTransform_0;
        private const string string_0 = "\r\n<RSAKeyValue>\r\n<Modulus>zLizNmLUd4VlIWee1GXgn/KxEwcghPASQ+NUzZhbY2fTGzpW64T6yEOdHlIbhX1DX6yAz2gMZKfnpQL2aFqxh5ACFV9dONSTzuQzkqeXwFEARsMxGP3eTQSWMpwVhEcraSn1zOqMb3CRDeQpgasq0lv4HRFhbwalOifKarjEL/8=</Modulus>\r\n<Exponent>AQAB</Exponent>\r\n<P>8+4qCwbxpWFuJje/UURm06Uec+Mk6a7Ye9FGvzVlnh7dYK38GR+bnf8NsoMRW8IlJnipfvqqEs1qnhRAJ4j96Q==</P>\r\n<Q>1tnh1UK2FxeVSbTXrrvKlSKAWqkjaQwLB+OQeMM5Ii3ogH++91bmO0Ku8GA4qE+r/KfypT4nECQID7i5ykkFpw==</Q>\r\n<DP>XTEqYtgeTf6xJGy77QJi/ozg24l2OskP8A3+J2LxFb3Y+ey+maKXw38D7qVgZlv/8Xi72MVPYKuWBhraf8A4sQ==</DP>\r\n<DQ>xBAk9FZikQQmahKr2HrqzdmkRBehhtVEo7hZOLr+wmAeklUBUfltNJsPxbApQ/8gtfoVhhIH18Tpzl8GvMCSdQ==</DQ>\r\n<InverseQ>889tPhprihee8OsPUN7Zyf1nH3tNK4uFiGmBCR1l/JMjbK62+wcQxssD7in8dZFzf/hUXZQl++DtiBUtc4O5Tw==</InverseQ>\r\n<D>ZNm0R12GZ17KhBtEzkNl1cW737DKH1MY3GK4GxQsKRszjx09Roba+B7+3rn6HtenghE733DVchyY69w6wQu0mj6fqZ/1ZqvmP0YH1d8otVjG2E6XhshYCJhZ7Ci0Z4n6UZwAG3NBDCtXAqNSUQY7NjPnTfcG5EkQ/nqlFJKdKLE=</D>\r\n</RSAKeyValue>\r\n";
        private const string string_1 = "<?xml version='1.0' encoding='utf-8'?>\r<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:msxsl='urn:schemas-microsoft-com:xslt' exclude-result-prefixes='msxsl'>\r  <xsl:output method='xml' indent='no' omit-xml-declaration='yes'/>\r  <xsl:param name='edition'/>\r  <xsl:param name='version'/>\r  <xsl:param name='userspurchased'/>\r  \r  <xsl:template match='activationrequest'>\r    <data>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates/>\r      <edition><xsl:value-of select='$edition'/></edition>\r      <xsl:text>\r</xsl:text>\r      <version><xsl:value-of select='$version'/></version>\r      <xsl:text>\r</xsl:text>\r      <userspurchased><xsl:value-of select='$userspurchased'/></userspurchased>\r      <xsl:text>\r</xsl:text>\r    </data>\r  </xsl:template>\r\r  <xsl:template match='machinehash'>\r    <machinehash>\r      <xsl:value-of select='text()'/>\r    </machinehash>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcode|majorversion|minorversion|serialnumber|session|edition|productname'>\r    <xsl:copy>\r      <xsl:value-of select='text()'/>\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcodes'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>    \r  </xsl:template>\r\r  <xsl:template match='product'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='text()'/>\r</xsl:stylesheet>\r\n";







        static void Main(string[] args)
        {
            try
            {
                DateTime beforDT = DateTime.Now;
                string key= Class3.smethod_0();

                string string_2 = @"<activationrequest>
<version>3</version>
<machinehash>1D18-29C7-4AB5-EE5E</machinehash>
<productcode>24</productcode>
<majorversion>9</majorversion>
<minorversion>0</minorversion>
<serialnumber>A3AP-6ZJW-YZ24-8ZX2-MTKH-4AYX</serialnumber>
<session>6352cf1d-da21-450d-966e-418f84c74af6</session>
<locale>zh-CN</locale>
<custom>
<email>ssd.cc.com</email>
<identifier>qsc@qsc</identifier>
<newsletter>true</newsletter>
</custom>
</activationrequest>";
               
                string string_3 = "A3AP-6ZJW-YZ24-8ZX2-MTKH-4AYX";
                int int_0 = 1;
                Class5();
                var eess=smethod_1(string_2, string_3, int_0);
                string writePath = @"C:\Users\qsc\Desktop\test.txt";
                Write(writePath, eess);

                Console.ForegroundColor = ConsoleColor.White;
                string path = @"C:\Users\qsc\Desktop\sql.txt";
                //Read(path);

                TianData();
                string content = "请于周一前来值班";
                var wavUrl= GetTTSUrlT(content);
                Console.WriteLine(wavUrl);

                //计算程序运行时间
                DateTime afterDT = DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforDT);
                Console.WriteLine(ts.TotalMilliseconds.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }
        static void  Class5()
        {
            XmlDocument stylesheet = new XmlDocument
            {
                PreserveWhitespace = true
            };
            stylesheet.LoadXml("<?xml version='1.0' encoding='utf-8'?>\r<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:msxsl='urn:schemas-microsoft-com:xslt' exclude-result-prefixes='msxsl'>\r  <xsl:output method='xml' indent='no' omit-xml-declaration='yes'/>\r  <xsl:param name='edition'/>\r  <xsl:param name='version'/>\r  <xsl:param name='userspurchased'/>\r  \r  <xsl:template match='activationrequest'>\r    <data>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates/>\r      <edition><xsl:value-of select='$edition'/></edition>\r      <xsl:text>\r</xsl:text>\r      <version><xsl:value-of select='$version'/></version>\r      <xsl:text>\r</xsl:text>\r      <userspurchased><xsl:value-of select='$userspurchased'/></userspurchased>\r      <xsl:text>\r</xsl:text>\r    </data>\r  </xsl:template>\r\r  <xsl:template match='machinehash'>\r    <machinehash>\r      <xsl:value-of select='text()'/>\r    </machinehash>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcode|majorversion|minorversion|serialnumber|session|edition|productname'>\r    <xsl:copy>\r      <xsl:value-of select='text()'/>\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcodes'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>    \r  </xsl:template>\r\r  <xsl:template match='product'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='text()'/>\r</xsl:stylesheet>\r\n");
            xslCompiledTransform_0 = new XslCompiledTransform();
            xslCompiledTransform_0.Load(stylesheet);
        }

        public static string smethod_1(string string_2, string string_3, int int_0)
        {
            try
            {
                XmlDocument input = new XmlDocument();
                input.LoadXml(string_2);
                XmlWriterSettings settings = xslCompiledTransform_0.OutputSettings.Clone();
                settings.NewLineChars = "\r\n";
                settings.NewLineHandling = NewLineHandling.Replace;
                StringBuilder output = new StringBuilder();
                XmlWriter results = XmlWriter.Create(output, settings);
                XsltArgumentList arguments = new XsltArgumentList();
                arguments.AddParam("version", "", 3);
                arguments.AddParam("edition", "", string_3);
                arguments.AddParam("userspurchased", "", int_0);
                xslCompiledTransform_0.Transform(input, arguments, results);
                return smethod_0(output.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string smethod_0(string string_2)
        {
            CspParameters parameters = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(parameters);
            provider.FromXmlString("\r\n<RSAKeyValue>\r\n<Modulus>zLizNmLUd4VlIWee1GXgn/KxEwcghPASQ+NUzZhbY2fTGzpW64T6yEOdHlIbhX1DX6yAz2gMZKfnpQL2aFqxh5ACFV9dONSTzuQzkqeXwFEARsMxGP3eTQSWMpwVhEcraSn1zOqMb3CRDeQpgasq0lv4HRFhbwalOifKarjEL/8=</Modulus>\r\n<Exponent>AQAB</Exponent>\r\n<P>8+4qCwbxpWFuJje/UURm06Uec+Mk6a7Ye9FGvzVlnh7dYK38GR+bnf8NsoMRW8IlJnipfvqqEs1qnhRAJ4j96Q==</P>\r\n<Q>1tnh1UK2FxeVSbTXrrvKlSKAWqkjaQwLB+OQeMM5Ii3ogH++91bmO0Ku8GA4qE+r/KfypT4nECQID7i5ykkFpw==</Q>\r\n<DP>XTEqYtgeTf6xJGy77QJi/ozg24l2OskP8A3+J2LxFb3Y+ey+maKXw38D7qVgZlv/8Xi72MVPYKuWBhraf8A4sQ==</DP>\r\n<DQ>xBAk9FZikQQmahKr2HrqzdmkRBehhtVEo7hZOLr+wmAeklUBUfltNJsPxbApQ/8gtfoVhhIH18Tpzl8GvMCSdQ==</DQ>\r\n<InverseQ>889tPhprihee8OsPUN7Zyf1nH3tNK4uFiGmBCR1l/JMjbK62+wcQxssD7in8dZFzf/hUXZQl++DtiBUtc4O5Tw==</InverseQ>\r\n<D>ZNm0R12GZ17KhBtEzkNl1cW737DKH1MY3GK4GxQsKRszjx09Roba+B7+3rn6HtenghE733DVchyY69w6wQu0mj6fqZ/1ZqvmP0YH1d8otVjG2E6XhshYCJhZ7Ci0Z4n6UZwAG3NBDCtXAqNSUQY7NjPnTfcG5EkQ/nqlFJKdKLE=</D>\r\n</RSAKeyValue>\r\n");
            byte[] bytes = Encoding.UTF8.GetBytes(string_2);
            string str = Convert.ToBase64String(provider.SignData(bytes, new SHA1CryptoServiceProvider()));
            return ("<activationresponse>\r\n" + string_2 + "\r\n<signature>\r\n" + str + "\r\n</signature>\r\n</activationresponse>\r\n");
        }




        public static void TianData()
        {
            var msgType = "";
            var msgTo = "";
            var msgContent = new MsgContent();
            var messageInfo = new List<Message>();
            for (var i = 0; i < 2; i++)
            {
                msgType = "phoNE";
                msgTo = "17317832559";
                msgContent.MsgBody = "ASDFGHFHGDFDSZ";
                msgContent.MsgSubject = "值班系统通知";
                messageInfo.Add(new Message()
                {
                    MsgType = msgType,
                    MsgTo = msgTo,
                    MsgContent = msgContent
                });
            }
            PostTest(messageInfo);
        }
        private static string GetTTSUrlT(string content)
        {
            string wavStr = "";
            string Url = string.Format("http://{0}:{1}/api/tts/getWavUrl",TTSServerHostName, TTSServerPort);
            string keyText = content;
            string postDataStr = "key=" + keyText;
            var data = HttpGet(Url, postDataStr);
            dynamic json = JsonConvert.DeserializeObject(data);
            if (json.result == true)
            {
                string url = json.data;
                wavStr = url.Replace(".pcm", "");
            }
            return wavStr;
        }
        public static void PostTest(List<Message> messageIn)
        {
            string url = "http://192.168.11.157:6789/api/Message/AddMessages";
            string postDataStr = ObjToJSON(messageIn);
            var resturst = Post(url, postDataStr);
            Console.WriteLine(resturst);

            JObject jo = (JObject)JsonConvert.DeserializeObject(resturst);
            string datataData = jo["data"].ToString();
            //JsonParser rb = JsonConvert.DeserializeObject<JsonParser>(resturst);
            //var rbs = JsonConvert.DeserializeObject<JsonParser>(resturst);
            //var rb = JsonConvert.DeserializeObject<Dictionary<string, data>>(resturst);
            var zbRequestId = "";
            zbRequestId = Guid.NewGuid().ToString();
            var returnMsgInfo = new List<ReturnMsgInfo>();
            string errorData = jo["error"].ToString();
            if (!string.IsNullOrEmpty(errorData))
            {
                var errorlIST = new List<error>();
                errorlIST = ParseFromJson<List<error>>(errorData);
                foreach (var msg in errorlIST)
                {
                    returnMsgInfo.Add(new ReturnMsgInfo
                    {
                        Error = "",
                        ErrorMsg = msg.errorMsg,
                        MsgType = msg.msgType,
                        MsgTo = msg.msgTo,
                        ZBRequestId = zbRequestId
                    });
                }
            }
            string dataData = jo["data"].ToString();
            if (!string.IsNullOrEmpty(dataData))
            {
                JObject js = (JObject)JsonConvert.DeserializeObject(dataData);
                string dataErrorData = js["error"].ToString();
                if (!string.IsNullOrEmpty(dataErrorData)&&dataErrorData.Length>2)
                {
                    var errorlIST = new List<error>();
                    errorlIST = ParseFromJson<List<error>>(errorData);
                    foreach (var msg in errorlIST)
                    {
                        returnMsgInfo.Add(new ReturnMsgInfo
                        {
                            Error = "",
                            ErrorMsg = msg.errorMsg,
                            MsgType = msg.msgType,
                            MsgTo = msg.msgTo,
                            ZBRequestId = zbRequestId
                        });
                    }
                }
                string detailData = js["detail"].ToString();
                if (!string.IsNullOrEmpty(detailData))
                {
                    string requestIdData = js["requestId"].ToString();                    
                    var datalist = new List<detail>();
                    datalist = ParseFromJson<List<detail>>(detailData);
                    foreach (var msg in datalist)
                    {
                        returnMsgInfo.Add(new ReturnMsgInfo
                        {
                            Error = "",
                            MsgType = msg.msgType,
                            MsgTo = msg.msgTo,
                            ZBRequestId = zbRequestId,
                            XsMsgId=msg.msgId,
                            XsRequestId = requestIdData
                        });
                    }
                }
            }

            foreach (var returnMsg in returnMsgInfo)
            {
                Console.WriteLine("ErrorMsg:{0}", returnMsg.ErrorMsg);
                Console.WriteLine("XsMsgId:{0}", returnMsg.XsMsgId);
                Console.WriteLine("XsRequestId:{0}",returnMsg.XsRequestId);
            }







        }
        public static void Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                //http://172.16.128.46:9890/api/tts/getWavUrl
                //http://localhost:9980/api/tts/getWavUrl
                string Url = "http://172.16.128.46:9890/api/tts/getWavUrl";
                string keyText = line.ToString();
                string postDataStr = "key=" + keyText;
                var data = HttpGet(Url, postDataStr);
                dynamic json = JsonConvert.DeserializeObject(data);
                Console.WriteLine(data);
                i++;
                if (json.result == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string url = json.data;
                    Console.WriteLine(json.data);
                    Console.WriteLine(url.LastIndexOf("pcm"));
                    Console.WriteLine(url.Replace(".pcm", ""));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("当前行数{0}", i);
                    string urlAndCount = string.Format("\n\n\n[{0}]返回url:{1},当前为第{2}个", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ff"), url.Replace(".pcm", ""), i);
                    string writePath = @"C:\Users\qsc\Desktop\test.txt";
                    Write(writePath, urlAndCount);
                }
            }
        }
        public static void Write(string path, string urlAndCount)
        {
            //FileStream fs = new FileStream(path, FileMode.Create);
            if (!File.Exists(path))
            {
                FileInfo myfile = new FileInfo(path);
                FileStream fs = myfile.Create();
                fs.Close();
            }
            StreamWriter sw = File.AppendText(path);
            //开始写入
            sw.WriteLine(urlAndCount);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            //fs.Close();
        }
        public static string HttpGet(string Url, string postDataStr)
        {
            string retString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                Console.WriteLine(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retString;
        }
        private static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }




        public static string Post(string Url, string jsonParas)
        {
            string strURL = Url;

            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";

            //设置参数，并进行URL编码 
            request.Headers.Add("appkey","det43");
            //var timeStamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var timeStamp = "11432113215";
            request.Headers.Add("timestamp", timeStamp);
            var appSecert = "fadf";
            var sourceSign = string.Format("{0}{1}", appSecert, timeStamp);
            var sign = ShaUtil.SHA256HashStringForUTF8String(sourceSign);
            request.Headers.Add("sign", sign);
            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            //将Json字符串转化为字节  
            payload = Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();


            //Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();


            return postContent;//返回Json数据
        }

        /// <summary>
        /// POST json数据请求与获取结果
        /// </summary>
        public static string HttpPostT(string Url, string postDataStr)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(postDataStr);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 6000000;
                request.ContentType = "application/json;charset=UTF-8";
                request.ContentLength = bytes.Length;
                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);
                //StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8);
                //writer.Write(postDataStr);
                //writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                return retString;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>  
        /// 指定Post地址使用Get 方式获取全部字符串  
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>  
        /// <returns></returns>  
        public static string PostT(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";

            #region 添加Post 参数  
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 生成JSON字符
        /// </summary>
        /// <param name="obj">待生成的对象,如泛型实体</param>
        /// <returns></returns>
        protected static string ObjToJSON(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();  //注意 要有T类型要有无参构造函数
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
