using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        //GET/api/products/1
        public Product GetProduct(int id)
        {
            var product = db.Products.SingleOrDefault(p => p.Id == id);
            
            if (product == null)
                new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        //POST/api/products/
        [HttpPost]
        public Product CreateProducts(Product product)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            db.Products.Add(product);
            db.SaveChanges();

            return product;
        }

        //PUT/api/products/1
        [HttpPut]
        public  void UpdateProduct(Product product, int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var productInDb = db.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            productInDb.Name = product.Name;
            productInDb.GenreId = product.GenreId;
            productInDb.Description = product.Description;
            productInDb.NumberInStock = product.NumberInStock;
            productInDb.Image = product.Image;

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
