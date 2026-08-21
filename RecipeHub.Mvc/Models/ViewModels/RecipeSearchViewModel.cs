using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeHub.Mvc.Models;

namespace RecipeHub.Mvc.Models.ViewModels
{
    public class RecipeSearchViewModel
    {
        // --- Filter parameters ---
        public string? SearchTitle { get; set; }
        public int? CategoryId { get; set; }
        public string? Cuisine { get; set; }
        public string? Difficulty { get; set; }

        // --- Results ---
        public IEnumerable<Recipe> Results { get; set; } = Enumerable.Empty<Recipe>();

        // --- Dropdown data ---
        public SelectList? Categories { get; set; }
        public IEnumerable<string> AvailableCuisines { get; set; } = Enumerable.Empty<string>();

        // --- Stats ---
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }

        // --- Helper ---
        public bool HasActiveFilters =>
            !string.IsNullOrWhiteSpace(SearchTitle) ||
            CategoryId.HasValue ||
            !string.IsNullOrWhiteSpace(Cuisine) ||
            !string.IsNullOrWhiteSpace(Difficulty);
    }
}
