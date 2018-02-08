using Akka.Actor;
using Blog.WriteSide.Events;

namespace Blog.WriteSide
{
    public class EventRootActor : ReceiveActor
    {
        public EventRootActor()
        {
            var articleDetailsEventHandler = Context.ActorOf<ArticleDetailsEventsHandler>();
            var sectionDetailsEventHandler = Context.ActorOf<SectionDetailsEventsHandler>();

            Receive<SaveArticleEvent>(message =>
            {
                articleDetailsEventHandler.Forward(message);
                sectionDetailsEventHandler.Forward(message);
            });

            Receive<SaveSectionEvent>(message => sectionDetailsEventHandler.Forward(message));
        }
    }
}