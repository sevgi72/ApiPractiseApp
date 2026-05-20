using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class ProductCreateVm
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public List<int> ColorsId { get; set; } = new List<int>();

    }
}
