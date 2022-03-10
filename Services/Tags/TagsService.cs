using Microsoft.AspNetCore.OData.Query;
using recipe_shuffler.Data;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services.Tags
{
    public class TagsService : ITagsService
    {
        private readonly DataContext _context;

        public TagsService(DataContext context)
        {
            _context = context;
        }

        public IQueryable GetList(ODataQueryOptions<Tag> queryOptions, Guid userId)
        {
            IQueryable list = _context.Tags
                .Where(x => x.UserId == userId)
                .Select(x => new Tag()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color,
                });

            list = queryOptions.ApplyTo(list);

            return list;
        }

        public async Task<Tag> Insert(TagInsert model)
        {
            Tag tag = ConvertToModel(model);
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> Update(TagInsert model)
        {
            Tag tag = ConvertToModel(model);
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> Delete(Guid id)
        {
            Tag? tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            _context.Remove(tag);

            await _context.SaveChangesAsync();

            return tag;
        }

        public Tag ConvertToModel(TagInsert model)
        {
            Tag tag = new();

            tag.Id = model.Id;
            tag.Name = model.Name;
            tag.Color = model.Color;
            tag.User = _context.Users.Find(model.UserId);

            return tag;
        }
    }
}
