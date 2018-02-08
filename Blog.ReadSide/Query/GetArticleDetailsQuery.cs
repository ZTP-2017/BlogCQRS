using Blog.ReadSide.Model;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query
{
    public class GetArticleDetailsQuery : IQuery<ArticleDetailsRecord>
    {
        public int Id { get; }

        public GetArticleDetailsQuery(int id)
        {
            Id = id;
        }
    }
}