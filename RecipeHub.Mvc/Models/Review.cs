namespace RecipeHub.Mvc.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int RecipeId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}