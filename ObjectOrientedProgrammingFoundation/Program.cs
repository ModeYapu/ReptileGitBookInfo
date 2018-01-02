using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectOrientedProgrammingFoundation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[场景说明]: 一个月明星稀的午夜,有两只老鼠在偷油吃\n\n\n");
            Mouse Jerry = new Mouse("Jerry", "白色");
            Mouse Jack = new Mouse("Jack", "黄色");


            Console.WriteLine("[场景说明]: 一只黑猫蹑手蹑脚的走了过来");
            Cat Tom = new Cat("Tom", "黑色");
            Console.WriteLine("[场景说明]: 为了安全的偷油，登记了一个猫叫的事件");
            Tom.CatShoutEvent += new Cat.CatShoutEventHandler(Jerry.MouseRun);
            Tom.CatShoutEvent += new Cat.CatShoutEventHandler(Jack.MouseRun);
            Console.WriteLine("[场景说明]: 猫叫了三声\n");
            Tom.CatShout();

            Console.WriteLine("\n\n\n");

            //当其他颜色的猫过来时
            Console.WriteLine("[场景说明]: 一只蓝色的猫蹑手蹑脚的走了过来");
            Cat BlueCat = new Cat("BlueCat", "蓝色");
            Console.WriteLine("[场景说明]: 为了安全的偷油，登记了一个猫叫的事件");
            BlueCat.CatShoutEvent += new Cat.CatShoutEventHandler(Jerry.MouseRun);
            BlueCat.CatShoutEvent += new Cat.CatShoutEventHandler(Jack.MouseRun);
            Console.WriteLine("[场景说明]: 猫叫了三声");
            BlueCat.CatShout();

            string MyFolder = "MyFolder";

            System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher(System.IO.Directory.GetCurrentDirectory() + "\\" + MyFolder);

            watcher.Renamed += watcherRenamed;
            watcher.Deleted += watcherDeleted;
            watcher.Changed += watcherChanged;
            watcher.Created += watcherCreated;

            watcher.EnableRaisingEvents = true;
            Console.ReadKey();
        }
        static void watcherRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            Console.WriteLine("文件\"" + e.OldName + "\"更名为：" + e.Name + "；\n");
        }
        static void watcherDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine("文件\"" + e.Name + "\"已被删除；\n");
        }
        static void watcherChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine("文件\"" + e.Name + "\"已被修改；\n");
        }
        static void watcherCreated(object sender, System.IO.FileSystemEventArgs e)
        {
            Console.WriteLine("新创建了文件\"" + e.Name + "\"；\n");
        }
    }
    class Cat
    {
        string catName;
        string catColor { get; set; }
        public Cat(string name, string color)
        {
            this.catName = name;
            catColor = color;
        }

        public void CatShout()
        {
            Console.WriteLine(catColor + " 的猫 " + catName + " 过来了，喵！喵！喵！\n");

            //猫叫时触发事件
            //猫叫时，如果CatShoutEvent中有登记事件，则执行该事件
            if (CatShoutEvent != null)
                CatShoutEvent(this, new CatShoutEventArgs() { catName = this.catName, catColor = this.catColor });
        }

        public delegate void CatShoutEventHandler(object sender, CatShoutEventArgs e);

        public event CatShoutEventHandler CatShoutEvent;

    }

    /// <summary>
    /// EventArgs类的作用就是让事件传递参数用的
    /// 我们定义一个类CatShout包含两个成员属性，以方便传递
    /// </summary>
    class CatShoutEventArgs : EventArgs
    {
        public string catColor { get; set; }
        public string catName { get; set; }
    }

    class Mouse
    {
        string mouseName;
        string mouseColor { get; set; }
        public Mouse(string name, string color)
        {
            this.mouseName = name;
            this.mouseColor = color;
        }

        public void MouseRun(object sender, CatShoutEventArgs e)
        {
            if (e.catColor == "黑色")
                Console.WriteLine(mouseColor + " 的老鼠 " + mouseName + " 说：\" " + e.catColor + " 猫 " + e.catName + " 来了，快跑！\"  \n我跑！！\n我使劲跑！！\n我加速使劲跑！！！\n");
            else
                Console.WriteLine(mouseColor + " 的老鼠 " + mouseName + " 说：\" " + e.catColor + " 猫 " + e.catName + " 来了，慢跑！\"  \n我跑！！\n我慢慢跑！！\n我慢慢悠悠跑！！！\n");

        }
    }

    

}
