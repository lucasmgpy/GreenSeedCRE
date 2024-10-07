using GreenSeedCREdev.Data;
using GreenSeedCREdev.Models;
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
        public async Task<IActionResult> AddEdit(Product product, int catId)
        {
            ViewBag.Categories = await categories.GetAllAsync();
            if (ModelState.IsValid)
            {
                // variavel para guardar o nome do novo arquivo de imagem
                string uniqueFileName = null;

                // Tratar o upload da imagem se uma nova imagem for fornecida
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(product.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    // nós vamos definir o ImageUrl mais tarde com base em se estamos adicionando ou editando
                }

                if (product.ProductId == 0)
                {
                    // Adicionando um novo produto
                    product.CategoryId = catId;

                    if (uniqueFileName != null)
                    {
                        product.ImageUrl = uniqueFileName;
                    }
                    else
                    {
                        // opsionalmente definir uma URL de imagem padrão se nenhuma imagem foi enviada
                        product.ImageUrl = "https://via.placeholder.com/150";
                    }

                    await products.AddAsync(product);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    // Editing an existing product
                    var existingProduct = await products.GetByIdAsync(product.ProductId, new QueryOptions<Product> { Includes = "Category" });

                    if (existingProduct == null)
                    {
                        ModelState.AddModelError("", "Product not found.");
                        ViewBag.Categories = await categories.GetAllAsync();
                        return View(product);
                    }

                    // atualizando as propriedades do produto
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.CategoryId = catId;

                    // uma nova imagem foi enviada, atualizar o ImageUrl e excluir o arquivo de imagem antigo
                    if (uniqueFileName != null)
                    {
                        // apagar o arquivo de imagem antigo se não for o padrão
                        if (!string.IsNullOrEmpty(existingProduct.ImageUrl) && existingProduct.ImageUrl != "https://via.placeholder.com/150")
                        {
                            string existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", existingProduct.ImageUrl);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }
                        // atualizar o ImageUrl com o novo nome do arquivo
                        existingProduct.ImageUrl = uniqueFileName;
                    }

                    try
                    {
                        await products.UpdateAsync(existingProduct);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Error: {ex.GetBaseException().Message}");
                        ViewBag.Categories = await categories.GetAllAsync();
                        return View(product);
                    }
                }
            }
            return RedirectToAction("Index", "Product");
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
                ModelState.AddModelError("", "Product not found.");
                return RedirectToAction("Index");
            }
        }
    }
}
