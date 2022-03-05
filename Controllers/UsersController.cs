using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.Data;
using recipe_shuffler.DTO.Users;
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
            if (id != Guid.Empty)
            {
                IQueryable<UserReturnModel> user = _service.Get(id);
                return Ok(user);
            }
            else return BadRequest("Invalid parameters");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Users model)
        {
            Users user = await _service.Insert(model);
            return this.Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateModel model)
        {
            Users user = await _service.Update(model);
            return this.Ok(user);
        }

        [HttpPut]
        [Route("ChangeActive")]
        public IActionResult ChangeActive([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Users user = _service.ChangeActive(id);
                return Ok(user);
            }
            else return BadRequest("Invalid parameters");
        }

    }
}
