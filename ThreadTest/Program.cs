using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class Program
    {
        [ThreadStatic]
        static int num = 0;
        static int result;
        static object locker = new object();
        static EventWaitHandle tellMe = new AutoResetEvent(false);
        static EventWaitHandle mre = new ManualResetEvent(false);
        static Semaphore sem = new Semaphore(5, 5);

        static readonly int totalThreads = 20;
        static void Main(string[] args)
        {
            CreateAsync();
            Console.ReadKey();
        }
        #region Therad线程
        private static void Test()
        {
            Thread myThreadTest = new Thread(() =>
            {
                Thread.Sleep(1000);
                Thread t = Thread.CurrentThread;
                Console.WriteLine("Name: " + t.Name);
                Console.WriteLine("ManagedThreadId: " + t.ManagedThreadId);
                Console.WriteLine("State: " + t.ThreadState);
                Console.WriteLine("Priority: " + t.Priority);
                Console.WriteLine("IsBackground: " + t.IsBackground);
                Console.WriteLine("IsThreadPoolThread: " + t.IsThreadPoolThread);
            })
            {
                Name = "线程测试",
                Priority = ThreadPriority.Highest
            };
            myThreadTest.Start();
        }
        private static void TestPassReference()
        {
            Console.WriteLine("关联进程的运行的线程数量：" + System.Diagnostics.Process.GetCurrentProcess().Threads.Count);
            new Thread(() => {
                for (int i = 0; i < 5; i++)
                    Console.WriteLine("我的线程一-[{0}]", i);
            }).Start();
        }
        private static void Priority()
        {
            int numberA = 0, numberB = 0;
            bool state = true;
            new Thread(() => { while (state) numberA++; }) { Priority = ThreadPriority.Highest, Name = "线程A" }.Start();
            new Thread(() => { while (state) numberB++; }) { Priority = ThreadPriority.Lowest, Name = "线程B" }.Start();
            //让主线程挂件1秒
            Thread.Sleep(1000);
            state = false;
            Console.WriteLine("线程A: {0}, 线程B: {1}", numberA, numberB);
        }
        private static void TheradBackground()
        {
            Thread myThread = new Thread(() => { for (int i = 0; i < 10000; i++) Console.WriteLine(i); });

            var key = Console.ReadLine();
            if (key == "1")
            {
                //应用程序不必考虑其是否全部完成，可以直接退出。应用程序退出时，自动终止后台线程
                myThread.IsBackground = true;        // 后台线程，很快就关闭了，不会等到10000个数字输完关闭
                myThread.Start();
            }
            else
            {
                //应用程序必须执行完所有的前台线程才能退出
                myThread.IsBackground = false;     //线程会等1000个数字输出完后，窗口关闭
                myThread.Start();
            }
        }
        private static void TheradStatic()
        {
            new Thread(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    ++num;
                Console.WriteLine("来自{0}:{1}", Thread.CurrentThread.Name, num);
            })
            { Name = "线程一" }.Start();
            new Thread(() =>
            {
                for (int i = 0; i < 2000000; i++)
                    ++num;
                Console.WriteLine("来自{0}:{1}", Thread.CurrentThread.Name, num);
            })
            { Name = "线程一" }.Start();
        }
        private static void RecouseSharing()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            Thread myThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                result = 100;
            });
            myThread.Start();
            Thread.Sleep(500);
            myThread.Join();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.WriteLine(result);
        }
        private static void TheradLocker()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
            {
                lock (locker)
                {
                    Thread.Sleep(1000);
                    result = 100;
                }
            });
            t1.Start();
            Thread.Sleep(100);
            lock (locker)
            {
                Console.WriteLine("线程耗时：" + watch.ElapsedMilliseconds);
                Console.WriteLine("线程输出：" + result);
            }
        }
        private static void ThreadNotification()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            Thread myThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                result = 100;
                tellMe.Set();
            });
            myThread.Start();
            tellMe.WaitOne();
            Console.WriteLine("线程耗时：" + watch.ElapsedMilliseconds);
            Console.WriteLine("线程输出：" + result);
        }
        private static void ThreadNotificationHandle()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            Thread myThreadFirst = new Thread(() =>
            {
                Thread.Sleep(1000);
                result = 100;
                mre.Set();
            })
            { Name = "线程一" };
            Thread myThreadSecond = new Thread(() =>
            {
                mre.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + "获取结果:" + result + "(" + System.DateTime.Now.ToString() + ")");
            })
            { Name = "线程二" };
            myThreadFirst.Start();
            myThreadSecond.Start();
            mre.WaitOne();
            Console.WriteLine("线程耗时：" + watch.ElapsedMilliseconds + "(" + System.DateTime.Now.ToString() + ")");
            Console.WriteLine("线程输出：" + result + "(" + System.DateTime.Now.ToString() + ")");
        }
        private static void ThreadSemaphore()
        {
            for (int i = 1; i <= 100; i++)
            {
                new Thread(() =>
                {
                    sem.WaitOne();
                    Thread.Sleep(30);
                    Console.WriteLine(Thread.CurrentThread.Name + "   " + DateTime.Now.ToString());
                    sem.Release();
                })
                { Name = "线程" + i }.Start();
            }
        }
        private static void ThreadPools()
        {
            //设置最小线程和最大线程数
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(20, 20);

            for (int i = 0; i < totalThreads; i++)
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    Thread.Sleep(1000);
                    int a, b;
                    ThreadPool.GetAvailableThreads(out a, out b);
                    Console.WriteLine(string.Format("({0}/{1}) #{2} : {3}", a, b, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString()));
                });
            }
            Console.WriteLine("主线程完成");
        }
        #endregion

        #region Tacsk线程
        /// <summary>
        /// task线程
        /// </summary>
        private static void SimpleTask()
        {
            //最简单的线程示例
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("我是使用Factory属性创建的线程");
            });
            //简单的Task实例创建线程
            Action<object> action = (object obj) =>
            {
                Console.WriteLine("Task={0}, obj={1}, Thread={2}", Task.CurrentId, obj.ToString(), Thread.CurrentThread.ManagedThreadId);
            };
            Task t1 = new Task(action, "参数");
            t1.Start();
        }
        private static void TaskMultithreading()
        {
            //简写上面实例，并创建100个线程
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            int m = 100;
            Task[] tasks = new Task[m];
            for (int i = 0; i < m; i++)
            {
                tasks[i] = new Task((object obj) =>
                {
                    Thread.Sleep(200);
                    Console.WriteLine("Task={0}, obj={1}, Thread={2},当前时间：{3}",
                    Task.CurrentId, obj.ToString(),
                    Thread.CurrentThread.ManagedThreadId,
                    System.DateTime.Now.ToString());
                }, "参数" + i.ToString()
                );
                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            Console.WriteLine("线程耗时：{0}，当前时间：{1}", watch.ElapsedMilliseconds, System.DateTime.Now.ToString());
        }
        private static void TaskIncomingParameterOne()
        {
            //Task传入一个参数
            Task myTask = new Task((parameter) => MyMethod(parameter.ToString()), "aaa");
            myTask.Start();
        }
        private static void TaskIncomingParameters()
        {
            //Task传入多个参数
            for (int i = 1; i <= 20; i++)
            {
                new Task(() => { MyMethod("我的线程", i, DateTime.Now); }).Start();
                Thread.Sleep(200);
            }
        }
        private static void TaskResult()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            Task<int> myTask = new Task<int>(() =>
            {
                int sum = 0;
                for (int i = 0; i < 10000; i++)
                    sum += i;
                return sum;
            });
            myTask.Start();
            Console.WriteLine("结果: {0} 耗时：{1}", myTask.Result, watch.ElapsedMilliseconds);
        }
        private static void TaskFactoryResult()
        {
            //使用Factory属性创建
            System.Diagnostics.Stopwatch watchSecond = System.Diagnostics.Stopwatch.StartNew();
            Task<int> myTaskSecond = Task.Factory.StartNew<int>(() =>
            {
                int sum = 0;
                for (int i = 0; i < 10000; i++)
                    sum += i;
                return sum;
            });
            Console.WriteLine("结果: {0} 耗时：{1}", myTaskSecond.Result, watchSecond.ElapsedMilliseconds);
        }
        #endregion

        #region 异步
        private static void ASampleAsync()
        {
            //使用一个有返回值的泛型委托来执行BeginInvoke
            Func<string> myFunc = new Func<string>(() => {
                Thread.Sleep(10);
                return "我是异步执行完成的返回值 当前时间：" + System.DateTime.Now.ToString();
            });
            IAsyncResult asynResult = myFunc.BeginInvoke(null, null);
            //在异步没有完成前，可以做别的事
            while (!asynResult.IsCompleted)
            {
                //当不是true时，就执行这里的代码
                Console.WriteLine("当前异步是否完成：" + asynResult.IsCompleted + " 当前时间：" + System.DateTime.Now.ToString());
            }
            string result = myFunc.EndInvoke(asynResult);//当是true时，就将结果返回显示

            Console.WriteLine(result);
        }
        private static void TimeAsync()
        {
            //使用一个有返回值的泛型委托来执行BeginInvoke
            Func<string> myFunc = new Func<string>(() => {
                int i = 0;
                while (i < 99999999)
                    ++i;
                return "异步执行完成的返回值" + (i).ToString() + " 当前时间：" + System.DateTime.Now.ToString();

            });
            IAsyncResult asynResult = myFunc.BeginInvoke(null, null);

            while (!asynResult.AsyncWaitHandle.WaitOne(10, false))
                Console.Write("*");

            string result = myFunc.EndInvoke(asynResult);
            Console.Write("\n");
            Console.WriteLine(result);
        }
        private static void BackAsync()
        {
            //使用一个有返回值的泛型委托来执行BeginInvoke
            Func<string> myFunc = new Func<string>(() => {
                int i = 0;
                while (i < 99999999)
                    ++i;
                return "异步执行完成的返回值" + (i).ToString() + " 当前时间：" + System.DateTime.Now.ToString();

            });
            IAsyncResult asynResult = myFunc.BeginInvoke((result) =>
            {
                string rst = myFunc.EndInvoke(result);
                Console.WriteLine("异步完成了，我该返回结果了！");
                Console.WriteLine(rst);
            }, null);
        }
        #endregion

        #region async/await 
       
        static void CreateAsync()
        {
            Console.WriteLine("主线程开始..");
            //AsyncMethod();
            Thread.Sleep(1000);
            Console.WriteLine("主线程结束..");
            
        }

        //static async void AsyncMethod()
        //{
        //    //Console.WriteLine("开始异步方法");
        //    var result =  MyMethod();
        //    //Console.WriteLine("异步方法结束");
        //}
        //static async Task<int> MyMethod()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.WriteLine("异步执行" + i.ToString() + "..");
        //        await Task.Delay(1000); //模拟耗时操作
        //        //Thread.Sleep(1000);
        //    }
        //    return 0;
        //}
        #endregion
        /// <summary>
        /// 一个参数的方法
        /// </summary>
        /// <param name="parameter"></param>
        static void MyMethod(string parameter)
        {
            Console.WriteLine("{0}", parameter);
        }
        /// <summary>
        /// 多个参数的方法
        /// </summary>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        /// <param name="parameter3"></param>
        static void MyMethod(string parameter1, int parameter2, DateTime parameter3)
        {
            Console.WriteLine("{0} {1} {2}", parameter1, parameter2.ToString(), parameter3.ToString());
        }
    }
}
