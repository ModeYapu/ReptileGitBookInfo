namespace HAPExplorer
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Extensions
    {
        public static bool IsEmpty(this string str) => 
            string.IsNullOrEmpty(str.Trim());
    }
}

