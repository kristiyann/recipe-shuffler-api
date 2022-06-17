using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services.Tags
{
    public interface ITagsService
    {
        IQueryable<TagList> GetList();

        Task<Tag> Insert(TagEdit model);

        Task<Tag> Update(TagEdit model);

        Task<bool> Delete(Guid id);
    }
}
