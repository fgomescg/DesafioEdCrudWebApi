using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(40, ErrorMessage = "Name can't be longer than 40 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Company is required")]
        public string Company { get; set; }

        [Required(ErrorMessage = "PublishYear is required")]
        [StringLength(4, ErrorMessage = "PublishYear cannot be loner then 4 characters")]
        public string PublishYear { get; set; }
                
        [DefaultValue(1)]
        public int Edit { get; set; }

        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        //[Range(0, 9999999999999999.99)]
        public decimal Value { get; set; }
    }
}
