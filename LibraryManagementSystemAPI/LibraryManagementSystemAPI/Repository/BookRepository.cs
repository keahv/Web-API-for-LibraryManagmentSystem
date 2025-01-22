using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Repository
{
    public class BookRepository
    {
        private static List<Book> Books = new List<Book>();

        // Get all books
        public List<Book> GetAllBooks()
        {
            return Books;
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            return Books.Where(b => b.Title.Contains(title, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Get a book by ISBN
        public Book GetBookByISBN(string isbn)
        {
            return Books.FirstOrDefault(b => b.ISBN == isbn);
        }

        // Add a new book
        public void AddBook(Book newBook)
        {
            Books.Add(newBook);
        }

        // Update an existing book
        public void UpdateBook(Book existingBook, Book updatedBook)
        {
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.Available = updatedBook.Available;
        }

        // Remove a book
        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }
    }
}
