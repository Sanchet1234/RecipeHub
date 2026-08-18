namespace RecipeHub.Mvc.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int RecipeId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
