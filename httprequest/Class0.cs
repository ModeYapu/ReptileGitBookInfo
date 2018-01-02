using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace httprequest
{
    internal class Class0
    {
        // Fields
        private static readonly char[] char_0 = new char[] {
        '2', '3', '4', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J',
        'K', 'M', 'N', 'P', 'R', 'T', 'W', 'X', 'Y', 'Z'
    };
        private readonly string string_0;

        // Methods
        internal Class0(string string_1)
        {
            this.string_0 = string_1;
        }

        private bool method_0(char char_1, char char_2) =>
            ((char_1 == this.string_0[2]) && (char_2 == this.string_0[3]));

        private static void smethod_0(StringBuilder stringBuilder_0)
        {
            stringBuilder_0[2] = '0';
            stringBuilder_0[3] = '0';
        }

        public static void smethod_1(string string_1, out char char_1, out char char_2)
        {
            uint num = Class2.smethod_0(string_1);
            char_1 = smethod_2((byte)(((long)num) % ((long)char_0.Length)));
            char_2 = smethod_2((byte)((((long)num) / ((long)char_0.Length)) % ((long)char_0.Length)));
        }

        public static char smethod_2(byte byte_0) =>
            char_0[byte_0 % char_0.Length];

        public override string ToString() =>
            this.string_0;

        // Properties
        public bool Boolean_0
        {
            get
            {
                if (string.IsNullOrEmpty(this.string_0))
                {
                    return false;
                }
                string str = $"[{new string(char_0)}]";
                return new Regex("^A3" + str + "{2}(-(" + str + "{4})){5}$").IsMatch(this.string_0);
            }
        }

        public bool Boolean_1
        {
            get
            {
                char ch;
                char ch2;
                StringBuilder builder = new StringBuilder(this.string_0);
                smethod_0(builder);
                smethod_1(builder.ToString(), out ch, out ch2);
                return this.method_0(ch, ch2);
            }
        }

        public int Int32_0 =>
            3;
    }
    
}
