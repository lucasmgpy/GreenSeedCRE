// Controllers/OrderController.cs
using GreenSeed.Services; // Adicione esta linha no topo
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GreenSeed.Data;
using GreenSeed.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenSeed.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _products;
        private Repository<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrderQueueService _queueService;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, OrderQueueService queueService)
        {
            _context = context;
            _userManager = userManager;
            _products = new Repository<Product>(context);
            _orders = new Repository<Order>(context);
            _queueService = queueService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddItem(int prodId, int prodQty)
        {
            var product = await _context.Products.FindAsync(prodId);
            if (product == null)
            {
                return NotFound();
            }

            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };

            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

            if (existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.Name
                });
            }

            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            HttpContext.Session.Set("OrderViewModel", model);

            return RedirectToAction("Create", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            // Cria uma nova entidade Order com Status definido
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User),
                Status = "Pending" // Define o status inicial
            };

            // Adicionar OrderItems à entidade Order
            foreach (var item in model.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            // Salvar a entidade Order no banco de dados
            await _orders.AddAsync(order);

            // Enviar mensagem para a fila
            await _queueService.SendOrderStatusAsync(order);

            // Limpar o OrderViewModel da sessão
            HttpContext.Session.Remove("OrderViewModel");

            // Redirecionar para a página de confirmação da encomenda
            return RedirectToAction("ViewOrders");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);

            var userOrders = await _orders.GetAllByIdAsync(userId, "UserId", new QueryOptions<Order>
            {
                Includes = "OrderItems.Product",
                OrderBy = o => o.OrderDate,
                OrderByDirection = "DESC"
            });

            return View(userOrders);
        }
    }
}
