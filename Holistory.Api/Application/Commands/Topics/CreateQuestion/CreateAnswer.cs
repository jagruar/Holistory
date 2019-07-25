using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Topics.CreateQuestion
{
    [DataContract]
    public class CreateAnswer
    {
        [DataMember]
        public string Text { get; private set; }

        [DataMember]
        public bool? IsCorrect { get; private set; }
    }
}
