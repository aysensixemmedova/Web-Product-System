using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public ushort InStock { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
