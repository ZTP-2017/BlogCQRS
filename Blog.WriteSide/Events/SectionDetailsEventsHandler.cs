using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.WriteSide.Models.Read;
using Blog.WriteSide.Models.Write;
using Core.CQRS.Command;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Blog.WriteSide.Events
{
    public class SectionDetailsEventsHandler : ReceiveActor
    {
        private readonly IMongoDatabase _mongoDb;

        public SectionDetailsEventsHandler()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDb = client.GetDatabase("blog_read");

            ReceiveAsync<SaveArticleEvent>(Handle);
            ReceiveAsync<SaveSectionEvent>(Handle);
        }

        private async Task Handle(SaveSectionEvent @event)
        {
            SectionRecord section;
            ICollection<ArticleRecord> articles;

            using (var context = new MySqlDbContext())
            {
                section = await context.Sections
                    .FirstOrDefaultAsync(x => x.Id == @event.Id);

                articles = context.Articles
                    .Where(x => x.SectionId == @event.Id).ToList();
            }

            if (section != null)
            {
                var record = GetRecord(section, articles);

                await SaveSection(record);
            }

            Sender.Tell(new CommandResult(), Self);
        }

        private async Task Handle(SaveArticleEvent @event)
        {
            SectionRecord section = null;
            List<ArticleRecord> articles;

            using (var context = new MySqlDbContext())
            {
                section = await context.Sections
                    .FirstOrDefaultAsync(x => x.Id == @event.SectionId);

                articles = context.Articles
                    .Where(x => x.SectionId == @event.SectionId).ToList();
            }

            if (section != null)
            {
                var record = GetRecord(section, articles);

                await UpdateSection(record);
            }

            Sender.Tell(new CommandResult(), Self);
        }

        private async Task SaveSection(SectionDetailsRecord section)
        {
            var collection = _mongoDb.GetCollection<SectionDetailsRecord>("sections");

            await collection.InsertOneAsync(section);
        }

        private async Task UpdateSection(SectionDetailsRecord section)
        {
            var collection = _mongoDb.GetCollection<SectionDetailsRecord>("sections");
            var filter = Builders<SectionDetailsRecord>.Filter.Eq(x => x.Id, section.Id);

            var result = await collection.ReplaceOneAsync(filter, section);
        }

        private SectionDetailsRecord GetRecord(SectionRecord section, 
            ICollection<ArticleRecord> articles)
        {
            return new SectionDetailsRecord
            {
                Id = section.Id,
                Name = section.Name,
                Articles = articles.Select(x => new ArticleListItemRecord
                {
                    Id = x.Id,
                    Title = x.Title,
                    Date = x.Date,
                    ImageUrl = x.ImageUrl
                }),
                ArticlesCount = articles.Count()
            };
        }
    }
}