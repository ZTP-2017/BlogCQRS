using Akka.Actor;
using Blog.ReadSide.Query.Article;

namespace Blog.ReadSide
{
    public class QueryRootActor : ReceiveActor
    {
        public QueryRootActor()
        {
            var articleHandlerProps = Props.Create<ArticleHandler>();
            var articleHandler = Context.ActorOf(articleHandlerProps);

            Receive<GetArticleQuery>(message => articleHandler.Forward(message));
            Receive<GetArticlesListQuery>(message => articleHandler.Forward(message));
        }
    }
}