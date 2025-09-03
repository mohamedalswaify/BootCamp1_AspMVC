using System.ComponentModel.DataAnnotations;

namespace BootCamp1_Api.Dtos
{
    public class CategoryPatchDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
