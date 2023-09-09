using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookDBContext _context;

        public BooksController(BookDBContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bookDBContext = _context.Books.Include(b => b.Author).Include(b => b.Category).Include(b => b.Subcategory);
            return View(await bookDBContext.ToListAsync());
        }

        //Api's for other dev's

        [HttpGet]
        public IActionResult getBooks() { 
            List<Book> books = _context.Books.ToList();

            return Json(books);
        }

        [HttpGet]
        public IActionResult getBookById(int Id)
        {
           Book book = _context.Books.Find(Id);

            return Json(book);
        }

        [HttpGet]
        public IActionResult getBookByCategoryId(int Id)
        {
           List< Book> books = _context.Books.Where(b=>b.CategoryID==Id).ToList();

            return Json(books);
        }

        [HttpGet]
        public IActionResult getBookBySubCategoryId(int Id)
        {
            List<Book> books = _context.Books.Where(b => b.SubcategoryID == Id).ToList();

            return Json(books);
        }

        [HttpGet]
        public IActionResult getBookByAuthorId(int Id)
        {
            List<Book> books = _context.Books.Where(b => b.AuthorID == Id).ToList();

            return Json(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Subcategory)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        private List<SelectListItem> GetCategories() { 
        
            var lstCategories =new List<SelectListItem>();

            List<Category> categories=_context.Categories.ToList();

            lstCategories=categories.Select(c => new SelectListItem() 
            { 
                Value=c.CategoryID.ToString(),
                Text=c.CategoryName
            }).ToList();

            var defItem = new SelectListItem()
            {

                Value = "",
                Text = "---Select Category---"
            };

            lstCategories.Insert(0,defItem);
            return lstCategories;
        }

        private List<SelectListItem> GetSubCategories(int categoryId=1)
        {

                
            List<SelectListItem>lstSubcategories=_context.Subcategories
                .Where(c=>c.CategoryID==categoryId)
                .OrderBy(n=>n.SubcategoryName)
                .Select(n=>
                new SelectListItem
                { 
                    Value=n.SubcategoryID.ToString(),   
                    Text=n.SubcategoryName
                
                }).ToList();


            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "---Select SubCategory---"
            };

            lstSubcategories.Insert(0,defItem);
            return lstSubcategories;
        }
        // GET: Books/Create
        public IActionResult Create()
        {
           
            Book book = new Book();
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "FirstName");
            ViewBag.CategoryID = GetCategories();
            ViewBag.SubcategoryID = GetSubCategories();
            return View(book);
        }

     

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,AuthorID,CategoryID,SubcategoryID")] Book book)
        {

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
     
        }
        [HttpGet]
        public JsonResult GetSubcategoryByCategory(int categoryId)
        { 
            List<SelectListItem> subCategory=GetSubCategories(categoryId);
            return Json(subCategory);
        
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "FirstName", book.AuthorID);
            ViewBag.CategoryID = GetCategories();
            ViewBag.SubcategoryID = GetSubCategories();
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,AuthorID,CategoryID,SubcategoryID")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
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

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Subcategory)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookDBContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookID == id)).GetValueOrDefault();
        }
    }
}
