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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMyId()
        {
            Guid userId = _service.GetMyId();
            return Ok(userId);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Insert(User model)
        {
            Guid userId = await _service.Insert(model);
            return Ok(userId);
        }

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
