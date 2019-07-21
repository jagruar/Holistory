using MediatR;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Portal.Users.GenerateAuthTokenCommand
{
    [DataContract]
    public class GenerateAuthTokenCommand : IRequest<string>
    {
        [DataMember]
        public string Username { get; private set; }

        [DataMember]
        public string Password { get; private set; }
    }
}
