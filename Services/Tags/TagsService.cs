using recipe_shuffler.Data;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services.Tags
{
    public class TagsService : ITagsService
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;

        public TagsService(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        public IQueryable<TagList> GetList()
        {
            IQueryable<Tag> query = _context.Tags
                .Where(x => x.UserId == _usersService.GetCurrentUserId());

            IQueryable<TagList> list = query
                .Select(x => new TagList()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color,
                });

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

        public async Task<bool> Delete(Guid id)
        {
            bool result = false;

            Tag tag = _context.Tags.
                Where(x => x.UserId == _usersService.GetCurrentUserId())
                .FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                _context.Remove(tag);
                await _context.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        private Tag ConvertEditToDbObj(TagEdit model)
        {
            Tag tag = new()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                User = _context.Users.Find(_usersService.GetCurrentUserId())
            };

            return tag;
        }
    }
}
