using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.UseCase
{
    public class CancelAttendance
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _accessor;
            public Handler(DataContext context, IUserAccessor accessor)
            {
                _accessor = accessor;
                _context = context;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if( activity == null){
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Activity was not found"} );
                }

                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == _accessor.GetCurrentUsername());

                var attendace = await  _context.UserActivities
                                                .SingleOrDefaultAsync(x => x.AppUserId == user.Id && x.ActivityId == activity.Id);
                if(attendace == null) {
                    throw new RestException(HttpStatusCode.BadRequest, new  { Attendence = "User was not attending this activity"});
                }
                if(attendace.IsHost) {
                    throw new RestException(HttpStatusCode.BadRequest, new  { Attendence = "User cant cancel attendance when he is a host"});
                }

                _context.UserActivities.Remove(attendace);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("An exception occurred on save");
            }
        }
    }
}