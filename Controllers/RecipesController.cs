using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.DTO;
using recipe_shuffler.Models;
using recipe_shuffler.Services;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using recipe_shuffler.DTO.Tags;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [ApiExplorerSettings(IgnoreApi = true)]
    public class RecipesController : ODataController
    {
        private readonly IRecipesService _service;

        public RecipesController(IRecipesService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery()]
        public IActionResult GetRecipeList(ODataQueryOptions<Recipe> queryOptions, Guid userId) 
        {
            if (userId != Guid.Empty && userId != default)
            {
                IQueryable list = _service.GetList(userId);
                
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
                return Ok(_service.GetRandom(userId));
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

        [HttpPut]
        [Route("InsertTag")]
        public async Task<IActionResult> InsertTag(TagInsertIntoRecipe model)
        {
            return Ok(await _service.InsertTag(model));
        }

        [HttpPut]
        [Route("RemoveTag")]
        public async Task<IActionResult> RemoveTag(TagInsertIntoRecipe model)
        {
            return Ok(await _service.RemoveTag(model));
        }

        [HttpDelete]
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
