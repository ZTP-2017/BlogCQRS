using System.Threading.Tasks;
using Akka.Actor;
using Blog.WriteSide.Events;
using Blog.WriteSide.Models.Write;
using Core.CQRS.Command;

namespace Blog.WriteSide.Command
{
    class AddSectionCommandHandler : ReceiveActor
    {
        public AddSectionCommandHandler()
        {
            ReceiveAsync<AddSectionCommand>(Handle);
        }

        private async Task Handle(AddSectionCommand addSection)
        {
            var record = new SectionRecord
            {
                Name = addSection.Name
            };
            
            using (var context = new MySqlDbContext())
            {
                await context.Sections.AddAsync(record);
                await context.SaveChangesAsync();
            }

            await Context.ActorOf<EventRootActor>()
                .Ask<CommandResult>(new SaveSectionEvent(record.Id));

            Sender.Tell(new IdCommandResult(record.Id), Self);
        }
    }
}