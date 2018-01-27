using Akka.Actor;
using Blog.WriteSide.Command;

namespace Blog.WriteSide
{
    public class CommandRootActor : ReceiveActor
    {
        public CommandRootActor()
        {
            var addArticleCommandHandler = Context.ActorOf<AddArticleCommandHandler>();
            Receive<AddArticleCommand>(message => addArticleCommandHandler.Forward(message));

            var addSectionCommandHandler = Context.ActorOf<AddSectionCommandHandler>();
            Receive<AddSectionCommand>(message => addSectionCommandHandler.Forward(message));
        }
    }
}