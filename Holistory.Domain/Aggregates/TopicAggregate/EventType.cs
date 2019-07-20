using Holistory.Domain.Seedwork;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class EventType : Enumeration
    {
        public EventType(int id, string name)
            : base(id, name)
        {
        }

        public static EventType Battle => new EventType(1, "Battle");

        public static EventType Religious => new EventType(2, "Religious");

        public static EventType Death => new EventType(3, "Death");
    }
}
