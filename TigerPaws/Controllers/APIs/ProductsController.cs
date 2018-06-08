using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TigerPaws.DTOs;
using TigerPaws.Models;

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
        public IHttpActionResult GetProducts()
        {
            var productsDto = db.Products.ToList().Select(Mapper.Map<Product, ProductDto>);
            return Ok(productsDto);
        }

        //GET/api/products/1
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.SingleOrDefault(p => p.Id == id);
            
            if (product == null)
                return NotFound();

            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        //POST/api/products/
        [HttpPost]
        public IHttpActionResult CreateProducts(ProductDto productDto)
        {
            if (!ModelState.IsValid)
               return BadRequest();

            var product = Mapper.Map<ProductDto, Product>(productDto);
            db.Products.Add(product);
            db.SaveChanges();

            productDto.Id = product.Id;


            return Created(new Uri(Request.RequestUri + "/" + product.Id),productDto);
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
