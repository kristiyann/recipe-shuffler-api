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

        public IQueryable<Tag> GetList(Guid userId)
        {
            IQueryable<Tag> query = _context.Tags
                .Where(x => x.UserId == userId);
            
            IQueryable<Tag> list = query
                .Select(x => new Tag()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color,
                    UserId = x.UserId,
                });

            // list = queryOptions.ApplyTo(list);

            return list;
        }

        public async Task<Tag> Insert(TagEdit model)
        {
            Tag tag = ConvertEditToDbObj(model);
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> Update(TagEdit model)
        {
            Tag tag = ConvertEditToDbObj(model);
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

        private Tag ConvertEditToDbObj(TagEdit model)
        {
            Tag tag = new()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                User = _context.Users.Find(model.UserId)
            };

            return tag;
        }

        public async Task<Tag> GetById(Guid id)
        {
            Tag tag = await _context.Tags.FindAsync(id);

            return tag;
        }
    }
}
