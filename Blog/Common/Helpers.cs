using System.Collections.Generic;
using System.Linq;
using Blog.ReadSide.Model;
using Blog.ViewModels;
using Blog.WriteSide.Model.ReadSide;
using ArticleDetailsRecord = Blog.ReadSide.Model.ArticleDetailsRecord;

namespace Blog.Common
{
    public static class Helpers
    {
        public static SectionModel MapToSectionModel(SectionDetailsRecord section, IEnumerable<ArticleListItemRecord> articles)
        {
            return new SectionModel
            {
                Name = section.Name,
                ArticlesCount = section.ArticlesCount,
                Articles = articles.ToArray()
            };
        }

        public static ArticleModel MapToArticleModel(ArticleDetailsRecord article)
        {
            return new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Date = article.Date,
                Text = article.Text,
                ImageUrl = article.ImageUrl
            };
        }
    }
}