using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.Data;
using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;
using recipe_shuffler.Services;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsersController : Controller
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty && id != default )
            {
                IQueryable<UserList> user = _service.Get(id);
                return Ok(user);
            }
            else return BadRequest("Invalid parameters");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(User model)
        {
            User user = await _service.Insert(model);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserEdit model)
        {
            User user = await _service.Update(model);
            return Ok(user);
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UserPasswordEdit model)
        {
            Guid userId = await _service.UpdatePassword(model);

            return Ok(userId);
        }

        [HttpGet]
        [Route("Auth")]
        public IActionResult UserAuth(string email, string password)
        {
            Guid userId = _service.UserAuth(email, password); 

             if (userId != Guid.Empty)
             {
                 return Ok(userId);
             }
             else return Unauthorized();
        }
    }
}
