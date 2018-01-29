using System.Collections.Generic;
using Blog.ReadSide.Model;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query
{
    public class GetSectionArticleListItems : IQuery<IEnumerable<ArticleListItemRecord>>
    {
        public int Id { get; }

        public GetSectionArticleListItems(int id)
        {
            Id = id;
        }
    }
}