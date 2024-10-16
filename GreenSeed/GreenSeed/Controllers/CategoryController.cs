using GreenSeed.Data;
using GreenSeed.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenSeedCREdev.Controllers
{
    public class CategoryController : Controller
    {
        private Repository<Category> categories;

        public CategoryController(ApplicationDbContext context)
        {
            categories = new Repository<Category>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await categories.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await categories.GetByIdAsync(id, new QueryOptions<Category>
            {
                Includes = "Products"
            });
            return View(category);
        }

        // Category/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                await categories.AddAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categories.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await categories.UpdateAsync(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating category: {ex.GetBaseException().Message}");
                }
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categories.GetByIdAsync(id, new QueryOptions<Category> { Includes = "Products" });
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await categories.GetByIdAsync(id, new QueryOptions<Category> { Includes = "Products" });
            if (category != null)
            {
                if (category.Products != null && category.Products.Any())
                {
                    ModelState.AddModelError("", "Não é possível excluir uma categoria com produtos associados.");
                    return View("Delete", category); // Especifica a view "Delete"
                }

                await categories.DeleteAsync(id);
            }
            return RedirectToAction("Index");
        }
    }
}
