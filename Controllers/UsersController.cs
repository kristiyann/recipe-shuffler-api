using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.DTO;
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

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        // [HttpGet]
        // public IActionResult Get(Guid id)
        // {
        //     if (id != Guid.Empty && id != default )
        //     {
        //         IQueryable<UserList> user = _service.Get(id);
        //         return Ok(user);
        //     }
        //     else return BadRequest("Invalid parameters");
        // }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Insert(User model)
        {
            Guid userId = await _service.Insert(model);
            return Ok(userId);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UserEdit model)
        {
            Guid userId = await _service.Update(model);
            return Ok(userId);
        }

        // [HttpPut]
        // [Route("UpdatePassword")]
        // public async Task<IActionResult> UpdatePassword(UserPasswordEdit model)
        // {
        //     Guid userId = await _service.UpdatePassword(model);
        //
        //     if (userId != Guid.Empty)
        //     {
        //         return Ok(userId);
        //     }
        //     else return NotFound();
        // }

        [HttpPost]
        [Route("Auth")]
        [AllowAnonymous]
        public IActionResult UserAuth(UserAuth userAuth)
        {
            string token = _service.UserAuth(userAuth.Email, userAuth.Password);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            else return Unauthorized("Wrong email or password");
        }
    }
}
