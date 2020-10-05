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
        public string Value { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
    }
}
