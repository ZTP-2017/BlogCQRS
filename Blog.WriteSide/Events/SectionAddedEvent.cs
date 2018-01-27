using Core.CQRS.Event;

namespace Blog.WriteSide.Events
{
    public class SectionAddedEvent : IEvent
    {
        public int Id { get; }

        public SectionAddedEvent(int id)
        {
            Id = id;
        }
    }
}