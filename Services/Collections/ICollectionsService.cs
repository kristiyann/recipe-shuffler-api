using recipe_shuffler.DataTransferObjects;

namespace recipe_shuffler.Services
{
    public interface ICollectionsService
    {
        IQueryable<CollectionList> GetList();
        Task<Guid> Insert(CollectionEdit toInsert);
    }
}
