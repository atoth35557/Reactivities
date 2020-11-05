using System.Threading.Tasks;
using Application.Photos.UseCase;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Photo>> PostTModel([FromForm] Add.Command command)
        {
            return await Mediator.Send(command);
        }
        
    }
}