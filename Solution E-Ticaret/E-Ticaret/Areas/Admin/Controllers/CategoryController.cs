using E_Ticaret.ActionFilter;
using E_Ticaret.Models.DbContext;
using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        [AuthenticationControl]
        public ActionResult Index()
        {
            List<Category> categoryList = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    categoryList = db.Category.ToList();                    
                }
            }
            catch (Exception)
            {
                return Content("Veriler yüklenirken hata meydana geldi");
            }

            return View(categoryList);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase ImageFile) 
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
                        category.categoryImagePath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        ImageFile.SaveAs(fileName);
                    }
                    else
                    {
                        category.categoryImagePath = "~/Images/" + "DefaultImage.png";
                    }
                    
                    db.Category.Add(category);

                    db.SaveChanges();                   

                    TempData["ResultMessage"] = "Kayıt Başarıyla Gerçekleştirildi";
                }
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Kayıt işlemi sırasında hata meydana geldi";

                return View(category);
            }
           
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Category category = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    category = db.Category.Find(id);                   
                }
            }
            catch (Exception)
            {
                return Content("Veriler yüklenirken hata meydana geldi");
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase ImageFile)
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
                        category.categoryImagePath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        ImageFile.SaveAs(fileName);
                    }                  

                    db.Entry(category).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    TempData["ResultMessage"] = "Kayıt Başarıyla Gerçekleştirildi";
                }
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Kayıt işlemi sırasında hata meydana geldi";

                return View(category);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Category category = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    category = db.Category.Find(id);

                    db.Entry(category).State = System.Data.Entity.EntityState.Deleted;

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