using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimerParcial.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        // Navegación
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
