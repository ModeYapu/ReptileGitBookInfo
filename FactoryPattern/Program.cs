using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            IFactory factory = new UndergreateFactory();//若要换成社区志愿者，可以换这里
            LiFeng students = factory.CreateLiFeng();
            students.Sweep();
            students.Wash();
            students.ByRice();
            Console.ReadKey();
        }
        interface IFactory
        {
            LiFeng CreateLiFeng();
        }
        class LiFeng
        {
            public void Sweep()
            {
                Console.WriteLine("扫地");
            }
            public void Wash()
            {
                Console.WriteLine("洗衣服");
            }
            public void ByRice()
            {
                Console.WriteLine("买米");
            }
        }
        class Undergreate : LiFeng
        {

        }
        class Volunteer : LiFeng { }
        class UndergreateFactory:IFactory
        {
            public LiFeng CreateLiFeng()
            {
                return new Undergreate();
            }
        }
        class VounteerFactory:IFactory
        {
            public LiFeng CreateLiFeng()
            {
                return new Volunteer();
            }
        }
    }
}

