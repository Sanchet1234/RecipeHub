namespace RecipeHub.Mvc.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }

        public string? UserId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int Servings { get; set; }

        public string Difficulty { get; set; } = string.Empty;

        public string? Cuisine { get; set; }

        public string Instructions { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        // Navigation property
        public Category? Category { get; set; }
    }
}