using System.Threading.Tasks;
using Akka.Actor;
using Blog.WriteSide.Events;
using Blog.WriteSide.Models.Write;
using Core.CQRS.Command;

namespace Blog.WriteSide.Command
{
    class AddArticleCommandHandler : ReceiveActor
    {
        public AddArticleCommandHandler()
        {
            ReceiveAsync<AddArticleCommand>(Handle);
        }

        private async Task Handle(AddArticleCommand addArticle)
        {
            var record = new ArticleRecord
            {
                SectionId = addArticle.SectionId,
                Title = addArticle.Title,
                Date = addArticle.Date.Date,
                Text = addArticle.Text,
                ImageUrl = addArticle.ImageUrl
            };

            using (var context = new MySqlDbContext())
            {
                await context.Articles.AddAsync(record);
                await context.SaveChangesAsync();
            }

            await Context.ActorOf<EventRootActor>()
                .Ask<CommandResult>(new SaveArticleEvent(record.Id, record.SectionId));

            Sender.Tell(new IdCommandResult(record.Id), Self);
        }
    }
}