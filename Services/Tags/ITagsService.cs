﻿using Microsoft.AspNetCore.OData.Query;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services.Tags
{
    public interface ITagsService
    {
        IQueryable GetList(ODataQueryOptions<Tag> queryOptions, Guid userId);

        Task<Tag> Insert(TagInsert model);

        Task<Tag> Update(TagInsert model);

        Task<Tag> Delete(Guid id);
    }
}