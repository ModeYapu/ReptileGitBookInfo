using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace TrueNewFile
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream  fsl = File.Open("D:\\timg.jpg", FileMode.Open, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fsl);
            while (true)
            {
                TestNewFile.btnOk_Click(br);//真实文件类型
                Console.WriteLine(br.ToString());
            }
            Console.ReadKey();
        }
    }
}
