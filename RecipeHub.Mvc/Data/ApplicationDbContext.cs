using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Mvc.Models;

namespace RecipeHub.Mvc.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext(options)
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<MealPlan> MealPlans { get; set; }

        public DbSet<MealPlanItem> MealPlanItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Category
            builder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            builder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(300);

            // Recipe
            builder.Entity<Recipe>()
                .HasKey(r => r.RecipeId);

            builder.Entity<Recipe>()
                .Property(r => r.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Entity<Recipe>()
                .Property(r => r.Description)
                .HasMaxLength(500);

            builder.Entity<Recipe>()
                .Property(r => r.UserId)
                .HasMaxLength(450);

            builder.Entity<Recipe>()
                .Property(r => r.Difficulty)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<Recipe>()
                .Property(r => r.Cuisine)
                .HasMaxLength(50);

            builder.Entity<Recipe>()
                .Property(r => r.Instructions)
                .IsRequired();

            builder.Entity<Recipe>()
                .Property(r => r.ImageUrl)
                .HasMaxLength(500);

            builder.Entity<Recipe>()
                .Property(r => r.CreatedDate)
                .HasColumnType("datetime2");

            builder.Entity<Recipe>()
                .Property(r => r.UpdatedDate)
                .HasColumnType("datetime2");

            // Recipe -> Category
            builder.Entity<Recipe>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ingredient
            builder.Entity<Ingredient>()
                .HasKey(i => i.IngredientId);

            builder.Entity<Ingredient>()
                .Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            // RecipeIngredient
            builder.Entity<RecipeIngredient>()
                .HasKey(ri => ri.RecipeIngredientId);

            builder.Entity<RecipeIngredient>()
                .Property(ri => ri.Unit)
                .HasMaxLength(30)
                .IsRequired();

            builder.Entity<RecipeIngredient>()
                .HasOne<Recipe>()
                .WithMany()
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RecipeIngredient>()
                .HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Favorite
            builder.Entity<Favorite>()
                .HasKey(f => f.FavoriteId);

            builder.Entity<Favorite>()
                .Property(f => f.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder.Entity<Favorite>()
                .Property(f => f.CreatedDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Entity<Favorite>()
                .HasOne<Recipe>()
                .WithMany()
                .HasForeignKey(f => f.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review
            builder.Entity<Review>()
                .HasKey(r => r.ReviewId);

            builder.Entity<Review>()
                .Property(r => r.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder.Entity<Review>()
                .Property(r => r.Comment)
                .HasMaxLength(1000);

            builder.Entity<Review>()
                .Property(r => r.CreatedDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Entity<Review>()
                .HasOne<Recipe>()
                .WithMany()
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // MealPlan
            builder.Entity<MealPlan>()
                .HasKey(mp => mp.MealPlanId);

            builder.Entity<MealPlan>()
                .Property(mp => mp.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder.Entity<MealPlan>()
                .Property(mp => mp.WeekStartDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Entity<MealPlan>()
                .Property(mp => mp.CreatedDate)
                .HasColumnType("datetime2")
                .IsRequired();

            // MealPlanItem
            builder.Entity<MealPlanItem>()
                .HasKey(mpi => mpi.MealPlanItemId);

            builder.Entity<MealPlanItem>()
                .Property(mpi => mpi.MealDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Entity<MealPlanItem>()
                .Property(mpi => mpi.MealType)
                .HasMaxLength(30)
                .IsRequired();

            builder.Entity<MealPlanItem>()
                .HasOne<MealPlan>()
                .WithMany()
                .HasForeignKey(mpi => mpi.MealPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MealPlanItem>()
                .HasOne<Recipe>()
                .WithMany()
                .HasForeignKey(mpi => mpi.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}