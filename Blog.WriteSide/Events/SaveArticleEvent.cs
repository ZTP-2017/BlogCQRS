using Core.CQRS.Event;

namespace Blog.WriteSide.Events
{
    public class SaveArticleEvent : IEvent
    {
        public int Id { get; }
        public int SectionId { get; }

        public SaveArticleEvent(int id, int sectionId)
        {
            Id = id;
            SectionId = sectionId;
        }
    }
}