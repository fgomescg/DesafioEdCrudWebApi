using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
   [Table("author")]
    public class Author 
    {
    public Author() {}
    public Author(int authorID, string name)
    {
      this.AuthorID = authorID;
      this.Name = name;
    }
    [Key]
    public int AuthorID { get; set; }
    
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(40, ErrorMessage = "Name can't be longer than 40 characters")]
    public string Name { get; set; }
    
    public IEnumerable<BookAuthor> BookAuthors { get; set; }
    }  
}