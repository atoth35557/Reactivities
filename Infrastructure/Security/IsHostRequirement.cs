using System;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement { }
    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _accessor;
        public IsHostRequirementHandler(IHttpContextAccessor accessor,
                                        DataContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var currentUserName = _accessor.HttpContext.User?.Claims?
                                           .SingleOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            var activityId = Guid.Parse(_accessor.HttpContext.Request.RouteValues
                                                 .SingleOrDefault(i => i.Key == "id").Value.ToString());
            var activity = _context.Activities.FindAsync(activityId).Result;
            
            var host = _context.UserActivities.FirstOrDefault(u => u.IsHost);

            if( host?.AppUser?.UserName == currentUserName){
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}