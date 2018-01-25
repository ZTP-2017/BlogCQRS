using Blog.Context.Model;
using Blog.ViewModels;

namespace Blog.Common
{
    public static class Helpers
    {
        public static ArticleModel MapArticleRecordToArticleModel(ArticleRecord articleRecord)
        {
            return new ArticleModel
            {
                Id = articleRecord.Id,
                Title = articleRecord.Title,
                Date = articleRecord.Date,
                Text = articleRecord.Content.Text,
                ImageUrl = articleRecord.Content.Image
            };
        }
    }
}