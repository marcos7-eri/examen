using Microsoft.EntityFrameworkCore;
using PrimerParcial.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PrimerParcial.Data
{
    public class RecetasDBContext : DbContext
    {
        // Constructor que acepta DbContextOptions para la configuración
        public RecetasDBContext(DbContextOptions<RecetasDBContext> options)
            : base(options)
        {
        }

        // DbSets (Colecciones) que mapean a las tablas de la base de datos
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Opcional: Configuración de la relación uno a muchos
        // Esto a menudo se puede omitir si las convenciones de EF Core se cumplen,
        // pero es buena práctica para claridad, especialmente en relaciones complejas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nombres de tablas explícitos (opcional, útil para claridad)
            modelBuilder.Entity<Recipe>().ToTable("Recipes");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Category>().ToTable("Categories");

            // Configura la relación uno a muchos entre Recipe e Ingredient
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe) // Un ingrediente tiene una receta
                .WithMany(r => r.Ingredients) // Una receta tiene muchos ingredientes
                .HasForeignKey(i => i.RecipeId) // Usa RecipeId como clave foránea
                .OnDelete(DeleteBehavior.Cascade); // Si elimino receta, se eliminan sus ingredientes

            // Configura la relación uno a muchos entre Category y Recipe
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category) // Una receta tiene una categoría
                .WithMany(c => c.Recipes) // Una categoría tiene muchas recetas
                .HasForeignKey(r => r.CategoryId) // Usa CategoryId como clave foránea
                .OnDelete(DeleteBehavior.Restrict); // Evita borrar categoría con recetas asociadas

            // (Opcional) Seeds mínimos para probar rápido
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Postres" },
                new Category { Id = 2, Name = "Sopas" }
            );

            base.OnModelCreating(modelBuilder); // <- mantiene comportamiento base
        }
    }
}
