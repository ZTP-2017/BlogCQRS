using System.Collections.Generic;

namespace Blog.WriteSide.Models.Read
{
    public class SectionDetailsRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArticlesCount { get; set; }
        public IEnumerable<ArticleListItemRecord> Articles { get; set; }
    }
}