using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Something.Application;
using System.Threading.Tasks;

namespace Something.API.Controllers
{
    [Authorize]
    [ApiController]
    public class ElseController : ControllerBase
    {
        private readonly ISomethingElseCreateInteractor createInteractor;
        private readonly ISomethingElseReadInteractor readInteractor;
        private readonly ISomethingElseUpdateInteractor updateInteractor;
        private readonly ISomethingElseDeleteInteractor deleteInteractor;

        public ElseController(ISomethingElseCreateInteractor createInteractor, ISomethingElseReadInteractor readInteractor, ISomethingElseUpdateInteractor updateInteractor, ISomethingElseDeleteInteractor deleteInteractor)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
            this.updateInteractor = updateInteractor;
            this.deleteInteractor = deleteInteractor;
        }
        [HttpPost]
        [Route("api/thingselse")]
        public async Task<IActionResult> CreateElseAsync([FromForm] string name, [FromForm] string[] othername)
        {
            if (name.Length < 1)
                return await GetAllSomethingElseIncludeSomethingAsync();

            await createInteractor.CreateSomethingElseAsync(name, othername);
            return await GetAllSomethingElseIncludeSomethingAsync();
        }
        [HttpPut]
        [Route("api/thingselse/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromForm] string othername)
        {
            if (id < 1)
                return await GetAllSomethingElseIncludeSomethingAsync();

            await updateInteractor.UpdateSomethingElseAddSomethingAsync(id, othername);
            return await GetAllSomethingElseIncludeSomethingAsync();
        }
        [HttpDelete]
        [Route("api/thingselse/{else_id}/{something_id}")]
        public async Task<ActionResult> DeleteAsync(int else_id, int something_id)
        {
            if (else_id < 1 || something_id < 1)
                return await GetAllSomethingElseIncludeSomethingAsync();

            await updateInteractor.UpdateSomethingElseDeleteSomethingAsync(else_id, something_id);
            return await GetAllSomethingElseIncludeSomethingAsync();
        }
        [HttpDelete]
        [Route("api/thingselse/{else_id}")]
        public async Task<ActionResult> DeleteAsync(int else_id)
        {
            if (else_id < 1)
                return await GetAllSomethingElseIncludeSomethingAsync();

            await deleteInteractor.DeleteSomethingElseAsync(else_id);
            return await GetAllSomethingElseIncludeSomethingAsync();
        }
        [HttpGet]
        [Route("api/thingselse")]
        public async Task<ActionResult> GetElseListAsync()
        {
            return await GetAllSomethingElseIncludeSomethingAsync();
        }
        private async Task<ActionResult> GetAllSomethingElseIncludeSomethingAsync()
        {
            var result = await readInteractor.GetSomethingElseIncludingSomethingsListAsync();
            return Ok(result);
        }
    }
}
