using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepo _repo;

        public BookController(IBookRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Book> ls = await _repo.GetBooksAsync();
            return View(ls);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchType, string searchValue)
        {
            List<Book> results;

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                results = await _repo.GetBooksAsync();
            }
            else if (searchType == "id")
            {
                if (int.TryParse(searchValue, out int id))
                {
                    results = await _repo.SearchById(id);
                }
                else
                {
                    results = new List<Book>();
                }
            }
            else
            {
                results = await _repo.SearchByName(searchValue);
            }

            return View(results);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Book obj)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", obj);
            }

            int res = await _repo.Insert(obj);
            if (res > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _repo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book obj)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", obj);
            }

            int res = await _repo.Update(obj);
            if (res > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _repo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}