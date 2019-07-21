namespace Holistory.Api.DataTranserObjects
{
    public class EventDto
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int EventTypeId { get; set; }
    }
}
