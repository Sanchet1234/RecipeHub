namespace RecipeHub.Mvc.Models
{
    public class MealPlan
    {
        public int MealPlanId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime WeekStartDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
