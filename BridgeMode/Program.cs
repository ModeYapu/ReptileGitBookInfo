using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BridgeMode
{
    class Program
    {
        static void Main(string[] args)
        {
            HandsetBrand ab;
            ab = new HandsetBrandN();

            ab.SetHandsetSoft(new HandsetGame());
            ab.Run();

            ab.SetHandsetSoft(new HandsetAddressList());
            ab.Run();

            ab = new HandsetBrandY();

            ab.SetHandsetSoft(new HandsetGame());
            ab.Run();

            ab.SetHandsetSoft(new HandsetAddressList());
            ab.Run();

            Console.Read();
        }
    }
    //手机软件
    abstract class HandsetSoft
    {
        public abstract void Run();
    }
    //手机游戏
    class HandsetGame : HandsetSoft
    {
        public override void Run()
        {
            Console.WriteLine("运行手机游戏");
        }
    }
    //手机通讯录
    class HandsetAddressList : HandsetSoft
    {
        public override void Run()
        {
            Console.WriteLine("运行通讯录程序");
        }
    }
    abstract class HandsetBrand
    {
        protected HandsetSoft soft;
        public void SetHandsetSoft(HandsetSoft soft)
        {
            this.soft = soft;
        }
        public abstract void Run();
    }

    class HandsetBrandN : HandsetBrand
    {
        public override void Run()
        {
            soft.Run();
        }
    }
    class HandsetBrandY : HandsetBrand
    {
        public override void Run()
        {
            soft.Run();
        }
    }
}
