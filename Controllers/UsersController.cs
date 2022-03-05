using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.Data;
using recipe_shuffler.Models;
using recipe_shuffler.Services;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            Users user = _service.Get(id);
            return Ok(user);
        }

    }
}
