using System.Threading.Tasks;
using Akka.Actor;
using Blog.Context;
using Blog.Context.Model;
using Core.CQRS.Command;

namespace Blog.WriteSide.Command.Article
{
    public class ArticleCommandHandler : ReceiveActor
    {
        public ArticleCommandHandler()
        {
            ReceiveAsync<AddArticleCommand>(Handle);
        }

        private async Task Handle(AddArticleCommand addArticle)
        {
            var entry = new ArticleRecord
            {
                Title = addArticle.Title,
                Date = addArticle.Date,
                Content = new ArticleContentRecord
                {
                    Image = addArticle.Image,
                    Text = addArticle.Text
                }
            };
                
            using (var context = new MySqlDbContext())
            {
                await context.Articles.AddAsync(entry);
                await context.SaveChangesAsync();
            }
            
            Sender.Tell(new CommandResult(), Self);
        }
    }
}