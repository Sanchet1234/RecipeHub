namespace RecipeHub.Mvc.Models
{
    public class MealPlanItem
    {
        public int MealPlanItemId { get; set; }

        public int MealPlanId { get; set; }

        public int RecipeId { get; set; }

        public DateTime MealDate { get; set; }

        public string MealType { get; set; } = string.Empty;
    }
}