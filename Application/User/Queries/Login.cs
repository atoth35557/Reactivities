using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.User {
    public class Login {
        public class Query : IRequest<User> {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class QueryValidator : AbstractValidator<Query> {
            public QueryValidator () {
                RuleFor (x => x.Email).NotEmpty ();
                RuleFor (x => x.Password).NotEmpty ();
            }
        }
        public class Handler : IRequestHandler<Query, User> {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJwtGenerator _generator;
            public Handler (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator generator) {
                _generator = generator;
                _signInManager = signInManager;
                _userManager = userManager;

            }
            public async Task<User> Handle (Query request, CancellationToken cancellationToken) {
                var user = await _userManager.FindByEmailAsync (request.Email);

                if (user == null) {
                    throw new RestException (HttpStatusCode.Unauthorized);
                }

                var result = await _signInManager.CheckPasswordSignInAsync (user, request.Password, false);

                if (result.Succeeded) {
                    return new User {
                        DisplayName = user.DisplayName,
                            Token = _generator.CreateToken(user),
                            UserName = user.UserName,
                            Image = user.Photos.FirstOrDefault(p => p.IsMain)?.Url
                    };
                }

                throw new RestException (HttpStatusCode.Unauthorized);
            }
        }
    }
}