using System.Collections.Generic;
using Blog.Context.Model;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query.Article
{
    public class GetArticlesListQuery : IQuery<IEnumerable<ArticleRecord>>
    {
        
    }
}