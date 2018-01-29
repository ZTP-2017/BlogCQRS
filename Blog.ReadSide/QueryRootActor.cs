using Akka.Actor;
using Blog.ReadSide.Query;

namespace Blog.ReadSide
{
    public class QueryRootActor : ReceiveActor
    {
        public QueryRootActor()
        {
            var articleHandlerProps = Props.Create<ArticleHandler>();
            var articleHandler = Context.ActorOf(articleHandlerProps);
            
            Receive<GetArticleDetails>(message => articleHandler.Forward(message));
            Receive<GetSectionArticleListItems>(message => articleHandler.Forward(message));

            var sectionHandlerProps = Props.Create<SectionHandler>();
            var sectionHandler = Context.ActorOf(sectionHandlerProps);

            Receive<GetSectionList>(message => sectionHandler.Forward(message));
        }
    }
}