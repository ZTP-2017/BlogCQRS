using Akka.Actor;
using Blog.WriteSide.Events;

namespace Blog.WriteSide
{
    public class EventRootActor : ReceiveActor
    {
        public EventRootActor()
        {
            var articleDetailsEventHandler = Context.ActorOf<ArticleDetailsEventHandler>();
            var sectionDetailsEventHandler = Context.ActorOf<SectionDetailsEventHandler>();

            Receive<SectionAddedEvent>(message => sectionDetailsEventHandler.Forward(message));

            Receive<ArticleAddedEvent>(message =>
            {
                articleDetailsEventHandler.Forward(message);
                sectionDetailsEventHandler.Forward(message);
            });
        }
    }
}