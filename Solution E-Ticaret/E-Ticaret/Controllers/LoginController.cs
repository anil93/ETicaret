using E_Ticaret.Models.DbContext;
using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LogIn userLogIn)
        {
            LogIn user = null;

            User userProfil = null;
                                       
            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    user = db.LogIn.Where(m => m.UserName == userLogIn.UserName.Trim()).FirstOrDefault();

                    if (user != null)
                    {
                        throw new Exception(userLogIn.UserName + " adlı kullanıcı zaten kayıtlı"); 
                    }
                                    
                    db.LogIn.Add(userLogIn);
                    
                    db.SaveChanges();

                    userProfil = new User();

                    userProfil.LogInId = userLogIn.Id;

                    db.User.Add(userProfil);

                    db.SaveChanges();

                    ViewBag.ResultSuccessMessage = "Kayıt başarılı. Giriş Yapabilirsiniz";

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ResultMessage = ex.Message ;

                return View(userLogIn);
            }           
        }

        public ActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SingIn(LogIn userLogIn)
        {
            LogIn entity = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    entity = db.LogIn.Where(m => m.UserName == userLogIn.UserName && m.Password == userLogIn.Password).FirstOrDefault();
                }

                if (entity == null)
                {
                    throw new Exception("Kullanıcı Adı veya Parola hatalı");
                }
                else if (entity.UserName == "admin" && entity.Password == "admin")
                {
                    Session["LogInId"] = entity.Id;
                    Session["UserName"] = entity.UserName;
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    Session["LogInId"] = entity.Id;
                    Session["UserName"] = entity.UserName;
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex )
            {
                ViewBag.ResultMessage = ex.Message;

                return View();
            }            
        }

        public ActionResult SignOut()
        {
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }
    }
}