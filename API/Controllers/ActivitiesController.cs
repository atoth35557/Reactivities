using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase {
        private readonly IMediator _mediator;
        public ActivitiesController (IMediator mediator) {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivity () {
            return await _mediator.Send (new List.Query ());
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Activity>> GetActivity (Guid id) {
            return await _mediator.Send (new Detail.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create (Create.Command command) {
            return await _mediator.Send (command);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Unit>> Edit (Guid id, Edit.Command command) {
            command.Id = id;
            return await _mediator.Send (command);
        }

    }
}