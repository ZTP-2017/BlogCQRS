using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.WriteSide.Model.ReadSide;
using Core.CQRS.Command;
using Microsoft.EntityFrameworkCore;

namespace Blog.WriteSide.Events
{
    public class SectionDetailsEventHandler : ReceiveActor
    {
        public SectionDetailsEventHandler()
        {
            ReceiveAsync<SectionAddedEvent>(Handle);
        }

        private async Task Handle(SectionAddedEvent @event)
        {
            using (var context = new MySqlDbContext())
            {
                var section = await context.Sections.FirstOrDefaultAsync((x => x.Id == @event.Id));

                var record = new SectionDetailsRecord
                {
                    Id = section.Id,
                    Name = section.Name,
                    ArticlesCount = 0
                };
                
                await context.SectionDetails.AddAsync(record);
                await context.SaveChangesAsync();
            }
            
            Sender.Tell(new CommandResult(), Self);
        }
        
        private async Task Handle(ArticleAddedEvent @event)
        {
            using (var context = new MySqlDbContext())
            {
                var section = await context.SectionDetails.FirstOrDefaultAsync(x => x.Id == @event.SectionId);
                section.ArticlesCount = context.Articles.Count(x => x.SectionId == @event.Id);
                
                await context.SaveChangesAsync();
            }
        }
    }
}