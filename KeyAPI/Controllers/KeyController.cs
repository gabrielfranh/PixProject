using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyController : ControllerBase
    {
        [HttpGet]
        [Route("{userId}")]
        public ActionResult<int> GetKeyByUserId([FromRoute] int userId)
        {
            return Ok(userId);
        }
    }
}
