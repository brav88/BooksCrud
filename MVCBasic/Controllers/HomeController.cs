using Microsoft.AspNetCore.Mvc;
using MVCBasic.Models;
using System.Diagnostics;

namespace MVCBasic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Books = DatabaseHelper.DatabaseHelper.GetBooks();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveBook(string txtIsbn, string txtTitle, string slAuthor, string txtDate)
        {
            Book book = new Book()
            {
                Title = txtTitle,
                Author = slAuthor,
                Isbn = Convert.ToInt64(txtIsbn),
                Photo = "",
                Date = Convert.ToDateTime(txtDate)
            };

            DatabaseHelper.DatabaseHelper.CreateBook(book);

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}