using System.ComponentModel.DataAnnotations;

namespace BlazorServer3.Database
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string ImageFilePath { get; set; }

        public Category Category { get; set; }
    }
}
