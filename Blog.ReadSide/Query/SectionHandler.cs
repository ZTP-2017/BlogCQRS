using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide.Model;
using MongoDB.Driver;

namespace Blog.ReadSide.Query
{
    public class SectionHandler : ReceiveActor
    {
        private readonly IMongoDatabase _mongoDb;

        public SectionHandler()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDb = client.GetDatabase("blog_read");

            ReceiveAsync<GetSectionListQuery>(Handle);
        }

        private async Task Handle(GetSectionListQuery query)
        {
            
            var collection = _mongoDb.GetCollection<SectionDetailsRecord>("sections");
            var result = await collection.Find(FilterDefinition<SectionDetailsRecord>.Empty).ToListAsync();

            Sender.Tell(result, Self);
        }
    }
}