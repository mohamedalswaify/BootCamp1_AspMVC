using System.ComponentModel.DataAnnotations;

namespace BootCamp1_Api.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        [MaxLength(10, ErrorMessage = "الاسم لا يجب أن يتجاوز 10 حرف")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "الوصف لا يجب أن يتجاوز 500 حرف")]
        public string? Description { get; set; }
    }
}
