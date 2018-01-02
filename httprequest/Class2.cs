using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace httprequest
{
    internal class Class2
    {
        // Fields
        private static Regex regex_0 = new Regex("^[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}$", RegexOptions.Compiled);

        // Methods
        public static uint smethod_0(string string_0)
        {
            long num = 0L;
            for (int i = 0; i < string_0.Length; i++)
            {
                int num3 = string_0[i];
                for (int j = 7; j >= 0; j--)
                {
                    bool flag = ((num & 0x8000L) == 0x8000L) ^ ((num3 & (((int)1) << j)) != 0);
                    num = (num & 0x7fffL) << 1;
                    if (flag)
                    {
                        num ^= 0x1021L;
                    }
                }
            }
            return (uint)num;
        }

        public static string smethod_1(int int_0, int int_1, int int_2)
        {
            string str = $"{int_0:X4}-{int_1:X4}-{int_2:X4}";
            return $"{str}-{smethod_0(str):X4}";
        }

        public static string smethod_2(string string_0, string string_1, string string_2)
        {
            string str = $"{smethod_0(string_0):X4}-{smethod_0(string_1):X4}-{smethod_0(string_2):X4}";
            return $"{str}-{smethod_0(str):X4}";
        }

        public static bool smethod_3(string string_0, string string_1)
        {
            if (!smethod_4(string_0) || !smethod_4(string_1))
            {
                return false;
            }
            if (((string_0.Substring(0, 4) != string_1.Substring(0, 4)) && (string_0.Substring(5, 4) != string_1.Substring(5, 4))) && (string_0.Substring(10, 4) != string_1.Substring(10, 4)))
            {
                return false;
            }
            return true;
        }

        public static bool smethod_4(string string_0)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                return false;
            }
            string_0 = string_0.ToUpperInvariant().Trim();
            if (!regex_0.IsMatch(string_0))
            {
                return false;
            }
            string str = string_0.Substring(0, 14);
            string str2 = $"{smethod_0(str):X4}";
            if (!string_0.EndsWith(str2))
            {
                return false;
            }
            return true;
        }
    }


}
