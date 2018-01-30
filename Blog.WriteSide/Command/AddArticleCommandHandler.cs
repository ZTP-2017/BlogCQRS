using System.Threading.Tasks;
using Akka.Actor;
using Blog.ContextModels;
using Blog.ContextWrite;

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
            using (var context = new MySqlDbContextWrite())
            {
                var article = new ArticleRecord
                {
                    SectionId = addArticle.SectionId,
                    Title = addArticle.Title,
                    Date = addArticle.Date.Date,
                    Text = addArticle.Text,
                    ImageUrl = addArticle.ImageUrl
                };
                
                await context.Articles.AddAsync(article);
                await context.SaveChangesAsync();
                
                Sender.Tell(new IdCommandResult(article.Id), Self);
            }
        }
    }
}