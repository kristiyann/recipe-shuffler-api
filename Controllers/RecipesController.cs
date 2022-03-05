using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_shuffler.Data;
using recipe_shuffler.DTO;
using recipe_shuffler.Models;
using recipe_shuffler.Services;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : Controller
    {
        private readonly IRecipesService _service;

        public RecipesController(IRecipesService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetList(Guid userId) 
        {
            if (userId != Guid.Empty)
            {
                List<Recipes> list = _service.GetList(userId);

                return this.Ok(list);
            }
            else return this.NotFound();
        }

        
        [HttpPost]
        public IActionResult Insert(RecipeInsertModel model) 
        { 
            return this.Ok(_service.Insert(model));
        }
    }
}
