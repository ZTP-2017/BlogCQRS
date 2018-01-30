using System.Threading.Tasks;
using Akka.Actor;
using Blog.ContextRead;
using Blog.ContextRead.Models;
using Blog.ContextWrite;
using Core.CQRS.Command;
using Microsoft.EntityFrameworkCore;

namespace Blog.WriteSide.Events
{
    public class ArticleDetailsEventHandler : ReceiveActor
    {
        public ArticleDetailsEventHandler()
        {
            ReceiveAsync<ArticleAddedEvent>(Handle);
        }

        private async Task Handle(ArticleAddedEvent @event)
        {
            ArticleRecordRead record;
            using (var context = new MySqlDbContextWrite())
            {
                var article = await context.Articles.FirstOrDefaultAsync((x => x.Id == @event.Id));

                record = new ArticleRecordRead
                {
                    Id = article.Id,
                    Title = article.Title,
                    Date = article.Date,
                    Text = article.Text,
                    ImageUrl = article.ImageUrl,
                    SectionId = article.SectionId
                };
            }

            using (var context = new MySqlDbContextRead())
            {
                context.ArticleDetails.Add(record);
                await context.SaveChangesAsync();
            }
            
            Sender.Tell(new CommandResult(), Self);
        }
    }
}