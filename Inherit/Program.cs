using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inherit
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle();
            int area;
            rectangle.SetHeight(6);
            rectangle.SetWidth(7);
            area = rectangle.getArea();
            Console.WriteLine("总面积："+area);
            Console.WriteLine("油漆总价:"+rectangle.getCost(area));

            //调用子类
            AppleTree appleTree = new AppleTree("苹果树");
            string amyName = appleTree.MyFruitName();

            //调用子类
            OrangeTree orangeTree = new OrangeTree("桔子树");
            string omyName = orangeTree.MyFruitName();
            Console.ReadKey();
        }
        class Shape
        {
            public void SetWidth(int w)
            {
                width = w;
            }
            public void SetHeight(int h)
            {
                height = h;
            }
            protected int width;
            protected int height;
        }
        public interface PaintCost
        {
            int getCost(int area);
        }
        class Rectangle : Shape, PaintCost
        {
            public int getArea()
            {
                return (width *height);
            }
            public int getCost(int area)
            {
                return (area*70);
            }
        }

        /// <summary>
        /// 果树类
        /// </summary>
        class FruitTree
        {
            /// <summary>
            /// 名称
            /// 说明：修饰符 protected 保护访问。只限于本类和子类访问，实例不能访问。
            /// </summary>
            protected string name;
            /// <summary>
            /// 构造函数
            /// </summary>
            public FruitTree()
            {
                this.name = "无名";
            }
            /// <summary>
            /// 构造函数二
            /// </summary>
            /// <param name="name"></param>
            public FruitTree(string name)
            {
                this.name = name;
            }
            object _leaf;
            object _root;
            object _flower;
            string _type;
            /// <summary>
            /// 叶子（公有属性）
            /// </summary>
            public object leaf
            {
                get { return _leaf; }
                set { _leaf = value; }
            }
            /// <summary>
            /// 根（公有属性）
            /// </summary>
            public object root
            {
                get { return _root; }
                set { _root = value; }
            }
            /// <summary>
            /// 花（公有属性）
            /// </summary>
            public object flower
            {
                get { return _flower; }
                set { _flower = value; }
            }
            /// <summary>
            /// 类别（不定义修饰符，默认为私有）
            /// </summary>
            string type
            {
                get { return _type; }
                set { _type = value; }
            }

        }

        /// <summary>
        /// 苹果树类
        /// 继承自：果树类
        /// </summary>
        class AppleTree : FruitTree
        {
            string _myName;
            /// <summary>
            /// 构造函数
            /// 说明：子类调用父类同样的构造函数，需要使用 :base()
            /// </summary>
            public AppleTree() : base()
            {
            }
            /// <summary>
            /// 构造函数二
            /// 说明：子类调用父类同样的构造函数，需要使用 :base(name)
            /// </summary>
            /// <param name="name"></param>
            public AppleTree(string name) : base(name)
            {
                _myName = name;
            }

            /// <summary>
            /// 返回果实的名字
            /// </summary>
            /// <returns></returns>
            public string MyFruitName()
            {
                return "我是：" + _myName + "；我的果实叫：苹果";
            }
        }
        /// <summary>
        /// 桔树类
        /// 继承自：果树类
        /// </summary>
        class OrangeTree : FruitTree
        {
            string _myName;
            /// <summary>
            /// 构造函数
            /// 说明：子类调用父类同样的构造函数，需要使用 :base()
            /// </summary>
            public OrangeTree() : base()
            {
            }
            /// <summary>
            /// 构造函数二
            /// 说明：子类调用父类同样的构造函数，需要使用 :base(name)
            /// </summary>
            /// <param name="name"></param>
            public OrangeTree(string name) : base(name)
            {
                _myName = name;
            }

            /// <summary>
            /// 返回果实的名字
            /// </summary>
            /// <returns></returns>
            public string MyFruitName()
            {
                return "我是：" + _myName + "；我的果实叫：桔子";
            }
        }
    }
}
