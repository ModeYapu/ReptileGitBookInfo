using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            //MartialArtsMasterIS();
            //XmlSerializations();
            JSONSerialization.JsonSerialization();
            Console.ReadKey();
        }
        
        private static void MartialArtsMasterIS()
        {
            //本例命名空间
            //using System.Runtime.Serialization.Formatters.Binary;
            //using System.IO;

            //初始化武林高手
            var master = new List<MartialArtsMaster>(){
                new MartialArtsMaster(){ Id = 1, Name = "黄蓉",    Age = 18, Menpai = "丐帮", Kungfu = "打狗棒法",  Level = 9  },
                new MartialArtsMaster(){ Id = 2, Name = "洪七公",  Age = 70, Menpai = "丐帮", Kungfu = "打狗棒法",  Level = 10 },
                new MartialArtsMaster(){ Id = 3, Name = "郭靖",    Age = 22, Menpai = "丐帮", Kungfu = "降龙十八掌",Level = 10 },
                new MartialArtsMaster(){ Id = 4, Name = "任我行",  Age = 50, Menpai = "明教", Kungfu = "葵花宝典",  Level = 1  },
                new MartialArtsMaster(){ Id = 5, Name = "东方不败",Age = 35, Menpai = "明教", Kungfu = "葵花宝典",  Level = 10 },
                new MartialArtsMaster(){ Id = 6, Name = "林平之",  Age = 23, Menpai = "华山", Kungfu = "葵花宝典",  Level = 7  },
                new MartialArtsMaster(){ Id = 7, Name = "岳不群",  Age = 50, Menpai = "华山", Kungfu = "葵花宝典",  Level = 8  }
            };

            //文件流写入
            using (FileStream fs = new FileStream(@"d:\master.obj", FileMode.Append))
            {
                var myByte = Serializer.SerializeBytes(master);
                fs.Write(myByte, 0, myByte.Length);
                fs.Close();
            };

            //文件流读取
            using (FileStream fsRead = new FileStream(@"d:\master.obj", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                var myObj = Serializer.DeserializeBytes(heByte) as List<MartialArtsMaster>;
                Console.WriteLine("编号---姓名---年龄---门派---武功---等级");
                myObj.ForEach(m =>
                    Console.WriteLine(m.Id + "---" + m.Name + "---" + m.Age + "---" + m.Menpai + "---" + m.Kungfu + "---" + m.Level)
                );
            }
        }
        private static void XmlSerializations()
        {
            //XML序列化命名空间：System.Xml.Serialization;
            //类：XmlSerializer
            //主要方法：Serialize和Deserialize

            //博客数据的对象化（反序列化）及序列化

            //(1)获取我博客园中最后10篇文章 (实际上这个过程也是反序列化)
            List<MyArticle> myArticleList = XmlSerialization.GetMyArticle(10);
            //(2)将上面的对象myArticleList序列化XML，并输出到控制台
            string xmlString = String.Empty;
            using (MemoryStream ms = new MemoryStream())
            {

                XmlSerializer xml = new XmlSerializer(typeof(List<MyArticle>));
                xml.Serialize(ms, myArticleList);
                byte[] arr = ms.ToArray();
                xmlString = Encoding.UTF8.GetString(arr, 0, arr.Length);
                ms.Close();
            }
            Console.WriteLine(xmlString);
            using (StringReader sr = new StringReader(xmlString))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<MyArticle>));
                ;
                List<MyArticle> myNewArticleList = xml.Deserialize(sr) as List<MyArticle>;

                //遍历输出反序化的新对象myNewArticleList
                myNewArticleList.ForEach(m =>
                    Console.WriteLine("文章编号：" + m.id + "\n文章标题:" + m.title)
                );
                sr.Close();
            }
        }
    }
    /// <summary>
    /// 类：武林高手
    /// MartialArtsMaster
    /// </summary>
    [Serializable]
    class MartialArtsMaster
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 门派
        /// </summary>
        public string Menpai { get; set; }
        /// <summary>
        /// 武学
        /// </summary>
        public string Kungfu { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }

    /// <summary>
    /// 二进制序列化和反序列化类
    /// </summary>
    public class Serializer
    {
        /// <summary> 
        /// 使用二进制序列化对象。 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static byte[] SerializeBytes(object value)
        {
            if (value == null) return null;

            var stream = new MemoryStream();
            new BinaryFormatter().Serialize(stream, value);

            //var dto = Encoding.UTF8.GetString(stream.GetBuffer()); 
            var bytes = stream.ToArray();
            return bytes;
        }

        /// <summary> 
        /// 使用二进制反序列化对象。 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static object DeserializeBytes(byte[] bytes)
        {
            if (bytes == null) return null;

            //var bytes = Encoding.UTF8.GetBytes(dto as string); 
            var stream = new MemoryStream(bytes);

            var result = new BinaryFormatter().Deserialize(stream);

            return result;
        }
    }
}
