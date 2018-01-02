using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace httprequest
{
    internal class Class3
    {
        // Fields
        private static string string_0 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Methods
        public static string smethod_0()
        {
            switch (Class4.int_0)
            {
                case 0:
                    return smethod_1();

                case 1:
                    return smethod_2();

                case 2:
                    return smethod_3();
            }
            return "Hey there!";
        }

        public static string smethod_1()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("LZ-{0}-{1}-", string_0[random.Next() & string_0.Length], string_0[random.Next() & string_0.Length]);
            builder.AppendFormat("{0}", random.Next(0x2710, 0x186a0));
            builder.AppendFormat("-{0:X4}", Class2.smethod_0(builder.ToString()));
            return builder.ToString();
        }

        public static string smethod_2()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}-{1}-", random.Next(100, 0x3e8), random.Next(100, 0x3e8));
            builder.AppendFormat("{0}", random.Next(0x186a0, 0xf4240));
            builder.AppendFormat("-{0:X4}", Class2.smethod_0(builder.ToString()));
            return builder.ToString();
        }

        public static string smethod_3()
        {
            char ch;
            char ch2;
            StringBuilder builder = new StringBuilder("A300");
            byte[] buffer = new byte[20];
            new Random().NextBytes(buffer);
            for (int i = 0; i < 5; i++)
            {
                builder.AppendFormat("-{0}{1}{2}{3}", new object[] { Class0.smethod_2(buffer[i * 4]), Class0.smethod_2(buffer[(i * 4) + 1]), Class0.smethod_2(buffer[(i * 4) + 2]), Class0.smethod_2(buffer[(i * 4) + 3]) });
            }
            Class0.smethod_1(builder.ToString(), out ch, out ch2);
            builder[2] = ch;
            builder[3] = ch2;
            return builder.ToString();
        }
    }
}
