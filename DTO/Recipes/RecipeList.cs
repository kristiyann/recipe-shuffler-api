﻿using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Enums;

namespace recipe_shuffler.DTO.Recipes
{
    public class RecipeList
    {
        public Guid Id { get; set; }

        public string? Title { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Ingredients { get; set; } = string.Empty;

        public string? Instructions { get; set; } = string.Empty;

        public IEnumerable<TagList>? Tags { get; set; }

        public uint Calories { get; set; }

        public uint Protein { get; set; }

        public Category Category { get; set; }
    }
}
