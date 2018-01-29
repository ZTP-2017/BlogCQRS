using System;

namespace Blog.ReadSide.Model
{
    public class ArticleDetailsRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}