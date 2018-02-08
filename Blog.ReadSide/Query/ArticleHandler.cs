using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide.Model;
using MongoDB.Driver;

namespace Blog.ReadSide.Query
{
    public class ArticleHandler : ReceiveActor
    {
        private readonly IMongoDatabase _mongoDb;

        public ArticleHandler()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDb = client.GetDatabase("blog_read");

            ReceiveAsync<GetArticleDetailsQuery>(Handle);
        }

        private async Task Handle(GetArticleDetailsQuery query)
        {
            var collection = _mongoDb.GetCollection<ArticleDetailsRecord>("articles");
            var filter = Builders<ArticleDetailsRecord>.Filter.Eq(x => x.Id, query.Id);
            var result = (await collection.FindAsync(filter)).FirstOrDefault();

            Sender.Tell(result, Self);
        }
    }
}