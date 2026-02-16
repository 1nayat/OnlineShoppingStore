using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.Home;
using Microsoft.AspNetCore.Http; // To access HttpContext.Session
using System.Linq;
using System.Text.Json; // For JsonSerializer

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SafainDbContext _context; // DbContext injected via constructor

        // Constructor now accepts SafainDbContext as a dependency
        public HomeController(ILogger<HomeController> logger, SafainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Index method to display products
        public IActionResult Index(string search, int? page)
        {
            var model = new HomeIndexViewModel(_context);
            model = model.CreateModel(search, 4, page);

            // Retrieve cart data from session
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();
            ViewData["Cart"] = cart;  // Pass the cart data to the view using ViewData

            return View(model);
        }


        // Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Add product to cart
        public ActionResult AddToCart(int productId)
        {
            var product = _context.TblProducts.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();

            var cartItem = cart.FirstOrDefault(i => i.Product.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;     
            }
            else
            {
                cart.Add(new Item
                {
                    Product = product,
                    Quantity = 1
                });
            }

            // Save the updated cart back to session
            HttpContext.Session.SetObjectAsJson("cart", cart);

            // Log the current cart count for debugging purposes
            var cartCount = cart.Count;
            _logger.LogInformation($"Cart Count: {cartCount}");

            return RedirectToAction("Index");
        }

        // View the cart
        public IActionResult ViewCart()
        {
            // Fetch cart from session and pass it to the view
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();

            // Return cart view
            return View(cart);
        }

        // Remove product from cart
        public IActionResult RemoveFromCart(int productId)
        {
            // Get the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();

            // Find the item in the cart
            var cartItem = cart.FirstOrDefault(i => i.Product.ProductId == productId);

            if (cartItem != null)
            {
                // If item exists, remove it from the cart
                cart.Remove(cartItem);
                // Save the updated cart back to session
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }

            return RedirectToAction("ViewCart"); // Redirect to cart view
        }
    }

    // Extension methods to make it easier to get and set complex objects in session
    public static class SessionExtensions
    {
        // Method to save an object as JSON in session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Method to retrieve an object from JSON in session
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
