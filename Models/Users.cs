﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace recipe_shuffler.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public IEnumerable<Recipes>? Recipes { get; set; }

        public bool Active { get; set; } = true;
    }
}
