using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.Context;
using Blog.Context.Model;
using Core.CQRS.Command;
using Microsoft.EntityFrameworkCore;

namespace Blog.WriteSide.Command.Article
{
    public class ArticleCommandHandler : ReceiveActor
    {
        public ArticleCommandHandler()
        {
            ReceiveAsync<AddArticleCommand>(Handle);
            ReceiveAsync<RemoveArticleCommand>(Handle);
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
        
        private async Task Handle(RemoveArticleCommand removeArticle)
        {
            using (var context = new MySqlDbContext())
            {
                var entityToRemove = context.Articles
                    .Include(x => x.Content)
                    .FirstOrDefault(x => x.Id == removeArticle.Id);

                if (entityToRemove != null)
                {
                    context.Remove(entityToRemove.Content);
                    
                    await context.SaveChangesAsync();
                }
            }

            Sender.Tell(new CommandResult(), Self);
        }
    }
}