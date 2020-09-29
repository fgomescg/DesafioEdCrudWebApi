using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class AuthorForCreateUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, ErrorMessage = "Name can't be longer than 40 characters")]
        public string Name { get; set; }       
    }
}
