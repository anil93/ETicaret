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
    public class ProductController : Controller
    {
        // GET: Product       
        public ActionResult Index(int categoryId)
        {           
            List<Product> productList = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    productList = db.Product.Where(m => m.CategoryId == categoryId).ToList();
                }

                return View(productList);
            }
            catch (Exception)
            {
                return Content("Ürünler listelenemedi");
            }
        }

        public ActionResult ProductDetail(int productId)
        {
            Product product = new Product();

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    product = db.Product.Where(m => m.Id == productId).FirstOrDefault();
                }

                return View(product);
            }
            catch (Exception)
            {
                return Content("Ürün yüklenemedi");
            }          
        }       
    }
}