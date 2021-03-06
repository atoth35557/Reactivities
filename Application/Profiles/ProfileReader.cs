using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles {
    public class ProfileReader : IProfileReader {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public ProfileReader (DataContext context, IUserAccessor userAccessor) {
            _userAccessor = userAccessor;
            _context = context;
        }

        public async Task<Profile> ReadProfile (string username) {
            var user = await _context.Users.SingleOrDefaultAsync (u => u.UserName == username);

            if (user == null) {
                throw new RestException (System.Net.HttpStatusCode.NotFound, new {
                    user = "User with this username was not found"
                });
            }
            var currentUser = await _context.Users.SingleOrDefaultAsync (u => u.UserName == _userAccessor.GetCurrentUsername ());

            var profile = new Profile {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Image = user.Photos.FirstOrDefault (p => p.IsMain)?.Url,
                Photos = user.Photos,
                Bio = user.Bio,
                FollowersCount = user.Followers.Count (),
                FollowingsCount = user.Followings.Count ()
            };

            if (currentUser.Followings.Any (u => u.TargetId == user.Id)){
                profile.IsFolowed = true;
            }
            return profile;
        }
    }
}