
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Mvc.Models;
using RecipeHub.Mvc.Data;

public class IngredientController : Controller
{
    private readonly ApplicationDbContext _context;

    public IngredientController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: INGREDIENTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Ingredients.ToListAsync());
    }

    // GET: INGREDIENTS/Details/5
    public async Task<IActionResult> Details(int? ingredientid)
    {
        if (ingredientid == null)
        {
            return NotFound();
        }

        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(m => m.IngredientId == ingredientid);
        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    // GET: INGREDIENTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: INGREDIENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IngredientId,Name")] Ingredient ingredient)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(ingredient);
    }

    // GET: INGREDIENTS/Edit/5
    public async Task<IActionResult> Edit(int? ingredientid)
    {
        if (ingredientid == null)
        {
            return NotFound();
        }

        var ingredient = await _context.Ingredients.FindAsync(ingredientid);
        if (ingredient == null)
        {
            return NotFound();
        }
        return View(ingredient);
    }

    // POST: INGREDIENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? ingredientid, [Bind("IngredientId,Name")] Ingredient ingredient)
    {
        if (ingredientid != ingredient.IngredientId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ingredient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(ingredient.IngredientId))
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
        return View(ingredient);
    }

    // GET: INGREDIENTS/Delete/5
    public async Task<IActionResult> Delete(int? ingredientid)
    {
        if (ingredientid == null)
        {
            return NotFound();
        }

        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(m => m.IngredientId == ingredientid);
        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    // POST: INGREDIENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? ingredientid)
    {
        var ingredient = await _context.Ingredients.FindAsync(ingredientid);
        if (ingredient != null)
        {
            _context.Ingredients.Remove(ingredient);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IngredientExists(int? ingredientid)
    {
        return _context.Ingredients.Any(e => e.IngredientId == ingredientid);
    }
}
