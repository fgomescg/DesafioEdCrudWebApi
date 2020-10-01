using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Books
{
    [Table("book")]
    public class Book
    {
    public Book() {}
    public Book(int Id, string title, string company, int edition, string publishYear, decimal value)
    {
        this.Id = Id;
        this.Title = title;
        this.Company = company;
        this.Edition = edition;
        this.PublishYear = publishYear;
        this.Value = value;        
    }
    [Key]
    [Column("BookId")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [StringLength(40, ErrorMessage = "Title can't be longer than 40 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Company is required")]
    [StringLength(40, ErrorMessage = "Company can't be longer than 40 characters")]
    public string Company { get; set; }

    [Required(ErrorMessage = "Edition is required")]
    public int Edition { get; set; }

    [Required(ErrorMessage = "PublishYear is required")]
    [StringLength(4, ErrorMessage = "PublishYear can't be longer than 4 characters")]
    public string PublishYear { get; set; }

    [Required(ErrorMessage = "Value is required")]
    public decimal Value { get; set; }

    public IEnumerable<BookAuthor> BookAuthors { get; set; }
    public IEnumerable<BookSubject> BookSubjects { get; set; }
        
  }  
}