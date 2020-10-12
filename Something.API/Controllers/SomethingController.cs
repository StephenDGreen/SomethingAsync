using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Something.Application;
using System.Threading.Tasks;

namespace Something.API.Controllers
{
    [Authorize]
    [ApiController]
    public class SomethingController : ControllerBase
    {

        private readonly ISomethingCreateInteractor createInteractor;
        private readonly ISomethingReadInteractor readInteractor;

        public SomethingController(ISomethingCreateInteractor createInteractor, ISomethingReadInteractor readInteractor)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
        }

        [HttpPost]
        [Route("api/things")]
        public async Task<ActionResult> CreateAsync([FromForm] string name)
        {
            if (name.Length < 1)
                return await GetAllAsync();

            createInteractor.CreateSomething(name);
            return await GetAllAsync();
        }

        [HttpGet]
        [Route("api/things")]
        public async Task<ActionResult> GetListAsync()
        {
            return await GetAllAsync();
        }
        private async Task<ActionResult> GetAllAsync()
        {
            var result = await readInteractor.GetSomethingListAsync();
            return Ok(result);
        }
    }
}
