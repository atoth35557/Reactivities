using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Errors;
using System.Net;

namespace Application.Profiles.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
                _context = context;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == _userAccessor.GetCurrentUsername());
                var photo = user.Photos.FirstOrDefault(p => p.Id == request.Id);

                if(photo == null) {
                    throw new RestException(HttpStatusCode.NotFound, new {Photo = "Photo with this id was not found"});
                }

                if (photo.IsMain) {
                    throw new RestException(HttpStatusCode.BadRequest, new { Photo = "You cannot delete your main photo"});
                }

                var result = _photoAccessor.DeletePhoto(photo.Id);

                if (result == null) {
                    throw new Exception("Failed delete the photo from cloudinary");
                }

                user.Photos.Remove(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("An exception occurred on save");
            }
        }
    }
}