using Blog.ReadSide.Model;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query
{
    public class GetArticleDetails : IQuery<ArticleDetailsRecord>
    {
        public int Id { get; }

        public GetArticleDetails(int id)
        {
            Id = id;
        }
    }
}