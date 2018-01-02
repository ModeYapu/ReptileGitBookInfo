using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        public delegate int DoWork(int count);
        static void Main(string[] args)
        {
            ActionTask();
            Console.ReadKey();
        }
        private static void AsyncTest()
        {
            DoWork d = new DoWork(WorkPro);//no.1
            IAsyncResult r = d.BeginInvoke(1000, CallBack, d);//no.2
            //int result = d.EndInvoke(r);
            //Console.WriteLine(result);
            for (var i = 0; i < 100; i++)//no.3
            {
                Thread.Sleep(10);//主线程需要做的事情
            }
            Console.WriteLine("主线程done");
        }
        private static int WorkPro(int count)
        {
            int sum = 0;
            for (var i=0;i<count;i++)
            {
                sum +=1;
            }
            return sum;
        }
        private static void CallBack(IAsyncResult r)
        {
            DoWork d = (DoWork)r.AsyncState;
            Console.WriteLine("异步调用完成，返回结果为{0}",d.EndInvoke(r));
        }
        private static void TaskTest()
        {
            Task t = new Task((c) =>
              {
                  int count = (int)c;
                  for (var i = 0; i < count; i++)
                  {
                      Thread.Sleep(10);
                  }
                  Console.WriteLine("任务处理完成");
              },100);
            t.Start();
            for (var i=0;i<100;i++)
            {
                Thread.Sleep(10);
            }
            Console.WriteLine("done");
        }
        private static void TaskGeneric()
        {
            Task<int> t = new Task<int>((c)=> 
            {
                int count = (int)c;
                int sum = 0;
                for (var i=0;i<count;i++)
                {
                    Thread.Sleep(10);
                    sum += 1;
                }
                Console.WriteLine("任务处理完成");
                return sum;
            },100);
            t.Start();
            t.Wait();
            Console.WriteLine("任务执行结果{0}",t.Result);
            for (var i=0;i<100;i++)
            {
                Thread.Sleep(10);
            }
            Console.WriteLine("done");
        }

        private static void CancelTask()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t = new Task<int>((c)=>Sum(cts.Token,(int)c),100);
            t.Start();
            cts.Cancel();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
            }
            Console.WriteLine("done");
        }
        static int Sum(CancellationToken ct,int count)
        {
            int sum = 0;
            for (var i=0;i<count;i++)
            {
                if (!ct.CanBeCanceled)
                {
                    Thread.Sleep(10);
                    sum += 1;
                }
                else
                {
                    Console.WriteLine("任务取消");
                    return -1;
                }
            }
            Console.WriteLine("任务处理完成");
            return sum;
        }
        private static void ActionTask()
        {
            Task<int> t = new Task<int>((c) => Sum1((int)c), 100);
            t.Start();
            t.ContinueWith(task => Console.WriteLine("任务完成的结果{0}", task.Result));//当任务执行完之后执行
            t.ContinueWith(task => Console.WriteLine(""), TaskContinuationOptions.OnlyOnFaulted);//当任务出现异常时才执行
            for (int i = 0; i < 200; i++)
            {
                Thread.Sleep(10);
            }
            Console.WriteLine("done");
        }
        static int Sum1(int count)
        {
            int sum = 0;
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(10);
                sum += i;
            }
            Console.WriteLine("任务处理完成");
            return sum;
        }
    }
}
