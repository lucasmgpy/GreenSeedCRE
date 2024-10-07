using GreenSeedCREdev.Data;
using GreenSeedCREdev.Models;
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
            return View(await categories.GetByIdAsync(id, new QueryOptions<Category>() { Includes = "CategoryProducts.Product" }));
        }

        //Pruduct/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId, Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await categories.AddAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //Productt/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await categories.GetByIdAsync(id, new QueryOptions<Category> { Includes = "CategoryProducts.Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            await categories.DeleteAsync(category.CategoryId);
            return RedirectToAction("Index");
        }

        //Product/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await categories.GetByIdAsync(id, new QueryOptions<Category> { Includes = "CategoryProducts.Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await categories.UpdateAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
