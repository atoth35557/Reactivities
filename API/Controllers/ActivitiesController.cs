using System;
using System.Threading.Tasks;
using Application.Activities;
using Application.Activities.UseCase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Activities.List;

namespace API.Controllers {
    public class ActivitiesController : BaseController {

        [HttpGet]
        public async Task<ActionResult<ActivitiesEnvelope>> GetActivities (
            int? limit, int? offset, bool isGoing, bool isHost, DateTime? startDate
        ) {
            return await Mediator.Send (
                new List.Query (limit, offset, isGoing, isHost, startDate));
        }

        [HttpGet ("{id}")]
        [Authorize]
        public async Task<ActionResult<ActivityDto>> GetActivity (Guid id) {
            return await Mediator.Send (new Detail.Query {
                Id = id
            });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create (Create.Command command) {
            return await Mediator.Send (command);
        }

        [HttpPost ("{id}/attend")]
        public async Task<ActionResult<Unit>> Attend (Guid id) {
            return await Mediator.Send (new Attend.Command {
                Id = id
            });
        }

        [HttpDelete ("{id}/attend")]
        public async Task<ActionResult<Unit>> CancelAttendence (Guid id) {
            return await Mediator.Send (new CancelAttendance.Command {
                Id = id
            });
        }

        [HttpPut ("{id}")]
        [Authorize (Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit (Guid id, Edit.Command command) {
            command.Id = id;
            return await Mediator.Send (command);
        }

        [HttpDelete ("{id}")]
        [Authorize (Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Delete (Guid id) {
            return await Mediator.Send (new Delete.Command {
                Id = id
            });
        }

    }
}