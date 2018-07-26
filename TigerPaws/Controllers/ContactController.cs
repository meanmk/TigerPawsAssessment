using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TigerPaws.ViewModels;
using System.Threading.Tasks;



namespace TigerPaws.Controllers
{
    [Audit]
    [AllowAnonymous]
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Index(ContactViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage(viewmodel.Email, "tigerpawscontact@gmail.com");                
                    msz.From = new MailAddress(viewmodel.Email);                                                                          
                    msz.Subject =  viewmodel.Subject;
                    msz.Body =  viewmodel.Name + "\n" + viewmodel.Email + "\n" + viewmodel.Message;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("tigerpawscontact@gmail.com", "rmnfexrafvgelxfh");
                    smtp.EnableSsl = true;
                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}