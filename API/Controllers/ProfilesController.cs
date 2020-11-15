using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profiles;
using Application.Profiles.Commands;
using Application.Profiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class ProfilesController : BaseController {
        [HttpGet("{username}")]
        public async Task<ActionResult<Profile>> Get(string username)
        {
            return await Mediator.Send(new Details.Query{UserName = username});
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Update(Edit.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{username}/activities")]
        public async Task<ActionResult<List<UserActivityDto>>> getActivities(string username, string predicate)
        {
            return await Mediator.Send(new ActivityList.Query{Username =username, Predicate = predicate});
        }
        
        
    }
}