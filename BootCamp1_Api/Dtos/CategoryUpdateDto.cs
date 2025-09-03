using System.ComponentModel.DataAnnotations;

namespace BootCamp1_Api.Dtos
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
