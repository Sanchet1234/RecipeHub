
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeHub.Mvc.Models;
using RecipeHub.Mvc.Data;

public class RecipeController : Controller
{
    private readonly ApplicationDbContext _context;

    public RecipeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RECIPES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Recipes.ToListAsync());
    }

    // GET: RECIPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(r => r.RecipeId == id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: RECIPES/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
        return View();
    }

    // POST: RECIPES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RecipeId,UserId,CategoryId,Title,Description,PreparationTime,CookingTime,Servings,Difficulty,Cuisine,Instructions,ImageUrl,CreatedDate,UpdatedDate")] Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            recipe.CreatedDate = DateTime.Now;
            recipe.UpdatedDate = null;

            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories,
            "CategoryId",
            "Name",
            recipe.CategoryId
        );

        return View(recipe);
    }

    // GET: RECIPES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories,
            "CategoryId",
            "Name",
            recipe.CategoryId
        );

        return View(recipe);
    }

    // POST: RECIPES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id,
    [Bind("RecipeId,CategoryId,Title,Description,PreparationTime,CookingTime,Servings,Difficulty,Cuisine,Instructions,ImageUrl")] Recipe recipe)
    {
        if (id != recipe.RecipeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingRecipe = await _context.Recipes.FindAsync(recipe.RecipeId);

                if (existingRecipe == null)
                {
                    return NotFound();
                }

                existingRecipe.CategoryId = recipe.CategoryId;
                existingRecipe.Title = recipe.Title;
                existingRecipe.Description = recipe.Description;
                existingRecipe.PreparationTime = recipe.PreparationTime;
                existingRecipe.CookingTime = recipe.CookingTime;
                existingRecipe.Servings = recipe.Servings;
                existingRecipe.Difficulty = recipe.Difficulty;
                existingRecipe.Cuisine = recipe.Cuisine;
                existingRecipe.Instructions = recipe.Instructions;
                existingRecipe.ImageUrl = recipe.ImageUrl;
                existingRecipe.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.RecipeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories,
            "CategoryId",
            "Name",
            recipe.CategoryId
        );

        return View(recipe);
    }

    // GET: RECIPES/Delete/5
    public async Task<IActionResult> Delete(int? recipeid)
    {
        if (recipeid == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(m => m.RecipeId == recipeid);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: RECIPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? recipeid)
    {
        var recipe = await _context.Recipes.FindAsync(recipeid);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(int? recipeid)
    {
        return _context.Recipes.Any(e => e.RecipeId == recipeid);
    }
}
