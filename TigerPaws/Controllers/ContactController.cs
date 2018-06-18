using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TigerPaws.ViewModels;



namespace TigerPaws.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Index(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(vm.Email);
                                                         
                    message.To.Add("contact@tigerpaws.com");
                    message.Subject = vm.Subject;
                    message.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.tigerpaws.com";

                    smtp.Port = 000;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("contact@tigerpaws.com", "password");

                    smtp.EnableSsl = true;

                    smtp.Send(message);

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