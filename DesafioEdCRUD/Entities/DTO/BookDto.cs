using Entities.Models;
using System.Collections.Generic;

namespace Entities.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Edition { get; set; }
        public string PublishYear { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public IEnumerable<BookSubject> BookSubjects { get; set; }
    }
}
