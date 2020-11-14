using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries {
    public class Details {
        public class Query : IRequest<Profile> {
            public string UserName {
                get;
                set;
            }
        }

        public class Handler : IRequestHandler<Query, Profile> {
            private readonly IProfileReader _reader;
            public Handler (IProfileReader reader) {
                _reader = reader;
            }
            public async Task<Profile> Handle (Query request, CancellationToken cancellationToken) {
                return await _reader.ReadProfile(request.UserName);
            }
        }
    }
}