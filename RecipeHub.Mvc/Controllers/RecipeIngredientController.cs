
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Mvc.Models;
using RecipeHub.Mvc.Data;

public class RecipeIngredientController : Controller
{
    private readonly ApplicationDbContext _context;

    public RecipeIngredientController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RECIPEINGREDIENTS
    public async Task<IActionResult> Index()
    {
        var recipeIngredients = await _context.RecipeIngredients
            .Include(ri => ri.Recipe)
            .Include(ri => ri.Ingredient)
            .ToListAsync();

        return View(recipeIngredients);
    }

    // GET: RECIPEINGREDIENTS/Details/5
    public async Task<IActionResult> Details(int? recipeingredientid)
    {
        if (recipeingredientid == null)
        {
            return NotFound();
        }

        var recipeingredient = await _context.RecipeIngredients
            .FirstOrDefaultAsync(m => m.RecipeIngredientId == recipeingredientid);
        if (recipeingredient == null)
        {
            return NotFound();
        }

        return View(recipeingredient);
    }

    // GET: RECIPEINGREDIENTS/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Recipes = new SelectList(
            await _context.Recipes.ToListAsync(),
            "RecipeId",
            "Title"
        );

        ViewBag.Ingredients = new SelectList(
            await _context.Ingredients.ToListAsync(),
            "IngredientId",
            "Name"
        );

        return View();
    }

    // POST: RECIPEINGREDIENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    [Bind("RecipeIngredientId,RecipeId,IngredientId,Quantity,Unit")]
    RecipeIngredient recipeingredient)
    {
        if (ModelState.IsValid)
        {
            _context.Add(recipeingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Recipes = new SelectList(
            await _context.Recipes.ToListAsync(),
            "RecipeId",
            "Title",
            recipeingredient.RecipeId
        );

        ViewBag.Ingredients = new SelectList(
            await _context.Ingredients.ToListAsync(),
            "IngredientId",
            "Name",
            recipeingredient.IngredientId
        );

        return View(recipeingredient);
    }

    // GET: RECIPEINGREDIENTS/Edit/5
    public async Task<IActionResult> Edit(int? recipeingredientid)
    {
        if (recipeingredientid == null)
        {
            return NotFound();
        }

        var recipeingredient = await _context.RecipeIngredients.FindAsync(recipeingredientid);
        if (recipeingredient == null)
        {
            return NotFound();
        }
        return View(recipeingredient);
    }

    // POST: RECIPEINGREDIENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? recipeingredientid, [Bind("RecipeIngredientId,RecipeId,IngredientId,Quantity,Unit")] RecipeIngredient recipeingredient)
    {
        if (recipeingredientid != recipeingredient.RecipeIngredientId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipeingredient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(recipeingredient.RecipeIngredientId))
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
        return View(recipeingredient);
    }

    // GET: RECIPEINGREDIENTS/Delete/5
    public async Task<IActionResult> Delete(int? recipeingredientid)
    {
        if (recipeingredientid == null)
        {
            return NotFound();
        }

        var recipeingredient = await _context.RecipeIngredients
            .FirstOrDefaultAsync(m => m.RecipeIngredientId == recipeingredientid);
        if (recipeingredient == null)
        {
            return NotFound();
        }

        return View(recipeingredient);
    }

    // POST: RECIPEINGREDIENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? recipeingredientid)
    {
        var recipeingredient = await _context.RecipeIngredients.FindAsync(recipeingredientid);
        if (recipeingredient != null)
        {
            _context.RecipeIngredients.Remove(recipeingredient);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeIngredientExists(int? recipeingredientid)
    {
        return _context.RecipeIngredients.Any(e => e.RecipeIngredientId == recipeingredientid);
    }
}
