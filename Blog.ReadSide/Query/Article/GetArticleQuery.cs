using Blog.Context.Model;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query.Article
{
    public class GetArticleQuery : IQuery<ArticleRecord>
    {
        public int Id { get; }

        public GetArticleQuery(int id)
        {
            Id = id;
        }
    }
}