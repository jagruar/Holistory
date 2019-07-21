using FluentValidation.Results;
using Holistory.Common.Configuration;
using Holistory.Common.Constants;
using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Portal.Users.GenerateAuthTokenCommand
{
    public class GenerateAuthTokenCommandHandler : IRequestHandler<GenerateAuthTokenCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _authOptions;

        public GenerateAuthTokenCommandHandler(
            UserManager<User> userManager,
            IOptions<JwtOptions> authOptions)
        {
            _userManager = userManager;
            _authOptions = authOptions.Value;
        }

        public async Task<string> Handle(GenerateAuthTokenCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(request.Username);
            
            if (user is null)
            {
                throw new DataValidationException(new ValidationFailure[]
                    {
                        new ValidationFailure(nameof(request.Username), "Username or password is incorrect."),
                        new ValidationFailure(nameof(request.Password), "Username or password is incorrect.")
                    });
            }

            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordCorrect)
            {
                throw new DataValidationException(new ValidationFailure[]
                    {
                        new ValidationFailure(nameof(request.Username), "Username or password is incorrect."),
                        new ValidationFailure(nameof(request.Password), "Username or password is incorrect.")
                    });
            }

            // User exists and has supplied the correct password, so generate them a JWT

            IList<string> roles = await _userManager.GetRolesAsync(user);

            List<Claim> userClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            userClaims.AddRange(roles.Select(x => new Claim(JwtClaims.Role, x)));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SigningKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(userClaims),
                Audience = _authOptions.Audience,
                Issuer = _authOptions.Issuer,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_authOptions.TokenExpiryMinutes),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(jwtToken);
        }
    }
}
