using E_Ticaret.ActionFilter;
using E_Ticaret.Models.DbContext;
using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home        
        public ActionResult Index()
        {
            List<Category> categoryList = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                   categoryList = db.Category.ToList();
                }

                TempData["CategoryList"] = categoryList;
            }
            catch (Exception)
            {
                
                throw;
            }

            return View();
        }       
    }
}