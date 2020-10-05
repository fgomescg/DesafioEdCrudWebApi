using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("subject")]
    public class Subject
    {
        public Subject() { }
        public Subject(int subjectId, string description)
        {
            this.SubjectId = subjectId;
            this.Description = description;
        }
        [Key]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(20, ErrorMessage = "Description can't be longer than 20 characters")]
        public string Description { get; set; }

        public IEnumerable<BookSubject> BookSubjects { get; set; }
    }
}