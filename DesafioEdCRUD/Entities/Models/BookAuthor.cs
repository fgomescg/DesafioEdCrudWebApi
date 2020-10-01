using Entities.Models.Books;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
  public class BookAuthor
  {
    public BookAuthor() { }
    public BookAuthor(int bookId, int authorId)
    {
        this.BookId = bookId;
        this.AuthorId = authorId;        
    }

    public int BookId { get; set; }
    public Book Book { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
  }  
}