using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace uploadWav
{
    class Program
    {
        private static string _serverIP = "192.168.10.218";//设备地址

        private static string serverPort = "80";
            //string.Format(ConfigurationManager.AppSettings["serverIP"]);
            //string.Format(ConfigurationManager.AppSettings["serverPort"]);
        private static int _serverPort = Convert.ToInt16(serverPort);//设备web远程端口
        private static IPAddress serverIPAddress;
        private static TcpClient tcpClient;
        static void Main(string[] args)
        {
            uploadLogin();
        }
        

        public static string uploadLogin()
        {
            string result = "";
            string httpsStr = @"GET /xml?method=gw.account.login&id51=1234.com HTTP/1.1
                                        Host:192.168.10.218
                                        Content-Length: 0";
            ExecuteTheInstruction(httpsStr);
            return result;

        }


        public static string ExecuteTheInstruction(string httpstr)
        {
            string resultstr = "";
            serverIPAddress = IPAddress.Parse(_serverIP);
            tcpClient = new TcpClient();
            tcpClient.Connect(serverIPAddress, _serverPort);
            if (0 == 1)
            {
                //LogHelper.Error("failed to connect server, please try it again!\n");
            }
            else
            {
                //获取一个和服务器关联的网络流
                NetworkStream networkStream = tcpClient.GetStream();
                //给服务器发送数据
                //string httpstr = string.Format("POST /xml HTTP/1.1\nContent-Type:text/html\nHost:192.168.10.218:8080\nContent-Length: {0}\n\n{1}", apixml.Length, apixml);
                //LogHelper.Info("send: " + httpstr + "\n");
                byte[] buf = Encoding.UTF8.GetBytes(httpstr);
                networkStream.Write(buf, 0, buf.Length);
                networkStream.Flush();
                //读取服务器返回的信息
                byte[] recvbuf = new byte[1600];
                networkStream.Read(recvbuf, 0, recvbuf.Length);
                networkStream.Flush();
                resultstr = Encoding.UTF8.GetString(recvbuf);
                //LogHelper.Info("recv: " + resultstr + "\n");
            }
            return resultstr;
        }
    }
}
