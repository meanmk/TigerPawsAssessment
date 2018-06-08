using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TigerPaws.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
  
        public int NumberInStock { get; set; }

        public string Description { get; set; }
     
        [Required]
        public byte GenreId { get; set; }

        public string Image { get; set; }

    }
}