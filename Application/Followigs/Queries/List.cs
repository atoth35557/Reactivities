using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Profiles;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followigs.Queries {
    public class List {
        public class Query : IRequest<List<Profile>> {
            public string Username {
                get;
                set;
            }
            public string Predicate {
                get;
                set;
            }
        }

        public class Handler : IRequestHandler<Query, List<Profile>> {
            private readonly DataContext _context;
            private readonly IProfileReader _reader;
            public Handler (DataContext context, IProfileReader reader) {
                _reader = reader;
                _context = context;

            }
            public async Task<List<Profile>> Handle (Query request, CancellationToken cancellationToken) {
                var queryable = _context.Followings.AsQueryable ();

                var userFollowings = new List<UserFollowing> ();
                var profiles = new List<Profile> ();

                switch (request.Predicate) {
                    case "followers":
                        {
                            userFollowings = await queryable.Where (f => f.Target.UserName == request.Username).ToListAsync ();

                            foreach(var follower in userFollowings){
                                profiles.Add(await _reader.ReadProfile(follower.Observer.UserName));
                            }
                            break;
                        }
                    case "following":
                        {
                            userFollowings = await queryable.Where (f => f.Observer.UserName == request.Username).ToListAsync ();

                            foreach(var follower in userFollowings){
                                profiles.Add(await _reader.ReadProfile(follower.Target.UserName));
                            }
                            break;
                        }
                }
                return profiles;
            }
        }
    }
}