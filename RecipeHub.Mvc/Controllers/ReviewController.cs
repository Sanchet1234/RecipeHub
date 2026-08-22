using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Mvc.Data;
using RecipeHub.Mvc.Models;

[Authorize]
public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ReviewController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // POST: Review/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int recipeId,
        int rating,
        string? comment)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);

        if (recipe == null)
        {
            return NotFound();
        }

        // Rating must be between 1 and 5.
        if (rating < 1 || rating > 5)
        {
            TempData["ReviewError"] = "Rating must be between 1 and 5.";
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Challenge();
        }

        // Only one review is allowed per user for a recipe.
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r =>
                r.RecipeId == recipeId &&
                r.UserId == user.Id);

        if (existingReview != null)
        {
            TempData["ReviewError"] =
                "You have already reviewed this recipe. You can edit your existing review instead.";

            return RedirectToAction(
                "Details",
                "Recipe",
                new { id = recipeId });
        }

        var review = new Review
        {
            RecipeId = recipeId,
            UserId = user.Id,
            Rating = rating,
            Comment = string.IsNullOrWhiteSpace(comment)
                ? null
                : comment.Trim(),
            CreatedDate = DateTime.Now
        };

        _context.Reviews.Add(review);

        await _context.SaveChangesAsync();

        TempData["ReviewSuccess"] =
            "Your review has been submitted successfully.";

        return RedirectToAction(
            "Details",
            "Recipe",
            new { id = recipeId });
    }

    // GET: Review/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Challenge();
        }

        // Only the owner of the review can access it.
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r =>
                r.ReviewId == id &&
                r.UserId == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // POST: Review/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        int rating,
        string? comment)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Challenge();
        }

        // Only the owner can edit the review.
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r =>
                r.ReviewId == id &&
                r.UserId == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        if (rating < 1 || rating > 5)
        {
            ModelState.AddModelError(
                "Rating",
                "Rating must be between 1 and 5.");
        }

        if (!ModelState.IsValid)
        {
            return View(review);
        }

        review.Rating = rating;

        review.Comment = string.IsNullOrWhiteSpace(comment)
            ? null
            : comment.Trim();

        await _context.SaveChangesAsync();

        TempData["ReviewSuccess"] =
            "Your review has been updated successfully.";

        return RedirectToAction(
            "Details",
            "Recipe",
            new { id = review.RecipeId });
    }

    // POST: Review/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Challenge();
        }

        // Only the owner can delete the review.
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r =>
                r.ReviewId == id &&
                r.UserId == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        var recipeId = review.RecipeId;

        _context.Reviews.Remove(review);

        await _context.SaveChangesAsync();

        TempData["ReviewSuccess"] =
            "Your review has been deleted successfully.";

        return RedirectToAction(
            "Details",
            "Recipe",
            new { id = recipeId });
    }
}
