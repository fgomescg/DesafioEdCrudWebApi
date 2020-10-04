using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class SubjectPut
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(40, ErrorMessage = "Description can't be longer than 20 characters")]
        public string Description { get; set; }
    }
}
