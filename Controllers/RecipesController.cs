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
        public IActionResult GetList([FromODataUri] Guid userId) 
        {
            if (userId != Guid.Empty && userId != default)
            {
                List<Recipes> list = _service.GetList(userId);

                return this.Ok(list);
            }
            else return BadRequest("Invalid parameters");
        }

        [HttpGet]
        [Route("GetRandom")]
        public IActionResult GetRandom(Guid userId)
        {
            if (userId != Guid.Empty && userId != default)
            {
                Recipes? recipe = _service.GetRandom(userId);

                return this.Ok(recipe);
            }
            else return BadRequest("Invalid parameters");
        }


        [HttpPost]
        public async Task<IActionResult> Insert(RecipeInsertModel model) 
        { 
            return this.Ok(await _service.Insert(model));
        }

        [HttpPut]
        public async Task<IActionResult> Update(RecipeInsertModel model)
        {
            return this.Ok(await _service.Update(model));
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty && id != default)
            {
                return this.Ok(_service.Delete(id));
            }
            else return BadRequest("Invalid parameters");
        }
    }
}
