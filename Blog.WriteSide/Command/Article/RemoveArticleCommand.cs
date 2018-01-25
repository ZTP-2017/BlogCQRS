using Core.CQRS.Command;

namespace Blog.WriteSide.Command.Article
{
    public class RemoveArticleCommand : ICommand
    {
        public int Id { get; }

        public RemoveArticleCommand(int id)
        {
            Id = id;
        }
    }
}