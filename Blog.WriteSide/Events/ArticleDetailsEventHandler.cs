using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.WriteSide.Model.ReadSide;
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
            using (var context = new MySqlDbContext())
            {
                var article = await context.Articles.FirstOrDefaultAsync((x => x.Id == @event.Id));

                var record = new ArticleDetailsRecord
                {
                    Id = article.Id,
                    Title = article.Title,
                    Date = article.Date,
                    Text = article.Text,
                    ImageUrl = article.ImageUrl,
                    SectionId = article.SectionId
                };

                context.ArticleDetails.Add(record);
                await context.SaveChangesAsync();
                
                var section = await context.SectionDetails.FirstOrDefaultAsync(x => x.Id == @event.SectionId);
                section.ArticlesCount = context.Articles.Count(x => x.SectionId == @event.SectionId);
                
                await context.SaveChangesAsync();
            }
            
            Sender.Tell(new CommandResult(), Self);
        }
    }
}