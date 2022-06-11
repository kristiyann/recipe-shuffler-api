﻿namespace recipe_shuffler.DTO
{
    public class RecipeInsert
    {
        public string? Title { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Ingredients { get; set; } = string.Empty;

        public string? Instructions { get; set; } = string.Empty;

        public IEnumerable<Guid>? TagIds { get; set; }
    }
}
