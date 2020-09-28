using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class SubjectForCreationDto
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(40, ErrorMessage = "Description can't be longer than 40 characters")]
        public string Description { get; set; }
    }
}
