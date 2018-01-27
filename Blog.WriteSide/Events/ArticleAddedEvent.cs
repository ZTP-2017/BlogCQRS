using Core.CQRS.Event;

namespace Blog.WriteSide.Events
{
    public class ArticleAddedEvent : IEvent
    {
        public int Id { get; }
        public int SectionId { get; }

        public ArticleAddedEvent(int id, int sectionId)
        {
            Id = id;
            SectionId = sectionId;
        }
    }
}