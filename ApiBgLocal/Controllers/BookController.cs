using BgLocal.DataAccess.Repository.IRepository;
using BgLocal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBgLocal.Controllers {
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase {
        private readonly IRepository<Book> _repository;

        public BookController(IRepository<Book> repository) {
            _repository = repository;
        }

        //GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks() {
            var books = await _repository.GetAllAsync();

            return Ok(books);
        }

        //GET: api/books/{id}
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<Book>> GetBook(int id) {
            var book = await _repository.GetByFilterAsync(a => a.Id == id);

            if (book == null) {
                return NotFound();
            }

            return Ok(book);
        }

        //POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book) {
            var createdBook = await _repository.AddAsync(book);
            await _repository.SaveChanges();

            return CreatedAtAction("GetBook", new { id = createdBook.Id }, createdBook);
        }

        //PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, Book book) {
            if (id != book.Id) {
                return BadRequest();
            }

            try {
                await _repository.UpdateAsync(id, book);
                await _repository.SaveChanges();

                return NoContent();
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }

        //DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) {
            var book = await _repository.GetByFilterAsync(a => a.Id == id);

            if (book == null) {
                return BadRequest();
            }
            try {
                await _repository.RemoveAsync(id);
                await _repository.SaveChanges();

                return NoContent();
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
    }
}