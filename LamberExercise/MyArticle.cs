using System;

namespace LamberExercise
{
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
