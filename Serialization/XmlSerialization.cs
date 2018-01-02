using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serialization
{
   
    public class XmlSerialization
    {
        /// <summary>
        /// 获取我的博客园中文章
        /// </summary>
        /// <returns></returns>
        public static List<MyArticle> GetMyArticle(int count)
        {
            var document = System.Xml.Linq.XDocument.Load(
                "http://wcf.open.cnblogs.com/blog/u/yubinfeng/posts/1/" + count
                );
            List<MyArticle> myArticleList = new List<MyArticle>();
            var elements = document.Root.Elements();

            //在进行这个工作之前，我们先获取我博客中的文章列表
            var result = elements.Where(m => m.Name.LocalName == "entry").Select(myArticle => new MyArticle
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
            }).OrderByDescending(m => m.published);
            myArticleList.AddRange(result);
            return myArticleList;


        }
    }
    /// <summary
    /// 我的博客文章实体类
    /// </summary>

    public class MyArticle
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 文章摘要
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime published { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime updated { get; set; }
        /// <summary>
        /// URL地址
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 推荐数
        /// </summary>
        public int diggs { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int views { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int comments { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
    }
}
