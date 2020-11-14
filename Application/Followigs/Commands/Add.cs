using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followigs.Commands {
    public class Add {
        public class Command : IRequest {
            public string Username {
                get;
                set;
            }
        }
        public class Handler : IRequestHandler<Command> {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler (DataContext context, IUserAccessor userAccessor) {
                _userAccessor = userAccessor;
                _context = context;

            }
            public async Task<Unit> Handle (Command request, CancellationToken cancellationToken) {
                var observer = await _context.Users
                    .SingleOrDefaultAsync (u => u.UserName == _userAccessor.GetCurrentUsername ());
                var target = await _context.Users
                    .SingleOrDefaultAsync (u => u.UserName == request.Username);
                if (target == null) {
                    throw new RestException (System.Net.HttpStatusCode.NotFound,
                        new {
                            target = "Following target was not found!"
                        });
                }
                var following = await _context.Followings
                    .SingleOrDefaultAsync (f => f.ObserverId == observer.Id && f.TargetId == target.Id);
                if (following != null) {
                    throw new RestException (System.Net.HttpStatusCode.BadRequest,
                        new {
                            following = "You already following this user"
                        });
                }
                if (following == null) {
                    following = new UserFollowing {
                        ObserverId = observer.Id,
                        Observer = observer,
                        TargetId = target.Id,
                        Target = target
                    };
                }

                _context.Followings.Add (following);

                var success = await _context.SaveChangesAsync () > 0;

                if (success) return Unit.Value;

                throw new Exception ("An exception occurred on save");
            }
        }
    }
}