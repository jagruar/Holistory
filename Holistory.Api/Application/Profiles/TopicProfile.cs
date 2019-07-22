using AutoMapper;
using Holistory.Api.DataTranserObjects;
using Holistory.Domain.Aggregates.TopicAggregate;

namespace Holistory.Api.Application.Profiles
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<Attempt, AttemptDto>();
        }
    }
}
