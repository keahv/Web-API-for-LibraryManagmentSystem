using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystemAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {


        private readonly BookRepository BookRepo = new();

        // GET /api/books
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = BookRepo.GetAllBooks();
            if (books == null || !books.Any())
            {
                return NotFound(new { Message = "No books found." });
            }
            return Ok(books);
        }
        // GET /api/books/{title}
        [HttpGet("{title}")]
        public IActionResult SearchBooksByTitle(string title)
        {
            var books = BookRepo.SearchBooksByTitle(title);
            if (books == null || !books.Any())
            {
                return NotFound(new { Message = "No books found with the given title." });
            }

            return Ok(books);
        }


        // POST /api/books
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate ISBN
            if (BookRepo.GetBookByISBN(newBook.ISBN) != null)
            {
                return Conflict(new { Message = "A book with this ISBN already exists." });
            }

            // Add book to in-memory list
            BookRepo.AddBook(newBook);
            return CreatedAtAction(nameof(SearchBooksByTitle), new { title = newBook.Title }, newBook);
        }

        // PUT /api/books/{isbn}
        [HttpPut("{isbn}")]
        public IActionResult UpdateBook(string isbn, [FromBody] Book updatedBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBook = BookRepo.GetBookByISBN(isbn);
            if (existingBook == null)
            {
                return NotFound(new { Message = "Book not found." });
            }

            // Update book details
            BookRepo.UpdateBook(existingBook, updatedBook);
            return Ok(existingBook);
        }

        // DELETE /api/books/{isbn}
        [HttpDelete("{isbn}")]
        public IActionResult DeleteBook(string isbn)
        {
            var book = BookRepo.GetBookByISBN(isbn);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found." });
            }

            // Remove book from the list
            BookRepo.RemoveBook(book);
            return NoContent();
        }


    }


}
