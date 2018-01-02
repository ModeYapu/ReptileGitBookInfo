using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TaskTest
{
    public partial class Form1 : Form
    {
        
        private readonly TaskScheduler contextTaskScheduler;//声明一个任务调度器

     
        delegate void AsynUpdateUI(int step);
        public Form1()
        {
            InitializeComponent();
            contextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();//no.1获得一个上下文任务调度器
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task<int> t = new Task<int>((n) => Sum((int)n), 100);
            t.Start();
            t.ContinueWith(task => this.textBox1.Text = task.Result.ToString(), contextTaskScheduler);//当任务执行完之后执行
            t.ContinueWith(task => MessageBox.Show("任务出现异常"), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, contextTaskScheduler);//当任务出现异常时才执行
        }
        int Sum(int count)
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

      
        public class DataWrite
        {
            public delegate void UpdateUI(int step);//声明一个更新主线程的委托
            public UpdateUI UpdateUIDelegate;

            public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
            public AccomplishTask TaskCallBack;

            public void Write(object lineCount)
            {
                StreamWriter writeIO = new StreamWriter("text.txt", false, Encoding.GetEncoding("gb2312"));
                string head = "编号,省,市";
                writeIO.Write(head);
                for (int i = 0; i < (int)lineCount; i++)
                {
                    writeIO.WriteLine(i.ToString() + ",湖南,衡阳");
                    //写入一条数据，调用更新主线程ui状态的委托
                    UpdateUIDelegate(1);
                }
                //任务完成时通知主线程作出相应的处理
                TaskCallBack();
                writeIO.Close();
            }
        }
        //更新UI
       

        //完成任务时需要调用
        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            MessageBox.Show("任务完成");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
