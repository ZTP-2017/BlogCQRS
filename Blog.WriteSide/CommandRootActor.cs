using Akka.Actor;
using Blog.WriteSide.Command.Article;

namespace Blog.WriteSide
{
    public class CommandRootActor : ReceiveActor
    {
        public CommandRootActor()
        {
            var articleCommandHandler = Context.ActorOf<ArticleCommandHandler>();

            Receive<AddArticleCommand>(message => articleCommandHandler.Forward(message));
            Receive<RemoveArticleCommand>(message => articleCommandHandler.Forward(message));
        }
    }
}