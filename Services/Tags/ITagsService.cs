using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services.Tags
{
    public interface ITagsService
    {
        IQueryable<Tag> GetList(Guid userId);

        Task<Tag> Insert(TagEdit model);

        Task<Tag> Update(TagEdit model);

        Task<Tag> Delete(Guid id);

        Task<Tag> GetById(Guid id);
    }
}
