using System.Threading.Tasks;
using Akka.Actor;
using Blog.ContextModels;
using Blog.ContextRead;
using Blog.ContextWrite;
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
            SectionRecord record;
            
            using (var context = new MySqlDbContextWrite())
            {
                var section = await context.Sections.FirstOrDefaultAsync((x => x.Id == @event.Id));

                record = new SectionRecord
                {
                    Id = section.Id,
                    Name = section.Name,
                };
            }
            
            using (var context = new MySqlDbContextRead())
            {
                await context.SectionDetails.AddAsync(record);
                await context.SaveChangesAsync();
            }
            
            Sender.Tell(new CommandResult(), Self);
        }
    }
}