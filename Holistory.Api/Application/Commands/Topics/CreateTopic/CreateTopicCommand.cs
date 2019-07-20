using MediatR;
using System;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Topics.CreateTopic
{
    [DataContract]
    public class CreateTopicCommand : IRequest<int>
    {
        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public DateTime? StartDate { get; private set; }

        [DataMember]
        public DateTime? EndDate { get; private set; }

        [DataMember]
        public string Map { get; private set; }

        [DataMember]
        public int? RegionId { get; private set; }

        [DataMember]
        public int? EraId { get; private set; }
    }
}
