using System.Threading.Tasks;
using Akka.Actor;
using Blog.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.ReadSide.Query.Article
{
    public class ArticleHandler : ReceiveActor
    {
        public ArticleHandler()
        {
            ReceiveAsync<GetArticleQuery>(Handle);
            ReceiveAsync<GetArticlesListQuery>(Handle);
        }

        private async Task Handle(GetArticleQuery query)
        {
            using (var context = new MySqlDbContext())
            {
                var result = await context.Articles
                    .Include(x => x.Content)
                    .FirstOrDefaultAsync(x => x.Id == query.Id);
                
                Sender.Tell(result, Self);
            }
        }
        
        private async Task Handle(GetArticlesListQuery query)
        {
            using (var context = new MySqlDbContext())
            {
                var result = await context.Articles
                    .Include(x => x.Content)
                    .ToListAsync();
                
                Sender.Tell(result, Self);
            }
        }
    }
}