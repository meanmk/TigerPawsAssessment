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
  [AllowAnonymous]
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
           

           
            return View();
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")] Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            else
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
        [ValidateAntiForgeryToken]
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

        //GET/Delete
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            Product product = db.Products.Include(p => p.Genre).Single(p => p.Id == Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //POST/Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Include(p => p.Genre).Single(p => p.Id == id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Browse(byte id)
        {
            Product product = db.Products.Include(p => p.Genre).SingleOrDefault(p => p.Id == id);
            Genre genre = db.Genres.Include(g => g.Products).Single(g => g.Id == id);

            return View(genre);
        }

        // GET: Products
        public ActionResult ReadOnlyIndex()
        {
            //       var products = db.Products.Include(p => p.Genre).ToList();
            //       var genres = db.Genres.ToList();  return View(genres);

            return View();
        }

        public FileContentResult ExportToCSV()
        {
            var products = db.Products.Include(g => g.Genre).ToList();
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Id\",\"Name\",\"Genre\",\"Description\",\"Number in Stock\"");
            foreach (var item in products)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                                           item.Id,
                                           item.Name,
                                           item.Genre.Name,
                                           item.Description,
                                           item.NumberInStock));
              
            }
            var fileName = "ProductList" + DateTime.Now.ToString() + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(sw.ToString()), "text/csv", fileName);
        }

    }
}	