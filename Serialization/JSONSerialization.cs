using Newtonsoft.Json;
using System;
using System.Data;

namespace Serialization
{
    public class JSONSerialization
    {
        public static void JsonSerialization()
        {
            //创建 DataTable 1
            DataTable dt1 = new DataTable("Top");

            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn { ColumnName = "Name", DataType = typeof(System.String) },
                new DataColumn { ColumnName = "Age", DataType = typeof(System.Int32) },
                new DataColumn { ColumnName = "MenPai", DataType = typeof(System.String) }
            });

            dt1.Rows.Add(new object[] { "周伯通", 22, "重阳宫" });
            dt1.Rows.Add(new object[] { "洪七公", 19, "丐帮" });
            dt1.Rows.Add(new object[] { "黄药师", 55, "桃花岛" });
            dt1.Rows.Add(new object[] { "欧阳锋", 49, "白驼山" });

            //创建 DataTable 2
            DataTable dt2 = dt1.Clone();
            dt2.TableName = "Second";
            dt2.Rows.Add(new object[] { "郭靖", 22, "丐帮" });
            dt2.Rows.Add(new object[] { "黄蓉", 19, "丐帮" });
            dt2.Rows.Add(new object[] { "梅超风", 55, "桃花岛" });
            dt2.Rows.Add(new object[] { "杨康", 49, "金" });

            //创建DataSet
            DataSet ds = new DataSet("Master");
            ds.Tables.AddRange(new DataTable[] { dt1, dt2 });

            //序列化DataSet为Json字符串
            string myJsonStr = JsonConvert.SerializeObject(ds);
            Console.WriteLine(myJsonStr);

            DataSet newDs = JsonConvert.DeserializeObject<DataSet>(myJsonStr);
            foreach (DataTable Ds in newDs.Tables)
            {
                Console.WriteLine(Ds.TableName + "\n");
                foreach (DataRow dr in Ds.Rows)
                {
                    foreach (DataColumn dc in Ds.Columns)
                        Console.Write(dc.ColumnName + ":" + dr[dc] + "   ");
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
