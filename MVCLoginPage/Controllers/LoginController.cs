using MVCLoginPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLoginPage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(AdminLogin adminLogin)
        {
            using (EmployeeDBEntities db = new EmployeeDBEntities())
            {
                var q1 = db.AdminLogins.Where(x => x.eMail == adminLogin.eMail && x.Password == adminLogin.Password).FirstOrDefault();
                if (q1 == null)
                {
                    adminLogin.LoginErrorMessage = "Yanlış E-Posta veya Şifre, Lütfen tekrar deneyiniz.";
                    return View("Index",adminLogin);
                }
                else
                {
                    Session["UserID"] = adminLogin.Id;
                    return RedirectToAction("Index","Employee");
                }
                
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}