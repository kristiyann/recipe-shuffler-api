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

        //[HttpGet]
        //[Authorize]
        //public IActionResult GetTagList()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    IQueryable list = _service.GetList();

        //    return Ok(list);
        //}

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert(TagEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _service.Insert(model));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(TagEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _service.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != Guid.Empty && id != default)
            {
                bool result = await _service.Delete(id);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid parameters");
                }
            }
            else
            {
                return BadRequest("Invalid parameters");
            }
        }
    }
}
