using MediatR;
using System;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand
{
    [DataContract]
    public class CreateUserCommand : IRequest<string>
    {
        [DataMember]
        public string Username { get; private set; }

        [DataMember]
        public string Password { get; private set; }

        [DataMember]
        public string ConfirmPassword { get; private set; }

        [DataMember]
        public string Email { get; private set; }
    }
}
