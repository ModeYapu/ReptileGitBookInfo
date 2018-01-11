using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleFactoryPattern
{
    class StrategyDome
    {
        public abstract class Strategy
        {
            public abstract void AlgorithmInterface();
        }
        public class ContreteStrategyA : Strategy
        {
            public override void AlgorithmInterface()
            {
                Console.WriteLine("算法A实现");
            }
        }
        public class ContreteStrategyB : Strategy
        {
            public override void AlgorithmInterface()
            {
                Console.WriteLine("算法B实现");
            }
        }
        public class ContreteStrategyC : Strategy
        {
            public override void AlgorithmInterface()
            {
                Console.WriteLine("算法C实现");
            }
        }
        public  class Context
        {
            Strategy strategy;
            public Context(Strategy strategy)
            {
                this.strategy = strategy;
            }
            public void ContextInterface()
            {
                strategy.AlgorithmInterface();
            }
        }
    }
}
