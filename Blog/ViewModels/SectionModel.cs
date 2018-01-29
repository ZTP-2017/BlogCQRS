using Blog.ReadSide.Model;

namespace Blog.ViewModels
{
    public class SectionModel
    {
        public string Name { get; set; }
        public int ArticlesCount { get; set; }
        public ArticleListItemRecord[] Articles { get; set; }
    }
}