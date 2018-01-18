using System;

namespace ProxyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SchoolGril jiaojiao = new SchoolGril();
            jiaojiao.Name = "娇娇";
            Proxy daili = new Proxy(jiaojiao);
            daili.GiveDolls();
            daili.GiveFlowers();
            daili.GiveChololate();

            Console.ReadKey();
        }
        interface GiveGift
        {
            void GiveDolls();
            void GiveFlowers();
            void GiveChololate();
        }
        class Pursuit : GiveGift
        {
            SchoolGril mm;
            public Pursuit(SchoolGril mm)
            {
                this.mm = mm;
            }
            public void GiveDolls()
            {
                Console.WriteLine(mm.Name +"送你洋娃娃");
            }
            public void GiveFlowers()
            {
                Console.WriteLine(mm.Name+"送你花");
            }
            public void GiveChololate()
            {
                Console.WriteLine(mm.Name+"送你巧克力");
            }
        }
        class Proxy : GiveGift
        {
            Pursuit gg;
            public Proxy(SchoolGril mm)
            {
                gg = new Pursuit(mm);
            }
            public void GiveDolls()
            {
                gg.GiveDolls();
            }
            public void GiveFlowers()
            {
                gg.GiveFlowers();
            }
            public void GiveChololate()
            {
                gg.GiveChololate();
            }
        }
        class SchoolGril
        {
            public string Name{ get; set; }
        }
    }
}
