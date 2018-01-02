using System;
using System.Linq;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace LamberExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, bool>> expression = (x, y) => x != 0 && x == y + 1;
            Func<int, int, bool> result = expression.Compile();
            bool result2 = expression.Compile()(9, 8);
            Console.WriteLine(result2);
            Console.WriteLine(result(3, 2));
            Console.WriteLine(result(5, 4));
            Console.WriteLine(result(6, 4));
            Console.WriteLine(result(-6, -7));

            //修改表达式树         
            OrElseModifier amf = new OrElseModifier();
            Expression newExp = amf.Modify(expression);
            Console.WriteLine("原表达式：      " + expression.ToString());
            Console.WriteLine("修改后的表达式：" + newExp.ToString());
           

            //动态查询 我在博客园中的文章分类查询

            //第一步，获取我在博客园中的文章
            List<MyArticle> myArticleList = new List<MyArticle>();
            var document = XDocument.Load(
                "http://wcf.open.cnblogs.com/blog/u/yubinfeng/posts/1/100"
                );

            var elements = document.Root.Elements();

            //在进行这个工作之前，我们先获取我博客中的文章列表
            var results = elements.Where(m => m.Name.LocalName == "entry").Select(myArticle => new MyArticle
            {
                id = Convert.ToInt32(myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "id").Value),
                title = myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "title").Value,
                published = Convert.ToDateTime(myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "published").Value),
                updated = Convert.ToDateTime(myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "updated").Value),
                diggs = Convert.ToInt32(myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "diggs").Value),
                views = Convert.ToInt32(myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "views").Value),
                comments = Convert.ToInt32(myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "comments").Value),
                summary = myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "summary").Value,
                link = myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "link").Attribute("href").Value,
                author = myArticle.Elements().SingleOrDefault(x => x.Name.LocalName == "author").Elements().SingleOrDefault(x => x.Name.LocalName == "name").Value
            });
            myArticleList.AddRange(results);


            IQueryable<MyArticle> resultsd = MySearchList(myArticleList.AsQueryable<MyArticle>(), new MyArticle() { views = 500, published = Convert.ToDateTime("2015-06") });

            foreach (MyArticle article in resultsd)
                Console.WriteLine(article.title + " \n [发布日期:" + article.published + "] [浏览数：" + article.views + "]");
            Console.ReadKey();
        }
        private static void TestLambdaExercise()
        {
            //表达式树(Expression)
            Expression<Func<int, int, bool>> expression = (x, y) => x != 0 && x == y + 1;

            BinaryExpression exr = expression.Body as BinaryExpression;
            //IReadOnlyList<ParameterExpression> param = expression.Parameters as IReadOnlyList<ParameterExpression>;
            BinaryExpression left = exr.Left as BinaryExpression;
            BinaryExpression right = exr.Right as BinaryExpression;
            ExpressionType exrType = exr.NodeType;

            ParameterExpression leftLeft = left.Left as ParameterExpression;
            ConstantExpression leftRight = left.Right as ConstantExpression;
            ExpressionType leftType = left.NodeType;

            ParameterExpression rightLeft = right.Left as ParameterExpression;
            BinaryExpression rightRight = right.Right as BinaryExpression;
            ExpressionType rightType = right.NodeType;

            ParameterExpression rightRightLeft = rightRight.Left as ParameterExpression;
            ExpressionType rightRightType = rightRight.NodeType;
            ConstantExpression rightRightRight = rightRight.Right as ConstantExpression;

            Console.WriteLine(exr.ToString());
        }
        private static void GetArticle()
        {
           
        }     
        public static IQueryable<T> MySearchList<T>(IQueryable<T> myArticleTable, T myArticle)
        {
            //第二步，动态查询我的文章

            // List<MyArticle> SearchMyArticleList = new List<MyArticle>();
            //1.我们先定义几个查询的参数(文章标题,浏览数,发布时间)              

            ParameterExpression myart = Expression.Parameter(typeof(T), "article");   //标题     
            ParameterExpression searchTitle = Expression.Parameter(typeof(string), "searchTitle");   //标题     
            ParameterExpression searchViews = Expression.Parameter(typeof(int), "searchViews");     //浏览数   
            ParameterExpression searchPublished = Expression.Parameter(typeof(DateTime), "searchPublished");//创建月份

            //2.使用表达式树，动态生成查询 （查询某个日期的文章）
            Expression left = Expression.Property(myart, typeof(T).GetProperty("published")); //访问属性的表达式
            Expression right = Expression.Property(Expression.Constant(myArticle), typeof(T).GetProperty("published"));//访问属性的表达式
            Expression e1 = Expression.GreaterThanOrEqual(left, right); //大于等于

            //2.使用表达式树，动态生成查询 （按点击数查询）
            Expression left2 = Expression.Property(myart, typeof(T).GetProperty("views")); //访问属性的表达式
            Expression right2 = Expression.Property(Expression.Constant(myArticle), typeof(T).GetProperty("views"));//访问属性的表达式
            Expression e2 = Expression.GreaterThanOrEqual(left2, right2);

            //3.构造动态查询 （按点击数和月份查询）
            Expression predicateBody = Expression.AndAlso(e1, e2);

            //4.构造过滤
            MethodCallExpression whereCallExpression = Expression.Call(
            typeof(Queryable),
            "Where",
            new Type[] { typeof(T) },
            myArticleTable.Expression,
            Expression.Lambda<Func<T, bool>>(predicateBody, new ParameterExpression[] { myart }));

            //构造排序
            MethodCallExpression orderByCallExpression = Expression.Call(
            typeof(Queryable),
            "OrderByDescending",
            new Type[] { typeof(T), typeof(int) },
            whereCallExpression,
            Expression.Lambda<Func<T, int>>(left2, new ParameterExpression[] { myart }));

            //创建查询表达式树
            IQueryable<T> results = myArticleTable.Provider.CreateQuery<T>(orderByCallExpression);

            return results;
        }
    }
    public class OrElseModifier : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }
        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.AndAlso)
            {
                Expression left = this.Visit(b.Left);
                Expression right = this.Visit(b.Right);

                return Expression.MakeBinary(ExpressionType.OrElse, left, right, b.IsLiftedToNull, b.Method);
            }

            return base.VisitBinary(b);
        }
    }
    internal interface IReadOnlyList<T>
    {
    }
}