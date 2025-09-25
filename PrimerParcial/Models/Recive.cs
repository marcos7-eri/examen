using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimerParcial.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 1000)]
        public int PreparationTimeMinutes { get; set; }

        [Range(1, 100)]
        public int Servings { get; set; }

        [Required]
        public string Instructions { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // FK
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Navegación
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
