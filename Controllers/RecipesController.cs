using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.DTO;
using recipe_shuffler.Models;
using recipe_shuffler.Services;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Formatter;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ODataController
    {
        private readonly IRecipesService _service;

        public RecipesController(IRecipesService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetList(ODataQueryOptions<Recipe> queryOptions, Guid userId) 
        {
            if (userId != Guid.Empty && userId != default)
            {
                IQueryable list = _service.GetList(queryOptions, userId);
                
                return Ok(list);
            }
            else return BadRequest("Invalid parameters");
        }

        [HttpGet]
        [Route("GetRandom")]
        public IActionResult GetRandom(Guid userId)
        {
            if (userId != Guid.Empty && userId != default)
            {
                Recipe? recipe = _service.GetRandom(userId);

                return Ok(recipe);
            }
            else return BadRequest("Invalid parameters");
        }


        [HttpPost]
        public async Task<IActionResult> Insert(RecipeInsert model) 
        { 
            return Ok(await _service.Insert(model));
        }

        [HttpPut]
        public async Task<IActionResult> Update(RecipeInsert model)
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
