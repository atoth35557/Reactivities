using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.UseCase {
    public class Attend {
        public class Command : IRequest {
            public Guid Id {
                get;
                set;
            }
        }
        public class Handler : IRequestHandler<Command> {
            private readonly DataContext _context;
            private readonly IUserAccessor _accessor;
            public Handler (DataContext context, IUserAccessor accessor) {
                _accessor = accessor;
                _context = context;

            }
            public async Task<Unit> Handle (Command request, CancellationToken cancellationToken) {
                var activity = await _context.Activities.FindAsync (request.Id);

                if (activity == null) {
                    throw new RestException (HttpStatusCode.NotFound, new {
                        error = "Activity not found "
                    });
                }

                var user = await _context.Users.SingleOrDefaultAsync (u => u.UserName == _accessor.GetCurrentUsername ());

                var attendance = await _context
                    .UserActivities.SingleOrDefaultAsync (a => a.ActivityId == activity.Id && a.AppUserId == user.Id);

                if (attendance != null) {
                    throw new RestException (HttpStatusCode.BadRequest,
                        new {
                            Attendence = "Allready attending this activity"
                        });
                }

                attendance = new UserActivity {
                    Activity = activity,
                    AppUser = user,
                    IsHost = false,
                    DateJoined = DateTime.Now
                };

                _context.UserActivities.Add(attendance);

                var success = await _context.SaveChangesAsync () > 0;

                if (success) return Unit.Value;

                throw new Exception ("An exception occurred on save");
            }
        }
    }
}