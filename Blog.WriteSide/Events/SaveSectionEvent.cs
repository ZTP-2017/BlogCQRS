using Core.CQRS.Event;

namespace Blog.WriteSide.Events
{
    public class SaveSectionEvent : IEvent
    {
        public int Id { get; }

        public SaveSectionEvent(int id)
        {
            Id = id;
        }
    }
}