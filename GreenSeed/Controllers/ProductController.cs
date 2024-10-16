using GreenSeed.Data;
using GreenSeed.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenSeedCREdev.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;
        private Repository<Category> categories;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            products = new Repository<Product>(context);
            categories = new Repository<Category>(context);
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await products.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Categories = await categories.GetAllAsync();
            if (id == 0)
            {
                ViewBag.Operation = "Adicionar";
                return View(new Product());
            }
            else
            {
                Product product = await products.GetByIdAsync(id, new QueryOptions<Product>
                {
                    Includes = "Category"
                });
                ViewBag.Operation = "Editar";
                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product)
        {
            ViewBag.Categories = await categories.GetAllAsync();
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(product.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                }

                if (product.ProductId == 0)
                {
                    // Adicionando um novo produto
                    if (uniqueFileName != null)
                    {
                        product.ImageUrl = uniqueFileName;
                    }
                    else
                    {
                        product.ImageUrl = "https://via.placeholder.com/150";
                    }

                    await products.AddAsync(product);
                }
                else
                {
                    // Editando um produto existente
                    var existingProduct = await products.GetByIdAsync(product.ProductId, new QueryOptions<Product> { Includes = "Category" });

                    if (existingProduct == null)
                    {
                        ModelState.AddModelError("", "Produto não encontrado.");
                        return View(product);
                    }

                    // Atualizar propriedades do produto
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.CategoryId = product.CategoryId;

                    // Atualizar imagem se necessário
                    if (uniqueFileName != null)
                    {
                        if (!string.IsNullOrEmpty(existingProduct.ImageUrl) && existingProduct.ImageUrl != "https://via.placeholder.com/150")
                        {
                            string existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", existingProduct.ImageUrl);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }
                        existingProduct.ImageUrl = uniqueFileName;
                    }

                    try
                    {
                        await products.UpdateAsync(existingProduct);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Erro: {ex.GetBaseException().Message}");
                        return View(product);
                    }
                }

                return RedirectToAction("Index", "Product");
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await products.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Produto não encontrado.");
                return RedirectToAction("Index");
            }
        }
    }
}
