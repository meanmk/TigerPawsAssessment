using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TigerPaws.Models;

namespace TigerPaws.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

       
        [Display(Name = "Number In Stock")]
        public int? NumberInStock { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        public string Image { get; set; }
    }
}