﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using recipe_shuffler.DataTransferObjects.Recipes;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
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

        //[HttpGet]
        //[EnableQuery()]
        //[Authorize]
        //public IActionResult GetRecipeList(ODataQueryOptions<Recipe> queryOptions)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    IQueryable<RecipeList> list = _service.GetList();
        //    return Ok(list);
        //}

        [HttpGet]
        [Route("GetRandom")]
        [Authorize]
        public IActionResult GetRandom([FromQuery] RecipeCustomFilter customFilter = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(_service.GetRandom(customFilter));

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert(RecipeInsert model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _service.Insert(model));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(RecipeEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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
                bool result = await _service.Delete(id);

                if (result)
                {
                    return Ok(result);
                }
                else return BadRequest("Invalid parameters");
            }
            else
            {
                return BadRequest("Invalid parameters");
            }
        }
    }
}
