using Microsoft.AspNetCore.OData.Query;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services.Tags
{
    public interface ITagsService
    {
        IQueryable GetList(ODataQueryOptions<Tag> queryOptions, Guid userId);

        Task<Tag> Insert(Tag model);

        Task<Tag> Update(Tag model);

        Task<Tag> Delete(Guid id);
    }
}
