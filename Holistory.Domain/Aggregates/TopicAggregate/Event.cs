using Holistory.Domain.Seedwork;
using System;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Event : Entity
    {
        private Event()
        {
        }

        public Event(
            string title, 
            string content,
            DateTime startDate,
            DateTime? endDate,
            int x,
            int y, 
            int eventTypeId)
        {
            Title = title;
            Content = content;
            StartDate = startDate;
            EndDate = endDate;
            X = x;
            Y = y;
            EventTypeId = eventTypeId;
        }

        public int TopicId { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int EventTypeId { get; private set; }

        public EventType EventType => Enumeration.FromValue<EventType>(EventTypeId);
    }
}
