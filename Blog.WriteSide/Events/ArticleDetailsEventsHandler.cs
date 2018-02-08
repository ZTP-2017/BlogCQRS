using Akka.Actor;
using Blog.WriteSide.Models.Write;
using Core.CQRS.Command;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Blog.WriteSide.Models.Read;
using MongoDB.Driver;

namespace Blog.WriteSide.Events
{
    public class ArticleDetailsEventsHandler : ReceiveActor
    {
        private readonly IMongoDatabase _mongoDb;

        public ArticleDetailsEventsHandler()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDb = client.GetDatabase("blog_read");

            ReceiveAsync<SaveArticleEvent>(Handle);
        }

        private async Task Handle(SaveArticleEvent @event)
        {
            ArticleRecord article;

            using (var context = new MySqlDbContext())
            {
                article = await context.Articles
                    .FirstOrDefaultAsync((x => x.Id == @event.Id));
            }

            if (article != null)
            {
                var record = new ArticleDetailsRecord
                {
                    Id = article.Id,
                    Title = article.Title,
                    Date = article.Date,
                    Text = article.Text,
                    ImageUrl = article.ImageUrl
                };

                await SaveArticle(record);
            }

            Sender.Tell(new CommandResult(), Self);
        }

        private async Task SaveArticle(ArticleDetailsRecord article)
        {
            var collection = _mongoDb.GetCollection<ArticleDetailsRecord>("articles");

            await collection.InsertOneAsync(article);
        }
    }
}