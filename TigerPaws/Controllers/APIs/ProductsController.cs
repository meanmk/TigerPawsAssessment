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
        public IEnumerable<ProductDto> GetProducts()
        {
            return db.Products.ToList().Select(Mapper.Map<Product, ProductDto>);
        }

        //GET/api/products/1
        public ProductDto GetProduct(int id)
        {
            var product = db.Products.SingleOrDefault(p => p.Id == id);
            
            if (product == null)
                new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Product, ProductDto>(product);
        }

        //POST/api/products/
        [HttpPost]
        public ProductDto CreateProducts(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var product = Mapper.Map<ProductDto, Product>(productDto);
            db.Products.Add(product);
            db.SaveChanges();

            productDto.Id = product.Id;


            return productDto;
        }

        //PUT/api/products/1
        [HttpPut]
        public  void UpdateProduct(ProductDto productDto, int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var productInDb = db.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<ProductDto, Product>(productDto, productInDb);           
            db.SaveChanges();
        }

        //DELETE/api/customers/1
        [HttpDelete] 
        public void DeleteProduct(int id)
        {
            var productInDb = db.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            db.Products.Remove(productInDb);
            db.SaveChanges();

        }
    }
}
