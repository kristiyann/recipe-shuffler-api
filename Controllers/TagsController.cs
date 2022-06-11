using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Services.Tags;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ODataController
    {
        private readonly ITagsService _service;

        public TagsController(ITagsService service)
        {
            _service = service;
        }

        [HttpGet]
        // [EnableQuery]
        [Authorize]
        public IActionResult GetTagList(Guid userId)
        {
            if (userId != Guid.Empty && userId != default)
            {
                IQueryable list = _service.GetList(userId);

                return Ok(list);
            }
            else return BadRequest("Invalid parameters");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert(TagEdit model)
        {
            return Ok(await _service.Insert(model));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(TagEdit model)
        {
            return Ok(await _service.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty && id != default)
            {
                return Ok(await _service.Delete(id));
            }
            else return BadRequest("Invalid parameters");
        }
    }
}
