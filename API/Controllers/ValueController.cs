using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        public ValueController()
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTModels()
        {
            return new string [] {"value1","value2"};
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetTModelById(int id)
        {
            return "value";
        }

        // [HttpPost("")]
        // public async Task<ActionResult<TModel>> PostTModel(TModel model)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return null;
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTModel(int id, TModel model)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult<TModel>> DeleteTModelById(int id)
        // {
        //     // TODO: Your code here
        //     await Task.Yield();

        //     return null;
        // }
    }
}