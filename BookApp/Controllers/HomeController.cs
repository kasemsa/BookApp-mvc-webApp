using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,BookDBContext context)
        {
            _logger = logger;

            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new BookViewModel
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);

        }
        public IActionResult GetSubcategory(int categoryId)
        {
            var subCategory = _context.Subcategories.Where(a => a.CategoryID == categoryId).ToList();
            return Ok(subCategory);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}