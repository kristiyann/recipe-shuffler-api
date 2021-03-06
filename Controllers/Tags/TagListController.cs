using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Services.Tags;

namespace recipe_shuffler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TagListController : ODataController
    {
        private readonly ITagsService _service;

        public TagListController(ITagsService service)
        {
            _service = service;
        }

        [EnableQuery(EnsureStableOrdering = false)]
        [Authorize]
        public IActionResult GetTagList(ODataQueryOptions<TagList> queryOptions, [FromQuery] TagCustomFilter? customFilter = null)
        {
            IQueryable<TagList> list = _service.GetList(customFilter);

            return Ok(list);
        }
    }
}
