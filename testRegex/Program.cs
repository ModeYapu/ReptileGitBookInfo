using httprequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace testRegex
{
    class Program
    {
        static void Main(string[] args)
        {
            //textLength();
            //testEnum();
            TestRegexAdvanced();
            BackwardReference();
            ZerowidthAssertion();
            NegativeZerowidthAssertion();
            NegativeZerowidthAssertion2();
            RegexComments();
            GreedyAndLazy();
            ProcessingOptions();
            AllMode();
            Console.ReadLine();
        }
        private static void TestRegexAdvanced()
        {
            Console.WriteLine("请输入一个任意字符串，测试分组：");
            string inputStr = Console.ReadLine();
            string strGroup1 = @"a{2}";
            Console.WriteLine("单字符重复2两次替换为22，结果为：" + Regex.Replace(inputStr, strGroup1, "22"));
            //重复 多个字符 使用（abcd）{n}进行分组限定
            string strGroup2 = @"(ab\w{2}){2}";
            Console.WriteLine("分组字符重复2两次替换为5555，结果为：" + Regex.Replace(inputStr, strGroup2, "5555"));
        }
        private static void CheckIPv4()
        {
            //示例：校验IP4地址（如：192.168.1.4，为四段，每段最多三位，每段最大数字为255，并且第一位不能为0）
            string regexStrIp4 = @"^(((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?))$";
            Console.WriteLine("请输入一个IP4地址：");
            string inputStrIp4 = Console.ReadLine();
            Console.WriteLine(inputStrIp4 + " 是否为合法的IP4地址：" + Regex.IsMatch(inputStrIp4, regexStrIp4));
            Console.WriteLine("请输入一个IP4地址：");
            string inputStrIp4Second = Console.ReadLine();
            Console.WriteLine(inputStrIp4 + " 是否为合法的IP4地址：" + Regex.IsMatch(inputStrIp4Second, regexStrIp4));
        }
        //后向引用
        //在后向引用前，我们先熟悉一下单词的查找  
        //复习一下前面学的几个元字符
        // * 匹配前面的子表达式任意次。例如，zo* 能匹配“z”，“zo”以及“zoo”。*等价于{ 0,}。
        // + 匹配前面的子表达式一次或多次(大于等于1次）。例如，“zo +”能匹配“zo”以及“zoo”，但不能匹配“z”。+等价于{ 1,}。
        // ? 匹配前面的子表达式零次或一次。例如，“do (es) ?”可以匹配“do”或“does”中的“do”。?等价于{ 0,1}。  
        // \w 匹配包括下划线的任何单词字符。类似但不等价于“[A-Za-z0-9_]”，这里的"单词"字符使用Unicode字符集。
        // \b 匹配一个单词边界，也就是指单词和空格间的位置（即正则表达式的“匹配”有两种概念，一种是匹配字符，一种是匹配位置，这里的\b就是匹配位置的）。例如，“er\b”可以匹配“never”中的“er”，但不能匹配“verb”中的“er”。       
        // \s 匹配任何不可见字符，包括空格、制表符、换页符等等。等价于[ \f\n\r\t\v]。
        private static void BackwardReference()
        {
           
            string str1 = "zoo zoo hello how you mess ok ok home house miss miss yellow";

            //示例一:查找单词中以e结尾的，并将单词替换为了*
            string regexStr1 = @"\w+e\b";
            Console.WriteLine("示例一：" + Regex.Replace(str1, regexStr1, "*"));

            //示例二：查询单词中以h开头的，并将单词替换为#
            string regexStr2 = @"\bh\w+";
            Console.WriteLine("示例二：" + Regex.Replace(str1, regexStr2, "#"));

            //示例三：查找单词中有重叠ll的单词，将单词替换为@
            string regexStr3 = @"\b\w+l+\w+\b";
            Console.WriteLine("示例三：" + Regex.Replace(str1, regexStr3, "@"));

            //使用 \1 代表第一个分组 组号为1的匹配文本
            //示例四：查询单词中有两个重复的单词，替换为%  
            string regexStr4 = @"(\b\w+\b)\s+\1";
            Console.WriteLine("示例四：" + Regex.Replace(str1, regexStr4, "%"));
            //上面示例中，第一个分组匹配任意一个单词 \s+ 表示任意空字符 \1 表示匹配和第一个分组相同

            //使用别名 代替 组号 ,别名定义 （?<name>exp）  ，别名后向引用 \k<name>
            //示例五：使用别名后向引用 查询 查询单词中有两个重复的单词，替换为%%  
            string regexStr5 = @"(?<myRegWord>\b\w+\b)\s+\k<myRegWord>";
            Console.WriteLine("示例五：" + Regex.Replace(str1, regexStr5, "%%"));
        }
        private static void ZerowidthAssertion()
        {
            //零宽断言
            //示例一：将下面字符串每三位使用逗号分开
            string stringFist = "dfalrewqrqwerl43242342342243434abccc";
            string regexStringFist = @"((?=\w)\w{3})";
            string newStrFist = String.Empty;
            Regex.Matches(stringFist, regexStringFist).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrFist += (m + ","));
            Console.WriteLine("示例一：" + newStrFist.TrimEnd(','));

            //示例二:查询字符串中，两个空格之间的所有数字
            string FindNumberSecond = "asdfas 3 dfasfas3434324 9 8888888 7 dsafasd342";
            string regexFindNumberSecond = @"((?<=\s)\d(?=\s))";
            string newFindNumberSecond = String.Empty;
            Regex.Matches(FindNumberSecond, regexFindNumberSecond).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newFindNumberSecond += (m + " "));
            Console.WriteLine("示例二：" + newFindNumberSecond);
        }
        private static void NegativeZerowidthAssertion()
        {
            //负向零宽断言
            //示例：查找单词中包含x并且x前面不是o的所有单词
            string strThird = "hot example how box house ox xerox his fox six my";
            string regexStrThird = @"\b\w*[^o]x\w*\b";
            string newStrThird = String.Empty;
            Regex.Matches(strThird, regexStrThird).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird += (m + " "));
            Console.WriteLine("示例一：" + newStrThird);
            //我们发现以上写法，如果以x开头的单词，就会出错，原因是[^o]必须要匹配一个非o的字符
            //为了解决以上问题，我们需要使用负向零宽断言 (?<!o])负向零宽断言能解决这样的问题，因为它只匹配一个位置，并不消费任何字符
            //改进以后如下
            string regexStrThird2 = @"\b\w*(?<!o)x\w*\b";
            string newStrThird2 = String.Empty;
            Regex.Matches(strThird, regexStrThird2).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird2 += (m + " "));
            Console.WriteLine("示例二：" + newStrThird2);

            //示例三：如查询上面示例，但是要求区配必须含o但 后面不能有x 则要使用 (?!x)
            string regexStrThird3 = @"\b\w*o(?!x)\w*\b";
            string newStrThird3 = String.Empty;
            Regex.Matches(strThird, regexStrThird3).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird3 += (m + " "));
            Console.WriteLine("示例三：" + newStrThird3);
        }
        private static void NegativeZerowidthAssertion2()
        {
            //实例一:取到<div name='Fist'></div>以内的内容   使用零宽断言 正负断言就可以解决了
            //以下为模拟一个HTML表格数据
            StringBuilder strHeroes = new StringBuilder();
            strHeroes.Append("<table>");
            strHeroes.Append("<tr><td>武林高手</td></tr>");
            strHeroes.Append("<tr><td>");
            strHeroes.Append("<div name='Fist'>");
            strHeroes.Append("<div>欧阳锋</div>");
            strHeroes.Append("<div>白驼山</div>");
            strHeroes.Append("<div>蛤蟆功</div>");
            strHeroes.Append("</div>");

            strHeroes.Append("<div>");
            strHeroes.Append("<div>黄药师</div>");
            strHeroes.Append("<div>桃花岛</div>");
            strHeroes.Append("<div>弹指神通</div>");
            strHeroes.Append("</div>");

            strHeroes.Append("</td></tr>");
            strHeroes.Append("</table>");

            Console.WriteLine("原字符串：" + strHeroes.ToString());

            string newTr = String.Empty;
            string regexTr = @"(?<=<div name='Fist'>).+(?=</div>)";
            Regex.Matches(strHeroes.ToString(), regexTr).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newTr += (m + "\n"));
            Console.WriteLine(Regex.IsMatch(strHeroes.ToString(), regexTr));
            Console.WriteLine("实例一:" + newTr);
        }
        private static void RegexComments()
        {
            //正则表达式注释
            //重写上面的例子，采用多行注释法
            string strThird = "hot example how box house ox xerox his fox six my";
            string regexStrThird4 = @"\b         #限定单词开头
                          \w*        #任意长度字母数字及下划线
                          o(?!x)     #含o字母并且后面的一个字母不是x
                          \w*        #任意长度字母数字及下划线
                          \b         #限定单词结尾
                          ";
            string newStrThird4 = String.Empty;
            Regex.Matches(strThird, regexStrThird4, RegexOptions.IgnorePatternWhitespace).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird4 += (m + " "));
            Console.WriteLine("示例四：" + newStrThird4);
        }
        private static void GreedyAndLazy()
        {
            //懒惰限定符
            string LazyStr = "xxyxy";
            string regexLazyStr = @"x.*y";
            string regexLazyStr2 = @"x.*?y";
            string newLazyStr = String.Empty;
            Regex.Matches(LazyStr, regexLazyStr).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newLazyStr += (m + " "));
            string newLazyStr2 = String.Empty;
            Regex.Matches(LazyStr, regexLazyStr2).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newLazyStr2 += (m + " "));
            Console.WriteLine("贪婪模式：" + newLazyStr);
            Console.WriteLine("懒惰模式：" + newLazyStr2);
        }
        private static void ProcessingOptions()
        {
            //处理选项
            //Compiled表示编译为程序集，使效率提高   
            //IgnoreCase表示匹配不分大小写
            Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string myWords = "hello hello How Hi hi are you you apple o o ";
            string newMyWords = String.Empty;
            rx.Matches(myWords).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newMyWords += (m + ","));
            Console.WriteLine("相同的单词有：" + newMyWords.Trim(','));
        }
        private static void AllMode()
        {
            //示例：重复单字符 和 重复分组字符
            //重复 单个字符
            Console.WriteLine("请输入一个任意字符串，测试分组：");
            string inputStr = Console.ReadLine();
            string strGroup1 = @"a{2}";
            Console.WriteLine("单字符重复2两次替换为22，结果为：" + Regex.Replace(inputStr, strGroup1, "22"));
            //重复 多个字符 使用（abcd）{n}进行分组限定
            string strGroup2 = @"(ab\w{2}){2}";
            Console.WriteLine("分组字符重复2两次替换为5555，结果为：" + Regex.Replace(inputStr, strGroup2, "5555"));

            Console.WriteLine("\n");

            //示例：校验IP4地址（如：192.168.1.4，为四段，每段最多三位，每段最大数字为255，并且第一位不能为0）
            string regexStrIp4 = @"^(((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?))$";
            Console.WriteLine("请输入一个IP4地址：");
            string inputStrIp4 = Console.ReadLine();
            Console.WriteLine(inputStrIp4 + " 是否为合法的IP4地址：" + Regex.IsMatch(inputStrIp4, regexStrIp4));
            Console.WriteLine("请输入一个IP4地址：");
            string inputStrIp4Second = Console.ReadLine();
            Console.WriteLine(inputStrIp4 + " 是否为合法的IP4地址：" + Regex.IsMatch(inputStrIp4Second, regexStrIp4));

            //后向引用

            //在后向引用前，我们先熟悉一下单词的查找  
            //复习一下前面学的几个元字符
            // * 匹配前面的子表达式任意次。例如，zo* 能匹配“z”，“zo”以及“zoo”。*等价于{ 0,}。
            // + 匹配前面的子表达式一次或多次(大于等于1次）。例如，“zo +”能匹配“zo”以及“zoo”，但不能匹配“z”。+等价于{ 1,}。
            // ? 匹配前面的子表达式零次或一次。例如，“do (es) ?”可以匹配“do”或“does”中的“do”。?等价于{ 0,1}。  
            // \w 匹配包括下划线的任何单词字符。类似但不等价于“[A-Za-z0-9_]”，这里的"单词"字符使用Unicode字符集。
            // \b 匹配一个单词边界，也就是指单词和空格间的位置（即正则表达式的“匹配”有两种概念，一种是匹配字符，一种是匹配位置，这里的\b就是匹配位置的）。例如，“er\b”可以匹配“never”中的“er”，但不能匹配“verb”中的“er”。       
            // \s 匹配任何不可见字符，包括空格、制表符、换页符等等。等价于[ \f\n\r\t\v]。
            string str1 = "zoo zoo hello how you mess ok ok home house miss miss yellow";

            //示例一:查找单词中以e结尾的，并将单词替换为了*
            string regexStr1 = @"\w+e\b";
            Console.WriteLine("示例一：" + Regex.Replace(str1, regexStr1, "*"));

            //示例二：查询单词中以h开头的，并将单词替换为#
            string regexStr2 = @"\bh\w+";
            Console.WriteLine("示例二：" + Regex.Replace(str1, regexStr2, "#"));

            //示例三：查找单词中有重叠ll的单词，将单词替换为@
            string regexStr3 = @"\b\w+l+\w+\b";
            Console.WriteLine("示例三：" + Regex.Replace(str1, regexStr3, "@"));

            //使用 \1 代表第一个分组 组号为1的匹配文本
            //示例四：查询单词中有两个重复的单词，替换为%  
            string regexStr4 = @"(\b\w+\b)\s+\1";
            Console.WriteLine("示例四：" + Regex.Replace(str1, regexStr4, "%"));
            //上面示例中，第一个分组匹配任意一个单词 \s+ 表示任意空字符 \1 表示匹配和第一个分组相同

            //使用别名 代替 组号 ,别名定义 （?<name>exp）  ，别名后向引用 \w<name>
            //示例五：使用别名后向引用 查询 查询单词中有两个重复的单词，替换为%%  
            string regexStr5 = @"(?<myRegWord>\b\w+\b)\s+\k<myRegWord>";
            Console.WriteLine("示例五：" + Regex.Replace(str1, regexStr5, "%%"));

            //零宽断言
            //示例一：将下面字符串每三位使用逗号分开
            string stringFist = "dfalrewqrqwerl43242342342243434abccc";
            string regexStringFist = @"((?=\w)\w{3})";
            string newStrFist = String.Empty;
            Regex.Matches(stringFist, regexStringFist).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrFist += (m + ","));
            Console.WriteLine("示例一：" + newStrFist.TrimEnd(','));

            //示例二:查询字符串中，两个空格之间的所有数字
            string FindNumberSecond = "asdfas 3 dfasfas3434324 9 8888888 7 dsafasd342";
            string regexFindNumberSecond = @"((?<=\s)\d(?=\s))";
            string newFindNumberSecond = String.Empty;
            Regex.Matches(FindNumberSecond, regexFindNumberSecond).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newFindNumberSecond += (m + " "));
            Console.WriteLine("示例二：" + newFindNumberSecond);

            //负向零宽断言
            //示例：查找单词中包含x并且x前面不是o的所有单词
            string strThird = "hot example how box house ox xerox his fox six my";
            string regexStrThird = @"\b\w*[^o]x\w*\b";
            string newStrThird = String.Empty;
            Regex.Matches(strThird, regexStrThird).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird += (m + " "));
            Console.WriteLine("示例一：" + newStrThird);
            //我们发现以上写法，如果以x开头的单词，就会出错，原因是[^o]必须要匹配一个非o的字符
            //为了解决以上问题，我们需要使用负向零宽断言 (?<!o])负向零宽断言能解决这样的问题，因为它只匹配一个位置，并不消费任何字符
            //改进以后如下
            string regexStrThird2 = @"\b\w*(?<!o)x\w*\b";
            string newStrThird2 = String.Empty;
            Regex.Matches(strThird, regexStrThird2).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird2 += (m + " "));
            Console.WriteLine("示例二：" + newStrThird2);

            //示例三：如查询上面示例，但是要求区配必须含o但 后面不能有x 则要使用 (?!x)
            string regexStrThird3 = @"\b\w*o(?!x)\w*\b";
            string newStrThird3 = String.Empty;
            Regex.Matches(strThird, regexStrThird3).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird3 += (m + " "));
            Console.WriteLine("示例三：" + newStrThird3);

            //正则表达式注释
            //重写上面的例子，采用多行注释法
            string regexStrThird4 = @"  \b         #限定单词开头
                            \w*        #任意长度字母数字及下划线
                            o(?!x)     #含o字母并且后面的一个字母不是x
                            \w*        #任意长度字母数字及下划线
                            \b         #限定单词结尾
                            ";
            string newStrThird4 = String.Empty;
            Regex.Matches(strThird, regexStrThird4, RegexOptions.IgnorePatternWhitespace).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newStrThird4 += (m + " "));
            Console.WriteLine("示例四：" + newStrThird4);

            //懒惰限定符
            string LazyStr = "xxyxy";
            string regexLazyStr = @"x.*y";
            string regexLazyStr2 = @"x.*?y";
            string newLazyStr = String.Empty;
            Regex.Matches(LazyStr, regexLazyStr).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newLazyStr += (m + " "));
            string newLazyStr2 = String.Empty;
            Regex.Matches(LazyStr, regexLazyStr2).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newLazyStr2 += (m + " "));
            Console.WriteLine("贪婪模式：" + newLazyStr);
            Console.WriteLine("懒惰模式：" + newLazyStr2);

            //处理选项
            //Compiled表示编译为程序集，使效率提高   
            //IgnoreCase表示匹配不分大小写
            Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string myWords = "hello hello How Hi hi are you you apple o o ";
            string newMyWords = String.Empty;
            rx.Matches(myWords).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newMyWords += (m + ","));
            Console.WriteLine("相同的单词有：" + newMyWords.Trim(','));

            //实例一:取到<div name='Fist'></div>以内的内容   使用零宽断言 正负断言就可以解决了
            //以下为模拟一个HTML表格数据
            StringBuilder strHeroes = new StringBuilder();
            strHeroes.Append("<table>");
            strHeroes.Append("<tr><td>武林高手</td></tr>");
            strHeroes.Append("<tr><td>");
            strHeroes.Append("<div name='Fist'>");
            strHeroes.Append("<div>欧阳锋</div>");
            strHeroes.Append("<div>白驼山</div>");
            strHeroes.Append("<div>蛤蟆功</div>");
            strHeroes.Append("</div>");

            strHeroes.Append("<div>");
            strHeroes.Append("<div>黄药师</div>");
            strHeroes.Append("<div>桃花岛</div>");
            strHeroes.Append("<div>弹指神通</div>");
            strHeroes.Append("</div>");

            strHeroes.Append("</td></tr>");
            strHeroes.Append("</table>");

            Console.WriteLine("原字符串：" + strHeroes.ToString());

            string newTr = String.Empty;
            string regexTr = @"(?<=<div name='Fist'>).+(?=</div>)";
            Regex.Matches(strHeroes.ToString(), regexTr).Cast<Match>().Select(m => m.Value).ToList<string>().ForEach(m => newTr += (m + "\n"));
            Console.WriteLine(Regex.IsMatch(strHeroes.ToString(), regexTr));
            Console.WriteLine("实例一:" + newTr);
        }

        private static void Test1()
        {
            var password = "1234";
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            Console.WriteLine(password);
            string st1 = "08:00";
            string st2 = "17:00";
            DateTime dt1 = Convert.ToDateTime(st1);
            DateTime dt2 = Convert.ToDateTime(st2);
            DateTime dt3 = DateTime.Now;
            var ssss = "";
            if (DateTime.Compare(dt1, dt3) < 0 && DateTime.Compare(dt2, dt3) > 0)
            {
                Console.WriteLine("yiyi");
            }
            else
                ssss = st1 + "<" + st2;
            ssss += "\r\n" + dt1.ToString();
            if (DateTime.Compare(dt1, dt3) > 0)
                ssss += "\r\n" + st1 + ">" + dt3.ToString();
            else
                ssss += "\r\n" + st1 + "<" + dt3.ToString();

            int a = 5;

            int b = 2;

            var e = Convert.ToString(Math.Ceiling((double)a / (double)b));

            //tests();
            //hashSetT();
            testRegex2();


            // testRegex();
            Console.ReadKey();
        }
        private static void testRegex2()
        {
            string str = "aaa34343a{cc}";
            if (str.Contains("{"))
            {
                Console.WriteLine("sos");
            }
            Regex reg = new Regex("[{}]");
            var m = reg.IsMatch(str);
            var me = Regex.IsMatch(str, @"[{}]");
            Console.WriteLine(m);
        }
        private static void tests()
        {
            //DateTime dt = new DateTime();
            //var dd = DateTime.Now.ToString("T");
            //Console.WriteLine("当前时间{0}", dd);
            //if (DateTime.Now.ToString("T") == "13:00:00") { }
            //System.Timers.ElapsedEventArgs ed = null;
            //int intHour = ed.SignalTime.Hour;
            //int intMinute = ed.SignalTime.Minute;
            //int intSecond = ed.SignalTime.Second;
            //if (intHour == 13 && intMinute == 30 && intSecond == 00)
            //{
            //    //定时发送
            //}
            // var timeStamp= ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var timeStamp = "21432113215";
            var nowTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (nowTimestamp - long.Parse(timeStamp) > 300) //超过
            {
                var errorStr = "授权签名已过期";
                Console.WriteLine(errorStr);
            }
            var timess = "11432113215";
            var iissi = 1512966537 - 11432113215;
            var ee = long.Parse(timess);
            Console.WriteLine(iissi);
            if (nowTimestamp - long.Parse(timess) > 300)
            {
                Console.WriteLine("授权签名过期");
            }
            var appSecert = "tfd43tfdddddd43t43543453243543546ffdgrg";
            var sourceSigns = string.Format("{0}{1}", appSecert, timeStamp);
            var sign = ShaUtil.SHA256HashStringForUTF8String(sourceSigns);


            //var sign = "sadadsas";
            var sourceSignStr = string.Format("{0}{1}", appSecert, timeStamp);
            var sourceSign = ShaUtil.SHA256HashStringForUTF8String(sourceSignStr);
            if (!sourceSign.Equals(sign))
            {
                var errorStr = "授权签名不正确";
                Console.WriteLine(errorStr);
            }
            else
            {
                Console.WriteLine("授权签名正确");
            }


        }
        private static void hashSetT()
        {
            Console.WriteLine("Using HashSet");
            //1. Defining String Array (Note that the string "mahesh" is repeated) 
            string[] names = new string[] {
                "mahesh",
                "vikram",
                "mahesh",
                "mayur",
                "suprotim",
                "saket",
                "manish"
            };
            //2. Length of Array and Printing array
            Console.WriteLine("Length of Array " + names.Length);
            Console.WriteLine();
            Console.WriteLine("The Data in Array");
            foreach (var n in names)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine();
            //3. Defining HashSet by passing an Array of string to it
            HashSet<string> hSet = new HashSet<string>(names);
            //4. Count of Elements in HashSet
            Console.WriteLine("Count of Data in HashSet " + hSet.Count);
            Console.WriteLine();
            //5. Printing Data in HashSet, this will eliminate duplication of "mahesh" 
            Console.WriteLine("Data in HashSet");
            foreach (var n in hSet)
            {
                Console.WriteLine(n);
            }
        }
        private static void textLength()
        {
            var text = "http://192.168.11.157:9980/wav/201711281603331185.pcm";
            var i = text.Length;
            Console.WriteLine(DateTime.Now.ToString("yyMMddHHmmssffff"));
            Console.WriteLine("{0}的长度为{1}",text,i);
        }
        private static void testXml()
        {
            var messageInfo = new MessageInfo();
            messageInfo.ToId = "17317832559";
            messageInfo.MsgContent = "执行";
            var in0 = string.Format("{0}\t{1}\t{2}\t{3}", messageInfo.ToId, messageInfo.MsgContent, "310104010120151659", "3101040101");
            Console.WriteLine(in0);

            var sendReulst = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><SMSPACKINFORSP><PACKNAME>310000000020151126113434</PACKNAME><RESULT>0</RESULT><RESULTMSG>成功</RESULTMSG></SMSPACKINFORSP>";
            System.Xml.XmlDocument docu = new System.Xml.XmlDocument();
            docu.LoadXml(sendReulst);
            var sendReulstMsg = docu.SelectSingleNode("SMSPACKINFORSP/RESULTMSG").InnerText;
            Console.WriteLine(sendReulstMsg);
        }
        private static void testEnum()
        {
            var messageType = "lYnC";
            MessageType falig;
            if (!Enum.TryParse<MessageType>(messageType, true, out falig))
            {
                Console.WriteLine("TryParse函数[{0}]:类型不在在enum中", messageType);
            }
            if (!Enum.IsDefined(typeof(MessageType), messageType.ToUpper()))
            {
                Console.WriteLine("IsDefined()函数[{0}]:类型不在在enum中", messageType);                
            }
        }
        private static void testRegex()
        {
            var sendMsgResult = "fas成功dfghj";
            var ffalos = Regex.IsMatch(sendMsgResult,@"成功");
            if (ffalos)
            {
                Console.WriteLine("发送成功:{0}",sendMsgResult);
            }

            var outerId = "2017-11-17_22_9,18717986819_16406";
            var fal = Regex.IsMatch(outerId, @".*?(\d+)_(\d+)_(w*?)");
            Regex reg = new Regex(@".*?(\d+)_(\d+)_(w*?)");
            var outerIDD = reg.Match(outerId).Groups[2].Value;


            var email = "17317832559";
            var errre = Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!errre)
            {
                Console.WriteLine("email不正确：{0}", email);
            }
            var ooo = ",3";
            //Regex rege = new Regex(@"^[0-9]{0,15}$");
            var falog = Regex.IsMatch(ooo, @"^[0-9]{1,15}$");
            if (!falog)
            {
                Console.WriteLine("字符不正确：{0}", ooo);
            }
            if (falog)
            {
                Console.WriteLine("字符串正确：{0}", ooo);
            }
            Regex reges = new Regex(@"^(?<k1>[0-9])\k<k1>{5}$");
            //var fasaA = Regex.IsMatch(ooo, @"^(?<k1>[0-9])\k<k>1{5}$");          
            var messageInfo = "1324576980asdf9764534314587998765432143535423";
            var falg = Regex.IsMatch(messageInfo, @"^\d+$");
            if (!falg)
            {
                //Console.WriteLine("字符不为数字{0}",messageInfo);
                Console.ReadKey();
            }
        }
    }
}
