using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using TigerPaws.Models;
using TigerPaws.ViewModels;

namespace TigerPaws.Controllers
{
    [Audit]
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

        [AllowAnonymous]
        // GET: Products
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageProducts))
                return View("Index");
            return View("ReadOnlyIndex");
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            var product = db.Products.Include(p => p.Genre).SingleOrDefault(p => p.Id == id);

            if (product == null)
                return HttpNotFound();
            return View(product);
        }


        //GET/
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Create()
        {
            var genres = db.Genres.ToList();
            var viewModel = new ProductViewModel
            {
                Genres = genres
            };
            return View("Create", viewModel);
        }


        //POST/Create
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageProducts)]
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
        [Authorize(Roles = RoleName.CanManageProducts)]
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
        [Authorize(Roles = RoleName.CanManageProducts)]
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

        [Authorize(Roles = RoleName.CanManageProducts)]
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

        [Authorize(Roles = RoleName.CanManageProducts)]
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
            sw.WriteLine("Id,Name,Genre,Description,Number in Stock");
            foreach (var item in products)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                                           item.Id,
                                           item.Name,
                                           item.Genre.Name,
                                           item.Description,
                                           item.NumberInStock));
            }


            var fileName = "ProductList" + DateTime.Now.ToString() + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(sw.ToString()), "text/csv", fileName);

        }
        public ActionResult ViewCSV()
        {

            var products = db.Products.Include(g => g.Genre).ToList();
            using (StringWriter sw = new StringWriter())
            {
                sw.WriteLine("Id,Name,Genre,Description,Number in Stock");
                foreach (var item in products)
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                                               item.Id,
                                               item.Name,
                                               item.Genre.Name,
                                               item.Description,
                                               item.NumberInStock));
                }
                string result = sw.ToString();
                ViewBag.Data = result;

            }
            return View();
        }

        public ActionResult CSVtoXML()
        {

            DataTable dt = new DataTable();
            var products = db.Products.Include(g => g.Genre).ToList();

            using (var mem = new MemoryStream())
            {
                using (var sw = new StreamWriter(mem, Encoding.UTF8))
                {
                    sw.WriteLine("Id,Name,Genre,Description,Number in Stock");

                    foreach (var item in products)
                    {
                        sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                                                   item.Id,
                                                   item.Name,
                                                   item.Genre.Name,
                                                   item.Description,
                                                   item.NumberInStock));
                    }
                    sw.Flush();
                    mem.Position = 0;

                    using (StreamReader streamReader = new StreamReader(mem))
                    {
                        string[] headers = streamReader.ReadLine().Split(',');

                        foreach (string header in headers)
                        {
                            dt.Columns.Add(header);
                        }

                        while (!streamReader.EndOfStream)
                        {
                            string[] rows = streamReader.ReadLine().Split(',');

                            if (rows.Length > 1)
                            {
                                DataRow dr = dt.NewRow();

                                for (int i = 0; i < headers.Length; i++)
                                {
                                    dr[i] = rows[i].Trim();
                                }

                                dt.Rows.Add(dr);
                            }

                        }
                    }
                    dt.TableName = "Product";
                    string filePath = Server.MapPath("~/Content/Temp/writexml.xml");
                    //Write xml file and save in the file path
                    dt.WriteXml(filePath);
                    var saveName = "ProductList" + DateTime.Now.ToString() + ".xml";
                    //File download
                    return File(Server.MapPath("~/Content/Temp/writexml.xml"), "application/xml", saveName);
               }
            }
        }
    }
}
