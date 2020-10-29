using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Validators;
using Domain;
using FluentValidation;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User {
    public class Register {
        public class Command : IRequest<User> {
            public string DisplayName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command> {
            public CommandValidator () {
                RuleFor (x => x.DisplayName).NotEmpty ();
                RuleFor (x => x.UserName).NotEmpty ();
                RuleFor (x => x.Email).NotEmpty () . EmailAddress();
                RuleFor (x => x.Password).Password();
            }
        }
        public class Handler : IRequestHandler<Command, User> {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Handler (DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator) {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _context = context;

            }
            public async Task<User> Handle (Command request, CancellationToken cancellationToken) {

                if (await _context.Users.AnyAsync (u => u.Email == request.Email)) {
                    throw new RestException (HttpStatusCode.BadRequest, new { Email = "User with this email already exist" });
                }

                if (await _context.Users.AnyAsync (u => u.UserName == request.UserName)) {
                    throw new RestException (HttpStatusCode.BadRequest, new { UserName = "User with this username already exist" });
                }

                var user = new AppUser {
                    DisplayName = request.DisplayName,
                    UserName = request.UserName,
                    Email = request.Email,
                };

                var result = await _userManager.CreateAsync (user, request.Password);

                if (result.Succeeded) {
                    return new User{
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user),
                        UserName = user.UserName,
                        Image = null
                    };
                }

                throw new Exception ("An exception occurred creating user");
            }
        }
    }
}