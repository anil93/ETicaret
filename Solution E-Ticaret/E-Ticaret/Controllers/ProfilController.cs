using E_Ticaret.ActionFilter;
using E_Ticaret.Models;
using E_Ticaret.Models.DbContext;
using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    [AuthenticationControl]
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            User user = null;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    user = db.User.Find(Session["LogInId"]);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            user.LogInId = (int)Session["LogInId"];

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    if (user.LogInId > 0)
                    {
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.User.Add(user);
                    }

                    db.SaveChanges();                   

                    ViewBag.ResultSuccessMessage = "Kayıt başarılı";                   
                }
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Kayıt işleminde hata oluştu";              
            }

            return View();
        }
       
        public ActionResult Address(int id = 0)
        {
            int userId = (int)Session["LogInId"];

            AddressMV addressMV = new AddressMV();

            List<Address> addressList = null;

            Address address = new Address();

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    addressList = db.Address.Where(m => m.UserId == userId).ToList();

                    addressMV.Addresses = addressList;

                    if (id > 0)
                    {
                        address = db.Address.Find(id);
                    }

                    addressMV.Address = address;
                }                            
            }
            catch (Exception)
            {
                throw;
            }

            return View(addressMV);
        }

        [HttpPost]
        public ActionResult Address(Address address)
        {
            AddressMV addressMV = new AddressMV();

            List<Address> addressList = null;

            int userId = (int)Session["LogInId"];

            address.UserId = userId;

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    if (address.Id > 0)
                    {
                        db.Entry(address).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Address.Add(address);
                    }

                    db.SaveChanges();

                    ModelState.Clear();

                    addressList = db.Address.Where(m => m.UserId == address.UserId).ToList();

                    addressMV.Addresses = addressList;                   
                }                              
            }
            catch (Exception)
            {
                throw;
            }
                       
            return View(addressMV);
        }
    }
}