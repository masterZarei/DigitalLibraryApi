using DigitalLibraryApi.Data;
using DigitalLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CRUDController : Controller
    {
        private readonly AppDbContext _db;

        public CRUDController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return Ok(await _db.Books.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            return Ok(await _db.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book is null)
            {
                return BadRequest("کتاب پیدا نشد");
            }
            return Ok(book);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<List<Book>>> RemoveBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book is null)
            {
                return BadRequest("کتاب پیدا نشد");
            }
            _db.Remove(book);
            await _db.SaveChangesAsync();

            return Ok(await _db.Books.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book book)
        {
            var isExist = _db.Books.AsNoTracking().Where(a => a.Id == book.Id);
            if (isExist is null)
            {
                return BadRequest("کتاب پیدا نشد");
            }
            _db.Update(book);
            await _db.SaveChangesAsync();

            return Ok(await _db.Books.ToListAsync());
        }
    }
}
