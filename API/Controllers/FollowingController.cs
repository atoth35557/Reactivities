using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Followigs.Commands;
using Application.Followigs.Queries;
using Application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("api/profiles")]
    public class FollowingController : BaseController {

        [HttpPost ("{username}/follow")]
        public async Task<ActionResult<Unit>> Follow (string username) {
            return await Mediator.Send (new Add.Command {
                Username = username
            });
        }

        [HttpDelete ("{username}/follow")]
        public async Task<ActionResult<Unit>> DeleteFollowing (string username) {
            return await Mediator.Send (new Delete.Command {
                Username = username
            });
        }

        [HttpGet ("{username}/follow")]
        public async Task<ActionResult<List<Profile>>> GetFollowings (string username, string predicate) {
            return await Mediator.Send (new List.Query {
                Username = username, Predicate = predicate
            });
        }

    }
}