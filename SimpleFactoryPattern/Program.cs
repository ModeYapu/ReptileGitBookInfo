using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SimpleFactoryPattern.StrategyDome;

namespace SimpleFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Operation oper;
            oper = OperationFactory.CreateOperate("+");
            oper.NumberA = 1;
            oper.NumberB = 2;
            double result = oper.GetRestult();
            Console.WriteLine(result);

            //策略模式
            Context context;
            context= new Context(new ContreteStrategyA());
            context.ContextInterface();
            Console.ReadKey();
        }
        public class Operation
        {
            private double _numberA = 0;
            private double _numberB = 0;
            public double NumberA
            {
                get { return _numberA; }
                set { _numberA=value; }
            }
            public double NumberB
            {
                get { return _numberB; }
                set { _numberB = value; }
            }
            public virtual double GetRestult()
            {
                double result = 0;
                return result;
            }
        }
        class OperationAdd : Operation
        {
            public override double GetRestult()
            {
                double result = 0;
                result = NumberA + NumberB;
                return result;
            }
        }
        class OperationSub : Operation
        {
            public override double GetRestult()
            {
                double result = 0;
                result = NumberA - NumberB;

                return result;
            }

        }
        class OperationMul : Operation
        {
            public override double GetRestult()
            {
                double result = 0;
                result = NumberA * NumberB;
                return result;
            }
        }
        class OperationDiv : Operation
        {
            public override double GetRestult()
            {
                double result = 0;
                if (NumberB != 0)
                    result = NumberA / NumberB;
                else
                    throw new Exception("除数不能为0");
                return result;
            }
        }

        public class OperationFactory
        {
            public static Operation CreateOperate(string operate)
            {
                Operation oper = null;
                switch (operate)
                {
                    case "+":
                        oper = new OperationAdd();
                        break;
                    case "-":
                        oper = new OperationSub();
                        break;
                    case "*":
                        oper = new OperationMul();
                        break;
                    case "/":
                        oper = new OperationDiv();
                        break;
                }
                return oper;
            }
        }
    }
}
