using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities {
    public class List {
        public class ActivitiesEnvelope {
            public List<ActivityDto> Activities {
                get;
                set;
            }
            public int ActivityCount {
                get;
                set;
            }
        }
        public class Query : IRequest<ActivitiesEnvelope> {
            public Query (int? limit, int? offset, bool isGoind, bool isHost, DateTime? startDate) {
                StartDate = startDate ?? DateTime.Now;
                IsHost = isHost;
                IsGoind = isGoind;
                Limit = limit;
                Offset = offset;

            }
            public int? Limit {
                get;
                set;
            }
            public int? Offset {
                get;
                set;
            }
            public bool IsGoind {
                get;
                set;
            }
            public bool IsHost {
                get;
                set;
            }
            public DateTime? StartDate {
                get;
                set;
            }
        }

        public class Handler : IRequestHandler<Query, ActivitiesEnvelope> {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler (DataContext context, IMapper mapper, IUserAccessor userAccessor) {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;

            }
            public async Task<ActivitiesEnvelope> Handle (Query request, CancellationToken cancellationToken) {
                var queryable = _context.Activities
                    .Where (q => q.Date >= request.StartDate)
                    .OrderBy (q => q.Date)
                    .AsQueryable ();

                if (request.IsGoind && !request.IsHost) {
                    queryable = queryable.Where (q => q.UserActivities
                        .Any (u => u.AppUser.UserName == _userAccessor.GetCurrentUsername ()));
                }

                if (request.IsHost && !request.IsGoind) {
                    queryable = queryable.Where (q => q.UserActivities
                        .Any (u => u.AppUser.UserName == _userAccessor.GetCurrentUsername () && u.IsHost));
                }

                var activities = await queryable
                    .Skip (request.Offset ?? 0)
                    .Take (request.Limit ?? 3)
                    .ToListAsync ();

                return new ActivitiesEnvelope {
                    Activities = _mapper.Map<List<Activity>, List<ActivityDto>> (activities),
                        ActivityCount = queryable.Count ()
                };
            }
        }
    }
}