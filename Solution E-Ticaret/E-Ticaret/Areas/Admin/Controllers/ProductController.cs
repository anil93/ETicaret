using E_Ticaret.ActionFilter;
using E_Ticaret.Models.DbContext;
using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [AuthenticationControl]
    public class ProductController : Controller
    {
        public void BindCategoryList()
        {
            List<Category> categoryList = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    categoryList = db.Category.ToList();
                }

                categoryList.ToList();

                ViewBag.CategorySelectList = new SelectList(categoryList, "Id", "Name");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            List<Product> productList = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb()) 
                {
                    productList = db.Product.Include(m => m.Category).ToList();
                }
            }
            catch (Exception)
            {
                return Content("Veriler yüklenirken hata meydana geldi");             
            }

            return View(productList);
        }
        public ActionResult Create()
        {
            try
            {
                BindCategoryList();
            }
            catch (Exception)
            {
                return Content("Veriler yüklenirken hata meydana geldi");   
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase ImageFile)
        {
            try
            {
                using (ETicaretDb db = new ETicaretDb()) 
                {
                    if (ImageFile != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string extension = Path.GetExtension(ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        product.productImagePath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        ImageFile.SaveAs(fileName);
                    }
                    else
                    {
                        product.productImagePath = "~/Images/" + "DefaultImage.png";
                    }

                    if(product.UnitPrice == 0)
                    {
                        throw new Exception("birim fiyat doğru girilmemiş");                        
                    }

                    db.Product.Add(product);

                    db.SaveChanges();
                }

                TempData["ResultMessage"] = "Kayıt Başarıyla Gerçekleştirildi";
            }
            catch (Exception ex)
            {
                ViewBag.ResultMessage = "Kayıt işlemi sırasında hata meydana geldi." + ex.Message;

                try
                {
                    BindCategoryList();
                }
                catch (Exception)
                {
                    return Content("Veriler yüklenirken hata meydana geldi");
                }

                return View(product);                
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Product product = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    product = db.Product.Find(id);

                    BindCategoryList();
                }
            }
            catch (Exception)
            {
                return Content("Veriler yüklenirken hata meydana geldi");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase ImageFile)
        {
            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    if (ImageFile != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string extension = Path.GetExtension(ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        product.productImagePath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        ImageFile.SaveAs(fileName);
                    }

                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    TempData["ResultMessage"] = "Kayıt Başarıyla Gerçekleştirildi";
                }
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Kayıt işlemi sırasında hata meydana geldi";

                try
                {
                    BindCategoryList();
                }
                catch (Exception)
                {
                    return Content("Veriler yüklenirken hata meydana geldi");
                }

                return View(product);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Product product = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    product = db.Product.Find(id);

                    db.Entry(product).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();

                    TempData["ResultMessage"] = "Kayıt Başarıyla silindi";
                }
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Kayıt silme işleminde hata meydana geldi";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}