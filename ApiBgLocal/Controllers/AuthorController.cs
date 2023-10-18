using BgLocal.DataAccess.Data;
using BgLocal.DataAccess.Repository.IRepository;
using BgLocal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBgLocal.Controllers {

    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase {
        private readonly IRepository<Author> _repository;

        public AuthorController(IRepository<Author> repository) {
            _repository = repository;
        }

        //GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors() {
            var authors = await _repository.GetAllAsync();

            return Ok(authors);
        }

        //GET: api/authors/{id}
        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<Author>> GetAuthor(int id) {
            var author = await _repository.GetByFilterAsync(a => a.Id == id);

            if (author == null) {
                return NotFound();
            }
            
            return Ok(author);
        }

        //POST: api/authors
        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author) {
            var createdAuthor = await _repository.AddAsync(author);
            await _repository.SaveChanges();

            return CreatedAtAction("GetAuthor", new { id = createdAuthor.Id }, createdAuthor);
        }

        //PUT: api/authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAuthor(int id, Author author) {
            if (id != author.Id) {
                return BadRequest();
            }

            try {
                await _repository.UpdateAsync(id, author);
                await _repository.SaveChanges();

                return NoContent();
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }

        //DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id) {
            var author = await _repository.GetByFilterAsync(a => a.Id == id);

            if (author == null) {
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