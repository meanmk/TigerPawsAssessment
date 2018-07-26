using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TigerPaws.Models;

namespace TigerPaws.Controllers
{
    [Audit]
    [Authorize(Roles = RoleName.CanManageProducts)]
    public class AuditController : Controller
    {
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Index()
        {
            var audits = new ApplicationDbContext().AuditRecords;
            return View(audits);
        }
     
    }
   
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            Audit audit = new Audit()
            {
                AuditID = Guid.NewGuid(),
                URLAccessed = request.RawUrl,
                TimeAccessed = DateTime.Now,
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
            };

            ApplicationDbContext context = new ApplicationDbContext();
            context.AuditRecords.Add(audit);
            context.SaveChanges();

            base.OnActionExecuting(filterContext);
        }
    }
}