using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TigerPaws.Models;
using TigerPaws.ViewModels;

namespace TigerPaws.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db;
        public ProductsController()
        {
            db = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();    
        }


        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Genre).ToList();

            return View(products);
        }

        public ActionResult Details(int? id)
        {
            var product = db.Products.Include(p => p.Genre).SingleOrDefault(p => p.Id == id);

            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        //GET/Create
        public ActionResult Create()
        {
            var genres = db.Genres.ToList();
            var viewModel =new  ProductViewModel {
                Genres = genres
            };
            return View("Create",viewModel);
        }

        //POST/Create
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (file != null)
            {
                product.Image = product.Name + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("~/Content/Images/" + product.Image));
            }

            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index", "Products");
        }

        //GET/Edit
        public ActionResult Edit(int id)
        {
            Product product = db.Products.Include(p => p.Genre).SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = new ProductViewModel
                {
                    Genres = db.Genres.ToList(),
                    Id = product.Id,
                    Name = product.Name,
                    GenreId = product.GenreId,
                    Description = product.Description,
                    NumberInStock = product.NumberInStock,
                    Image = product.Image
                };
                return View(viewModel);
            }   
                    
        }

        //POST/Edit
        [HttpPost]
        public ActionResult Edit(Product product, int id, HttpPostedFileBase file)
        {
            var productToEdit = db.Products.Include(p => p.Genre).Single(p => p.Id == product.Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = product.Name + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("~/Content/Images/" + productToEdit.Image));
                }

               
                productToEdit.Name = product.Name;
                productToEdit.GenreId = product.GenreId;
                productToEdit.Description = product.Description;
                productToEdit.NumberInStock = product.NumberInStock;
              
            
                             
                db.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
           
        }

    }
}	