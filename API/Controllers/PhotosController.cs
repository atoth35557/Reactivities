using System.Threading.Tasks;
using Application.Activities.Commands;
using Application.Photos.UseCase;
using Application.Profiles.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class PhotosController : BaseController {
        [HttpPost]
        public async Task<ActionResult<Photo>> PostTModel ([FromForm] Add.Command command) {
            return await Mediator.Send (command);
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<Unit>> DeleteUnitById (string id) {
            return await Mediator.Send (new Delete.Command {
                Id = id
            });
        }

        [HttpPost ("{id}/setmain")]
        public async Task<ActionResult<Unit>> PostTModel (string id) {
            return await Mediator.Send (new SetMain.Command {
                Id = id
            });
        }

    }
}