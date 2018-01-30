using System.Threading.Tasks;
using Akka.Actor;
using Blog.ContextModels;
using Blog.ContextWrite;

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
            var section = new SectionRecord
            {
                Name = addSection.Name
            };
            
            using (var context = new MySqlDbContextWrite())
            {
                await context.Sections.AddAsync(section);
                await context.SaveChangesAsync();
            }
        
            Sender.Tell(new IdCommandResult(section.Id), Self);
        }
    }
}