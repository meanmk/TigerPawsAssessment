using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TigerPaws.DTOs;
using TigerPaws.Models;
using System.Data.Entity;




namespace TigerPaws.Controllers.APIs
{
   
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db;
        public ProductsController()
        {
            db = new ApplicationDbContext();
        }

        //GET/api/products
        public IEnumerable<ProductDto> GetProducts()
        {
            return db.Products
                .Include(p => p.Genre)
                .ToList()
                .Select(Mapper.Map<Product, ProductDto>);
           
        }

        //GET/api/products/1
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.Include(p=>p.Genre).SingleOrDefault(p => p.Id == id);
            
            if (product == null)
                return NotFound();

            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        //POST/api/products/1
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = Mapper.Map<ProductDto, Product>(productDto);
                db.Products.Add(product);
                db.SaveChanges();

                productDto.Id = product.Id;

                return Created(new Uri(Request.RequestUri + "/" + product.Id), productDto);
            }
            else
            {              
                return BadRequest();
            } 

           
        }

        //PUT/api/products/1
        [HttpPut]
        public  IHttpActionResult UpdateProduct(ProductDto productDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productInDb = db.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                return NotFound();

            Mapper.Map<ProductDto, Product>(productDto, productInDb);           
            db.SaveChanges();
            return Ok();
        }

        //DELETE/api/customers/1
        [HttpDelete] 
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = db.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
               return NotFound();

            db.Products.Remove(productInDb);
            db.SaveChanges();

            return Ok();
        }

    }
}
