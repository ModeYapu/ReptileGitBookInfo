using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototryple
{
    class Program
    {
        static void Main(string[] args)
        {
            Reusme a = new Reusme("大鸟");
            a.SetPersonalInfo("29", "男");
            a.SetWorkException("2001--2006","xx company");
            Reusme b = (Reusme)a.Clone();
            b.SetWorkException("2001--2006","xx company");
            Reusme c = (Reusme)a.Clone();
            c.SetWorkException("2001--2006","xx company");
            a.Display();
            b.Display();
            c.Display();

            //深度复制
            DeepReusme deepA = new DeepReusme("大鸟");
            deepA.SetPersonalInfo("29", "男");
            deepA.SetWorkException("2004--2007", "xx company");
            DeepReusme deepB = (DeepReusme)deepA.Clone();
            deepB.SetWorkException("2001--2004", "NN company");
            DeepReusme deepC = (DeepReusme)deepA.Clone();
            deepC.SetWorkException("1998--2001", "JJ company");
            deepA.Display();
            deepB.Display();
            deepC.Display();
            Console.ReadKey();
        }
    }
    class Reusme : ICloneable
    {
        private string name;
        private string sex;
        private string age;
        private string timeArea;
        private string company;
        public Reusme(string name)
        {
            this.name = name;

        }
        public void SetPersonalInfo(string sex,string age)
        {
            this.sex = sex;
            this.age = age;
        }
        public void SetWorkException(string timeArea,string company)
        {
            this.timeArea = timeArea;
            this.company = company;
        }
        public void Display()
        {
            Console.WriteLine(string.Format("{0}\t{1}\t{2}",name,sex,age));
            Console.WriteLine("工作经历：{0}，时间{1}",company,timeArea);
        }
        public Object Clone()
        {
            return (Object)this.MemberwiseClone();
        }
    }
    class DeepReusme : ICloneable
    {
        private string name;
        private string sex;
        private string age;
        private WorkException work;
        public DeepReusme(string name)
        {
            this.name = name;
            work = new WorkException();
        }
        private DeepReusme(WorkException work)
        {
            this.work = (WorkException)work.Clone();
        }
        public void SetPersonalInfo(string sex, string age)
        {
            this.sex = sex;
            this.age = age;
        }
        public void SetWorkException(string workDate, string company)
        {
            work.WorkDate = workDate;
            work.Company = company;
        }
        public void Display()
        {
            Console.WriteLine(string.Format("{0}\t{1}\t{2}", name, sex, age));
            Console.WriteLine("工作经历：{0}，时间{1}", work.Company, work.WorkDate);
        }
        public Object Clone()
        {
            DeepReusme obj = new DeepReusme(this.work);
            obj.name = this.name;
            obj.sex = this.sex;
            obj.age = this.age;
            return obj;
        }
    }
    class WorkException:ICloneable
    {
        public string WorkDate { get; set; }
        public string Company { get; set; }
        public Object Clone()
        {
            return (Object)this.MemberwiseClone();
        }
    }
}
