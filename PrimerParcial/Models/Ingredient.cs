using System.ComponentModel.DataAnnotations;

namespace PrimerParcial.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [StringLength(60)]
        public string? Unit { get; set; } // g, ml, taza, unidad, etc.

        [Range(0.0, 100000.0)]
        public decimal Quantity { get; set; }

        // FK
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
