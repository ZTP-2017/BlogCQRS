using System;
using Microsoft.AspNetCore.Http;

namespace Blog.ViewModels
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}