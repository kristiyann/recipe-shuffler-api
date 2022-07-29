using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.Services;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RecipeListController : ODataController
    {
        private readonly IRecipesService _service;

        public RecipeListController(IRecipesService service)
        {
            _service = service;
        }

        [EnableQuery(EnsureStableOrdering = false)]
        [Authorize]
        public IActionResult GetRecipeList(ODataQueryOptions<RecipeList> queryOptions, bool shuffle = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IQueryable<RecipeList> list = _service.GetList(shuffle);
            return Ok(list);
        }
    }
}
