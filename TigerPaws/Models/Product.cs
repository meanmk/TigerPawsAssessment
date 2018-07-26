using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TigerPaws.Models
{
    public class Product
    {
       
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Number In Stock")]
        public int NumberInStock { get; set; }

        public string Description { get; set; }

        public Genre Genre { get; set; }
        [Required]
        [Display (Name="Genre")]
        public byte GenreId { get; set; }

        public string Image { get; set; }


     
    }
   
}