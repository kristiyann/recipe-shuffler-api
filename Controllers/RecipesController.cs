using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;
using recipe_shuffler.Services;

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
        [Authorize]
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
        [Authorize]
        public IActionResult GetRandom(Guid userId)
        {
            if (userId != Guid.Empty && userId != default)
            {
                return Ok(_service.GetRandom(userId));
            }
            else return BadRequest("Invalid parameters");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert(RecipeInsert model)
        {
            return Ok(await _service.Insert(model));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(RecipeEdit model)
        {
            return Ok(await _service.Update(model));
        }

        //[HttpPut]
        //[Route("InsertTag")]
        //public async Task<IActionResult> InsertTag(TagInsertIntoRecipe model)
        //{
        //    return Ok(await _service.InsertTag(model));
        //}

        //[HttpPut]
        //[Route("RemoveTag")]
        //public async Task<IActionResult> RemoveTag(TagInsertIntoRecipe model)
        //{
        //    return Ok(await _service.RemoveTag(model));
        //}

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
