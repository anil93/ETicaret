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
    public class BasketController : Controller
    {
        // GET: Basket        
        public ActionResult Index()
        {           
            int userId = (int)Session["LogInId"];
                      
            List<BasketMV> basket = new List<BasketMV>();

            List<Address> addressList = null;

            BasketAddressMV basketAddressMV = new BasketAddressMV();

            if (Session["basket"] != null)
            {
                basket = (List<BasketMV>)Session["basket"];
            }

            basketAddressMV.BasketMVList = basket;
           
            foreach (var item in basketAddressMV.BasketMVList)
            {
                basketAddressMV.TotalPrice += item.TotalRow;
            }
            
            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    addressList = db.Address.Where(m => m.UserId == userId).ToList();
                }

                basketAddressMV.Addresses = addressList;
            }
            catch (Exception)
            {
                throw;
            }
           
            return View(basketAddressMV);
        }

        public int Create(int productId, string productName, string imagePath, int unitPrice, int count)
        {                       
            BasketMV basketMV = new BasketMV();
            
            var basket = (List<BasketMV>)Session["basket"];

            if (basket == null)
            {
                basket = new List<BasketMV>();
                Session["basket"] = basket;
            }

            basketMV.Id = productId;
            basketMV.Name = productName;
            basketMV.UnitPrice = unitPrice;
            basketMV.Quantity = count;
            basketMV.ImagePath = imagePath;

            basket.Add(basketMV);

            Session["basketCount"] = basket.Count;

            return basket.Count;           
        }
                
        public ActionResult ApproveOrder(string Addresses)
        {
            string shipAddress = Request.Form["Addresses"];

            Product product;

            Order order = new Order();

            order.OrderDate = DateTime.Now;
            order.UserId = (int)Session["LogInId"];
            order.ShipAddress = Addresses;

            OrderDetail orderDetail = null;
                     
            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    db.Order.Add(order);

                    db.SaveChanges();

                    foreach (var item in (List<BasketMV>)Session["basket"])
                    {
                        orderDetail = new OrderDetail();

                        orderDetail.OrderId = order.Id;
                        orderDetail.ProductId = item.Id;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.UnitPrice = item.UnitPrice;

                        db.OrderDetail.Add(orderDetail);
                    }

                    db.SaveChanges();

                    foreach (var item in (List<BasketMV>)Session["basket"])
                    {
                        product = new Product();

                        product = db.Product.Find(item.Id);

                        product.UnitInStock -= item.Quantity;

                        db.Entry(product).State = System.Data.Entity.EntityState.Modified;                       
                    }

                    db.SaveChanges();
                }

                Session.Remove("basket");

                Session["basketCount"] = 0;

                TempData["ResultSuccessMessage"] = "Siraşiniz onaylandı";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.ResultMessage = "Siraşiniz onaylanmadı";

                return RedirectToAction("Index", "Basket");
            }
        }

        public ActionResult DeleteOrder(int productId)
        {
            List<BasketMV> basket = (List<BasketMV>)Session["basket"];

            try
            {               
                for (int i = basket.Count - 1; i >= 0; i--)
                {

                    if (basket[i].Id == productId)
                        basket.Remove(basket[i]);
                }

                Session["basket"] = basket;

                Session["basketCount"] = basket.Count;
            }
            catch (Exception)
            {
                throw;
            }
          
            return RedirectToAction("Index", "Basket");        
        }

        public ActionResult PastBaskets(int userId) 
        {
            List<Order> orderList = new List<Order>();
                   
            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    orderList= db.Order.Where(m => m.UserId == userId).ToList();                                       
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return View(orderList);
        }

        public ActionResult PastBasketDetails(int orderId) 
        {
            List<OrderDetail> orderDetailList = new List<OrderDetail>();

            try
            {
                using (ETicaretDb db = new ETicaretDb())
                {
                    orderDetailList = db.OrderDetail.Include("Product").Where(m => m.OrderId == orderId).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(orderDetailList);
        }
    }
}