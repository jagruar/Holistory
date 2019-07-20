using Holistory.Common.Exceptions;
using Holistory.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Topic : Entity, IAggregateRoot
    {
        private readonly List<Event> _events = new List<Event>();
        private readonly List<Question> _questions = new List<Question>();

        private Topic()
        {
        }

        public Topic(
            string title,
            string description,
            DateTime startDate,
            DateTime endDate,
            string map,
            int regionId,
            int eraId)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Map = map;
            RegionId = regionId;
            EraId = eraId;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public string Map { get; private set; }

        public Region Region => Enumeration.FromValue<Region>(RegionId);

        public int RegionId { get; private set; }

        public Era Era => Enumeration.FromValue<Era>(EraId);

        public int EraId { get; private set; }

        public IReadOnlyCollection<Event> Events => _events;

        public IReadOnlyCollection<Question> Questions => _questions;

        public Event AddEvent(
            string title,
            string content,
            DateTime startDate,
            DateTime? endDate,
            int x,
            int y,
            int eventTypeId)
        {
            Event @event = new Event(title, content, startDate, endDate, x, y, eventTypeId);
            _events.Add(@event);
            return @event;
        }

        public Question AddQuestion(int? eventId, string text)
        {
            Question question = new Question(eventId, text);
            _questions.Add(question);
            return question;
        }

        public Answer AddAnswer(int questionId, string text, bool isCorrect)
        {
            Question question = _questions.FirstOrDefault(x => x.Id == questionId);
            NotFoundException.ThrowIfNull(question, nameof(question));
            Answer answer = question.AddAnswer(text, isCorrect);
            return answer;
        }
    }
}
