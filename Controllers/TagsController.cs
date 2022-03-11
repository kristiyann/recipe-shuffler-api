using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;
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
        [EnableQuery]
        public IActionResult GetList(ODataQueryOptions<Tag> queryOptions, Guid userId)
        {
            if (userId != Guid.Empty && userId != default)
            {
                IQueryable list = _service.GetList(queryOptions, userId);

                return Ok(list);
            }
            else return BadRequest("Invalid parameters");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TagInsert model)
        {
            return Ok(await _service.Insert(model));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TagInsert model)
        {
            return Ok(await _service.Update(model));
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty && id != default)
            {
                return Ok(_service.Delete(id));
            }
            else return BadRequest("Invalid parameters");
        }
    }
}
